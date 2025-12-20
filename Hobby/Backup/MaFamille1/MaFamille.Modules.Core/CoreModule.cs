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
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;
using MaFamille.Modules.Core.Views;
namespace MaFamille.Modules.Core
{
    [ModuleExport(typeof(CoreModule))]
    public class CoreModule:IModule
    {
        [Import]
        public IRegionManager RegionManager { get; set; }

        public void Initialize()
        {
            var view = new MyAlbumNew();
            RegionManager.AddToRegion("ContentRegion", view);
        }
    }
}
