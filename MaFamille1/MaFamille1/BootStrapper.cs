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
            //return base.CreateModuleCatalog();
            return Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(new Uri("/MaFamille1;component/ModuleCatalog.xaml",UriKind.Relative));
        }
    }
}
