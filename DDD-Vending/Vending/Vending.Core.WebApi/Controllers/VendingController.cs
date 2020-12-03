using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vending.Common;
using Vending.Core.Infrastructure;
using Vending.Core.WebApi.Models;

namespace Vending.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendingController : ControllerBase
    {
        private IVendingService _service;
        private IMapper _mapper;

        public VendingController(IVendingService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("insert/{money}")]
        public IActionResult InsertMoney(Enums.Transaction money)
        {
            var moneyInTransaction = _service.InsertMoney(money);
                     
            return Ok(moneyInTransaction);
        }

        [HttpPost("buy")]
        public IActionResult BuySnack([FromBody]MoneyDto money)
        {
            var moneyInside = _service.BuySnack(_mapper.Map<Money>(money));
            return Ok(moneyInside);
        }

        [HttpPost("return")]
        public IActionResult ReturnMoney()
        {
            var vending = new VendingMachine();
            vending.ReturnMoney();
            return Ok(Money.None);
        }
    }
}
