using Autofac;
using ShoppingCart.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.RulesService
{
    public class RuleModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ShoppingCartMockData>().AsImplementedInterfaces();
        }
    }
}
