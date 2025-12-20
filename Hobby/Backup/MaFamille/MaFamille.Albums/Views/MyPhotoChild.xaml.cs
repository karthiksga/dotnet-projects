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
using MaFamille.Common;

namespace MaFamille.Albums.Views
{
    [ExportAsView("MyPhotoChild")]
    public partial class MyPhotoChild : ChildWindow,IModalWindow
    {
        public MyPhotoChild()
        {
            InitializeComponent();
        }

        [Export]
        public ViewModelRoute Binding
        {
            get { return ViewModelRoute.Create("MyPhotoChild", "MyPhotoChild"); }
        }
    }
}

