﻿#pragma checksum "D:\Visual Studio Projects\Silverlight Samples\MaFamille\MaFamille.Common\CountDown\FlipClock.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E32CEC5CB291B4B68FF17DFD2DC9ACEC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaFamille.Common.CountDown;
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


namespace MaFamille.Common.CountDown {
    
    
    public partial class FlipClock : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal MaFamille.Common.CountDown.Flipper FlipDays;
        
        internal MaFamille.Common.CountDown.Flipper FlipHours;
        
        internal MaFamille.Common.CountDown.Flipper FlipSeconds;
        
        internal MaFamille.Common.CountDown.Flipper FlipMinutes;
        
        internal System.Windows.Controls.TextBlock textBlockTitle;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/MaFamille.Common;component/CountDown/FlipClock.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.FlipDays = ((MaFamille.Common.CountDown.Flipper)(this.FindName("FlipDays")));
            this.FlipHours = ((MaFamille.Common.CountDown.Flipper)(this.FindName("FlipHours")));
            this.FlipSeconds = ((MaFamille.Common.CountDown.Flipper)(this.FindName("FlipSeconds")));
            this.FlipMinutes = ((MaFamille.Common.CountDown.Flipper)(this.FindName("FlipMinutes")));
            this.textBlockTitle = ((System.Windows.Controls.TextBlock)(this.FindName("textBlockTitle")));
        }
    }
}

