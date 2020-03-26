using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Channels;
using WCFServiceLibrary;

namespace WCFService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceHost host = new ServiceHost(typeof(WCFServiceLibrary.Service));
                host.Open();
                Console.WriteLine(string.Format("The service is running....."));
            }
            catch (Exception exc)
            {
                Console.WriteLine(string.Format("Error: {0}",exc.Message));
            }
            Console.ReadLine();            
        }
    }
}
