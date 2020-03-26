using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ServiceLib;
using System.Configuration;
using System.Messaging;
namespace MsmqHost
{
    public class Program:IOrderProcessor
    {
        static void Main(string[] args)
        {
            string queueName = ConfigurationManager.AppSettings["queue"];
            if (!MessageQueue.Exists(queueName))
            {
                MessageQueue.Create(queueName);
            }

            using (ServiceHost host = new ServiceHost(typeof(Program)))
            {
                host.Open();
                Console.WriteLine("host opened...");
                Console.ReadLine();
            }
        }

        //[OperationBehavior(TransactionScopeRequired=true,TransactionAutoComplete=true)]
        public void SubmitOrder(int order)
        {
            Console.WriteLine("Order no:" + order);
        }
    }
}
