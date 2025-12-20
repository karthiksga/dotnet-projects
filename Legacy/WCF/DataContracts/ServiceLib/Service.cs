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
        public Domain.MathA AddResultA(Domain.MathA math)
        {
            return new Domain.MathA { Result = math.NumberA + math.NumberB, StartTime = DateTime.Now };
        }

        public Domain.MathB AddResultB(Domain.MathB math)
        {
            return new Domain.MathB { Result = math.NumberA + math.NumberB, StartTime = DateTime.Now };
        }
    }
}
