using ShoppingCart.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Rules
{
    public abstract class RuleBase
    {
        protected abstract int id { get; }
        protected Rule ORule;
        protected virtual void GetRuleDetails()
        {
            switch (id)
            {
                case 1:
                    ORule = new Rule { Id = 1, IsTaxAppliedBeforeDiscount = false, PriceRatio = 1 };
                    break;
                case 2:
                    ORule = new Rule { Id = 2, IsTaxAppliedBeforeDiscount = true, PriceRatio = 2};
                    break;
                default:
                    ORule = new Rule { Id = 1, IsTaxAppliedBeforeDiscount = false, PriceRatio = 1 };
                    break;
            }
        }
        public abstract void Apply(Product product);
    }
}
