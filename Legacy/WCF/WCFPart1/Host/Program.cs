using System;
using System.Collections.Generic;

using System.Text;
using HelloIndigo;
using System.ServiceModel;
namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(HelloIndigo.HelloIndigoService)))
            {
                host.AddServiceEndpoint(typeof(HelloIndigo.IHelloIndigoService), new NetTcpBinding(), "net.tcp://localhost:9000/HelloIndigo");
                host.Open();
                Console.ReadLine();
            }            
        }
    }
}
