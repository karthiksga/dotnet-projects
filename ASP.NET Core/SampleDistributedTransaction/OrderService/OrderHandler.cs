using DotNetCore.CAP;

namespace OrderService
{
    // SagaOrderHandler.cs
    public class SagaOrderHandler : ICapSubscribe
    {
        private readonly OrderDbContext _db;

        public SagaOrderHandler(OrderDbContext db) => _db = db;

        [CapSubscribe("saga.order.create")]
        public async Task HandleSagaCreate(OrderDto dto)
        {
            var order = new Order
            {
                /* map dto */
            };

            _db.Orders.Add(order);
            await _db.SaveChangesWithEventAsync("saga.order.created", order);
        }
    }

}
