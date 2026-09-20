using ShoppingCart.DataModel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.CouponService
{
    public class CouponBusiness : ICouponService
    {
        IDataAccess DataAccess;
        public CouponBusiness(IDataAccess dataAccess)
        {
            DataAccess = dataAccess;
        }
        public decimal GetValue(string code)
        {
            var coupon= DataAccess.Discounts.Where(d => d.Type == DataModel.Model.DiscountType.Coupon && d.Code.Equals(code)).FirstOrDefault();
            if(coupon!=null)
            {
                var date = coupon.StartDate.AddDays(coupon.DaysAfterExpire);
                if (DateTime.Now < date)
                {
                    return coupon.Value;
                }
                else
                {
                    return 0.0m;
                }               
            }
            else
            {
                return 0.0m;
            }
        }
    }
}
