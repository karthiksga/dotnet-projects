using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ServiceLib
{
    class SampleDuplex:InterfaceDuplex
    {
        public void doHugeTask()
        {
            
        }
    }

    [ServiceContract]    
    public interface InterfaceDuplex
    {
        [OperationContract(IsOneWay=true)]
        void doHugeTask();
    }

    
    public interface InterfaceDuplexCallBack
    {
        [OperationContract(IsOneWay=true)]
        void taskCompleted();
    }

}
