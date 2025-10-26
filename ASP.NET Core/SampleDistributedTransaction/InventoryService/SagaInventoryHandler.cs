using DotNetCore.CAP;

namespace InventoryService
{
    // SagaInventoryHandler.cs
    public class SagaInventoryHandler : ICapSubscribe
    {
        private readonly InventoryDbContext _db;
        private readonly ICapPublisher _cap;

        public SagaInventoryHandler(InventoryDbContext db, ICapPublisher cap)
        {
            _db = db;
            _cap = cap;
        }

        [CapSubscribe("saga.inventory.reserve")]
        public async Task HandleSagaInventory(dynamic payload)
        {
            int orderId = payload.OrderId;
            // Deduct stock...
            await _db.SaveChangesAsync();
            // Notify orchestrator
            await _cap.PublishAsync("saga.inventory.reserved",
                new { OrderId = orderId });
        }
    }

}
