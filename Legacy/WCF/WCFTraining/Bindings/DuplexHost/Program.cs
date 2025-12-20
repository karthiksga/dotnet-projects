using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace DuplexHost
{
    
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host=new ServiceHost(typeof(SampleDuplex)))
            {
                host.Open();
                Console.WriteLine("host opened...");
                Console.ReadLine();
            }
            
        }        
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class SampleDuplex : InterfaceDuplex
    {
        public void doHugeTask()
        {
            Console.WriteLine("Called by client...");
            System.Threading.Thread.Sleep(5000);          
            Console.WriteLine("Task done...");
            OperationContext.Current.GetCallbackChannel<InterfaceDuplexCallBack>().taskCompleted();
        }
    }


    [ServiceContract(SessionMode=SessionMode.Required,CallbackContract=typeof(InterfaceDuplexCallBack))]
    public interface InterfaceDuplex
    {
        [OperationContract(IsOneWay = true)]
        void doHugeTask();
    }


    public interface InterfaceDuplexCallBack
    {
        [OperationContract(IsOneWay = true)]
        void taskCompleted();
    }
}
