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

namespace MaFamille.Model
{
    public class Globals
    {
        /// <summary>
        /// Default screen width
        /// </summary>
        public static double ScreenWidth = 1280;

        /// <summary>
        /// Default screen height
        /// </summary>
        public static double ScreenHeight = 1024;

        /// <summary>
        /// Random generator used throughout the app
        /// </summary>
        public static Random Random = new Random();
    }
}
