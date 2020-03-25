using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.DataModel.Model
{
    public class CartRequest
    {
        public Product Product { get; set; }
        public string State { get; set; }
    }
}
