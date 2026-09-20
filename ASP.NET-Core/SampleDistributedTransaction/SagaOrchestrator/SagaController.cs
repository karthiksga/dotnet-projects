using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;

namespace SagaOrchestrator
{
    // SagaController.cs
    [ApiController]
    [Route("[controller]")]
    public class SagaController : ControllerBase
    {
        private readonly ICapPublisher _cap;

        public SagaController(ICapPublisher cap) => _cap = cap;

        [HttpPost("start")]
        public async Task<IActionResult> Start(OrderDto dto)
        {
            // Kick off the saga
            await _cap.PublishAsync("saga.order.create", dto);
            return Accepted();
        }

        [CapSubscribe("saga.order.created")]
        public async Task OnOrderCreated(Order order)
        {
            // Next step: process payment
            await _cap.PublishAsync("saga.payment.process", new { order.Id });
        }

        [CapSubscribe("saga.payment.processed")]
        public async Task OnPaymentProcessed(Payment payment)
        {
            // Then, reserve inventory
            await _cap.PublishAsync("saga.inventory.reserve", new { payment.OrderId });
        }

        [CapSubscribe("saga.inventory.reserved")]
        public Task OnInventoryReserved(dynamic result)
        {
            // Saga completes successfully
            return Task.CompletedTask;
        }
    }

}
