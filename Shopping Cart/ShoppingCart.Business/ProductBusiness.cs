using ShoppingCart.DataModel.Interface;
using ShoppingCart.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Business
{
    public class ProductBusiness : BusinessBase
    {
        public ProductBusiness(CartRequest request, IDataAccess dAccess, ICouponService cService, IPromotionService pService, IRuleService rService):base(request,dAccess, cService, pService, rService)
        {

        }

        protected override void GetProductOrder()
        {
            Product = DataAccess.Products.Where(p => p.Id == Request.ProductId).FirstOrDefault();                              
        }
        protected override void GetRule()
        {
            Rule = RuleService.GetRule(Request.State, RuleType.Product).FirstOrDefault();
        }

        protected override void Calculate()
        {
            if(Product==null || Rule==null)
            {
                throw new ArgumentNullException("Either Product or Rule not found");
            }
            
            if(Rule.IsTaxAppliedBeforeDiscount)
            {
                ApplyTax();
                ApplyCoupon();
                ApplyPromotion();
            }
            else
            {
                ApplyCoupon();
                ApplyPromotion();
                ApplyTax();
            }
            Product.TotalCost = Product.Cost * Request.Quantity;
            Product.TotalTaxAmount = Product.TaxAmount * Request.Quantity;
            Product.FinalCost = Product.TotalCost + Product.TotalTaxAmount;
            Response.Product = Product;
        }

        private void ApplyTax()
        {
            Product.TaxPercent = Product.TaxPercent * (Product.ValueType == ProductValueType.Luxury ? Rule.LuxuryItemTaxRatio : 1);
            Product.TaxAmount = Product.Cost * (Product.TaxPercent/100) ;            
        }

        private void ApplyCoupon()
        {
            decimal couponValue = CouponService.GetValue(Request.CouponCode);
            Product.Cost = Product.Cost - couponValue;
        }

        private void ApplyPromotion()
        {
            decimal promotionValue = PromotionService.GetValue();
            Product.Cost = Product.Cost - promotionValue;
        }
    }
}
