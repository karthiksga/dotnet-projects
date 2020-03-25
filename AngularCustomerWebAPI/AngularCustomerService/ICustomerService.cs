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
    interface ICustomerService
    {
        [OperationContract]
        [FaultContract(typeof(CustomerError))]
        Customer GetCustomer(int id);

        [OperationContract]
        [FaultContract(typeof(CustomerError))]
        List<Customer> GetCustomers();
    }
}
