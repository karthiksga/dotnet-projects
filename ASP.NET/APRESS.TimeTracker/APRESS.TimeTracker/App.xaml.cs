using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Unity;
using APRESS.TimeTracker.Services;
using System.Threading;

namespace APRESS.TimeTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IUnityContainer container;
        private INavigationService navigation;

        public App()
        {
            ConfigureContainer();
        }

        private void ConfigureContainer()
        {
            container = new UnityContainer();
            container.RegisterType<IDialogService, DialogService>()
                .RegisterType<INavigationService, NavigationService>()
                .RegisterType<MainWindow>();
            navigation = container.Resolve<INavigationService>();
                
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Thread.Sleep(2000);
            //var mainWindow = navigation.ShowMainWindow();
            var mainView = new MainWindow();
            mainView.Show();
        }

        protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
        {
            base.OnSessionEnding(e);
            e.Cancel = navigation.ConfirmClose();
        }
    }
}
