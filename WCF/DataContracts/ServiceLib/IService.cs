using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Domain;
namespace ServiceLib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        Domain.MathA AddResultA(Domain.MathA math);

        [OperationContract]
        Domain.MathB AddResultB(Domain.MathB math);

        // TODO: Add your service operations here
    }    
    
}
