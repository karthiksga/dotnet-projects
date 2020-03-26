using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Client
{
    //[ServiceContract(Namespace = "http://www.xyz.com/2010/10")]
    //public interface IHelloWorldService
    //{
    //    [OperationContract]
    //    string Message(string message);
    //}
    class Program
    {
        static void Main(string[] args)
        {
            //IHelloWorldService proxy = ChannelFactory<IHelloWorldService>.CreateChannel(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8000/helloworld"));
            HelloWorldServiceReference.HelloWorldServiceClient proxy = new Client.HelloWorldServiceReference.HelloWorldServiceClient();
            Console.WriteLine(proxy.Message("hello world"));
            Console.ReadLine();
        }
    }
}
