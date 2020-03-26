using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
namespace MyLibrary
{
    public class Service:IService
    {
        public GetStudentResponse GetStudent(GetStudentRequest request)
        {
            return new GetStudentResponse { Stud = new Student { Id = 3 } };
        }
    }

    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        GetStudentResponse GetStudent(GetStudentRequest request);
    }

    [MessageContract(IsWrapped=true)]
    public class GetStudentRequest
    {
        int studId;

        [MessageBodyMember]
        public int StudId
        {
            get { return studId; }
            set { studId = value; }
        }
    }

    [MessageContract(IsWrapped = true)]
    public class GetStudentResponse
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
        int id;

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
