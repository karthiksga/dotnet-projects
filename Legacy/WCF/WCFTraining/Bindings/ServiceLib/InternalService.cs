using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
namespace ServiceLib
{
    public class InternalService:IInternalService
    {
        public string InternalOperationA()
        {
            return "this is InternalOperationA";
        }

        public string InternalOperationB()
        {
            return "this is InternalOperationB";
        }
    }

    [ServiceContract(Name = "InternalService", Namespace = "http://www.mysamplewcfservice.com/2012/08")]
    interface IInternalService
    {
        [OperationContract(Name = "InternalOperation-A")]
        string InternalOperationA();

        [OperationContract]
        string InternalOperationB();
    }
}
