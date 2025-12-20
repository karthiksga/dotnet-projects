using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
namespace ServiceLib
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    public class CustomBind:ICustomBind
    {
        int i = 0;
        public string HelloWorld()
        {
            return "Hellow World";
        }

        public int GetCounter()
        {
            return i += 1;
        }
    }

    [ServiceContract]
    public interface ICustomBind
    {
        [OperationContract]
        string HelloWorld();

        [OperationContract]
        int GetCounter();
    }
}
