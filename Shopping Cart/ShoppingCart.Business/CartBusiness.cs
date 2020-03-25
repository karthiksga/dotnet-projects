using ShoppingCart.DataModel.Interface;
using ShoppingCart.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business
{
    public class CartBusiness<T> where T:BusinessBase
    {
        IDataAccess DataAccess;
        ICouponService CouponService;
        IPromotionService PromotionService;
        IRuleService RuleService;
        public CartBusiness(IDataAccess dAccess, ICouponService cService, IPromotionService pService, IRuleService rService)
        {
            DataAccess = dAccess;
            CouponService = cService;
            PromotionService = pService;
            RuleService = rService;
        }

        public T Create(CartRequest request)
        {
            return (T)Activator.CreateInstance(typeof(T), request, DataAccess, CouponService, PromotionService, RuleService);
        }
    }
}
