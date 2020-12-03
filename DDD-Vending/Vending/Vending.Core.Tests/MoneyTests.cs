using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vending.Core.Tests
{
    [TestClass]
    public class MoneyTests
    {
        [TestMethod]
        [DataRow(1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0)]
        [DataRow(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2)]
        public void Sum_of_two_money_produce_correct_result(int oneCent_a, int tenCent_a, int twentyCent_a, int oneDollar_a, int fiveDollar_a, int twentyDollar_a,
                                       int oneCent_b, int tenCent_b, int twentyCent_b, int oneDollar_b, int fiveDollar_b, int twentyDollar_b,
                                       int oneCent_sum, int tenCent_sum, int twentyCent_sum, int oneDollar_sum, int fiveDollar_sum, int twentyDollar_sum)
        {
            var money1 = new Money(oneCent_a, tenCent_a, twentyCent_a, oneDollar_a, fiveDollar_a, twentyDollar_a);
            var money2 = new Money(oneCent_b, tenCent_b, twentyCent_b, oneDollar_b, fiveDollar_b, twentyDollar_b);

            var sum = money1 + money2;

            sum.OneCentCount.Should().Be(oneCent_sum);
            sum.TenCentCount.Should().Be(tenCent_sum);
            sum.TwentyFiveCentCount.Should().Be(twentyCent_sum);
            sum.OneDollarCount.Should().Be(oneDollar_sum);
            sum.FiveDollarCount.Should().Be(fiveDollar_sum);
            sum.TwentyDollarCount.Should().Be(twentyDollar_sum);
        }

        [TestMethod]
        public void Two_money_instances_are_equal()
        {
            var money_a = new Money(1, 2, 3, 4, 5, 6);
            var money_b = new Money(1, 2, 3, 4, 5, 6);

            money_a.Should().Be(money_b);
            money_a.GetHashCode().Should().Be(money_b.GetHashCode());
        }

        [TestMethod]
        public void Two_money_instances_are_not_equal()
        {
            var money_a = new Money(1, 2, 3, 4, 5, 6);
            var money_b = new Money(7, 8, 9, 10, 11, 12);

            money_a.Should().NotBe(money_b);
            money_a.GetHashCode().Should().NotBe(money_b.GetHashCode());
        }

        [TestMethod]
        [DataRow(-1, 0, 0, 0, 0, 0)]
        [DataRow(0, -1, 0, 0, 0, 0)]
        [DataRow(0, 0, -1, 0, 0, 0)]
        [DataRow(0, 0, 0, -1, 0, 0)]
        [DataRow(0, 0, 0, 0, -1, 0)]
        [DataRow(0, 0, 0, 0, 0, -1)]
        public void Cannot_creat_money_with_negative_values(int oneCent_a, int tenCent_a, int twentyCent_a, int oneDollar_a, int fiveDollar_a, int twentyDollar_a)
        {
            Action action = () => new Money(oneCent_a, tenCent_a, twentyCent_a, oneDollar_a, fiveDollar_a, twentyDollar_a);

            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        [DataRow(1, 0, 0, 0, 0, 0, 0.01)]
        [DataRow(1, 1, 1, 1, 1, 1, 26.36)]
        public void Amount_is_calculated_correctly(int oneCent_a, int tenCent_a, int twentyCent_a, int oneDollar_a, int fiveDollar_a, int twentyDollar_a, double amount)
        {
            var money = new Money(oneCent_a, tenCent_a, twentyCent_a, oneDollar_a, fiveDollar_a, twentyDollar_a);

            money.Amount.Should().Be((decimal)amount);
        }

        [TestMethod]
        [DataRow(1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)]
        [DataRow(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0)]
        public void Subtraction_produces_correct_result(int oneCent_a, int tenCent_a, int twentyCent_a, int oneDollar_a, int fiveDollar_a, int twentyDollar_a,
                                       int oneCent_b, int tenCent_b, int twentyCent_b, int oneDollar_b, int fiveDollar_b, int twentyDollar_b,
                                       int oneCent_sum, int tenCent_sum, int twentyCent_sum, int oneDollar_sum, int fiveDollar_sum, int twentyDollar_sum)
        {
            var money1 = new Money(oneCent_a, tenCent_a, twentyCent_a, oneDollar_a, fiveDollar_a, twentyDollar_a);
            var money2 = new Money(oneCent_b, tenCent_b, twentyCent_b, oneDollar_b, fiveDollar_b, twentyDollar_b);

            var sum = money1 - money2;

            sum.OneCentCount.Should().Be(oneCent_sum);
            sum.TenCentCount.Should().Be(tenCent_sum);
            sum.TwentyFiveCentCount.Should().Be(twentyCent_sum);
            sum.OneDollarCount.Should().Be(oneDollar_sum);
            sum.FiveDollarCount.Should().Be(fiveDollar_sum);
            sum.TwentyDollarCount.Should().Be(twentyDollar_sum);
        }

        [TestMethod]
        [DataRow(1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0)]       
        public void Cannot_subtract_when_does_not_exists(int oneCent_a, int tenCent_a, int twentyCent_a, int oneDollar_a, int fiveDollar_a, int twentyDollar_a,
                                       int oneCent_b, int tenCent_b, int twentyCent_b, int oneDollar_b, int fiveDollar_b, int twentyDollar_b)
        {
            var money_a = new Money(oneCent_a, tenCent_a, twentyCent_a, oneDollar_a, fiveDollar_a, twentyDollar_a);
            var money_b = new Money(oneCent_b, tenCent_b, twentyCent_b, oneDollar_b, fiveDollar_b, twentyDollar_b);

            Action sum =  () => { var money = money_a - money_b; };

            sum.Should().Throw<InvalidOperationException>();
        }
    }
}
