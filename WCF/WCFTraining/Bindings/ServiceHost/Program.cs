﻿using System;
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
            ServiceHost mathService = new ServiceHost(typeof(ServiceLib.Service));
            ServiceHost internalServiceHost = new ServiceHost(typeof(ServiceLib.InternalService));
            ServiceHost adminServiceHost = new ServiceHost(typeof(ServiceLib.AdminService));
            ServiceHost customBindHost = new ServiceHost(typeof(ServiceLib.CustomBind));
            mathService.Open();
            internalServiceHost.Open();
            adminServiceHost.Open();
            customBindHost.Open();
            Console.WriteLine("Host opened and listening");
            Console.ReadLine();
        }
    }
}