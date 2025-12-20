using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
}
