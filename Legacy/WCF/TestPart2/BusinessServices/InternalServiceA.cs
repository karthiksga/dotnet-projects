using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessServiceContracts;
namespace BusinessServices
{
    public class InternalServiceA:IAdmin,IServiceA
    {
        #region IAdmin Members

        public string AdminOperation1()
        {
            return "Invoked IAdmin.Operation1";
        }

        public string AdminOperation2()
        {
            return "Invoked IAdmin.Operation2";
        }

        #endregion

        #region IServiceA Members

        public string Operation1()
        {
            return "Invoked IServiceA.Operation1";
        }

        public string Operation2()
        {
            return "Invoked IServiceA.Operation2";
        }

        #endregion
    }
}
