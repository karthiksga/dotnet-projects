﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceLib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service : IService, IInternalService, IAdminService
    {
        public int Add(int i, int j)
        {
            return i + j;
        }

        public string InternalOperationA()
        {
            return "this is InternalOperation A";
        }

        public string InternalOperationB()
        {
            return "this is InternalOperation B";
        }

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