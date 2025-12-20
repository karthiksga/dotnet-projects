using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MaFamille.CommonModel
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