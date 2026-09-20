using ShoppingCart.DataModel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.PromottionService
{
    public class PromotionBusiness:IPromotionService
    {
        IDataAccess DataAccess;
        public PromotionBusiness(IDataAccess dataAccess)
        {
            DataAccess = dataAccess;
        }
        public decimal GetValue()
        {
            var promotion = DataAccess.Discounts.Where(d => d.Type == DataModel.Model.DiscountType.Promotion && DateTime.Now < d.StartDate.AddDays(d.DaysAfterExpire)).FirstOrDefault();
            if (promotion != null)
            {
                return promotion.Value;
            }
            else
            {
                return 0.0m;
            }
        }
    }
}
