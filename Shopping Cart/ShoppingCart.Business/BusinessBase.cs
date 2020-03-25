using ShoppingCart.DataModel.Interface;
using ShoppingCart.DataModel.Model;

namespace ShoppingCart.Business
{
    public abstract class BusinessBase
    {
        protected CartRequest Request;
        protected CartResponse Response;
        protected Product Product;
        protected Order Order;
        protected Rule Rule;
        protected IDataAccess DataAccess;
        protected ICouponService CouponService;
        protected IPromotionService PromotionService;
        protected IRuleService RuleService;
        public BusinessBase(CartRequest request,IDataAccess dAccess, ICouponService cService, IPromotionService pService, IRuleService rService)
        {
            Request = request;
            Response = new CartResponse();
            DataAccess = dAccess;
            CouponService = cService;
            PromotionService = pService;
            RuleService = rService;
        }
        protected abstract void GetProductOrder();

        protected abstract void GetRule();

        protected abstract void Calculate();
        public CartResponse Save()
        {
            GetProductOrder();
            GetRule();
            Calculate();
            return Response;
        }
    }
}
