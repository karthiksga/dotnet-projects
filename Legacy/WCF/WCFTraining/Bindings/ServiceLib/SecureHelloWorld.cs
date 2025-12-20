using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
namespace ServiceLib
{
    public class SecureHelloWorld:ISecureHelloWorld
    {
        public string HelloWorld()
        {
            return "hello world secure";
        }
    }

    [ServiceContract(Namespace = "http://www.thatindigogirl.com/samples/2006/06")]
    public interface ISecureHelloWorld
    {
        [OperationContract]
        string HelloWorld();
    }
}
