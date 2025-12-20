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
using System.Globalization;
using System.Linq;
using System.Collections.Generic;

namespace SilverlightControls
{
    [TemplatePart(Name = HostRoot_Part, Type = typeof(Grid))]
    [TemplatePart(Name = ContentRoot_Part, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = HostCanvas_Part, Type = typeof(Canvas))]
    public class WindowHost : ContentControl
    {
        private const string HostRoot_Part = "HostRoot_Part";
        private const string ContentRoot_Part = "ContentRoot_Part";
        private const string HostCanvas_Part = "HostCanvas_Part";

        private Grid root;
        private FrameworkElement contentRoot;
        private Canvas hostCanvas;

        private bool isTemplateApplied;

        public WindowHost()
        {
            DefaultStyleKey = typeof(WindowHost);
        }

        public Canvas HostPanel
        {
            get { return hostCanvas; }
        }

        internal bool IsLayoutUpdated { get; private set; }

        public override void OnApplyTemplate()
        {
            UnSubscribeEvents();
            base.OnApplyTemplate();

            root = GetTemplatePart<Grid>(HostRoot_Part);
            contentRoot = GetTemplatePart<FrameworkElement>(ContentRoot_Part);
            hostCanvas = GetTemplatePart<Canvas>(HostCanvas_Part);
            SubscribeEvents();

            isTemplateApplied = true;
        }

        internal void DisplayWindow(Action<Point> action, Point _point)
        {
            if (IsLayoutUpdated)
            {
                action(_point);
            }
            else
            {
                this.ControlRendered += (s, e) => { action(_point); };
            }
        }

        internal void DisplayWindow(Action<Thickness> action, Thickness _margin)
        {
            if (IsLayoutUpdated)
            {
                action(_margin);
            }
            else
            {
                this.ControlRendered += (s, e) => { action(_margin); };
            }

        }

        public void Add(FloatWindow window)
        {
            if (window == null)
                throw new ArgumentNullException("window");
            if (!isTemplateApplied)
                isTemplateApplied = ApplyTemplate();

            if (!hostCanvas.Children.Contains(window))
            {
                hostCanvas.Children.Add(window);
                window.Host = this;
            }
        }

        public IEnumerable<FloatWindow> GetWindow()
        {
            if (hostCanvas != null && hostCanvas.Children != null)
                return hostCanvas.Children.OfType<FloatWindow>();
            else
                return null;
        }

        private void SubscribeEvents()
        {
            this.LayoutUpdated += new EventHandler(WindowHost_LayoutUpdated);
        }

        private void UnSubscribeEvents()
        {
            this.LayoutUpdated -= new EventHandler(WindowHost_LayoutUpdated);
        }

        void WindowHost_LayoutUpdated(object sender, EventArgs e)
        {
            if (!IsLayoutUpdated)
            {
                this.LayoutUpdated -= new EventHandler(WindowHost_LayoutUpdated);
                IsLayoutUpdated = true;
                OnControlRendered(EventArgs.Empty);
            }
        }

        protected virtual void OnControlRendered(EventArgs e)
        {
            EventHandler handler = ControlRendered;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler ControlRendered;

        private T GetTemplatePart<T>(string partName) where T : class
        {
            T part = this.GetTemplateChild(partName) as T;
            if (part == null)
                throw new NotImplementedException(string.Format(CultureInfo.InvariantCulture, "Template Part {0} is required.", partName));
            return part;
        }
    }
}
