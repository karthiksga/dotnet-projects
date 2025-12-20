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
        private int id;

        [MessageBodyMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
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
    public class Student
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
    }
}
