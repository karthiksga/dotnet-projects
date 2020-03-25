using ShoppingCart.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.DataModel.Interface
{
    public interface IDataAccess
    {
        List<Product> Products { get; }
        List<Discount> Discounts { get;  }
        List<Rule> Rules { get;}
        List<StateRuleMap> StateRules { get;  }
    }
}
