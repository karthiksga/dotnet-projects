using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MaFamille1.Model
{
    public class BorderItemsControl:ItemsControl
    {
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            FrameworkElement contentitem = element as FrameworkElement;
            System.Windows.Data.Binding leftBinding = new System.Windows.Data.Binding("CanvasLeft");
            System.Windows.Data.Binding topBinding = new System.Windows.Data.Binding("CanvasTop");
            contentitem.SetBinding(Canvas.LeftProperty, leftBinding);
            contentitem.SetBinding(Canvas.TopProperty, topBinding);
            base.PrepareContainerForItemOverride(element, item);
        }

    }
}
