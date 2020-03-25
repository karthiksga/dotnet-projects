using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.DataModel.Interface
{
    public interface ICouponService
    {
        decimal GetValue(string code);
    }
}
