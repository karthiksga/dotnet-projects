using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace WCFServiceLibrary
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        int GetProduct(int x, int y);        
    }
}
