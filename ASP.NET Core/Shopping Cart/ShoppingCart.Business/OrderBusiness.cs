using ShoppingCart.DataModel.Interface;
using ShoppingCart.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Business
{
    public class OrderBusiness : BusinessBase
    {
        public OrderBusiness(CartRequest request, IDataAccess dAccess, ICouponService cService, IPromotionService pService, IRuleService rService) : base(request, dAccess, cService, pService, rService)
        {

        }

        protected override void GetProductOrder()
        {
            Order = new Order();
            Order.Products = Request.Products;
        }
        protected override void GetRule()
        {
            Rule = RuleService.GetRule(Request.State, RuleType.Order).FirstOrDefault();
        }

        protected override void Calculate()
        {
            if (Order == null || Rule == null || (Order!=null && !Order.Products.Any()))
            {
                throw new ArgumentNullException("Either Order or Products or Rule not found");
            }     
            if(Rule!=null && !(Rule.TaxPercent>=0))
            {
                throw new ArgumentNullException("Tax cannot be 0%");
            }
            ApplyTax();
            FinalizeOrder();
        }

        private void ApplyTax()
        {
            Order.TaxAmount = Order.Products.Sum(p => p.FinalCost) * (Rule.TaxPercent/100);
        }

        private void FinalizeOrder()
        {
            Order.TotalCost = Order.Products.Sum(p => p.FinalCost);
            Order.PreTaxAmount = Order.Products.Sum(p => p.TotalCost);
            Order.TotalTaxAmount = Order.Products.Sum(p => p.TotalTaxAmount) + Order.TaxAmount;
            Response.Order = Order;
        }
    }
}
