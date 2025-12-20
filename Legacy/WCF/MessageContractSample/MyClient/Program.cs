using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MyMessageService.ServiceClient client = new MyMessageService.ServiceClient();
            MyMessageService.Student stud= client.GetStudent(1);
            Console.WriteLine(stud.Id);
            Console.ReadLine();
        }
    }
}
