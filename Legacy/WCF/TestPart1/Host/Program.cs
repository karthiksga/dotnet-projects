using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using(ServiceHost host=new ServiceHost(typeof(HelloWorld.HelloWorldService)))
            {
                host.AddServiceEndpoint(typeof(HelloWorld.IHelloWorldService), new NetTcpBinding(), "net.tcp://localhost:8000/helloworld");
                host.Open();
                Console.ReadLine();
            }
        }
    }
}
