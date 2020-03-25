using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AngularCustomerService
{
    [DataContract]
    public class CustomerError
    {
        private string _ErrorMsg;
        [DataMember]
        public string ErrorMsg { get { return _ErrorMsg; } set { _ErrorMsg = value; } }
    }
}