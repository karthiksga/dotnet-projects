using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MaFamille1
{
	public partial class CanvasScreen : UserControl
	{
        AlbumViewModel albumVM;
		public CanvasScreen()
		{
            albumVM = new AlbumViewModel();
			// Required to initialize variables
			InitializeComponent();
            this.DataContext = albumVM;
		}
	}
}