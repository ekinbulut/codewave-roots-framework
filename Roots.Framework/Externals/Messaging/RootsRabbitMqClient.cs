using System.Text;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Roots.Framework.Configuration;
using Roots.Framework.Settings;

namespace Roots.Framework.Externals.Messaging;

public class RootsRabbitMqClient : IDisposable, IRootsRabbitMqClient
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RootsRabbitMqClient(IOptions<RabbitMQSettings> options)
    {
        var factory = new ConnectionFactory()
        {
            HostName = options.Value.HostName,
            UserName = options.Value.Username,
            Password = options.Value.Password
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void DeclareQueue(string queueName)
    {
        _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    public void DeclareQueue(string queueName, bool durable, bool exclusive, bool autoDelete,
        IDictionary<string, object> arguments)
    {
        _channel.QueueDeclare(queue: queueName, durable: durable, exclusive: exclusive, autoDelete: autoDelete,
            arguments: arguments);
    }

    public void DeclareExchange(string exchangeName, string exchangeType)
    {
        _channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType);
    }

    public void DeclareExchange(string exchangeName, string exchangeType, bool durable, bool autoDelete,
        IDictionary<string, object> arguments)
    {
        _channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType, durable: durable, autoDelete: autoDelete,
            arguments: arguments);
    }

    public void BindQueue(string queueName, string exchangeName, string routingKey)
    {
        _channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);
    }

    public void PublishToExchange(string exchangeName, string routingKey, string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: body);
    }

    public async Task PublishToExchangeAsync(string exchangeName, string routingKey, string message,
        CancellationToken cancellationToken = default)
    {
        var body = Encoding.UTF8.GetBytes(message);
        await Task.Run(() =>
                _channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null,
                    body: body), cancellationToken
        );
    }

    public void Publish(string queueName, string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
    }

    public async Task PublishAsync(string queueName, string message,
        CancellationToken cancellationToken = default)
    {
        var body = Encoding.UTF8.GetBytes(message);
        await Task.Run(() => _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body), cancellationToken);
        
    }

    public async Task<bool> PublishWithAckAsync(string queueName, string message,
        CancellationToken cancellationToken = default)
    {
        _channel.ConfirmSelect();
        var body = Encoding.UTF8.GetBytes(message);

        var taskCompletionSource = new TaskCompletionSource<bool>();

        void Handler(object? sender, BasicAckEventArgs basicAckEventArgs)
        {
            taskCompletionSource.SetResult(true);
            _channel.BasicAcks -= Handler;
        }

        _channel.BasicAcks += Handler;
        _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);

        return await taskCompletionSource.Task;
    }
    
    public void Consume(string queueName, Action<string> onMessageReceived)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            onMessageReceived(message);
        };
        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}