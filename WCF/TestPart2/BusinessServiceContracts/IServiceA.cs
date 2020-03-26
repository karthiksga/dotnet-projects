using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BusinessServiceContracts
{
    // NOTE: If you change the interface name "IServiceA" here, you must also update the reference to "IServiceA" in App.config.
    [ServiceContract(Namespace = "http://www.thatindigogirl.com/2010/10")]
    public interface IServiceA
    {
        [OperationContract]
        string Operation1();

        [OperationContract]
        string Operation2();
    }
}
