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
            mathService.Open();            
            Console.WriteLine("Host opened and listening");
            Console.ReadLine();
        }
    }
}