using ShoppingCart.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.DataModel.Interface
{
    public interface IRuleService
    {
        List<Rule> GetRule(string state, RuleType type);
    }
}
