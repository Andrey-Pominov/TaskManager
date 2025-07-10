using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TaskManager.Infrastructure.MessageBroker.Messages;

public abstract class RabbitMqConsumerBase<T> : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly RabbitMqSettings _settings;
    private readonly string _queueName;
    private readonly string _exchange;
    private readonly string _routingKey;

    protected RabbitMqConsumerBase(IServiceScopeFactory scopeFactory, IOptions<RabbitMqSettings> options,
        string exchange, string queueName, string routingKey)
    {
        _scopeFactory = scopeFactory;
        _settings = options.Value;
        _exchange = exchange;
        _queueName = queueName;
        _routingKey = routingKey;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _settings.HostName,
            Port = _settings.Port,
            UserName = _settings.UserName,
            Password = _settings.Password
        };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.ExchangeDeclare(_exchange, ExchangeType.Direct, durable: true);
        channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);
        channel.QueueBind(_queueName, _exchange, _routingKey);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var json = Encoding.UTF8.GetString(ea.Body.ToArray());
            var message = JsonSerializer.Deserialize<T>(json);
            if (message != null) await HandleMessageAsync(message);
        };

        channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        return Task.CompletedTask;
    }

    protected abstract Task HandleMessageAsync(T message);
}
