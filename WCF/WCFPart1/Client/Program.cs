using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Client
{
    //[ServiceContract(Namespace = "http://www.thatindigogirl.com/2007/07")]
    //public interface IHelloIndigoService
    //{
    //    [OperationContract]
    //    String HelloIndigo(string message);
    //}

    class Program
    {
        static void Main(string[] args)
        {
            //IHelloIndigoService proxy = ChannelFactory<IHelloIndigoService>.CreateChannel(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:9000/HelloIndigo"));
            ServiceReference1.HelloIndigoServiceClient proxy = new Client.ServiceReference1.HelloIndigoServiceClient();
            
            //Console.WriteLine(proxy.HelloIndigo("Hello from client..."));
            Console.WriteLine(proxy.HelloIndigo(("Hello from client...")));
            Console.ReadLine();
        }
    }
}
