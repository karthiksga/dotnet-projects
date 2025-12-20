using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<IService> factory = new ChannelFactory<IService>("Add");
            factory.Open();
            IService addService = factory.CreateChannel();
            AddService.ServiceClient client = new AddService.ServiceClient();
            Console.WriteLine(client.Add(1,2)+ ":"+ addService.Add(2,4));
            Console.ReadLine();
        }
    }

    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        int Add(int i, int j);
    }
}
