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
        int Add(Domain.Math math);

        [OperationContract]
        Domain.Math AddResult(Domain.Math math);

        [OperationContract]
        [ServiceKnownType(typeof(Square))]
        string GetShape(Domain.Shape shape);

        // TODO: Add your service operations here
    }    
    
}
