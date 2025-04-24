using Microsoft.EntityFrameworkCore;
using Tuto.RabbitMq.Web.Services;

namespace Tuto.RabbitMq.Web.Datas;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected OrderDbContext()
    {
    }

    public DbSet<Order> Orders { get; set; }
}
