using DotNetCore.CAP;

namespace PaymentService
{
    // SagaPaymentHandler.cs
    public class SagaPaymentHandler : ICapSubscribe
    {
        private readonly PaymentDbContext _db;

        public SagaPaymentHandler(PaymentDbContext db) => _db = db;

        [CapSubscribe("saga.payment.process")]
        public async Task HandleSagaPayment(dynamic payload)
        {
            int orderId = payload.Id;
            var payment = new Payment
            {
                OrderId = orderId,
                Status = "Success"
            };

            _db.Payments.Add(payment);
            await _db.SaveChangesWithEventAsync("saga.payment.processed", payment);
        }
    }

}
