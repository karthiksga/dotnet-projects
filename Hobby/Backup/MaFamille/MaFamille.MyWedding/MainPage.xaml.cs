using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Jounce.Core.View;
using System.ComponentModel.Composition;
using Jounce.Core.ViewModel;
using MaFamille.MyWedding.Model;
using MaFamille.MyWedding.ViewModel;
//using MaFamille.MyWedding.Views;

namespace MaFamille.MyWedding
{
    [ExportAsView("MainPage")]
    public partial class MainPage
    {        
        private StarField _starField;
        private DateTime _lastUpdate = DateTime.Now;

        public MainPage()
        {
            InitializeComponent();
            //this.DataContext = new MainPageViewModel();
        }

        [Export]
        public ViewModelRoute Binding
        {
            get { return ViewModelRoute.Create("MainPageViewModel", "MainPage"); }
        }

        //private static bool _initializedAfterScreenSizeChanged = false;
        //private void InitIfNeededAfterScreenSizeIsKnown()
        //{
        //    if (_initializedAfterScreenSizeChanged) return;
        //    _initializedAfterScreenSizeChanged = true;

        //    _starField = new StarField(panelStarfield);
        //    _lastUpdate = DateTime.Now;
        //    CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
        //}

        //void CompositionTarget_Rendering(object sender, EventArgs e)
        //{
        //    DateTime now = DateTime.Now;
        //    double msec = (now - _lastUpdate).TotalMilliseconds;
        //    if (msec == 0)
        //    {
        //        return;
        //    }

        //    _starField.UpdateStars(msec);

        //    _lastUpdate = DateTime.Now;
        //}

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        //    Globals.ScreenWidth = LayoutRoot.ActualWidth;
        //    Globals.ScreenHeight = LayoutRoot.ActualHeight;

        //    InitIfNeededAfterScreenSizeIsKnown();
        }

        //public UserControl CurrentContent
        //{
        //    get { return (UserControl)GetValue(CurrentContentProperty); }
        //    set { SetValue(CurrentContentProperty, value); }
        //}
        //public static readonly DependencyProperty CurrentContentProperty =
        //    DependencyProperty.Register("CurrentContent", typeof(UserControl), typeof(MainPage), new PropertyMetadata(null));

        //private void btnInfo_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    // TODO: Add event handler implementation here.
        //    CurrentContent=new Info();
        //}

        //private void btnEngagementGallery_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    CurrentContent=new MyEngagement();
        //}

        //private void btnWeddingGallery_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    // TODO: Add event handler implementation here.
        //    CurrentContent=new Views.MyWedding();
        //}

    }
}
