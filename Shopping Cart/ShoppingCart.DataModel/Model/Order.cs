using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.DataModel.Model
{
    public class Order
    {
        public List<Product> Products { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal PreTaxAmount { get; set; }
        public decimal TotalTaxAmount { get; set; }
    }
}
