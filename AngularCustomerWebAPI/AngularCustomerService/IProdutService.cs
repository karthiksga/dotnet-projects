using CustomerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AngularCustomerService
{
    [ServiceContract]
    interface IProductService
    {
        [OperationContract]
        Product GetProduct(int id);
        [OperationContract]
        List<Product> GetProducts();
    }
}
