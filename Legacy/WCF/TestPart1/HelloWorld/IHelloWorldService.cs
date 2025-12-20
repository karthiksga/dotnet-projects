using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HelloWorld
{
    // NOTE: If you change the interface name "IHelloWorldService" here, you must also update the reference to "IHelloWorldService" in App.config.
    [ServiceContract(Namespace="http://www.xyz.com/2010/10")]
    public interface IHelloWorldService
    {
        [OperationContract]
        string Message(string message);
    }
}
