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
using SilverlightControls;
using Jounce.Core.View;
using System.ComponentModel.Composition;
using Jounce.Core.ViewModel;

namespace MaFamille.Albums.Views
{
    //[ExportAsView("FileUpload", Category = "Navigation", MenuName = "FileUpload", ToolTip = "Click to upload Photo")]
    //[ExportAsView("FileUpload")]
    public partial class FileUpload:FloatWindow
    {
        public FileUpload()
        {
            InitializeComponent();
        }

        //[Export]
        //public ViewModelRoute Binding
        //{
        //    get { return ViewModelRoute.Create("FileUpload", "FileUpload"); }
        //}
    }
}
