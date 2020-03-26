using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = null;
            try
            {
                host = new ServiceHost(typeof(BusinessServices.InternalServiceA));
                host.Open();
                Console.ReadLine();         
            }
            finally
            {
                host.Close();
            }            
        }
    }
}
