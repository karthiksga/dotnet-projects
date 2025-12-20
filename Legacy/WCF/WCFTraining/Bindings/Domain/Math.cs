using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Domain
{
    [DataContract]
    public class Math
    {
        private int numberA;
        [DataMember(Name="NumA")]
        public int NumberA
        {
            get { return numberA; }
            set { numberA = value; }
        }

        private int numberB;
        [DataMember(Name="NumB")]
        public int NumberB
        {
            get { return numberB; }
            set { numberB = value; }
        }

        private int result;
        [DataMember(IsRequired=true)]
        public int Result
        {
            get { return result; }
            set { result = value; }
        }

        private DateTime startTime;
        //[DataMember(IsRequired=true,EmitDefaultValue=false)]
        [DataMember(IsRequired = true)]
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        private string string1;
    }
}
