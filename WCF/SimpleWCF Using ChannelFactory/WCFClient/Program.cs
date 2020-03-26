using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WCFServiceLibrary;
namespace WCFClient
{
    class Program
    {
        static void Main(string[] args)
        {   
            ChannelFactory<WCFServiceLibrary.IService> factory = new ChannelFactory<IService>("ClientTcpBinding");
            factory.Open();
            IService serv = factory.CreateChannel();
            Console.WriteLine(serv.GetProduct(1, 3));
            Console.ReadLine();            
        }
    }
}
