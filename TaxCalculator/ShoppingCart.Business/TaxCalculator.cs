using ShoppingCart.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business
{
    public class TaxCalculator : BusinessBase
    {
        public TaxCalculator(CartRequest request) : base(request)
        {

        }
        protected override void ApplyRule()
        {
            Request.Product.
        }

        protected override void CalculateTax()
        {
            throw new NotImplementedException();
        }
    }
}
