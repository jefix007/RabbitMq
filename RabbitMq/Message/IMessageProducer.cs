namespace Tuto.RabbitMq.Web.RabbitMq.Message;

using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Tuto.RabbitMq.Web.RabbitMq.Connection;

internal interface IMessageProducer
{
    void SendMessage<T>(T message);
}

internal class RabbitMqProducer(IRabbitMqConnection connection) : IMessageProducer
{
    private readonly IRabbitMqConnection _connection = connection;

    public void SendMessage<T>(T message)
    {
        using var channel = _connection.Connection.CreateModel();
        channel.QueueDeclare("orders",exclusive: false);
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(
            exchange: "",
            routingKey: "orders",
            body: body
        );
    }
}
