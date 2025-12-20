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
using System.Windows.Controls.Primitives;
using System.Linq;

namespace SilverlightControls
{
    [TemplatePart(Name = WindowTitle_Part, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = TitleContent_Part, Type = typeof(ContentControl))]
    //[TemplatePart(Name = CloseButton_Part, Type = typeof(ButtonBase))]
    [TemplatePart(Name = WindowContent_Part, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ContentRoot_Part, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ContentBorder_Part, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = WindowRoot_Part, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = MinimizeButton_Part, Type = typeof(ButtonBase))]
    [TemplateVisualState(Name = VSMState_Minimized, GroupName = VSMGroup_Window)]
    [TemplateVisualState(Name = VSMState_Normal, GroupName = VSMGroup_Window)]
    public class FloatWindow : ContentControl, IDisposable
    {
        private const string WindowTitle_Part = "WindowTitle";
        private const string TitleContent_Part = "TitleContent";
        //private const string CloseButton_Part = "CloseButton";
        private const string WindowContent_Part = "WindowContent";
        private const string WindowRoot_Part = "WindowRoot";
        private const string ContentRoot_Part = "ContentRoot";
        private const string ContentBorder_Part = "ContentBorder";
        private const string MinimizeButton_Part = "MinimizeButton";

        //States
        private const string VSMState_Minimized = "Minimized";
        private const string VSMState_Normal = "Normal";
        private const string VSMSTATE_Restored = "Restored";

        //Groups
        private const string VSMGroup_Window = "WindowStates";

        private FrameworkElement windowRoot;
        private FrameworkElement contentRoot;
        private Border contentBorder;
        private FrameworkElement windowTitle;
        private ContentControl titleContent;
        private FrameworkElement windowContent;

        private ButtonBase minimizeButton;

        public FloatWindow()
        {
            DefaultStyleKey = typeof(FloatWindow);
        }

        public WindowHost Host { get; set; }

        #region Position
        public Point Position
        {
            get { return (Point)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(Point), typeof(FloatWindow), new PropertyMetadata(new Point(double.NaN, double.NaN), OnPositionPropertyChanged));

        private static void OnPositionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FloatWindow window = (FloatWindow)d;

            if (window != null)
            {
                //if (window.FloatingWindowHost.IsLayoutUpdated)
                //    window.MoveWindow((Point)e.NewValue);
            }
        }
        #endregion

        public event EventHandler Minimized;
        public event EventHandler Restored;

        private Point clickPosition;
        private Point mouseClickPoint;
        private WindowAction windowAction;
        private bool isMouseCaptured;
        public WindowState windowState = WindowState.Normal;
        private Point previousPosition;
        private Size previousSize;
        private WindowState previousWindowState;

        public bool IsOpen { get; private set; }

        public Panel HostPanel
        {
            get { return this.Host == null ? null : this.Host.HostPanel; }
        }

        public void DisplayWindow()
        {
            DisplayWindow(Position);            
        }

        public void DisplayWindow(Point _point)
        {
            CheckHost();
            Action<Point> action = new Action<Point>(Display);
            this.Host.DisplayWindow(action, _point);
        }

        public void DisplayWindow(Thickness _margins)
        {
            CheckHost();
            Action<Thickness> action = new Action<Thickness>(Display);
            this.Host.DisplayWindow(action, _margins);
        }

        private void Display(Thickness _margins)
        {
            Width = Math.Max(MinWidth, HostPanel.ActualWidth - _margins.Horizontal());
            Height = Math.Max(MinHeight, HostPanel.ActualHeight - _margins.Vertical());
        }

        private void Display(Point _point)
        {
            if (!IsOpen)
            {
                SubscribePartEvents();
                ApplyTemplate();
                Point position = _point;
                if (_point.IsNotSet())
                    position = CenteredWindowPosition;
                MoveWindow(position);
            }
        }

