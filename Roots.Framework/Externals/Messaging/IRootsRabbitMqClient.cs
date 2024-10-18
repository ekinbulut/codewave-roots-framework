namespace Roots.Framework.Externals.Messaging;

public interface IRootsRabbitMqClient
{
    void DeclareQueue(string queueName);
    void DeclareQueue(string queueName, bool durable, bool exclusive, bool autoDelete, IDictionary<string, object> arguments);
    void DeclareExchange(string exchangeName, string exchangeType);
    void DeclareExchange(string exchangeName, string exchangeType, bool durable, bool autoDelete, IDictionary<string, object> arguments);
    void BindQueue(string queueName, string exchangeName, string routingKey);
    void PublishToExchange(string exchangeName, string routingKey, string message);
    Task PublishToExchangeAsync(string exchangeName, string routingKey, string message, CancellationToken cancellationToken = default);
    void Publish(string queueName, string message);
    Task PublishAsync(string queueName, string message, CancellationToken cancellationToken = default);
    void Consume(string queueName, Action<string> onMessageReceived);
    void Dispose();
}