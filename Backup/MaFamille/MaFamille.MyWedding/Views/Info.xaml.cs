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
using System.Collections.Generic;
using System.Collections;
using MaFamille.Common;
using MaFamille.MyWedding.ViewModel;
using Jounce.Core.Fluent;

namespace MaFamille.MyWedding.Views
{ 
    [ExportAsView("Info", Category = "Wedding", MenuName = "Info", ToolTip = "Home")]
	public partial class Info:IPartImportsSatisfiedNotification
	{
		public Info()
		{
			// Required to initialize variables
			InitializeComponent();
            Loaded += new RoutedEventHandler(Info_Loaded);
		}

        void Info_Loaded(object sender, RoutedEventArgs e)
        {
            model = Router.ResolveViewModel<AlbumViewModel>("AlbumViewModel");
        }       

        private List<Storyboard> storybrdsLeft3D = new List<Storyboard>();
        private List<Storyboard> storybrdsRight3D = new List<Storyboard>();
        private List<Storyboard> storybrdsReverse3D = new List<Storyboard>();        
        void AnimateAlbums()
        {
            List<UIElement> items = new List<UIElement>();           
            
            GetChildren(MyAlbumListBox, typeof(BorderItemsControl), ref items);
            if (items.Count == 0) return;
            foreach (UIElement item in items)
            {
                BorderItemsControl albums = item as BorderItemsControl;

                #region AlbumsMouseEnter
                albums.MouseEnter += (s, es) =>
                {

                    DependencyObject container1 = albums.ItemContainerGenerator.ContainerFromIndex(0);
                    Border b1 = (Border)VisualTreeHelper.GetChild(container1, 0);

                    DependencyObject container2 = albums.ItemContainerGenerator.ContainerFromIndex(1);
                    Border b2 = (Border)VisualTreeHelper.GetChild(container2, 0);

                    DependencyObject container3 = albums.ItemContainerGenerator.ContainerFromIndex(2);
                    Border b3 = (Border)VisualTreeHelper.GetChild(container3, 0);

                    Storyboard sbBorder = new Storyboard();

                    var planProjectionAnimation1 = new PlaneProjection
                    {
                        CenterOfRotationX = 0.5,
                        CenterOfRotationY = 0.5
                    };

                    var planProjectionAnimation2 = new PlaneProjection
                    {
                        CenterOfRotationX = 0.5,
                        CenterOfRotationY = 0.5
                    };

                    var planProjectionAnimation3 = new PlaneProjection
                    {
                        CenterOfRotationX = 0.5,
                        CenterOfRotationY = 0.5
                    };

                    b1.Projection = planProjectionAnimation1;
                    b2.Projection = planProjectionAnimation2;
                    b3.Projection = planProjectionAnimation3;

                    DoubleAnimation borderAnimationRotation1 = new DoubleAnimation
                    {
                        BeginTime = TimeSpan.FromMilliseconds(0),
                        Duration = TimeSpan.FromMilliseconds(250),
                        From = 0,
                        To = -30,
                        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
                    };
                    DoubleAnimation borderAnimationRotation2 = new DoubleAnimation
                    {
                        BeginTime = TimeSpan.FromMilliseconds(0),
                        Duration = TimeSpan.FromMilliseconds(250),
                        From = 0,
                        To = -30,
                        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
                    };
                    DoubleAnimation borderAnimationRotation3 = new DoubleAnimation
                    {
                        BeginTime = TimeSpan.FromMilliseconds(0),
                        Duration = TimeSpan.FromMilliseconds(250),
                        From = 0,
                        To = -30,
                        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
                    };

                    DoubleAnimation borderAnimationGlobalOffSet1 = new DoubleAnimation
                    {
                        BeginTime = TimeSpan.FromMilliseconds(0),
                        Duration = TimeSpan.FromMilliseconds(250),
                        From = 0,
                        To = -14,
                        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
                    };
                    DoubleAnimation borderAnimationGlobalOffSet2 = new DoubleAnimation
                    {
                        BeginTime = TimeSpan.FromMilliseconds(0),
                        Duration = TimeSpan.FromMilliseconds(250),
                        From = 0,
                        To = -8,
                        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
                    };
                    DoubleAnimation borderAnimationGlobalOffSet3 = new DoubleAnimation
                    {
                        BeginTime = TimeSpan.FromMilliseconds(0),
                        Duration = TimeSpan.FromMilliseconds(250),
                        From = 0,
                        To = 0,
                        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
                    };

                    Storyboard.SetTarget(borderAnimationRotation1, planProjectionAnimation1);
                    Storyboard.SetTarget(borderAnimationRotation2, planProjectionAnimation2);
                    Storyboard.SetTarget(borderAnimationRotation3, planProjectionAnimation3);

                    Storyboard.SetTarget(borderAnimationRotation1, b1);
                    Storyboard.SetTarget(borderAnimationRotation2, b2);
                    Storyboard.SetTarget(borderAnimationRotation3, b3);

                    Storyboard.SetTarget(borderAnimationGlobalOffSet1, b1);
                    Storyboard.SetTarget(borderAnimationGlobalOffSet2, b2);
                    Storyboard.SetTarget(borderAnimationGlobalOffSet3, b3);

                    Storyboard.SetTargetProperty(borderAnimationRotation1, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
                    Storyboard.SetTargetProperty(borderAnimationGlobalOffSet1, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));
                    Storyboard.SetTargetProperty(borderAnimationRotation2, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
                    Storyboard.SetTargetProperty(borderAnimationGlobalOffSet2, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));
                    Storyboard.SetTargetProperty(borderAnimationRotation3, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
                    Storyboard.SetTargetProperty(borderAnimationGlobalOffSet3, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));

                    sbBorder.Children.Add(borderAnimationRotation1);
                    sbBorder.Children.Add(borderAnimationRotation2);
                    sbBorder.Children.Add(borderAnimationRotation3);
                    sbBorder.Children.Add(borderAnimationGlobalOffSet1);
                    sbBorder.Children.Add(borderAnimationGlobalOffSet2);
                    sbBorder.Children.Add(borderAnimationGlobalOffSet3);

                    sbBorder.Begin();
                };
                #endregion

                albums.MouseLeave += (se, el) =>
                {
                    //sb.Stop();
                    ReverseAnimate(albums);
                };

                albums.MouseLeftButtonDown += (ss, ec) =>
                {
                    //sb.Stop();
                    ReverseAnimate(albums);
                };    
            }            
        }

