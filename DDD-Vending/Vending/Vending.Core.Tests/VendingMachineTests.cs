using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using static Vending.Core.Money;
namespace Vending.Core.Tests
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void Return_money_empties_money_in_transaction()
        {
            var vending = new VendingMachine();
            vending.InsertMoney(OneDollar);

            vending.ReturnMoney();

            vending.MoneyInTransaction.Amount.Should().Be(0m);
        }

        [TestMethod]
        public void Insert_money_goes_to_money_in_transaction()
        {
            var vending = new VendingMachine();
            vending.InsertMoney(OneDollar);
            vending.InsertMoney(OneCent);

            vending.MoneyInTransaction.Amount.Should().Be(1.01m);
        }

        [TestMethod]
        public void Cannot_insert_more_than_one_money()
        {
            var vending = new VendingMachine();
            var cent = OneCent + OneCent;

            Action action = () =>
            {
                vending.InsertMoney(cent);
            };

            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void Money_in_transaction_should_be_empty_after_purchase()
        {
            var vending = new VendingMachine();
            vending.InsertMoney(OneDollar);
            vending.InsertMoney(OneDollar);

            vending.BuySnack();

            vending.MoneyInTransaction.Should().Be(None);
            vending.MoneyInside.Amount.Should().Be(2.00m);
        }
    }
}
