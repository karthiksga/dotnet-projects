using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            InternalServiceAReference.AdminClient admin = new InternalServiceAReference.AdminClient();
            Console.WriteLine(admin.AdminOperation1());
        }
    }
}
