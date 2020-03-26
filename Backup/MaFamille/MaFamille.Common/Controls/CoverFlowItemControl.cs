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

namespace MaFamille.Common.Controls
{
    public class CoverFlowItemControl : ContentControl
    {
        public event ItemSelectedHandler ItemSelected;
        public delegate void ItemSelectedHandler(object sender, CoverFlowEventArgs e);

        private FrameworkElement LayoutRoot;
        private PlaneProjection planeProjection;
        private Storyboard Animation;
        private ScaleTransform scaleTransform;
        private EasingDoubleKeyFrame rotationKeyFrame, offestZKeyFrame, scaleXKeyFrame, scaleYKeyFrame;

        public delegate void AnimationCompletedEvent();
        public event AnimationCompletedEvent AnimationCompleted;
        
        private double yRotation;
        public double YRotation
        {
            get
            {
                return yRotation;
            }
            set
            {
                yRotation = value;
                if (planeProjection != null)
                {
                    planeProjection.RotationY = value;
                }
            }
        }

        private double zOffset;
        public double ZOffset
        {
            get
            {
                return zOffset;
            }
            set
            {
                zOffset = value;
                if (planeProjection != null)
                {
                    planeProjection.LocalOffsetZ = value;
                }
            }
        }

        private double scale;
        public double Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                if (scaleTransform != null)
                {
                    scaleTransform.ScaleX = scale;
                    scaleTransform.ScaleY = scale;
                }
            }
        }

        private Duration duration;
        private IEasingFunction easingFunction;

        private DoubleAnimation xAnimation;

        private double x;
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
                Canvas.SetLeft(this, value);
            }
        }

        private bool isAnimating;


        private ContentControl ContentPresenter; //ContentPresenter

        public CoverFlowItemControl()
        {
            DefaultStyleKey = typeof(CoverFlowItemControl);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ContentPresenter = (ContentControl)GetTemplateChild("ContentPresenter");
            planeProjection = (PlaneProjection)GetTemplateChild("Rotator");
            LayoutRoot = (FrameworkElement)GetTemplateChild("LayoutRoot");

            Animation = (Storyboard)GetTemplateChild("Animation");
            Animation.Completed += new EventHandler(Animation_Completed);
            rotationKeyFrame = (EasingDoubleKeyFrame)GetTemplateChild("rotationKeyFrame");
            offestZKeyFrame = (EasingDoubleKeyFrame)GetTemplateChild("offestZKeyFrame");
            scaleXKeyFrame = (EasingDoubleKeyFrame)GetTemplateChild("scaleXKeyFrame");
            scaleYKeyFrame = (EasingDoubleKeyFrame)GetTemplateChild("scaleYKeyFrame");
            scaleTransform = (ScaleTransform)GetTemplateChild("scaleTransform");

            planeProjection.RotationY = yRotation;
            planeProjection.LocalOffsetZ = zOffset;
            if (ContentPresenter != null)
            {
                ContentPresenter.MouseLeftButtonUp += new MouseButtonEventHandler(ContentPresenter_MouseLeftButtonUp);
                //ContentPresenter.MouseEnter += new MouseEventHandler(ContentPresenter_MouseEnter);
            }

            if (Animation != null)
            {
                xAnimation = new DoubleAnimation();
                //xAnimation.EasingFunction = EasingFunction;

                //xAnimation.Duration = Duration;
                Animation.Children.Add(xAnimation);

                Storyboard.SetTarget(xAnimation, this);
                Storyboard.SetTargetProperty(xAnimation, new PropertyPath("(Canvas.Left)"));


                // Make the Storyboard a resource.
                //Resources.Add("unique_id" + Guid.NewGuid(), sb);
            }
        }

        

        void Animation_Completed(object sender, EventArgs e)
        {
            isAnimating = false;
            if (AnimationCompleted != null)
                AnimationCompleted();
        }

        void ContentPresenter_MouseEnter(object sender, MouseEventArgs e)
        {
            if (ItemSelected != null)
                ItemSelected(this, new CoverFlowEventArgs{ MouseClick=false});
        }

        void ContentPresenter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (ItemSelected != null)
                ItemSelected(this, new CoverFlowEventArgs { MouseClick = true });
        }

        public void SetValues(double x, int zIndex, double r, double z, double s, Duration d, IEasingFunction ease, bool useAnimation)
        {
            if (useAnimation)
            {
                if (!isAnimating && Canvas.GetLeft(this) != x)
                    Canvas.SetLeft(this, this.x);

                rotationKeyFrame.Value = r;
                offestZKeyFrame.Value = z;
                scaleYKeyFrame.Value = s;
                scaleXKeyFrame.Value = s;
                xAnimation.To = x;

                if (duration != d)
                {
                    duration = d;
                    rotationKeyFrame.KeyTime = KeyTime.FromTimeSpan(d.TimeSpan);
                    offestZKeyFrame.KeyTime = KeyTime.FromTimeSpan(d.TimeSpan);
                    scaleYKeyFrame.KeyTime = KeyTime.FromTimeSpan(d.TimeSpan);
                    scaleXKeyFrame.KeyTime = KeyTime.FromTimeSpan(d.TimeSpan);
                    xAnimation.Duration = d;
                }
                if (easingFunction != ease)
                {
                    easingFunction = ease;
                    rotationKeyFrame.EasingFunction = ease;
                    offestZKeyFrame.EasingFunction = ease;
                    scaleYKeyFrame.EasingFunction = ease;
                    scaleXKeyFrame.EasingFunction = ease;
                    xAnimation.EasingFunction = ease;
                }

                isAnimating = true;                
                Animation.Begin();
                Canvas.SetZIndex(this, zIndex);
            }
            this.x = x;
        }
    }

    public class CoverFlowEventArgs : EventArgs
    {
        public int Index { get; set; }
        public object Item { get; set; }
        public bool MouseClick { get; set; }
    }

}
