using System;
using System.Collections.Generic;
using System.Text;
using Vending.Common;

namespace Vending.Core.Infrastructure
{
    public interface IVendingService
    {
        Money InsertMoney(Enums.Transaction money);
        Money BuySnack(Money money);
    }
}
