using Microsoft.EntityFrameworkCore;
using Tuto.RabbitMq.Web.Datas;
using Tuto.RabbitMq.Web.RabbitMq.Connection;
using Tuto.RabbitMq.Web.RabbitMq.Message;
using Tuto.RabbitMq.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseInMemoryDatabase("AspnetCoreRabbitMq"));

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddSingleton<IRabbitMqConnection>(new RabbitMqConnection());
builder.Services.AddSingleton<IMessageProducer, RabbitMqProducer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
