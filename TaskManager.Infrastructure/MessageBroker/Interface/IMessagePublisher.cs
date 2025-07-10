namespace TaskManager.Infrastructure.MessageBroker.Interface;

public interface IMessagePublisher
{
    Task PublishAsync<T>(string exchange, string routingKey, T message);
}