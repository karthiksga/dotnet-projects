using ShoppingCart.DataModel.Interface;
using ShoppingCart.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Rules
{
    public class RuleTwo : RuleBase, IRule
    {
        protected override int id { get => 2; }
        public override void Apply(Product product)
        {
             
        }
    }
}
