﻿#pragma checksum "D:\Visual Studio Projects\Silverlight Samples\MaFamille\MaFamille.MyWedding\Views\MyEngagement.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FDACBE8EF27D61D76D5802058F723E5C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaFamille.Common.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace MaFamille.MyWedding.Views {
    
    
    public partial class MyEngagement : System.Windows.Controls.UserControl {
        
        internal System.Windows.Media.Animation.Storyboard Spinning;
        
        internal System.Windows.Media.Animation.Storyboard StartSlideShow;
        
        internal System.Windows.Media.Animation.Storyboard StopSlideShow;
        
        internal System.Windows.Media.Animation.Storyboard ZoomSlideShow0;
        
        internal System.Windows.Media.Animation.Storyboard ZoomReset0;
        
        internal System.Windows.Media.Animation.Storyboard ShowFullImage;
        
        internal System.Windows.Media.Animation.Storyboard HideFullImage;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal MaFamille.Common.Controls.CoverFlowControl flowControl;
        
        internal System.Windows.Controls.Button btnFullScreen;
        
        internal System.Windows.Controls.Image imgFullScreen;
        
        internal System.Windows.Media.Animation.Storyboard ZoomSlideShow;
        
        internal System.Windows.Media.Animation.Storyboard FadeOutAnimation;
        
        internal System.Windows.Controls.Image imgFullScreen2;
        
        internal System.Windows.Media.Animation.Storyboard ZoomSlideShow2;
        
        internal System.Windows.Media.Animation.Storyboard FadeOutAnimation2;
        
        internal System.Windows.Controls.Button btnSlideShow;
        
        internal System.Windows.Controls.StackPanel stackPanel;
        
        internal System.Windows.Controls.TextBlock textBlock;
        
        internal System.Windows.Controls.TextBlock textBlock1;
        
        internal System.Windows.Controls.TextBlock textBlock2;
        
        internal System.Windows.Controls.TextBlock textBlock3;
        
        internal System.Windows.Controls.TextBlock textBlock4;
        
        internal System.Windows.Controls.TextBlock textBlock5;
        
        internal System.Windows.Controls.TextBlock textBlock6;
        
        internal System.Windows.Controls.TextBlock textBlock7;
        
        internal System.Windows.Controls.TextBlock textBlock8;
        
        internal System.Windows.Controls.TextBlock textBlock9;
        
        internal System.Windows.Controls.TextBlock txtPhotosCount;
        
        internal System.Windows.Controls.Grid grdFullView;
        
        internal System.Windows.Controls.Border border;
        
        internal System.Windows.Controls.Image image;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/MaFamille.MyWedding;component/Views/MyEngagement.xaml", System.UriKind.Relative));
            this.Spinning = ((System.Windows.Media.Animation.Storyboard)(this.FindName("Spinning")));
            this.StartSlideShow = ((System.Windows.Media.Animation.Storyboard)(this.FindName("StartSlideShow")));
            this.StopSlideShow = ((System.Windows.Media.Animation.Storyboard)(this.FindName("StopSlideShow")));
            this.ZoomSlideShow0 = ((System.Windows.Media.Animation.Storyboard)(this.FindName("ZoomSlideShow0")));
            this.ZoomReset0 = ((System.Windows.Media.Animation.Storyboard)(this.FindName("ZoomReset0")));
            this.ShowFullImage = ((System.Windows.Media.Animation.Storyboard)(this.FindName("ShowFullImage")));
            this.HideFullImage = ((System.Windows.Media.Animation.Storyboard)(this.FindName("HideFullImage")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.flowControl = ((MaFamille.Common.Controls.CoverFlowControl)(this.FindName("flowControl")));
            this.btnFullScreen = ((System.Windows.Controls.Button)(this.FindName("btnFullScreen")));
            this.imgFullScreen = ((System.Windows.Controls.Image)(this.FindName("imgFullScreen")));
            this.ZoomSlideShow = ((System.Windows.Media.Animation.Storyboard)(this.FindName("ZoomSlideShow")));
            this.FadeOutAnimation = ((System.Windows.Media.Animation.Storyboard)(this.FindName("FadeOutAnimation")));
            this.imgFullScreen2 = ((System.Windows.Controls.Image)(this.FindName("imgFullScreen2")));
            this.ZoomSlideShow2 = ((System.Windows.Media.Animation.Storyboard)(this.FindName("ZoomSlideShow2")));
            this.FadeOutAnimation2 = ((System.Windows.Media.Animation.Storyboard)(this.FindName("FadeOutAnimation2")));
            this.btnSlideShow = ((System.Windows.Controls.Button)(this.FindName("btnSlideShow")));
            this.stackPanel = ((System.Windows.Controls.StackPanel)(this.FindName("stackPanel")));
            this.textBlock = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock")));
            this.textBlock1 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock1")));
            this.textBlock2 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock2")));
            this.textBlock3 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock3")));
            this.textBlock4 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock4")));
            this.textBlock5 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock5")));
            this.textBlock6 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock6")));
            this.textBlock7 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock7")));
            this.textBlock8 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock8")));
            this.textBlock9 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock9")));
            this.txtPhotosCount = ((System.Windows.Controls.TextBlock)(this.FindName("txtPhotosCount")));
            this.grdFullView = ((System.Windows.Controls.Grid)(this.FindName("grdFullView")));
            this.border = ((System.Windows.Controls.Border)(this.FindName("border")));
            this.image = ((System.Windows.Controls.Image)(this.FindName("image")));
        }
    }
}

