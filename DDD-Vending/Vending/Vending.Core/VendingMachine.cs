using System;
using System.Linq;
using static Vending.Core.Money;
namespace Vending.Core
{
    public sealed class VendingMachine : Entity
    {
        public Money MoneyInside { get; private set; } = None;
        public Money MoneyInTransaction { get; private set; } = None;

        public void InsertMoney(Money money)
        {
            Money[] allowedMoney = { OneCent, TenCent, TwentyFiveCent, OneDollar, FiveDollar, TwentyDollar };
            if(!allowedMoney.Contains(money))
            {
                throw new InvalidOperationException();
            }
            MoneyInTransaction += money;
        }

        public void InsertMoneyForTransaction(Money money)
        {
            MoneyInTransaction += money;
        }

        public void BuySnack()
        {            
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = None;
        }

        public void ReturnMoney()
        {
            MoneyInTransaction = None;
        }
    }
}
