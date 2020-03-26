using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using WCFServiceLibrary;

namespace WCFClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChannelFactory<WCFServiceLibrary.IService> factory = new ChannelFactory<IService>("ClientTcpBinding");
                factory.Open();
                IService remoteService = factory.CreateChannel();
                int x=4;
                int y=4;
                int retVal = remoteService.GetProduct(x, y);

                Console.WriteLine(string.Format("The product of {0} and {1} is {2} ",x,y,retVal));
            }
            catch (Exception exc)
            {
                Console.WriteLine(string.Format("Error:{0}",exc.Message));
            }
            Console.ReadLine();
        }
    }
}
