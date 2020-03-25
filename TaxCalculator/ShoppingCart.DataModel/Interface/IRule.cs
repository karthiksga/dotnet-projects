using ShoppingCart.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.DataModel.Interface
{
    public interface IRule
    {
        void Apply(Product product);
    }
}
