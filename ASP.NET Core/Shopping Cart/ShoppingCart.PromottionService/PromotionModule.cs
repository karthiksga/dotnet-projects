using Autofac;
using ShoppingCart.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.PromottionService
{
    public class PromotionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ShoppingCartMockData>().AsImplementedInterfaces();
        }
    }
}
