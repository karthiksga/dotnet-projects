using Jounce.Core.ViewModel;
using System.ComponentModel.Composition;
using Jounce.Core.Event;
using Jounce.Core.View;
using System.Linq;
using Jounce.Core.Application;
using System;
using MaFamille.MaFamilleService;
using System.Windows;
using System.ServiceModel;
using MaFamille.Model;
using System.Windows.Media;
using Jounce.Core.Fluent;

namespace MaFamille.ViewModels
{
    /// <summary>
    /// Sample view model showing design-time resolution of data
    /// </summary>
    [ExportAsViewModel(typeof (MainViewModel))]
    public class MainViewModel : BaseViewModel, IPartImportsSatisfiedNotification, IEventSink<ViewNavigationArgs>
    {
        public MainViewModel()
        {
            
        }
        public string Welcome
        {
            get { return InDesigner ? "Jounce Design-time View" : "Welcome to Jounce."; }
        }

        public string CurrentModule
        {
            get { return _currentModule; }
            set
            {
                _currentModule = value;
                RaisePropertyChanged(() => CurrentModule);
            }
        }private string _currentModule;

        private object _navigation;
        public object Navigation
        {
            get { return _navigation; }
            set
            {
                _navigation = value;
                RaisePropertyChanged(() => Navigation);               
            }
        }

        private object _currentView;

        /// <summary>
        ///     The current view - this is where we'll set the control
        /// </summary>
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                RaisePropertyChanged(() => CurrentView);
            }
        }

        private object _windowHostView;
        public object WindowHostView
        {
            get { return _windowHostView; }
            set
            {
                _windowHostView = value;
                RaisePropertyChanged(() => WindowHostView);
            }
        }

        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                _isLoaded = value;
                RaisePropertyChanged(() => IsLoaded);
            }
        }private bool _isLoaded;

        [Import]
        public IDeploymentService Deployment { get; set; }

        [Import]
        public IFluentViewModelRouter FluentRouter { get; set; }


        /// <summary>
        /// Called when a part's imports have been satisfied and it is safe to use.
        /// </summary>
        public void OnImportsSatisfied()
        {
            EventAggregator.SubscribeOnDispatcher(this);

            ServiceClient client = new ServiceClient();
            client.GetActiveModuleCompleted += delegate(object sender, GetActiveModuleCompletedEventArgs e)
            {
                if (e.Error is FaultException<MaFamilleService.CustomException>)
                {
                    MessageBox.Show(e.Error.Message);
                    return;
                }
                if (e.Result == "Albums")
                {
                    CurrentModule = "Albums";
                    Deployment.RequestXap("MaFamille.TaskBar.xap", ex =>
                    {
                        // ask the router for the navigation control
                        Navigation = Router["Navigation"];
                        // publish this so the binding happens
                        EventAggregator.Publish(new ViewNavigationArgs("Navigation"));
                        EventAggregator.Publish(new ViewNavigationArgs("MyAlbum"));
                        IsLoaded = true;
                    });
                }
                else if (e.Result == "MyWedding")
                {
                    CurrentModule = "MyWedding";
                    Deployment.RequestXap("MaFamille.MyWedding.xap", ex =>
                    {
                        // ask the router for the navigation control
                        Navigation = Router["MainPage"];
                        //CurrentView = Router["MainPage"];
                        // publish this so the binding happens
                        EventAggregator.Publish(new ViewNavigationArgs("MainPage"));
                        EventAggregator.Publish(new ViewNavigationArgs("Info"));
                        IsLoaded = true;                        
                    });
                }
            };
            client.GetActiveModuleAsync();
        }

        bool _isFileUploadLoaded;
        /// <summary>
        ///     Our event sink is the raised event for a navigation
        /// </summary>
        /// <param name="publishedEvent"></param>
        public void HandleEvent(ViewNavigationArgs publishedEvent)
        {
            //  we'll use the router to reference the view based on the passed type
            if (!publishedEvent.ViewType.Equals("Navigation"))
            {
                if (publishedEvent.ViewType == "FileUploadHost")
                {
                    var viewModel = Router.ResolveViewModel("FileUploadHost");              
                    if (viewModel != null)
                    {
                        var type = viewModel.GetType();
                        var method = type.GetMethod("RestoreWindow");
                        if(_isFileUploadLoaded) method.Invoke(viewModel, null);                        
                        WindowHostView = Router[publishedEvent.ViewType];
                        //method.Invoke(viewModel, null);                        
                        _isFileUploadLoaded = true;
                    }
                    //if (viewModel != null) _isFileUploadLoaded = true;
                }                
                else if(publishedEvent.ViewType!="MainPage")
    	        {
                    CurrentView = Router[publishedEvent.ViewType];
	            }
            }
        }        
    }
}