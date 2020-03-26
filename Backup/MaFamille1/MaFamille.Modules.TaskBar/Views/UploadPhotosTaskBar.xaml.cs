using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MaFamille.Modules.TaskBar.ViewModel;
using SilverlightControls;
namespace MaFamille.Modules.TaskBar.Views
{
    public partial class UploadPhotosTaskBar : FloatWindow
	{
        //UploadPhotosViewModel uploadPhotos;
        public UploadPhotosTaskBar()
		{
            //uploadPhotos = new UploadPhotosViewModel(this.Dispatcher);
			// Required to initialize variables
			InitializeComponent();
            //this.DataContext = uploadPhotos;
            //this.DataContext = App.uploadVM;
		}
	}
}