using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SilverlightControls;
namespace MaFamille1
{
	public partial class CanvasScreen : UserControl
	{        
		public CanvasScreen()
		{     
			// Required to initialize variables
			InitializeComponent();
            this.Loaded += new RoutedEventHandler(CanvasScreen_Loaded);
            
		}

        void CanvasScreen_Loaded(object sender, RoutedEventArgs e)
        {
            //FloatWindow window = new FloatWindow();
            //host.Add(window);
            //window.DisplayWindow();
            //window.DisplayWindow(new Thickness(50,100,50,150));
        }
	}
}