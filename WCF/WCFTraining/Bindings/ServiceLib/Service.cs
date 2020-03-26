using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Domain;

namespace ServiceLib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.    
    public class Service : IService, IInternalService, IAdminService
    {
        private Domain.Math objMath;

        public int Add(Domain.Math math)
        {
            return math.NumberA+math.NumberB;
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

        public Domain.Math AddResult(Domain.Math math)
        {
            return new Domain.Math { Result = math.NumberA + math.NumberB,StartTime=DateTime.Now };
        }


        public string GetShape(Domain.Shape shape)
        {
            return shape.Type;
        }


        public Domain.GetStudentResponseMessage GetStudent(Domain.GetStudentRequestMessage reqMessage)
        {
            return new Domain.GetStudentResponseMessage { Stud = new Domain.Student { Name = "sfsdfsd", StudentId = reqMessage.Id } };
        }

        public Domain.SaveStudentResponseMessage SaveStudent(Domain.SaveStudentRequestMessage reqMessage)
        {
            //Save student based on data from reqMessage
            //reqMessage.Stud.Name;
            //reqMessage.Stud.StudentId;
            return new Domain.SaveStudentResponseMessage { OutputMessage = "Saved" };
        }
    }

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        int Add(Domain.Math math);

        [OperationContract]
        Domain.Math AddResult(Domain.Math math);

        [OperationContract]
        [ServiceKnownType(typeof(Square))]
        string GetShape(Domain.Shape shape);

        [OperationContract]
        GetStudentResponseMessage GetStudent(GetStudentRequestMessage reqMessage);

        [OperationContract]
        SaveStudentResponseMessage SaveStudent(SaveStudentRequestMessage reqMessage);


        // TODO: Add your service operations here
    }  
}
