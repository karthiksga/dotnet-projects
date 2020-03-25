using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.DataModel.Model
{
    public class Discount
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public string Code { get; set; }
        public decimal Value { get; set; }
        public DateTime StartDate { get; set; }
        public int DaysAfterExpire { get; set; }
        public DiscountType Type { get; set; }
    }

    public enum DiscountType
    {
        Promotion = 1,
        Coupon = 2
    }
}
