﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Jounce.Core.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Jounce.Framework.Command;
using System.ComponentModel.Composition;
using Jounce.Core.View;
using Jounce.Core.Command;
using Jounce.Framework;
using System.Linq;
using Jounce.Core.Application;

namespace MaFamille.TaskBar.ViewModels
{
    [ExportAsViewModel("Navigation")]
    public class NavigationViewModel : BaseViewModel, IPartImportsSatisfiedNotification
    {
        public NavigationViewModel()
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
                view=> true
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

        [Import]
        public IDeploymentService Deployment { get; set; }

        public void LoadModules()
        {
            if (!_loaded)
            {
                Deployment.RequestXap("MaFamille.Albums.xap", ex =>
                {
                    _loaded = true;
                    LoadModules();
                });
            }
            else
            {
                if (_buttonInfo.Count == 0)
                {
                    if (Views != null && Views.Count() > 0 && _loaded)
                    {
                        _WireButtonInfo();
                    }
                }
            }
        }

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
                              where viewInfo.Metadata.Category.Equals("Navigation")
                              select Tuple.Create((ICommand)NavigateCommand,
                              viewInfo.Metadata.ExportedViewType,
                              viewInfo.Metadata.MenuName,
                              viewInfo.Metadata.ToolTip))
            {
                _buttonInfo.Add(v);
                //IExportAsViewMetadata
                //Lazy<UserControl, IExportAsViewMetadata> mainPage = new Lazy<UserControl, IExportAsViewMetadata>();

            }
        }

        public void OnImportsSatisfied()
        {
            EventAggregator.Publish("FileUploadHost".AsViewNavigationArgs());
        }
    }
}