        void sbBorder_Completed(object sender, EventArgs e)
        {
            Storyboard storyboard = sender as Storyboard;
            foreach (DictionaryEntry dictionaryEntry in this.LayoutRoot.Resources)
            {
                Storyboard resourceStoryboard = dictionaryEntry.Value as Storyboard;
                if (resourceStoryboard != null)
                {
                    if (resourceStoryboard.GetValue(NameProperty) == storyboard.GetValue(NameProperty))
                        this.LayoutRoot.Resources.Remove(dictionaryEntry.Key);
                }
            }
        }

        private void ReverseAnimate(BorderItemsControl albums)
        {
            DependencyObject container1 = albums.ItemContainerGenerator.ContainerFromIndex(0);
            Border b1 = (Border)VisualTreeHelper.GetChild(container1, 0);

            DependencyObject container2 = albums.ItemContainerGenerator.ContainerFromIndex(1);
            Border b2 = (Border)VisualTreeHelper.GetChild(container2, 0);

            DependencyObject container3 = albums.ItemContainerGenerator.ContainerFromIndex(2);
            Border b3 = (Border)VisualTreeHelper.GetChild(container3, 0);
            
            Storyboard sbBorderReverse = new Storyboard();

            var planProjectionAnimation1 = new PlaneProjection
            {
                CenterOfRotationX = 0.5,
                CenterOfRotationY = 0.5//,
                //RotationY = 0,
                //GlobalOffsetX = 0
            };

            var planProjectionAnimation2 = new PlaneProjection
            {
                CenterOfRotationX = 0.5,
                CenterOfRotationY = 0.5//,
                //RotationY = 0,
                //GlobalOffsetX = 0
            };

            var planProjectionAnimation3 = new PlaneProjection
            {
                CenterOfRotationX = 0.5,
                CenterOfRotationY = 0.5//,
                //RotationY = 0,
                //GlobalOffsetX = 0
            };

            b1.Projection = planProjectionAnimation1;
            b2.Projection = planProjectionAnimation2;
            b3.Projection = planProjectionAnimation3;

            DoubleAnimation borderAnimationRotation1 = new DoubleAnimation
            {
                BeginTime = TimeSpan.FromMilliseconds(0),
                Duration = TimeSpan.FromMilliseconds(250),
                From = -30,
                To = 0,
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };
            DoubleAnimation borderAnimationRotation2 = new DoubleAnimation
            {
                BeginTime = TimeSpan.FromMilliseconds(0),
                Duration = TimeSpan.FromMilliseconds(250),
                From = -30,
                To = 0,
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };
            DoubleAnimation borderAnimationRotation3 = new DoubleAnimation
            {
                BeginTime = TimeSpan.FromMilliseconds(0),
                Duration = TimeSpan.FromMilliseconds(250),
                From = -30,
                To = 0,
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };

            DoubleAnimation borderAnimationGlobalOffSet1 = new DoubleAnimation
            {
                BeginTime = TimeSpan.FromMilliseconds(0),
                Duration = TimeSpan.FromMilliseconds(250),
                From = -14,
                To = 0,
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };
            DoubleAnimation borderAnimationGlobalOffSet2 = new DoubleAnimation
            {
                BeginTime = TimeSpan.FromMilliseconds(0),
                Duration = TimeSpan.FromMilliseconds(250),
                From = -8,
                To = 0,
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };
            DoubleAnimation borderAnimationGlobalOffSet3 = new DoubleAnimation
            {
                BeginTime = TimeSpan.FromMilliseconds(0),
                Duration = TimeSpan.FromMilliseconds(250),
                From = 0,
                To = 0,
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };

            Storyboard.SetTarget(borderAnimationRotation1, planProjectionAnimation1);

            Storyboard.SetTarget(borderAnimationRotation1, b1);
            Storyboard.SetTarget(borderAnimationRotation2, b2);
            Storyboard.SetTarget(borderAnimationRotation3, b3);

            Storyboard.SetTarget(borderAnimationGlobalOffSet1, b1);
            Storyboard.SetTarget(borderAnimationGlobalOffSet2, b2);
            Storyboard.SetTarget(borderAnimationGlobalOffSet3, b3);

            Storyboard.SetTargetProperty(borderAnimationRotation1, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
            Storyboard.SetTargetProperty(borderAnimationGlobalOffSet1, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));
            Storyboard.SetTargetProperty(borderAnimationRotation2, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
            Storyboard.SetTargetProperty(borderAnimationGlobalOffSet2, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));
            Storyboard.SetTargetProperty(borderAnimationRotation3, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
            Storyboard.SetTargetProperty(borderAnimationGlobalOffSet3, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));

            //Storyboard.SetTargetName(borderAnimationRotation1, "border");
            //Storyboard.SetTargetName(borderAnimationRotation1, "border");
            //Storyboard.SetTargetName(borderAnimationRotation1, "border");

            //Storyboard.SetTarget(borderAnimation1, b1);
            //Storyboard.SetTarget(borderAnimation2, b2);
            //Storyboard.SetTarget(borderAnimation3, b3);  

            sbBorderReverse.Children.Add(borderAnimationRotation1);
            sbBorderReverse.Children.Add(borderAnimationRotation2);
            sbBorderReverse.Children.Add(borderAnimationRotation3);
            sbBorderReverse.Children.Add(borderAnimationGlobalOffSet1);
            sbBorderReverse.Children.Add(borderAnimationGlobalOffSet2);
            sbBorderReverse.Children.Add(borderAnimationGlobalOffSet3);
            sbBorderReverse.Begin();
        }

