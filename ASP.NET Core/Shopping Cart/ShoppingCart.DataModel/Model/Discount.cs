using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.DataModel.Model
{
    public class Discount
    {
        public int Id { get; set; }

        //public int ProductId { get; set; }
        public string Code { get; set; }
        public decimal Value { get; set; }
        public DateTime StartDate { get; set; }
        public byte DaysAfterExpire { get; set; }
        public string TypeLookup { get; set; }
        public DiscountType Type { get => (DiscountType)Enum.Parse(typeof(DiscountType), TypeLookup); }
    }

    public enum DiscountType
    {
        Promotion = 1,
        Coupon = 2
    }
}
