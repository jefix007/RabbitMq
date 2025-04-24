namespace Tuto.RabbitMq.Web.Services;

using Tuto.RabbitMq.Web.Datas;
using Tuto.RabbitMq.Web.RabbitMq.Message;

internal class OrderService(
    OrderDbContext context,
    IMessageProducer messageProducer) : IOrderService
{
    public OrderDbContext _context = context;
    private readonly IMessageProducer _messageProducer = messageProducer;

    public async Task<Order> Save(OrderDto order)
    {
        var entity = new Order
        {
            Id = order.Id,
            Name = order.Name,
            CreatedAt = DateTime.UtcNow
        };

        _context.Orders.Add(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Order?> SaveOrder(OrderDto dto)
    {
        var order = await Save(dto);
        if (order is not null)
        {
            _messageProducer.SendMessage(order);
        }

        return order;
    }
}

public interface IOrderService
{
    Task<Order> Save(OrderDto order);
    Task<Order> SaveOrder(OrderDto dto);
}

public record class Order
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}

public record class OrderDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}
