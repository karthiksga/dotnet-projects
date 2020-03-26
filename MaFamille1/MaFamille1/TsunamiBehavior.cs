using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Interactivity;
using System.Net;
using MaFamille1.Model;
//using Microsoft.Expression.Interactivity.Core;

namespace MaFamille1
{
	public class TsunamiBehavior : Behavior<Border>
	{
        private static Random randomNumber;        
		public TsunamiBehavior()
		{
			// Insert code required on object creation below this point.

			//
			// The line of code below sets up the relationship between the command and the function
			// to call. Uncomment the below line and add a reference to Microsoft.Expression.Interactions
			// if you choose to use the commented out version of MyFunction and MyCommand instead of
			// creating your own implementation.
			//
			// The documentation will provide you with an example of a simple command implementation
			// you can use instead of using ActionCommand and referencing the Interactions assembly.
			//
			//this.MyCommand = new ActionCommand(this.MyFunction);
		}

		protected override void OnAttached()
		{
			base.OnAttached();

			// Insert code that you would want run when the Behavior is attached to an object.
            randomNumber = new Random();            
            this.AssociatedObject.Loaded+=new RoutedEventHandler(ApplicationLoaded);
		}

        void ApplicationLoaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)this.AssociatedObject;
            FrameworkElement localCopy = element;
            double xPosition = Canvas.GetLeft(localCopy);
            double yPosition = Canvas.GetTop(localCopy);

            double speed = 5 * randomNumber.NextDouble();
            CompositionTarget.Rendering += delegate(object o, EventArgs arg)
            {
                if (xPosition < Application.Current.RootVisual.RenderSize.Width)
                {
                    xPosition += speed;
                }
                else
                {
                    xPosition = -localCopy.Width;
                }
                TsunamiModel model = (TsunamiModel)localCopy.DataContext;
                model.CanvasLeft = (decimal)xPosition;
                model.CanvasTop = (decimal)yPosition;
            };
            
        }
       
		protected override void OnDetaching()
		{
			base.OnDetaching();

			// Insert code that you would want run when the Behavior is removed from an object.
		}		
	}
}