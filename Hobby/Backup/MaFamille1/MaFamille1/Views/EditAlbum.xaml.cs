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

namespace MaFamille1
{
    public partial class EditAlbum : Page
    {
        EditAlbumViewModel albumVM;
        public EditAlbum()
        {
            albumVM = new EditAlbumViewModel();
            InitializeComponent();
            this.DataContext = albumVM;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
