using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BusinessServiceContracts
{
    // NOTE: If you change the interface name "IAdmin" here, you must also update the reference to "IAdmin" in App.config.
    [ServiceContract(Namespace="http://www.thatindigogirl.com/2010/10")]
    public interface IAdmin
    {
        [OperationContract]
        string AdminOperation1();

        [OperationContract]
        string AdminOperation2();
    }
}