        private void SubscribePartEvents()
        {
            if (minimizeButton != null)
                minimizeButton.Click += new RoutedEventHandler(minimizeButton_Click);
        }

        void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            MinimizeWindow();
        }

        public void MinimizeWindow()
        {
            if (windowState != WindowState.Minimized)
            {
                if (minimizeButton != null)
                    VisualStateManager.GoToState(this, VSMState_Normal, true);
                if (windowState == WindowState.Normal)
                {
                    previousPosition = Position;
                    previousSize = new Size(ActualWidth, ActualHeight);
                }
                previousWindowState = windowState;
                VisualStateManager.GoToState(this, VSMState_Minimized, true);
                OnMinimized(EventArgs.Empty);
            }
            windowState = WindowState.Minimized;
        }

        /// <summary>
        /// Restores window state, size and its position.
        /// </summary>
        public void RestoreWindow()
        {
            switch (windowState)
            {
                case WindowState.Minimized:
                    if (previousWindowState == WindowState.Maximized)
                    {
                        Width = HostPanel.ActualWidth;
                        Height = HostPanel.ActualHeight;
                    }
                    
                    windowState = previousWindowState;
                    VisualStateManager.GoToState(this, VSMSTATE_Restored, true);
                    OnRestored(EventArgs.Empty);
                    break;
                case WindowState.Normal:                    
                    EnsureVisible();
                    break;
            }
            Focus();
        }

