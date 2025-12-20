using System;

namespace Vending.Core
{
    public class Money : ValueObject<Money>
    {
        public int OneCentCount { get; }
        public int TenCentCount { get; }
        public int TwentyFiveCentCount { get; }
        public int OneDollarCount { get; }
        public int FiveDollarCount { get; }
        public int TwentyDollarCount { get; }

        public static readonly Money None = new Money(0, 0, 0, 0, 0, 0);
        public static readonly Money OneCent = new Money(1, 0, 0, 0, 0, 0);
        public static readonly Money TenCent = new Money(0, 1, 0, 0, 0, 0);
        public static readonly Money TwentyFiveCent = new Money(0, 0, 1, 0, 0, 0);
        public static readonly Money OneDollar = new Money(0, 0, 0, 1, 0, 0);
        public static readonly Money FiveDollar = new Money(0, 0, 0, 1, 0, 0);
        public static readonly Money TwentyDollar = new Money(0, 0, 0, 0, 0, 1);
               
        public decimal Amount =>
                       OneCentCount* 0.01m +
                       TenCentCount* 0.10m +
                       TwentyFiveCentCount* 0.25m +
                       OneDollarCount* 1 +
                       FiveDollarCount* 5 +
                       TwentyDollarCount* 20;

        public Money()
        {
        }

        public Money(int oneCent=0, int tenCent=0, int twentyCent=0, int oneDollar = 0, int fiveDollar = 0, int twentyDollar = 0)
        {
            if (oneCent < 0 || tenCent < 0 || twentyCent < 0 || oneDollar < 0 || fiveDollar < 0 || twentyDollar < 0)
                throw new InvalidOperationException();
            OneCentCount += oneCent;
            TenCentCount += tenCent;
            TwentyFiveCentCount += twentyCent;
            OneDollarCount += oneDollar;
            FiveDollarCount += fiveDollar;
            TwentyDollarCount += twentyDollar;
        }

        public static Money operator +(Money money1, Money money2)
        {
            Money sum = new Money(money1.OneCentCount + money2.OneCentCount,
                money1.TenCentCount + money2.TenCentCount,
                money1.TwentyFiveCentCount + money2.TwentyFiveCentCount,
                money1.OneDollarCount + money2.OneDollarCount,
                money1.FiveDollarCount + money2.FiveDollarCount,
                money1.TwentyDollarCount + money2.TwentyDollarCount);
            return sum;
        }

        public static Money operator -(Money money1, Money money2)
        {
            Money sum = new Money(money1.OneCentCount - money2.OneCentCount,
                money1.TenCentCount - money2.TenCentCount,
                money1.TwentyFiveCentCount - money2.TwentyFiveCentCount,
                money1.OneDollarCount - money2.OneDollarCount,
                money1.FiveDollarCount - money2.FiveDollarCount,
                money1.TwentyDollarCount - money2.TwentyDollarCount);
            return sum;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = OneCentCount;
                hashCode = (hashCode * 397) ^ TenCentCount;
                hashCode = (hashCode * 397) ^ TwentyFiveCentCount;
                hashCode = (hashCode * 397) ^ OneDollarCount;
                hashCode = (hashCode * 397) ^ FiveDollarCount;
                hashCode = (hashCode * 397) ^ TwentyDollarCount;
                return hashCode;
            }            
        }

        protected override bool EqualsCore(Money other)
        {
            return OneCentCount == other.OneCentCount &&
                   TenCentCount == other.TenCentCount &&
                   TwentyFiveCentCount == other.TwentyFiveCentCount &&
                   OneDollarCount == other.OneDollarCount &&
                   FiveDollarCount == other.FiveDollarCount &&
                   TwentyDollarCount == other.TwentyDollarCount;                    
        }


    }
}
