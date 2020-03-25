using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.DataModel.Model
{
    public class Order
    {
        public List<Product> Products { get; set; }
        public decimal FinalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public float TaxPercent { get => 1.5f; }
        public decimal PreTaxAmount
        {
            get=> (this.FinalAmount>this.TaxAmount)?this.FinalAmount - this.TaxAmount:0.0m; 
        }
    }
}
