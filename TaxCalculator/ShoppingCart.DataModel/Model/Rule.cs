using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.DataModel.Model
{
    public class Rule
    {
        public int Id { get; set; }
        public float PriceRatio { get; set; }
        public bool IsTaxAppliedBeforeDiscount { get; set; }
    }
}
