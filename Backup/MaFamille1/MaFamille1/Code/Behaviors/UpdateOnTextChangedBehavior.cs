using System.Windows.Controls;
using System.Windows.Interactivity;

namespace MaFamille1.Behaviors
{
    public class UpdateOnTextChangedBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.TextChanged += AssociatedObject_TextChanged;
        }

        private void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            var binding = this.AssociatedObject.GetBindingExpression(TextBox.TextProperty);
            if (binding != null)
                binding.UpdateSource();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
        }
    }
}
