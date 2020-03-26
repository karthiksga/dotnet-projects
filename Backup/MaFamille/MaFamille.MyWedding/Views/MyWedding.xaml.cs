using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Jounce.Core.View;
using System.ComponentModel.Composition;
using Jounce.Core.ViewModel;

namespace MaFamille.MyWedding.Views
{
    [ExportAsView("MyWedding", Category = "Weddingg", MenuName = "MyWedding", ToolTip = "My Wedding")]
	public partial class MyWedding
	{
		public MyWedding()
		{
			// Required to initialize variables
			InitializeComponent();
            MouseWheel += new MouseWheelEventHandler(MainPage_MouseWheel);
        }

        void MainPage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            int i = -(e.Delta / 120);
            int index = flowControl.SelectedIndex + i;
            if (index < 0)
                flowControl.First();
            else if (index > flowControl.Items.Count - 1)
                flowControl.Last();
            else
                flowControl.SelectedIndex = index;
        }

        [Export]
        public ViewModelRoute Binding
        {
            get { return ViewModelRoute.Create("PhotoViewModel", "MyWedding"); }
        }
    }
}