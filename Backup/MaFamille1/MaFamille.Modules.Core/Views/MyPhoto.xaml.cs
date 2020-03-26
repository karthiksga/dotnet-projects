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
using MaFamille.Modules.Core.ViewModel;
using System.Text.RegularExpressions;
using MaFamille.Modules.Core.MaFamilleService;

namespace MaFamille.Modules.Core
{
    public partial class MyPhotos : UserControl
    {        
        public MyPhotos()
        {            
            InitializeComponent();            
        }

        // Executes when the user navigates to this page.
        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    string album = "";
        //    if (this.NavigationContext.QueryString.ContainsKey("Album"))
        //    {
        //        album = this.NavigationContext.QueryString["Album"];
        //        albumVM = new PhotoViewModel(album.Substring(1, album.Length - 2));
        //    }
        //    this.DataContext = albumVM;            
        //}
    }
}
