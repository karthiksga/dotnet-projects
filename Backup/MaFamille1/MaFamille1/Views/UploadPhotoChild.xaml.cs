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

namespace MaFamille1
{
    public partial class UploadPhotoChild : ChildWindow
    {
        public UploadPhotoChild()
        {
            InitializeComponent();
            this.DataContext = App.uploadVM;            
        }

        private string _albumName;        

        public string AlbumName
        {
            get { return _albumName; }
            set 
            { 
                _albumName = value; 

            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            App.uploadVM = this.DataContext as ViewModel.UploadPhotosViewModel;
            this.DialogResult = true;
        }
    }
}

