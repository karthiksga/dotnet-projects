using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MaFamille.Albums.Model;

namespace MaFamille.Albums.Views
{
	public partial class FileUploadRowControl : UserControl
	{
        FileUploadModel file;
		public FileUploadRowControl()
		{
			// Required to initialize variables
			InitializeComponent();
            Loaded += new RoutedEventHandler(FileUploadRowControl_Loaded);
		}

        void FileUploadRowControl_Loaded(object sender, RoutedEventArgs e)
        {
            file = DataContext as FileUploadModel;
        }


	}
}