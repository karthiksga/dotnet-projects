using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Domain;

namespace ServiceLib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.    
    
    public class Service : IService,IWSService
    {   
        public void ThrowException()
        {
            throw new NotImplementedException();
            //throw new FaultException("Method not implemented");
        }

        public void ThrowExceptionOneWay()
        {
            throw new NotImplementedException();
            //throw new FaultException<NotImplementedException>(new NotImplementedException("Method Not Implemented"), "Not Implemented");            
        }

        public void SetCounter1(int i)
        {
            try
            {
                if (i > 15)
                    throw new InvalidOperationException();                
            }
            catch (InvalidOperationException ex)
            {                
                throw new FaultException<MyFault>(new MyFault { Description = "Invalid Operation" },"This is invalid operation");
            }                        
        }

        public void SetCounter2(int i)
        {
            if (i < 10)
                throw new InvalidOperationException();
            else if (i > 10)
                throw new NotImplementedException();         
        }

        public string GoodOperation()
        {
            return "have a nice day";
        }

        public string GoodOperationWS()
        {
            return "this message is from wsHttpBinding";
        }

        public void ThrowExceptionWS()
        {
            throw new NotImplementedException();
        }
    }
}
