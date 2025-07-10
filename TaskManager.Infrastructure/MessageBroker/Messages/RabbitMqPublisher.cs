using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using TaskManager.Infrastructure.MessageBroker.Interface;

namespace TaskManager.Infrastructure.MessageBroker.Messages;

public class RabbitMqPublisher : IMessagePublisher
{
    private readonly IConnection _connection;

    public RabbitMqPublisher(IOptions<RabbitMqSettings> options)
    {
        var factory = new ConnectionFactory
        {
            HostName = options.Value.HostName,
            Port = options.Value.Port,
            UserName = options.Value.UserName,
            Password = options.Value.Password
        };
        _connection = factory.CreateConnection();
    }

    public Task PublishAsync<T>(string exchange, string routingKey, T message)
    {
        using var channel = _connection.CreateModel();
        channel.ExchangeDeclare(exchange, ExchangeType.Direct, durable: true);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(exchange, routingKey, null, body);
        return Task.CompletedTask;
    }
}