using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //ChannelFactory<SecureWorldLib.ISecureWorld> factory = new ChannelFactory<SecureWorldLib.ISecureWorld>("Secure");
            //factory.Open();
            //SecureWorldLib.ISecureWorld service = factory.CreateChannel();
            //Console.WriteLine(service.HelloWorld());

            ServiceReference1.SecureWorldClient client = new ServiceReference1.SecureWorldClient();
            Console.WriteLine(client.HelloWorld());
        }
    }
}
