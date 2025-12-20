using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HelloIndigo
{
    // NOTE: If you change the interface name "IHelloIndigoService" here, you must also update the reference to "IHelloIndigoService" in App.config.
    [ServiceContract(Namespace="http://www.thatindigogirl.com/2007/07")]
    public interface IHelloIndigoService
    {
        [OperationContract]
        String HelloIndigo(string message);
    }
}
