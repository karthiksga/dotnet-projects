using DotNetCore.CAP;

namespace InventoryService
{
    // InventoryConsumer.cs
    public class InventoryConsumer : ICapSubscribe
    {
        private readonly InventoryDbContext _db;

        public InventoryConsumer(InventoryDbContext db)
        {
            _db = db;
        }

        [CapSubscribe("payment.processed")]
        public async Task HandlePaymentProcessed(Payment payment)
        {
            // Deduct stock for the ordered product
            _db.Products
                .Where(p => p.Id == payment.OrderId)
                .ToList()
                .ForEach(p => p.Stock--);
            await _db.SaveChangesAsync();
        }
    }

}
