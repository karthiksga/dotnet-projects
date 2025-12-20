using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new ServiceReference1.ServiceClient().GetSum(1, 4).ToString());
            Console.ReadLine();
        }
    }
}
