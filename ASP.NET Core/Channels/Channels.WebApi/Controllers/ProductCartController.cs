using Channels.WebApi.Models;
using Channels.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Channels.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductCartController : ControllerBase
{
    private readonly WriteBackCacheProductCartService _service;

    public ProductCartController(WriteBackCacheProductCartService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<ProductCartResponse>> CreateCart(ProductCartRequest request)
    {
        var response = await _service.AddAsync(request);
        return CreatedAtAction(nameof(GetCart), new { id = response.Id }, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductCartResponse>> GetCart(Guid id)
    {
        var cart = await _service.GetAsync(id);
        if (cart is null)
        {
            return NotFound();
        }

        return cart;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCart(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
