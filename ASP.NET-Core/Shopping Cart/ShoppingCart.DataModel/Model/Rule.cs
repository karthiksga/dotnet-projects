using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.DataModel.Model
{
    public class Rule
    {
        public int Id { get; set; }
        public decimal LuxuryItemTaxRatio { get; set; }
        public bool IsTaxAppliedBeforeDiscount { get; set; }
        public string TypeLookup { get; set; }

        public decimal TaxPercent { get; set; }
        public RuleType Type { get => (RuleType)Enum.Parse(typeof(RuleType), TypeLookup); }
    }

    public enum RuleType
    {
        Product=1,
        Order=2
    }
}
