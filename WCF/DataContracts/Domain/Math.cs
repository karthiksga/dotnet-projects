using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Domain
{       
    [Serializable]
    public class MathA
    {
        private int numberA;                
        public int NumberA
        {
            get { return numberA; }
            set { numberA = value; }
        }

        private int numberB;        
        public int NumberB
        {
            get { return numberB; }
            set { numberB = value; }
        }

        private int result;        
        public int Result
        {
            get { return result; }
            set { result = value; }
        }

        [NonSerialized]        
        private DateTime startTime;        
        
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }        
    }

    [DataContract]    
    public class MathB
    {
        private int numberA;
        [DataMember(Name = "NumA")]
        public int NumberA
        {
            get { return numberA; }
            set { numberA = value; }
        }

        private int numberB;
        [DataMember(Name = "NumB")]
        public int NumberB
        {
            get { return numberB; }
            set { numberB = value; }
        }

        private int result;
        [DataMember]
        public int Result
        {
            get { return result; }
            set { result = value; }
        }
                
        private DateTime startTime;
        [DataMember(IsRequired = true, EmitDefaultValue = false)]        
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }        
    }
}
