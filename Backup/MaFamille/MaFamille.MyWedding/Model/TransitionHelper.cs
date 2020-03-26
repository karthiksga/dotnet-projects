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
using TransitionEffects;

namespace MaFamille.MyWedding.Model
{
    public class TransitionHelper
    {
        public static Storyboard CreateTransition(TransitionEffect effect, UIElement element, TimeSpan duration, double from, double to, Brush old)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = new Duration(duration);
            da.From = from;
            da.To = to;
            da.FillBehavior = FillBehavior.HoldEnd;


            Storyboard sb = new Storyboard();
            sb.Children.Add(da);
            Storyboard.SetTarget(da, effect);
            Storyboard.SetTargetProperty(da, new PropertyPath("(TransitionEffect.Progress)"));

            effect.OldImage = old;
            element.Effect = effect;
            return sb;

        }
    }
}
