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
using Jounce.Core.ViewModel;
using Jounce.Core.Command;
using Jounce.Core.Event;
using Jounce.Framework;
using Jounce.Framework.Command;

namespace MaFamille.Albums.ViewModels
{
    [ExportAsViewModel("Green")]
    public class GreenViewModel:BaseViewModel
    {
        public IActionCommand<string> NavigateCommand { get; private set; }

        public GreenViewModel()
        {
            NavigateCommand = new ActionCommand<string>(view => { EventAggregator.Publish(view.AsViewNavigationArgs()); },view=>true);
        }
    }
}
