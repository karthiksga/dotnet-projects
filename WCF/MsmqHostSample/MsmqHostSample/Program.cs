using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Messaging;
using System.Configuration;
namespace MsmqHostSample
{
    class Program:IOrder
    {
        static void Main(string[] args)
        {
            string queue = ConfigurationManager.AppSettings["myQueue"];
            if (!MessageQueue.Exists(queue))
            {
                MessageQueue.Create(queue);
            }
            using (ServiceHost host=new ServiceHost(typeof(Program)))
            {
                host.Open();
                Console.WriteLine("host opened and running");
                Console.ReadLine();
            }
        }

        public void DisplayOrder(int i)
        {
            Console.WriteLine(i);
        }
    }    

    [ServiceContract]
    public interface IOrder
    {
        [OperationContract(IsOneWay=true)]
        void DisplayOrder(int i);
    }
}

