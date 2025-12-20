using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WcfService1.Model
{
    [DataContract]
    public class FileUploadModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int ID { get; set; }
    }
}