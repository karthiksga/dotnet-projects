using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WCFBusiness;
namespace WCFService
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(WCFBusiness.Service));
            host.Open();
            Console.WriteLine("service is running...");
            Console.ReadLine();
        }
    }
}
