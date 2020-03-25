using ShoppingCart.DataModel.Interface;
using ShoppingCart.DataModel.Model;
using System;

namespace ShoppingCart.Rules
{
    public class RuleOne : RuleBase, IRule
    {
        protected override int id { get => 1; }
        public override void Apply(Product product)
        {
            GetRuleDetails();
            
        }
    }
}
