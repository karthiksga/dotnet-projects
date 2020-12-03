using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vending.Core.WebApi.Models;

namespace Vending.Core.WebApi.Mapping
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            CreateMap<MoneyDto, Money>().ConvertUsing(new MoneyConverter());
        }
    }
}
