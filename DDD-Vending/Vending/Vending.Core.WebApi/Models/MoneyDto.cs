using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Vending.Core.WebApi.Models
{    
    public class MoneyDto
    {        
        public int OneCentCount { get; set; }
        public int TenCentCount { get; set; }
        public int TwentyFiveCentCount { get; set; }
        public int OneDollarCount { get; set; }
        public int FiveDollarCount { get; set; }
        public int TwentyDollarCount { get; set; }
    }
}
