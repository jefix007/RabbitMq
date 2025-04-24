using RabbitMQ.Client;

namespace Tuto.RabbitMq.Web.RabbitMq.Connection;

public class RabbitMqConnection : IRabbitMqConnection, IDisposable
{
    private IConnection? _connection;
    public IConnection Connection => _connection!;

    public RabbitMqConnection() => InitConnection();

    private void InitConnection()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
        };

        _connection = factory.CreateConnection();
    }

    public void Dispose() => _connection?.Dispose();
}
