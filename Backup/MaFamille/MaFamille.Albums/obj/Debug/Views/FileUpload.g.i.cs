﻿#pragma checksum "D:\Visual Studio Projects\Silverlight Samples\MaFamille\MaFamille.Albums\Views\FileUpload.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "45F3D7C1FC5D6D35490CD408244B3E72"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SilverlightControls;
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


namespace MaFamille.Albums.Views {
    
    
    public partial class FileUpload : SilverlightControls.FloatWindow {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Button AddFilesButton;
        
        internal System.Windows.Controls.Button UploadFilesButton;
        
        internal System.Windows.Controls.Button btnClear;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/MaFamille.Albums;component/Views/FileUpload.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.AddFilesButton = ((System.Windows.Controls.Button)(this.FindName("AddFilesButton")));
            this.UploadFilesButton = ((System.Windows.Controls.Button)(this.FindName("UploadFilesButton")));
            this.btnClear = ((System.Windows.Controls.Button)(this.FindName("btnClear")));
        }
    }
}

