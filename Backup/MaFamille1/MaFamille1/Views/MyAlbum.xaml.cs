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
using System.Windows.Navigation;
using MaFamille1.ViewModel;
using MaFamille1.Model;

namespace MaFamille1.Views
{
    public partial class MyAlbumNew : Page
    {
        AlbumViewModel albumVM;
        public MyAlbumNew()
        {
            albumVM = new AlbumViewModel();
            InitializeComponent();
            this.DataContext = albumVM;            
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }                

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            Button albumButton = (Button)e.OriginalSource;
            if (albumButton.Content.ToString() == "New Album")
            {
                //MyAlbumChild child = new MyAlbumChild(new AlbumChild(album.Content.ToString())); 
                AlbumModel album = albumVM.AlbumList.First(a => a.AlbumName == albumButton.Content.ToString());
                MyAlbumChild child = new MyAlbumChild(album);
                child.SubmitClicked += delegate(object windowsender, EventArgs es)
                {
                    //album.Content= ((MyAlbumChild)windowsender).Child.AlbumName;
                    albumVM.RenameAlbum(album);
                    //this.NavigationService.Navigate(new Uri("/MyPhoto/{" + child.Child.AlbumName + "}", UriKind.Relative));
                };
                child.Show();
            }
            else
            {
                this.NavigationService.Navigate(new Uri("/MyPhoto/{" + albumButton.Content.ToString() + "}", UriKind.Relative));
            }            
        }
    }
}
