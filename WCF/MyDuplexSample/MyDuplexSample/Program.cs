using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
namespace MyDuplexSample
{
    class Program
    {
        static void Main(string[] args)
        {
            using(ServiceHost host=new ServiceHost(typeof(Service)))
            {
                host.Open();
                Console.WriteLine("host opened and running");
                Console.ReadLine();
            }
        }
    }

    public class Service : IServiceDuplex
    {
        public void DoHugeTask()
        {
            Console.WriteLine("Called from client");
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("Task done...");
            Console.WriteLine("Calling client");
            OperationContext.Current.GetCallbackChannel<IServiceClientDuplex>().taskCompleted();
        }
    }

    [ServiceContract(SessionMode=SessionMode.Required,CallbackContract=typeof(IServiceClientDuplex))]
    public interface IServiceDuplex
    {
        [OperationContract(IsOneWay=true)]
        void DoHugeTask();
    }

    
    public interface IServiceClientDuplex
    {
        [OperationContract(IsOneWay=true)]
        void taskCompleted();
    }
}
