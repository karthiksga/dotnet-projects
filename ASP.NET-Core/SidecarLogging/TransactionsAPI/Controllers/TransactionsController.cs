using Microsoft.AspNetCore.Mvc;
using TransactionsAPI.Infrastructure;
using TransactionsAPI.Models;

namespace TransactionsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ISidecarMessageQueue _transactionsMessageQueue;
        public TransactionsController(ISidecarMessageQueue transactionsMessageQueue)
        {
            _transactionsMessageQueue = transactionsMessageQueue;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]TransactionRequest transactionRequest)
        {
            if (transactionRequest.TransactionId <= 0)
            {
                await _transactionsMessageQueue.Enqueue(LogLevel.Error.ToString(),
                    "Transaction Id must be > 0.");
                return BadRequest();
            }

            if (transactionRequest.TransactionQuantity <= 0)
            {
                await _transactionsMessageQueue.Enqueue(LogLevel.Error.ToString(),
                    "Transaction Quantity must be > 0.");
                return BadRequest();
            }

            bool isTransactionTypeValid = Enum.IsDefined(typeof(TransactionType),
            transactionRequest.TransactionType);

            if (!isTransactionTypeValid)
            {
                await _transactionsMessageQueue.Enqueue(LogLevel.Error.ToString(),
                    $"{transactionRequest.TransactionType} " +
                    $"is an invalid transaction type");
                return BadRequest();
            }

            await _transactionsMessageQueue.Enqueue(LogLevel.Information.ToString(),
                $"Created a new transaction record having transaction Id: " +
                $"{transactionRequest.TransactionId}");

            return Ok(new
            {
                success = true,
                data = transactionRequest,
                id = transactionRequest.TransactionId
            });
        }
    }
}
