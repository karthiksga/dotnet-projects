using CustomerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularCustomerService
{
    public class ProductSvc : IProductService
    {
        public Product GetProduct(int id)
        {
            return new Product { Name="abc", Color="blue", Discount=10, Price=2000, Type="Box"  };
        }

        public List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product { Name="abc", Color="blue", Discount=10, Price=2000, Type="Box"  },
                new Product { Name="abc", Color="red", Discount=20, Price=3000, Type="Car"  },
                new Product { Name="abc", Color="green", Discount=30, Price=4500, Type="Bike"  }
            };
        }
    }
}