        protected virtual void OnRestored(EventArgs e)
        {
            EventHandler handler = Restored;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void EnsureVisible()
        {
            if (HostPanel != null && (Position.X >= HostPanel.ActualWidth || Position.Y >= HostPanel.ActualHeight))
            {
                Position = CenteredWindowPosition;
            }
        }

        protected virtual void OnMinimized(EventArgs e)
        {
            EventHandler handler = Minimized;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void CheckHost()
        {
            if (this.Host == null)
                throw new InvalidOperationException("Floating window is not added to the host");
        }

        private void MoveWindow(Point _point)
        {
            if (contentRoot != null && !(_point.IsNotSet()))
            {
                // Round coordinates to avoid blured window
                double x = Math.Round(Math.Max(0, _point.X));
                double y = Math.Round(Math.Max(0, _point.Y));

                var transformGroup = contentRoot.RenderTransform as TransformGroup;
                var translateTransform = transformGroup.Children.OfType<TranslateTransform>().FirstOrDefault();
                if (translateTransform == null)
                {
                    transformGroup.Children.Add(new TranslateTransform() { X = x, Y = y });
                }
                else
                {
                    translateTransform.X = x;
                    translateTransform.Y = y;
                }

                Point newPosition = new Point(x, y);

                if (Position != newPosition)
                    Position = newPosition;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            windowRoot = GetTemplateChild(WindowRoot_Part) as FrameworkElement;
            contentRoot = GetTemplateChild(ContentRoot_Part) as FrameworkElement;
            contentBorder = GetTemplateChild(ContentBorder_Part) as Border;
            windowTitle = GetTemplateChild(WindowTitle_Part) as FrameworkElement;
            titleContent = GetTemplateChild(TitleContent_Part) as ContentControl;
            windowContent = GetTemplateChild(WindowContent_Part) as FrameworkElement;

            if (windowRoot == null)
                throw new NotImplementedException("Template WindowRoot_Part is required to display Floating Window");
            if (contentRoot == null)
                throw new NotImplementedException("Template ContentRoot_Part is required to display Floating Window");
            if (contentBorder == null)
                throw new NotImplementedException("Template ContentBorder_Part is required to display Floating Window");

            minimizeButton = GetTemplateChild(MinimizeButton_Part) as ButtonBase;
            SetInitialRootPosition();
            //DisplayWindow();
        }

        private void SetInitialRootPosition()
        {
            double x = Math.Round(-this.Margin.Left);
            double y = Math.Round(-this.Margin.Top);

            var transformGroup = windowRoot.RenderTransform as TransformGroup;
            if (transformGroup == null)
            {
                transformGroup = new TransformGroup();
                transformGroup.Children.Add(windowRoot.RenderTransform);
                windowRoot.RenderTransform = transformGroup;
            }

            var translateTransform = transformGroup.Children.OfType<TranslateTransform>().FirstOrDefault();
            if (translateTransform == null)
            {
                transformGroup.Children.Add(new TranslateTransform() { X = x, Y = y });
            }
            else
            {
                translateTransform.X = x;
                translateTransform.Y = y;
            }
        }

        private Point CenteredWindowPosition
        {
            get
            {
                return new Point((HostPanel.ActualWidth - Width.ValueOrZero()) / 2, (HostPanel.ActualHeight - Height.ValueOrZero()) / 2);
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (windowAction == WindowAction.Move)
            {

            }

            if (isMouseCaptured)
            {
                contentRoot.ReleaseMouseCapture();
                isMouseCaptured = false;
            }

            windowAction = WindowAction.None;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (windowState == WindowState.Normal)
            {
                mouseClickPoint = e.GetPosition(HostPanel);
                clickPosition = Position;
                if (windowTitle != null)
                {
                    // If the mouse was clicked on the chrome - start dragging the window
                    Point point = e.GetPosition(windowTitle);

                    if (windowTitle.ContainsPoint(point))
                    {
                        CaptureMouseCursor();
                        windowAction = WindowAction.Move;
                    }
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (windowState == WindowState.Normal)
            {
                Point mousePosition = e.GetPosition(contentRoot);

                if (IsMouseOverButtons(mousePosition, contentRoot))
                    this.Cursor = Cursors.Arrow;
            }

            Point p = e.GetPosition(HostPanel);
            double dx = p.X - mouseClickPoint.X;
            double dy = p.Y - mouseClickPoint.Y;

            if (windowAction == WindowAction.Move)
            {
                Point point = clickPosition.Add(dx, dy);
                MoveWindow(point);
            }
        }

        private bool IsMouseOverButtons(Point position, UIElement origin)
        {
            return (minimizeButton.ContainsPoint(position, origin));
        }

        private void CaptureMouseCursor()
        {
            contentRoot.CaptureMouse();
            isMouseCaptured = true;
        }

        public void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }

    public static class Extensions
    {
        public static double ValueOrZero(this double number)
        {
            return double.IsNaN(number) ? 0 : number;
        }

        public static bool IsNotSet(this Point point)
        {
            return double.IsNaN(point.X) || double.IsNaN(point.Y);
        }

        public static double Horizontal(this Thickness thickness)
        {
            if (thickness == null)
                throw new ArgumentNullException("thickness");

            return thickness.Left + thickness.Right;
        }

        public static double Vertical(this Thickness thickness)
        {
            if (thickness == null)
                throw new ArgumentNullException("thickness");

            return thickness.Top + thickness.Bottom;
        }

        public static bool ContainsPoint(this FrameworkElement element, Point point, UIElement origin = null)
        {
            bool result = false;

            if (element != null)
            {
                Point elementOrigin = (origin == null) ? new Point(0, 0) : element.GetRelativePosition(origin);
                Rect rect = new Rect(elementOrigin, new Size(element.ActualWidth, element.ActualHeight));
                result = rect.Contains(point);
            }

            return result;
        }

        public static Point GetRelativePosition(this UIElement element, UIElement rootElement)
        {
            if (element == null)
                throw new ArgumentNullException("element");

            if (rootElement == null)
                throw new ArgumentNullException("rootElement");

            var transformFromClientToRoot = element.TransformToVisual(rootElement);
            return transformFromClientToRoot.Transform(new Point(0, 0));
        }

        public static Point Add(this Point point, double x, double y)
        {
            return new Point(point.X + x, point.Y + y);
        }
    }

    internal enum WindowAction
    {
        None = 0,
        Move = 1
    }
}
