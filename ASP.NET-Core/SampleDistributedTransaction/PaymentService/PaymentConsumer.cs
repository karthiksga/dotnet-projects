using DotNetCore.CAP;

namespace PaymentService
{
    // PaymentConsumer.cs
    public class PaymentConsumer : ICapSubscribe
    {
        private readonly PaymentDbContext _db;
        private readonly ICapPublisher _cap;

        public PaymentConsumer(PaymentDbContext db, ICapPublisher cap)
        {
            _db = db;
            _cap = cap;
        }

        [CapSubscribe("order.created")]
        public async Task HandleOrderCreated(Order order)
        {
            // Simulate payment processing
            var payment = new Payment
            {
                OrderId = order.Id,
                Status = "Success"
            };
            _db.Payments.Add(payment);
            // Save and publish processed payment event together
            await _db.SaveChangesWithEventAsync("payment.processed", payment);
        }
    }

}
