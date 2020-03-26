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
using MaFamille.Common;
using Jounce.Core.View;
using System.ComponentModel.Composition;
using Jounce.Core.ViewModel;
namespace MaFamille.Albums.Views
{
    //[ExportAsView("EditAlbum", Category = "Navigations", MenuName = "Blue", ToolTip = "Click to view Blue.")]
    public partial class EditAlbum : ChildWindow, IModalWindow
    {
        public EditAlbum()
        {
            InitializeComponent();
        }

        //[Export]
        //public ViewModelRoute Binding
        //{
        //    get
        //    {
        //        return ViewModelRoute.Create("EditAlbum", "EditAlbum");
        //    }
        //}

        public event EventHandler SubmitClicked;

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (SubmitClicked != null)
            {
                SubmitClicked(this, new EventArgs());
                this.DialogResult = true;
            }
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        //public static DependencyProperty AlbumNameProperty = DependencyProperty.Register("AlbumName", typeof(string), typeof(EditAlbum), new PropertyMetadata(""));
        //public string AlbumName
        //{
        //    get { return (string)GetValue(AlbumNameProperty); }
        //    set { SetValue(AlbumNameProperty, value); }
        //}
    }
}

