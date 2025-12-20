using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace myHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(MyLibrary.Service)))
            {
                host.Open();
                Console.WriteLine("host opened and running...");
                Console.ReadLine();
            }
        }
    }
}
