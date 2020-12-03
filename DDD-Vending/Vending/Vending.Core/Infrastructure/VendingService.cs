using System;
using System.Collections.Generic;
using System.Text;
using Vending.Common;

namespace Vending.Core.Infrastructure
{
    public class VendingService : IVendingService
    {
        private VendingMachine _vending;
        public VendingService(VendingMachine vending)
        {
            _vending = vending;
        }
        public Money InsertMoney(Enums.Transaction money)
        {
            switch (money)
            {
                case Enums.Transaction.OneCent:
                    _vending.InsertMoney(Money.OneCent);
                    break;
                case Enums.Transaction.TenCent:
                    _vending.InsertMoney(Money.TenCent);
                    break;
                case Enums.Transaction.TwentyFiveCent:
                    _vending.InsertMoney(Money.TwentyFiveCent);
                    break;
                case Enums.Transaction.OneDollar:
                    _vending.InsertMoney(Money.OneDollar);
                    break;
                case Enums.Transaction.FiveDollar:
                    _vending.InsertMoney(Money.FiveDollar);
                    break;
                case Enums.Transaction.TwentyDollar:
                    _vending.InsertMoney(Money.TwentyDollar);
                    break;
                default:
                    break;
            }
            return _vending.MoneyInTransaction;
        }

        public Money BuySnack(Money money)
        {
            _vending.InsertMoneyForTransaction(money);
            _vending.BuySnack();
            return _vending.MoneyInside;
        }
    }
}
