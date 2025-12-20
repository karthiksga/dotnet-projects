using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BusinessServiceContracts
{
    // NOTE: If you change the interface name "IServiceB" here, you must also update the reference to "IServiceB" in App.config.
    [ServiceContract(Namespace = "http://www.thatindigogirl.com/2010/10")]
    public interface IServiceB
    {
        [OperationContract]
        void DoWork();
    }
}
