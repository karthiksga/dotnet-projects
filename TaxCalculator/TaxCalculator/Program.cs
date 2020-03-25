using ShoppingCart.Business;
using ShoppingCart.DataModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Console
{
    class Program
    {
        private static Order Order = new Order();
        static void Main(string[] args)
        {
            CartResponse response = new ShoppingCartApp().AddToCart(1, 2, "GA");
            if (response != null && response.Product != null)
            {
                Order.Products.Add(response.Product);
            }
        }        
    }

    public class ShoppingCartApp
    {        
        List<Product> Products;
        public ShoppingCartApp()
        {
            BuildProduct();
        }
        public CartResponse AddToCart(int productid, int quantity, string state)
        {
            Product p = Products.Where(s => s.Id == productid).FirstOrDefault();
            if (p == null) return null;
            p.Quantity = quantity;
            CartRequest req = new CartRequest { Product = p, State = state };
            return new TaxCalculator(req).Calculate();
        }

        private void BuildProduct()
        {
            Products = new List<Product>();
            Products.Add(new Product { Id = 1, Name = "Watch", Cost = 2.0m });
            Products.Add(new Product { Id = 2, Name = "Toy", Cost = 5.0m });
        }
    }
}
