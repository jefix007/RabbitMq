namespace Tuto.RabbitMq.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Tuto.RabbitMq.Web.Services;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    private IOrderService _orderService = orderService;

    [HttpPost]
    public async Task<IActionResult> Post(OrderDto order)
    {
        var result = await _orderService.SaveOrder(order);
        if (result is null)
        {
            return BadRequest("Error");
        }

        return Ok(result.Id);
    }
}
