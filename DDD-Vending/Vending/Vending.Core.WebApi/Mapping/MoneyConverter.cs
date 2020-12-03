using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vending.Core.WebApi.Models;

namespace Vending.Core.WebApi.Mapping
{
    public class MoneyConverter : ITypeConverter<MoneyDto, Money>
    {
        public Money Convert(MoneyDto source, Money destination, ResolutionContext context)
        {
            return new Money(source.OneCentCount, 
                             source.TenCentCount, 
                             source.TwentyFiveCentCount, 
                             source.OneDollarCount, 
                             source.FiveDollarCount, 
                             source.TwentyDollarCount);
        }
    }
}
