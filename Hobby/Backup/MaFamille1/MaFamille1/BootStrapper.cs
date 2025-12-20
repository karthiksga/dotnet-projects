using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.MefExtensions;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
namespace MaFamille1
{
    public class BootStrapper:MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            CanvasScreen shell = new CanvasScreen();
            Application.Current.RootVisual = shell;
            return shell;
        }

        protected override Microsoft.Practices.Prism.Modularity.IModuleCatalog CreateModuleCatalog()
        {
            return Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(new Uri("/MaFamille1;component/ModuleCatalog.xaml", UriKind.Relative));
        }

        protected override CompositionContainer CreateContainer()
        {
            var container = base.CreateContainer();
            CompositionHost.Initialize(container);
            return container;
        }

        //protected override void ConfigureAggregateCatalog()
        //{
        //    base.ConfigureAggregateCatalog();
        //    this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(BootStrapper).Assembly));            
        //}

        //protected override Microsoft.Practices.Prism.Regions.RegionAdapterMappings ConfigureRegionAdapterMappings()
        //{
        //    var mappings = base.ConfigureRegionAdapterMappings();
        //    mappings.RegisterMapping(typeof(Frame), new FrameRegionAdapter(ServiceLocator.Current.GetInstance<IRegionBehaviorFactory>()));
        //    return mappings;
        //}

        //protected override void InitializeModules()
        //{
        //    base.InitializeModules();
        //}

        //protected override DependencyObject CreateShell()
        //{
        //    return this.Container.GetExportedValue<ShellNavigation>();
        //}

        //protected override void InitializeShell()
        //{
        //    base.InitializeShell();
        //    Application.Current.RootVisual = (UIElement)this.Shell;
        //}
    }
}
