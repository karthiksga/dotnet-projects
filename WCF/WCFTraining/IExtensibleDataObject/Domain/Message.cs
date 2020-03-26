using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
namespace Domain
{
    [MessageContract(IsWrapped = true)]
    public class SaveStudentRequestMessage
    {
        private string schoolName;

        [MessageHeader]
        public string SchoolName
        {
            get { return schoolName; }
            set { schoolName = value; }
        }
        
        private Student stud;

        [MessageBodyMember]
        public Student Stud
        {
            get { return stud; }
            set { stud = value; }
        }
    }

    [MessageContract(IsWrapped = true)]
    public class SaveStudentResponseMessage
    {
        private string outputMessage;

        [MessageBodyMember]
        public string OutputMessage
        {
            get { return outputMessage; }
            set { outputMessage = value; }
        }
    }

    [MessageContract(IsWrapped = true)]
    public class GetStudentRequestMessage
    {
        private Student student;

        [MessageBodyMember]
        public Student Student
        {
            get { return student; }
            set { student = value; }
        }
    }

    [MessageContract(IsWrapped = true)]
    public class GetStudentResponseMessage
    {
        private Student stud;

        [MessageBodyMember]
        public Student Stud
        {
            get { return stud; }
            set { stud = value; }
        }
    }

    [DataContract]
    public class Student//:IExtensibleDataObject
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

        private string description;
        [DataMember]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        //private ExtensionDataObject extensionData;
        //private ExtensionDataObject ext;
        //public ExtensionDataObject ExtensionData
        //{
        //    get
        //    {
        //        return ext;
        //    }
        //    set
        //    {
        //        ext = value;
        //    }
        //}
    }
}
