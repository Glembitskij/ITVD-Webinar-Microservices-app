using Microsoft.AspNetCore.Mvc;
using Store.Stock.Api.Services;

namespace Store.Stock.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController(InventoryService inventory) : ControllerBase
{
    [HttpGet("{productId}")]
    public IActionResult GetStock(int productId)
    {
        var qty = inventory.GetQuantity(productId);
        return Ok(new { ProductId = productId, Quantity = qty });
    }

    [HttpPost("reserve")]
    public IActionResult Reserve(int productId, int qty)
    {
        var success = inventory.Reserve(productId, qty);

        if (!success)
        {
            return BadRequest(new { Message = "Недостатньо товару на складі" });
        }

        return Ok(new { Message = "Зарезервовано", Remaining = inventory.GetQuantity(productId) });
    }
}