using ShoppingCart.DataModel.Model;
using ShoppingCart.Rules;
using System;

namespace ShoppingCart.Business
{
    public abstract class BusinessBase
    {
        protected CartRequest Request;        
        protected RuleBase Rule;
        public BusinessBase(CartRequest request)
        {
            Request = request;
        }

        private void GetRule()
        {
            Rule = new RuleData().GetRuleByState(Request.State);
        }

        protected abstract void ApplyRule();

        protected abstract void CalculateTax();
        public CartResponse Calculate()
        {
            GetRule();
            ApplyRule();
            CalculateTax();
            return new CartResponse { Product = Request.Product };
        }
    }
}
