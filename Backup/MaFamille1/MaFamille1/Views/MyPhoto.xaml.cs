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
using System.Text.RegularExpressions;

namespace MaFamille1
{
    public partial class MyPhotos : Page
    {
        PhotoViewModel albumVM;
        public MyPhotos()
        {            
            InitializeComponent();            
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string album="";            
            if (this.NavigationContext.QueryString.ContainsKey("Album"))
            {
                album=this.NavigationContext.QueryString["Album"];
                albumVM = new PhotoViewModel(album.Substring(1,album.Length-2));
            }
            this.DataContext = albumVM;            
        }
    }
}
