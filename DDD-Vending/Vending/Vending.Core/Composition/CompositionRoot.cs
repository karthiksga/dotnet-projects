using System;
using System.Collections.Generic;
using System.Text;
using LightInject;
using Vending.Core.Infrastructure;

namespace Vending.Core.Composition
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<VendingMachine>();
            serviceRegistry.Register<IVendingService, VendingService>();

        }
    }
}
