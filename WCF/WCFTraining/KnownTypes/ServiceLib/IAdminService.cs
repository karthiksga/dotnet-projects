using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ServiceLib
{
    [ServiceContract]
    interface IAdminService
    {
        [OperationContract]
        string AdminOperationA();

        [OperationContract]
        string AdminOperationB();
    }
}
