using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceLib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.    
    public class Service : IService
    {
        public Domain.StudentA GetStudA(Domain.StudentA stud)
        {
            Domain.StudentA studentA = stud;
            studentA.Name = "FromServer";
            return studentA;
        }

        public Domain.StudentB GetStudB(Domain.StudentB stud)
        {
            Domain.StudentB studentB = stud;
            studentB.Name = "FromServer";
            return studentB;
        }
    }
}
