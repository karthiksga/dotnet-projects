using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
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

    [ServiceContract]
    interface IAdminService
    {
        [OperationContract]
        string AdminOperationA();

        [OperationContract]
        string AdminOperationB();
    }
}
