using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
namespace SecureWorldLib
{
    public class SecureWorld:ISecureWorld
    {
        public string HelloWorld()
        {
            return "hai";
        }
    }

    [ServiceContract]
    public interface ISecureWorld
    {
        [OperationContract]
        string HelloWorld();
    }
}
