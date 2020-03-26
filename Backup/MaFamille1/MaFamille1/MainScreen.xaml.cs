using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.Windows.Media.Imaging;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MaFamille1.MaFamilleService;
//using MaFamilleService;
using System.Threading;
using System.Collections.ObjectModel;
using MaFamille1.ViewModel;
namespace MaFamille1
{
	public partial class MainScreen : UserControl
	{
        TsunamiViewModel tsunamiVM;
        FrameworkElement localCopy;
		public MainScreen()
		{
            tsunamiVM = new TsunamiViewModel();
			// Required to initialize variables
			InitializeComponent();
            this.DataContext = tsunamiVM;
            //LoadImage();
		}

        void LoadImage()
        {
            //ObservableCollection<AlbumModel> picList = new AlbumViewModel().PictureList;            
            //int i = 0;
            //if (picList != null)
            //{
                //foreach (FrameworkElement element in canvas.Children)
                //{
                //    if (i > picList.Count - 1)
                //        i = 0;
                //    AlbumModel pic = picList[i];
                //    localCopy = element;
                //    Rectangle rec = (Rectangle)localCopy;
                //    BitmapImage image = new BitmapImage();
                //    image.SetSource(new MemoryStream(pic.ImageStream));
                //    ImageBrush brush = new ImageBrush();
                //    brush.ImageSource = image;
                //    rec.Fill = brush;
                //    brush = null;
                //    image = null;
                //    i += 1;
                //}
            //}
        }        
       
		// After the Frame navigates, ensure the HyperlinkButton representing the current page is selected
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            //foreach (UIElement child in LinksStackPanel.Children)
            //{
            //    HyperlinkButton hb = child as HyperlinkButton;
            //    if (hb != null && hb.NavigateUri != null)
            //    {
            //        if (hb.NavigateUri.ToString().Equals(e.Uri.ToString()))
            //        {
            //            VisualStateManager.GoToState(hb, "ActiveLink", true);
            //        }
            //        else
            //        {
            //            VisualStateManager.GoToState(hb, "InactiveLink", true);
            //        }
            //    }
            //}
        }

        // If an error occurs during navigation, show an error window
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
            ChildWindow errorWin = new ErrorWindow(e.Uri);
            errorWin.Show();
        }

        private void btnUploadProgress_Click(object sender, RoutedEventArgs e)
        {            
            App.uploadVM.IsUploadEnabled = false;
            //child.SubmitClicked += new EventHandler(child_SubmitClicked);
            //child.AlbumName = SelectedItem.AlbumName;
            App.uploadWindow.Show();
        }
	}
}