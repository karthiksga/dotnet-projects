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
        string GoodOperation();

        [OperationContract]
        void ThrowException();

        [OperationContract(IsOneWay=true)]
        void ThrowExceptionOneWay();        

        [OperationContract]
        [FaultContract(typeof(MyFault))]
        void SetCounter1(int i);

        [OperationContract]            
        [FaultContract(typeof(MyFault))]
        void SetCounter2(int i);

        // TODO: Add your service operations here
    }

    [ServiceContract]
    public interface IWSService
    {
        [OperationContract]
        string GoodOperationWS();

        [OperationContract]
        void ThrowExceptionWS();
    }
    
}