        [Export]
        public ViewModelRoute Binding
        {
            get 
            {
                //model = Router.ResolveViewModel<AlbumViewModel>("AlbumViewModel");
                return ViewModelRoute.Create("AlbumViewModel", "Info"); 
            }
        }

        [Import]
        public IViewModelRouter Router { get; set; }

        [Import]
        public IFluentViewModelRouter FluentRouter { get; set; }

        AlbumViewModel model;

        public void OnImportsSatisfied()
        {
            //FluentRouter.RouteViewModelForView("AlbumViewModel", "Info");
            model = Router.ResolveViewModel<AlbumViewModel>("AlbumViewModel");
        }

        private void GetChildren(UIElement parent, Type targetType, ref List<UIElement> children)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    UIElement child = (UIElement)VisualTreeHelper.GetChild(parent, i);
                    if (child.GetType() == targetType)
                    {
                        children.Add(child);
                    }
                    GetChildren(child, targetType, ref children);
                }
            }
        }

        private string DisplayVisualTree(DependencyObject depObj, int depth)
        {
            string response = string.Empty.PadRight(depth) + depObj.ToString();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                response += Environment.NewLine + DisplayVisualTree(VisualTreeHelper.GetChild(depObj, i), depth + 1);
            }
            return response;
        }

        public static T FindVisualChildByName<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                string controlName = child.GetValue(Control.NameProperty) as string;
                if (controlName == name)
                {
                    return child as T;
                }
                else
                {
                    T result = FindVisualChildByName<T>(child, name);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }

        private void MyAlbumListBox_LayoutUpdated(object sender, EventArgs e)
        {
            if(MyAlbumListBox.Items.Count>0)
                AnimateAlbums(); 
        }
	}
}