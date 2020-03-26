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

namespace MaFamille.Albums.Views
{
    [ExportAsView("FileUploadHost", Category = "Navigation", MenuName = "FileUploadHost", ToolTip = "")]
    public partial class FileUploadHost
    {
        public FileUploadHost()
        {
            InitializeComponent();
        }

        [Export]
        public ViewModelRoute Binding
        {
            get { return ViewModelRoute.Create("FileUploadHost", "FileUploadHost"); }
        }
    }
}
