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
using System.Windows.Interactivity;

namespace MaFamille.Common.CommonBehaviors
{
    public class TextBoxChangedBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.TextChanged += AssociatedObject_TextChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
        }

        static void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb == null) return;
            var binding = tb.GetBindingExpression(TextBox.TextProperty);
            if (binding != null)
            {
                binding.UpdateSource();
            }
        }
    }
}
