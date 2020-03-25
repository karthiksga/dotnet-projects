using Autofac;
using Autofac.Core;
using ShoppingCart.Business;
using ShoppingCart.CouponService;
using ShoppingCart.DataAccess;
using ShoppingCart.DataModel.Interface;
using ShoppingCart.DataModel.Model;
using ShoppingCart.PromottionService;
using ShoppingCart.RulesService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Console
{
    class Program
    {
        private static Order Order = new Order { Products = new List<Product>() };
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {
            Startup();
            //GA
            using (ShoppingCartApp app = new ShoppingCartApp())
            {
                app.Container = Container;
                Order.Products.Clear();
                var state = "GA";
                Product prd1 = app.AddToCart(1, 2, state, "efgh").Product;
                Order.Products.Add(prd1);
                Product prd2 = app.AddToCart(3, 1, state, "ijkl").Product;
                Order.Products.Add(prd2);
                Order = app.FinalizeOrder(Order.Products, state).Order;
            }

            //FL
            using (ShoppingCartApp app = new ShoppingCartApp())
            {
                app.Container = Container;
                Order.Products.Clear();
                var state = "FL";
                Product prd1 = app.AddToCart(1, 2, state, "efgh").Product;
                Order.Products.Add(prd1);
                Product prd2 = app.AddToCart(3, 1, state, "ijkl").Product;
                Order.Products.Add(prd2);
                Order = app.FinalizeOrder(Order.Products, state).Order;
            }
        }

        static void Startup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ShoppingCartMockData>().AsImplementedInterfaces();
            builder.RegisterAssemblyModules(typeof(CouponModule).Assembly);
            builder.RegisterAssemblyModules(typeof(PromotionModule).Assembly);
            builder.RegisterAssemblyModules(typeof(RuleModule).Assembly);
            builder.RegisterType<CouponBusiness>().As<ICouponService>();
            builder.RegisterType<PromotionBusiness>().As<IPromotionService>();
            builder.RegisterType<RuleBusiness>().As<IRuleService>();
            builder.RegisterGeneric(typeof(CartBusiness<>)).InstancePerLifetimeScope();
            Container = builder.Build();
        }
    }

    public class ShoppingCartApp:IDisposable
    {      
        public IContainer Container { get; set; }
       
        public CartResponse AddToCart(int productid, byte quantity, string state, string code)
        {
            using (ILifetimeScope scope = Container.BeginLifetimeScope())
            {
                CartRequest request = new CartRequest { ProductId = productid, Quantity = quantity, State = state, CouponCode = code };
                return scope.Resolve<CartBusiness<ProductBusiness>>().Create(request).Save();
            }
        }

        public CartResponse FinalizeOrder(List<Product> products, string state)
        {
            using (ILifetimeScope scope = Container.BeginLifetimeScope())
            {
                CartRequest request = new CartRequest { Products =products, State=state };
                return scope.Resolve<CartBusiness<OrderBusiness>>().Create(request).Save();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
