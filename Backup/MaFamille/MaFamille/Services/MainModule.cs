using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using Jounce.Core.Application;
using Jounce.Core.Event;
using Jounce.Core.Fluent;
using MaFamille.ViewModels;
using Jounce.Core.View;

namespace MaFamille.Services
{
    [Export(typeof(IModuleInitializer))]
    public class MainModule : IModuleInitializer, IEventSink<UnhandledExceptionEvent>
    {
        [Import]
        public IFluentViewModelRouter Router { get; set; }

        [Import]
        public IEventAggregator EventAggregator { get; set; }

        [Import]
        public IDeploymentService Deployment { get; set; }

        public bool Initialized { get; set; }
        
        /// <summary>
        /// This method will initialize the application. While you can directly
        /// export view model and view bindings, you can also boot strap them
        /// here using the fluent interface as shown. This module class is also
        /// subscribing to the unhandled exception event
        /// </summary>
        public void Initialize()
        {            
            Router.RouteViewModelForView<MainViewModel, MainPage>();
            // ReSharper disable RedundantTypeArgumentsOfMethod
            EventAggregator.Subscribe<UnhandledExceptionEvent>(this);
            // ReSharper restore RedundantTypeArgumentsOfMethod                        
            Initialized = true; // avoid re-initialization
        }

        /// <summary>
        ///     Give Jounce a hint so when the view is requested, Jounce can automatically
        ///     load the needed Xap
        /// </summary>
        //[Export]
        //public ViewXapRoute DynamicRoute
        //{
        //    get
        //    {
        //        return ViewXapRoute.Create("Navigation", "MaFamille.Albums.xap");
        //    }
        //}

        /// <summary>
        /// This is an example of managing unhandled exceptions. In the debugger it 
        /// simply allows the exception to throw. If you are not debugging, it will
        /// block the UI thread and use the JavaScript message box to display the 
        /// error message. This is not the ideal way but is here to provide an example for your
        /// project template.
        /// </summary>
        /// <param name="publishedEvent">The instance of the <see cref="UnhandledExceptionEvent"/></param>
        public void HandleEvent(UnhandledExceptionEvent publishedEvent)
        {
            if (Debugger.IsAttached)
            {
                publishedEvent.Handled = false;
            }
            else
            {
                publishedEvent.Handled = true;
                MessageBox.Show(publishedEvent.UncaughtException.Message);
            }
        }
    }
}