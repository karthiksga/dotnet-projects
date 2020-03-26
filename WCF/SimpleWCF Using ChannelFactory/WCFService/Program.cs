using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WCFServiceLibrary;
using WCFBusiness;
namespace WCFService
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost service = new ServiceHost(typeof(WCFBusiness.Service));
            service.Open();
            Console.WriteLine("service is running...");            
            Console.ReadLine();
        }
    }
}
