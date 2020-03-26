using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Domain;
namespace ServiceLib
{    
    [ServiceContract]    
    public interface IService
    {
        [OperationContract]
        [ServiceKnownType(typeof(Square))] //KnownType can be specified here also....
        string GetShape(Domain.Shape shape);
    }    
    
}
