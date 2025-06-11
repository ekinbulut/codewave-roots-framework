# RabbitMQ Messaging

Abstractions and implementations for RabbitMQ messaging.

## Contents

- `IRootsRabbitMqClient`: Interface for messaging operations.
- `RootsRabbitMqClient`: Implementation for publishing and consuming messages.
- `RabbitMQSettings`: Configuration for RabbitMQ connection.

## Usage

- Configure `RabbitMQSettings` in your `appsettings.json`.
- Inject `IRootsRabbitMqClient` to send or receive messages.

### Sample: Configure RabbitMQSettings in appsettings.json

```json
{
  "RabbitMQSettings": {
    "HostName": "localhost",
    "Username": "guest",
    "Password": "guest"
  }
}
```

### Sample: Register and Use RabbitMQ Client

```csharp
services.AddRootsMessaging(Configuration);

public class MessagingService
{
    private readonly IRootsRabbitMqClient _rabbitMqClient;
    public MessagingService(IRootsRabbitMqClient rabbitMqClient)
    {
        _rabbitMqClient = rabbitMqClient;
    }

    public void PublishMessage<T>(T message)
    {
        _rabbitMqClient.Publish(message, "queueName");
    }
}
```

---

**Directory:** `Roots.Framework/Externals/Messaging/`
