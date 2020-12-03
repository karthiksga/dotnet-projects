using AutoMapper;
using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreComposition = Vending.Core.Composition.CompositionRoot;

namespace Vending.Core.WebApi.Composition
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IMapper>(factory=> CreateConfiguration().CreateMapper());
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(GetType().Assembly);
                cfg.AddMaps(typeof(CoreComposition).Assembly);
            });
            return config;
        }
    }
}
