using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.DataModel.Model
{
    public class CartRequest
    {
        public int ProductId { get; set; }
        public byte Quantity { get; set; }
        public string State { get; set; }
        public string CouponCode { get; set; }
        public List<Product> Products { get; set; }
    }
}
