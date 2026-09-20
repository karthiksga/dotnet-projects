using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;

namespace OrderService
{
    // OrdersController.cs
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ICapPublisher _cap;
        private readonly OrderDbContext _db;

        public OrdersController(ICapPublisher cap, OrderDbContext db)
        {
            _cap = cap;
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto dto)
        {
            var order = new Order
            {
                /* map dto */
            };
            _db.Orders.Add(order);
            // Enlist DB save and event publish in one transaction
            await _db.SaveChangesWithEventAsync("order.created", order);
            return Ok(order);
        }
    }

}
