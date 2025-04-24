namespace Tuto.RabbitMq.Web.RabbitMq.Connection;

using RabbitMQ.Client;

internal interface IRabbitMqConnection
{
    public IConnection Connection { get; }
}
