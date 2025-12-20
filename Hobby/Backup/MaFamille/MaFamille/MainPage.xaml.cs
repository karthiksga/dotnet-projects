using Jounce.Core.View;
using System.ComponentModel.Composition;
using System.Windows;
using Jounce.Framework;
using System.Linq;
using MaFamille.Model;
using System;
using System.Windows.Media;
using System.Windows.Input;
using Jounce.Core.Fluent;
using Jounce.Core.ViewModel;
using MaFamille.ViewModels;

namespace MaFamille
{
    /// <summary>
    /// The shell view is the main view over all of the application
    /// </summary>
    [ExportAsView(typeof(MainPage), IsShell=true)]
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += new System.Windows.RoutedEventHandler(MainPage_Loaded);

            flipClockCountDown.CountDownDate = new DateTime(2013, 5, 20);
            flipClockCountDown.Title = "My Wedding in";
        }

        void MainPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var groups = VisualStateManager.GetVisualStateGroups(LayoutRoot);
            
            foreach (var group in groups.Cast<VisualStateGroup>().Where(g => g.Name.Equals("NavigationStates")))
            {
                group.CurrentStateChanged += GroupCurrentStateChanged;
            }
        }
        
        static void GroupCurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            JounceHelper.ExecuteOnUI(() => MessageBox.Show(string.Format("Transition {0}=>{1}", e.OldState.Name, e.NewState.Name)));
        }

        // the code below shows how to export a binding rather than using the fluent
        // method in the boot strapper - add a using for System.ComponentModel.Composition,
        // Jounce.Core.ViewModel, and MaFamille.ViewModels in order to 
        // uncomment the code and use this method instead

        //[Export]
        //public ViewModelRoute Binding
        //{
        //    get { return ViewModelRoute.Create<MainViewModel, MainPage>(); }
        //}        

        private StarField _starField;
        private DateTime _lastUpdate = DateTime.Now;
        private static bool _initializedAfterScreenSizeChanged = false;
        private void InitIfNeededAfterScreenSizeIsKnown()
        {
            if (_initializedAfterScreenSizeChanged) return;
            _initializedAfterScreenSizeChanged = true;

            _starField = new StarField(panelStarfield);
            _lastUpdate = DateTime.Now;
            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            double msec = (now - _lastUpdate).TotalMilliseconds;
            if (msec == 0)
            {
                return;
            }

            _starField.UpdateStars(msec);

            _lastUpdate = DateTime.Now;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Globals.ScreenWidth = LayoutRoot.ActualWidth;
            Globals.ScreenHeight = LayoutRoot.ActualHeight;

            InitIfNeededAfterScreenSizeIsKnown();
        }        
    }
}
