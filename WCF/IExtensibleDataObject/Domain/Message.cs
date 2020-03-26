using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
namespace Domain
{

    [DataContract]
    public class StudentA
    {
        private int studentId;

        [DataMember]
        public int StudentId
        {
            get { return studentId; }
            set { studentId = value; }
        }
        private string name;

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        //private string description;
        //[DataMember]
        //public string Description
        //{
        //    get { return description; }
        //    set { description = value; }
        //}        
    }

    [DataContract]
    public class StudentB:IExtensibleDataObject
    {
        private int studentId;

        [DataMember]
        public int StudentId
        {
            get { return studentId; }
            set { studentId = value; }
        }
        private string name;

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        
        //private string description;
        //[DataMember]
        //public string Description
        //{
        //    get { return description; }
        //    set { description = value; }
        //} 
                
        private ExtensionDataObject ext;
        public ExtensionDataObject ExtensionData
        {
            get
            {
                return ext;
            }
            set
            {
                ext = value;
            }
        }
    }
}
