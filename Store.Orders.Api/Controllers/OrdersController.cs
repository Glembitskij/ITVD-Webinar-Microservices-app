using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Orders.Api.Data;
using Store.Orders.Api.Services;

namespace Store.Orders.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(OrdersDbContext db, IStockClient stockClient) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    { 
        return Ok(await db.Orders.ToListAsync()); 
    }

    [HttpPost]
    public async Task<IActionResult> Create(int productId, int quantity)
    {
        var product = await stockClient.GetProductAsync(productId);

        if (product == null || product.Quantity < quantity) 
        { 
            return BadRequest("Товар недоступний");
        }

        var order = new Order
        {
            Id = Guid.NewGuid(),
            ProductId = product.Id,
            AppliedPrice = product.Price,
            Quantity = quantity
        };

        db.Orders.Add(order);
        await db.SaveChangesAsync();

        return Ok(order);
    }
}