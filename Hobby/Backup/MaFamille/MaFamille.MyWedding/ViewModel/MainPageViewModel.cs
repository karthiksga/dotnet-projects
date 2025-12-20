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
using Jounce.Core.Event;
using Jounce.Core.View;
using Jounce.Core.Command;
using Jounce.Framework;
using System.Linq;
using Jounce.Core.Application;
using Jounce.Framework.View;
using Jounce.Core.ViewModel;
using Jounce.Framework.Command;
using MaFamille.MyWedding.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MaFamille.MyWedding.ViewModel
{
    [ExportAsViewModel("MainPageViewModel")]
    public class MainPageViewModel : BaseViewModel, IPartImportsSatisfiedNotification
    {
        public IActionCommand<object> ClickCommand { get; set; }       

        public UserControl CurrentContent
        {
            get
            {
                return currentContent;
            }
            set
            {
                currentContent = value;
                RaisePropertyChanged(() => CurrentContent);
            }
        }private UserControl currentContent;

        [Import]
        public IEventAggregator EventAggregator { get; set; }

        public MainPageViewModel()
        {            
            // this is just an easy way to pull out the pieces we want/need, normally we'd
            // probably type this more strongly
            _buttonInfo = new ObservableCollection<Tuple<ICommand, string, string, string>>();

            // give the designer something
            if (DesignerProperties.IsInDesignTool)
            {
                NavigateCommand = new ActionCommand<string>();
                ButtonInfo.Add(Tuple.Create((ICommand)NavigateCommand, "Test", "Test", "Test"));

            }
            else
            {
                // when fired, set the current view and publish the navigation
                NavigateCommand = new ActionCommand<string>(
                view =>
                {
                    CurrentView = view;
                    EventAggregator.Publish(view.AsViewNavigationArgs());
                },
                    //view => !string.IsNullOrEmpty(view) && !view.Equals(CurrentView)
                view => true
                );
            }
        }

        /// <summary>
        ///     Grab the full list of views
        /// </summary>
        [ImportMany(AllowRecomposition = true)]
        public Lazy<UserControl, IExportAsViewMetadata>[] Views { get; set; }

        private bool _loaded;

        /// <summary>
        ///     We could wire everything on imports satisfied, this is just an example of doing
        ///     it "lazy" and waiting until the list is requested
        /// </summary>
        private readonly ObservableCollection<Tuple<ICommand, string, string, string>> _buttonInfo;

        public ObservableCollection<Tuple<ICommand, string, string, string>> ButtonInfo
        {
            get
            {
                LoadModules();
                return _buttonInfo;
            }
        }

        public void LoadModules()
        {            
            if (_buttonInfo.Count == 0)
            {
                if (Views != null && Views.Count() > 0)
                {
                    _WireButtonInfo();
                }
            }         
        }

        [Import]
        public IDeploymentService Deployment { get; set; }

        /// <summary>
        ///     Keep the command here to reference for each sub-class
        /// </summary>
        public IActionCommand<string> NavigateCommand { get; private set; }

        private string _currentView;

        /// <summary>
        ///     The current view
        /// </summary>
        public string CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                RaisePropertyChanged(() => CurrentView);
                NavigateCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Called when a part's imports have been satisfied and it is safe to use.
        /// </summary>
        public void _WireButtonInfo()
        {
            // filter only those views that are in the navigation category
            foreach (var v in from viewInfo in Views
                              where viewInfo.Metadata.Category.Equals("Wedding")
                              select Tuple.Create((ICommand)NavigateCommand,
                              viewInfo.Metadata.ExportedViewType,
                              viewInfo.Metadata.MenuName,
                              viewInfo.Metadata.ToolTip))
            {
                _buttonInfo.Add(v);
            }
        }

        public void OnImportsSatisfied()
        {
            //throw new NotImplementedException();
        }
    }
}
