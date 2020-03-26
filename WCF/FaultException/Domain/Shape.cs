using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Domain
{
    [DataContract]
    [KnownType(typeof(Square))]
    [KnownType(typeof(Triangle))]
    public class Shape
    {
        
        private string type;

        [DataMember]
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public Shape()
        {
        }        
    } 
}
