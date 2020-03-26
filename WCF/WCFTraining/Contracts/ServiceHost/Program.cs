using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
namespace ServicesHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost internalServiceHost = new ServiceHost(typeof(ServiceLib.InternalService));
            ServiceHost adminServiceHost = new ServiceHost(typeof(ServiceLib.AdminService));
            internalServiceHost.Open();
            adminServiceHost.Open();
            Console.WriteLine("Host opened and listening");
            Console.ReadLine();
        }
    }
}
