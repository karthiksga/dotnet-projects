using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;


namespace ServiceLib
{
    [ServiceContract(Name="InternalService",Namespace="http://www.mysamplewcfservice.com/2012/08")]
    interface IInternalService
    {
        [OperationContract(Name="InternalOperation-A")]
        string InternalOperationA();

        [OperationContract]
        string InternalOperationB();
    }
}
