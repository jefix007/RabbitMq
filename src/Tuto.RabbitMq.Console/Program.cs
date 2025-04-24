namespace Tuto.RabbitMq.Console;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

internal class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
        };

        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare("orders", exclusive: false);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, args) =>
        {
            var body = args.Body.ToArray();
            var message = System.Text.Encoding.UTF8.GetString(body);

            Console.WriteLine($"Received {message}");
        };

        channel.BasicConsume("orders", autoAck: true, consumer: consumer);

        Console.ReadKey();
    }
}
