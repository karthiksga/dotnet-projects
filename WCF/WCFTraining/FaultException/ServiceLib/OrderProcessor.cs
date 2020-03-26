using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
namespace ServiceLib
{    
    [ServiceContract]
    public interface IOrderProcessor
    {
        [OperationContract(IsOneWay=true)]
        void SubmitOrder(int order);
    }
}
