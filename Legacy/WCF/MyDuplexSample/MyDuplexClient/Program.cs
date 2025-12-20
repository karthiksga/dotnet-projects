using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace MyDuplexClient
{
    class Program
    {
        static void Main(string[] args)
        {
            InstanceContext instance=new InstanceContext(new MyCallBack());
            MyDuplex.ServiceDuplexClient client = new MyDuplex.ServiceDuplexClient(instance);
            Console.WriteLine("Calling server...");
            client.DoHugeTask();
            Console.ReadLine();
        }
    }

    public class MyCallBack : MyDuplex.IServiceDuplexCallback
    {
        public void taskCompleted()
        {
            Console.WriteLine("Called from server...");
        }
    }
}
