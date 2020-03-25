using CustomerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AngularCustomerService
{
    public class CustomerSvc : ICustomerService
    {
        public Customer GetCustomer(int id)
        {
            throw new FaultException<CustomerError>(new CustomerError { ErrorMsg = "Customer Error" });
            return new Customer { FirstName = "Apple1", LastName = "Apple2", Age = 10, Sex = "M", PhoneNumber = "1234567890" };
        }

        public List<Customer> GetCustomers()
        {
            //throw new FaultException<CustomerError>(new CustomerError { ErrorMsg = "Customer Error" });
            return new List<Customer>
            {
                new Customer { FirstName="Apple1", LastName="Apple2", Age=10, Sex="M", PhoneNumber="1234567890" },
                new Customer { FirstName="Banana1", LastName="Banana2", Age=20, Sex="F", PhoneNumber="5647854262" },
                new Customer { FirstName="Cat1", LastName="Cat2", Age=30, Sex="M", PhoneNumber="6359876542" }
            };
        }
    }
}
