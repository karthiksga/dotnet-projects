using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
namespace ServiceContracts
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        int GetSum(int i, int j);
    }
}
