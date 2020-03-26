using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLib
{
    public class AdminService:IAdminService
    {
        public string AdminOperationA()
        {
            return "this is AdminOperation A";
        }

        public string AdminOperationB()
        {
            return "this is AdminOperation B";
        }
    }
}
