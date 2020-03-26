using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MsmqClientSample
{
    class Program
    {
        static void Main(string[] args)
        {
            MyMQ.OrderClient client = new MyMQ.OrderClient();
            client.DisplayOrder(1);
            client.DisplayOrder(2);
            client.DisplayOrder(3);
            client.DisplayOrder(4);
            Console.ReadLine();
        }
    }
}
