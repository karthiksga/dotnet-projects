using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.DataModel.Model
{
    public class ShoppingCartData
    {
        public List<Product> Products { get; set; }
        public List<Discount> Discounts { get; set; }
        public List<Rule> Rules { get; set; }
        public List<StateRuleMap> StateRules { get; set; }
    }

    public class StateRuleMap
    {
        public string State { get; set; }
        public int RuleId { get; set; }
    }
}
