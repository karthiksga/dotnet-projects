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
using System.ComponentModel.Composition;
using Jounce.Core.Application;
using Jounce.Framework;

namespace MaFamille.Albums
{
    [Export(typeof(IModuleInitializer))]
    public class ModuleInit : IModuleInitializer
    {
        public bool Initialized { get; set; }

        public void Initialize()
        {
            //JounceHelper.ExecuteOnUI(() => MessageBox.Show("Dynamic module loaded."));
            Initialized = true;
        }
    }
}
