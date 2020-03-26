using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Jounce.Core.View;
using System.ComponentModel.Composition;
using Jounce.Core.ViewModel;
using MaFamille.Common;
using System.Windows.Threading;
using System.Collections;
using MaFamille.Albums.ViewModels;
namespace MaFamille.Albums.Views
{
    [ExportAsView("MyAlbum", Category = "Navigation", MenuName = "MyAlbum", ToolTip = "")]
    public partial class MyAlbum : IPartImportsSatisfiedNotification
    {
        public MyAlbum()
        {
            InitializeComponent();                        
            //MyAlbumListBox.ItemContainerGenerator.ItemsChanged += new System.Windows.Controls.Primitives.ItemsChangedEventHandler(ItemContainerGenerator_ItemsChanged);            
        }

        private List<Storyboard> storybrdsLeft3D = new List<Storyboard>();
        private List<Storyboard> storybrdsRight3D = new List<Storyboard>();
        private List<Storyboard> storybrdsReverse3D = new List<Storyboard>();

        //void ItemContainerGenerator_ItemsChanged(object sender, System.Windows.Controls.Primitives.ItemsChangedEventArgs e)
        //{
        //    if (MyAlbumListBox.Items.Count == 0)
        //        return;
        //    List<UIElement> items = new List<UIElement>();
        //    GetChildren(MyAlbumListBox, typeof(BorderItemsControl), ref items);
        //    if (items.Count==0) return;
        //    BorderItemsControl albums = items[items.Count-1] as BorderItemsControl;

        //    #region AlbumsMouseEnter
        //    albums.MouseEnter += (s, es) =>
        //    {
        //        #region CommentedAnimation
        //        //for (int i = 0; i < albums.Items.Count; i++)
        //        //{                    
        //            //DependencyObject container = albums.ItemContainerGenerator.ContainerFromIndex(i);
        //            //Border b = (Border)VisualTreeHelper.GetChild(container, 0);
        //            ////var duration = TimeSpan.FromMilliseconds(2);
        //            ////var sb = new Storyboard { Duration = duration };
        //            //Storyboard sb = new Storyboard();                    
        //            //var doubleAnimation = new DoubleAnimation
        //            //{
        //            //    BeginTime = TimeSpan.FromMilliseconds(0),
        //            //    Duration = TimeSpan.FromMilliseconds(300),
        //            //    To = 0,
        //            //    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
        //            //};
        //            //sb.Children.Add(doubleAnimation);
        //            //Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.Rotation)"));
        //            //Storyboard.SetTargetName(doubleAnimation, "border");
        //            //Storyboard.SetTarget(doubleAnimation, b);
                                        
        //            //var scalexAnimation = new DoubleAnimationUsingKeyFrames();
        //            //Storyboard.SetTargetProperty(scalexAnimation, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.ScaleX)"));
        //            //Storyboard.SetTargetName(scalexAnimation, "border");
        //            //Storyboard.SetTarget(scalexAnimation, b);
        //            //scalexAnimation.KeyFrames.Add(new EasingDoubleKeyFrame
        //            //{
        //            //    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0)),
        //            //    Value=1,
        //            //    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }                        
        //            //});
        //            //scalexAnimation.KeyFrames.Add(new EasingDoubleKeyFrame
        //            //{
        //            //    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300)),
        //            //    Value = 1.1,
        //            //    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
        //            //});
        //            //sb.Children.Add(scalexAnimation);
        //            //var scaleyAnimation = new DoubleAnimationUsingKeyFrames();
        //            //Storyboard.SetTargetProperty(scaleyAnimation, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.ScaleY)"));
        //            //Storyboard.SetTargetName(scaleyAnimation, "border");
        //            //Storyboard.SetTarget(scaleyAnimation, b);                    
        //            //scaleyAnimation.KeyFrames.Add(new EasingDoubleKeyFrame
        //            //{
        //            //    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0)),
        //            //    Value = 1,
        //            //    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        //            //});
        //            //scaleyAnimation.KeyFrames.Add(new EasingDoubleKeyFrame
        //            //{
        //            //    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300)),
        //            //    Value = 1.1,
        //            //    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        //            //});                    
        //            //sb.Children.Add(scaleyAnimation);                    
        //            //sb.Begin();                  
        //        //}
        //        #endregion

        //        DependencyObject container1 = albums.ItemContainerGenerator.ContainerFromIndex(0);
        //        Border b1 = (Border)VisualTreeHelper.GetChild(container1, 0);

        //        DependencyObject container2 = albums.ItemContainerGenerator.ContainerFromIndex(1);
        //        Border b2 = (Border)VisualTreeHelper.GetChild(container2, 0);

        //        DependencyObject container3 = albums.ItemContainerGenerator.ContainerFromIndex(2);
        //        Border b3 = (Border)VisualTreeHelper.GetChild(container3, 0);
                                
        //        Storyboard sbBorder = new Storyboard();                

        //        var planProjectionAnimation1 = new PlaneProjection
        //        {
        //            CenterOfRotationX = 0.5,
        //            CenterOfRotationY = 0.5                    
        //        };

        //        var planProjectionAnimation2 = new PlaneProjection
        //        {
        //            CenterOfRotationX = 0.5,
        //            CenterOfRotationY = 0.5                    
        //        };

        //        var planProjectionAnimation3 = new PlaneProjection
        //        {
        //            CenterOfRotationX = 0.5,
        //            CenterOfRotationY = 0.5                    
        //        };

        //        b1.Projection = planProjectionAnimation1;
        //        b2.Projection = planProjectionAnimation2;
        //        b3.Projection = planProjectionAnimation3;

        //        DoubleAnimation borderAnimationRotation1 = new DoubleAnimation
        //        {
        //            BeginTime = TimeSpan.FromMilliseconds(0),
        //            Duration = TimeSpan.FromMilliseconds(250),
        //            From = 0,
        //            To = -30,
        //            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
        //        };
        //        DoubleAnimation borderAnimationRotation2 = new DoubleAnimation
        //        {
        //            BeginTime = TimeSpan.FromMilliseconds(0),
        //            Duration = TimeSpan.FromMilliseconds(250),
        //            From = 0,
        //            To = -30,
        //            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
        //        };
        //        DoubleAnimation borderAnimationRotation3 = new DoubleAnimation
        //        {
        //            BeginTime = TimeSpan.FromMilliseconds(0),
        //            Duration = TimeSpan.FromMilliseconds(250),
        //            From = 0,
        //            To = -30,
        //            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
        //        };

        //        DoubleAnimation borderAnimationGlobalOffSet1 = new DoubleAnimation
        //        {
        //            BeginTime = TimeSpan.FromMilliseconds(0),
        //            Duration = TimeSpan.FromMilliseconds(250),
        //            From = 0,
        //            To = -14,
        //            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
        //        };
        //        DoubleAnimation borderAnimationGlobalOffSet2 = new DoubleAnimation
        //        {
        //            BeginTime = TimeSpan.FromMilliseconds(0),
        //            Duration = TimeSpan.FromMilliseconds(250),
        //            From = 0,
        //            To = -8,
        //            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
        //        };
        //        DoubleAnimation borderAnimationGlobalOffSet3 = new DoubleAnimation
        //        {
        //            BeginTime = TimeSpan.FromMilliseconds(0),
        //            Duration = TimeSpan.FromMilliseconds(250),
        //            From = 0,
        //            To = 0,
        //            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
        //        };

        //        Storyboard.SetTarget(borderAnimationRotation1, planProjectionAnimation1);
        //        Storyboard.SetTarget(borderAnimationRotation2, planProjectionAnimation2);
        //        Storyboard.SetTarget(borderAnimationRotation3, planProjectionAnimation3);

        //        Storyboard.SetTarget(borderAnimationRotation1, b1);
        //        Storyboard.SetTarget(borderAnimationRotation2, b2);
        //        Storyboard.SetTarget(borderAnimationRotation3, b3);

        //        Storyboard.SetTarget(borderAnimationGlobalOffSet1, b1);
        //        Storyboard.SetTarget(borderAnimationGlobalOffSet2, b2);
        //        Storyboard.SetTarget(borderAnimationGlobalOffSet3, b3);                

        //        Storyboard.SetTargetProperty(borderAnimationRotation1, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
        //        Storyboard.SetTargetProperty(borderAnimationGlobalOffSet1, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));
        //        Storyboard.SetTargetProperty(borderAnimationRotation2, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
        //        Storyboard.SetTargetProperty(borderAnimationGlobalOffSet2, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));
        //        Storyboard.SetTargetProperty(borderAnimationRotation3, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
        //        Storyboard.SetTargetProperty(borderAnimationGlobalOffSet3, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));
                                
        //        sbBorder.Children.Add(borderAnimationRotation1);
        //        sbBorder.Children.Add(borderAnimationRotation2);
        //        sbBorder.Children.Add(borderAnimationRotation3);
        //        sbBorder.Children.Add(borderAnimationGlobalOffSet1);
        //        sbBorder.Children.Add(borderAnimationGlobalOffSet2);
        //        sbBorder.Children.Add(borderAnimationGlobalOffSet3);

        //        sbBorder.Begin();
        //    };
        //    #endregion

        //    albums.MouseLeave += (se, el) =>
        //    {
        //        //sb.Stop();
        //        ReverseAnimate(albums);
        //    };

        //    albums.MouseLeftButtonDown += (ss, ec) =>
        //    {
        //        //sb.Stop();
        //        ReverseAnimate(albums);
        //    };

        //    #region Commented
        //    //Storyboard sbBorder = null;
        //    //#region 3DBorderLeft
        //    //for (int i = 0; i < items.Count - 1; i++)
        //    //{
        //    //    DependencyObject container1 = albums.ItemContainerGenerator.ContainerFromIndex(0);
        //    //    Border b1 = (Border)VisualTreeHelper.GetChild(container1, 0);

        //    //    DependencyObject container2 = albums.ItemContainerGenerator.ContainerFromIndex(1);
        //    //    Border b2 = (Border)VisualTreeHelper.GetChild(container2, 0);

        //    //    DependencyObject container3 = albums.ItemContainerGenerator.ContainerFromIndex(2);
        //    //    Border b3 = (Border)VisualTreeHelper.GetChild(container3, 0);

        //    //    //<Storyboard x:Name="_3dimage">
        //    //    //    <DoubleAnimation Duration="0:0:0.5" To="20" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.RotationY)" Storyboard.TargetName="border1" d:IsOptimized="True"/>
        //    //    //    <DoubleAnimation Duration="0:0:0.5" To="-5" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.GlobalOffsetX)" Storyboard.TargetName="border1" d:IsOptimized="True"/>
        //    //    //    <DoubleAnimation Duration="0:0:0.5" To="20" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.RotationY)" Storyboard.TargetName="border" d:IsOptimized="True"/>
        //    //    //    <DoubleAnimation Duration="0:0:0.5" To="6" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.GlobalOffsetX)" Storyboard.TargetName="border" d:IsOptimized="True"/>
        //    //    //    <DoubleAnimation Duration="0:0:0.5" To="20" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.RotationY)" Storyboard.TargetName="border2" d:IsOptimized="True"/>
        //    //    //    <DoubleAnimation Duration="0:0:0.5" To="-13" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.GlobalOffsetX)" Storyboard.TargetName="border2" d:IsOptimized="True"/>
        //    //    //</Storyboard>

        //    //    sbBorder = new Storyboard();
        //    //    //sbBorder.Completed += new EventHandler(sbBorder_Completed);

        //    //    var planProjectionAnimation1 = new PlaneProjection
        //    //    {
        //    //        CenterOfRotationX = 0.5,
        //    //        CenterOfRotationY = 0.5
        //    //        //RotationY = 20,
        //    //        //GlobalOffsetX = -5
        //    //    };

        //    //    var planProjectionAnimation2 = new PlaneProjection
        //    //    {
        //    //        CenterOfRotationX = 0.5,
        //    //        CenterOfRotationY = 0.5
        //    //        //RotationY = 20,
        //    //        //GlobalOffsetX = 6
        //    //    };

        //    //    var planProjectionAnimation3 = new PlaneProjection
        //    //    {
        //    //        CenterOfRotationX = 0.5,
        //    //        CenterOfRotationY = 0.5
        //    //        //RotationY = 20,
        //    //        //GlobalOffsetX = -13
        //    //    };

        //    //    b1.Projection = planProjectionAnimation1;
        //    //    b2.Projection = planProjectionAnimation2;
        //    //    b3.Projection = planProjectionAnimation3;

        //    //    DoubleAnimation borderAnimationRotation1 = new DoubleAnimation
        //    //    {
        //    //        BeginTime = TimeSpan.FromMilliseconds(0),
        //    //        Duration = TimeSpan.FromMilliseconds(250),
        //    //        From = 0,
        //    //        To = 20,
        //    //        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
        //    //    };
        //    //    DoubleAnimation borderAnimationRotation2 = new DoubleAnimation
        //    //    {
        //    //        BeginTime = TimeSpan.FromMilliseconds(0),
        //    //        Duration = TimeSpan.FromMilliseconds(250),
        //    //        From = 0,
        //    //        To = 20,
        //    //        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
        //    //    };
        //    //    DoubleAnimation borderAnimationRotation3 = new DoubleAnimation
        //    //    {
        //    //        BeginTime = TimeSpan.FromMilliseconds(0),
        //    //        Duration = TimeSpan.FromMilliseconds(250),
        //    //        From = 0,
        //    //        To = 20,
        //    //        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
        //    //    };

        //    //    DoubleAnimation borderAnimationGlobalOffSet1 = new DoubleAnimation
        //    //    {
        //    //        BeginTime = TimeSpan.FromMilliseconds(0),
        //    //        Duration = TimeSpan.FromMilliseconds(500),
        //    //        From = 0,
        //    //        To = -10,
        //    //        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
        //    //    };
        //    //    DoubleAnimation borderAnimationGlobalOffSet2 = new DoubleAnimation
        //    //    {
        //    //        BeginTime = TimeSpan.FromMilliseconds(0),
        //    //        Duration = TimeSpan.FromMilliseconds(500),
        //    //        From = 0,
        //    //        To = 10,
        //    //        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
        //    //    };
        //    //    DoubleAnimation borderAnimationGlobalOffSet3 = new DoubleAnimation
        //    //    {
        //    //        BeginTime = TimeSpan.FromMilliseconds(0),
        //    //        Duration = TimeSpan.FromMilliseconds(500),
        //    //        From = 0,
        //    //        To = -20,
        //    //        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
        //    //    };

        //    //    Storyboard.SetTarget(borderAnimationRotation1, planProjectionAnimation1);
        //    //    Storyboard.SetTarget(borderAnimationRotation2, planProjectionAnimation2);
        //    //    Storyboard.SetTarget(borderAnimationRotation3, planProjectionAnimation3);

        //    //    Storyboard.SetTarget(borderAnimationRotation1, b1);
        //    //    Storyboard.SetTarget(borderAnimationRotation2, b2);
        //    //    Storyboard.SetTarget(borderAnimationRotation3, b3);

        //    //    Storyboard.SetTarget(borderAnimationGlobalOffSet1, b1);
        //    //    Storyboard.SetTarget(borderAnimationGlobalOffSet2, b2);
        //    //    Storyboard.SetTarget(borderAnimationGlobalOffSet3, b3);

        //    //    //Storyboard.SetTargetProperty(borderAnimationRotation1, new PropertyPath(PlaneProjection.RotationYProperty));
        //    //    //Storyboard.SetTargetProperty(borderAnimationRotation2, new PropertyPath(PlaneProjection.RotationYProperty));
        //    //    //Storyboard.SetTargetProperty(borderAnimationRotation3, new PropertyPath(PlaneProjection.RotationYProperty));

        //    //    //Storyboard.SetTargetProperty(borderAnimationGlobalOffSet1, new PropertyPath(PlaneProjection.GlobalOffsetXProperty));
        //    //    //Storyboard.SetTargetProperty(borderAnimationGlobalOffSet2, new PropertyPath(PlaneProjection.GlobalOffsetXProperty));
        //    //    //Storyboard.SetTargetProperty(borderAnimationGlobalOffSet3, new PropertyPath(PlaneProjection.GlobalOffsetXProperty));

        //    //    //Storyboard.SetTarget(borderAnimationRotation1, b1.RenderTransform);
        //    //    //Storyboard.SetTarget(borderAnimationRotation2, b2.RenderTransform);
        //    //    //Storyboard.SetTarget(borderAnimationRotation3, b3.RenderTransform);

        //    //    Storyboard.SetTargetProperty(borderAnimationRotation1, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
        //    //    Storyboard.SetTargetProperty(borderAnimationGlobalOffSet1, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));
        //    //    Storyboard.SetTargetProperty(borderAnimationRotation2, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
        //    //    Storyboard.SetTargetProperty(borderAnimationGlobalOffSet2, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));
        //    //    Storyboard.SetTargetProperty(borderAnimationRotation3, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
        //    //    Storyboard.SetTargetProperty(borderAnimationGlobalOffSet3, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));

        //    //    //Storyboard.SetTargetName(borderAnimationRotation1, "border");
        //    //    //Storyboard.SetTargetName(borderAnimationRotation2, "border");
        //    //    //Storyboard.SetTargetName(borderAnimationRotation3, "border");

        //    //    sbBorder.Children.Add(borderAnimationRotation1);
        //    //    sbBorder.Children.Add(borderAnimationRotation2);
        //    //    sbBorder.Children.Add(borderAnimationRotation3);
        //    //    sbBorder.Children.Add(borderAnimationGlobalOffSet1);
        //    //    sbBorder.Children.Add(borderAnimationGlobalOffSet2);
        //    //    sbBorder.Children.Add(borderAnimationGlobalOffSet3);

        //    //    sbBorder.Begin();

        //    //    //sbBorder.Children.Add(borderAnimationGlobalOffSet1);
        //    //    //sbBorder.Children.Add(borderAnimationGlobalOffSet2);
        //    //    //sbBorder.Children.Add(borderAnimationGlobalOffSet3);                
        //    //}
        //    //if(sbBorder!=null)
        //    //    storybrdsLeft3D.Add(sbBorder);
        //    //#endregion

        //    //#region 3DBorderReverse
        //    //Storyboard sbBorderReverse = null;
        //    //for (int i = 0; i < items.Count - 1; i++)
        //    //{
        //    //    DependencyObject container1 = albums.ItemContainerGenerator.ContainerFromIndex(0);
        //    //    Border b1 = (Border)VisualTreeHelper.GetChild(container1, 0);

        //    //    DependencyObject container2 = albums.ItemContainerGenerator.ContainerFromIndex(1);
        //    //    Border b2 = (Border)VisualTreeHelper.GetChild(container2, 0);

        //    //    DependencyObject container3 = albums.ItemContainerGenerator.ContainerFromIndex(2);
        //    //    Border b3 = (Border)VisualTreeHelper.GetChild(container3, 0);


        //    //    //<Storyboard x:Name="_3dimage">
        //    //    //    <DoubleAnimation Duration="0:0:0.5" To="20" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.RotationY)" Storyboard.TargetName="border1" d:IsOptimized="True"/>
        //    //    //    <DoubleAnimation Duration="0:0:0.5" To="-5" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.GlobalOffsetX)" Storyboard.TargetName="border1" d:IsOptimized="True"/>
        //    //    //    <DoubleAnimation Duration="0:0:0.5" To="20" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.RotationY)" Storyboard.TargetName="border" d:IsOptimized="True"/>
        //    //    //    <DoubleAnimation Duration="0:0:0.5" To="6" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.GlobalOffsetX)" Storyboard.TargetName="border" d:IsOptimized="True"/>
        //    //    //    <DoubleAnimation Duration="0:0:0.5" To="20" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.RotationY)" Storyboard.TargetName="border2" d:IsOptimized="True"/>
        //    //    //    <DoubleAnimation Duration="0:0:0.5" To="-13" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.GlobalOffsetX)" Storyboard.TargetName="border2" d:IsOptimized="True"/>
        //    //    //</Storyboard>

        //    //    sbBorderReverse = new Storyboard();

        //    //    var planProjectionAnimation1 = new PlaneProjection
        //    //    {
        //    //        CenterOfRotationX = 0.5,
        //    //        CenterOfRotationY = 0.5//,
        //    //        //RotationY = 0,
        //    //        //GlobalOffsetX = 0
        //    //    };

        //    //    //var planProjectionAnimation2 = new PlaneProjection
        //    //    //{
        //    //    //    CenterOfRotationX = 0.5,
        //    //    //    CenterOfRotationY = 0.5,
        //    //    //    RotationY = 0,
        //    //    //    GlobalOffsetX = 0
        //    //    //};

        //    //    //var planProjectionAnimation3 = new PlaneProjection
        //    //    //{
        //    //    //    CenterOfRotationX = 0.5,
        //    //    //    CenterOfRotationY = 0.5,
        //    //    //    RotationY = 0,
        //    //    //    GlobalOffsetX = 0
        //    //    //};

        //    //    b1.Projection = planProjectionAnimation1;
        //    //    b2.Projection = planProjectionAnimation1;
        //    //    b3.Projection = planProjectionAnimation1;

        //    //    DoubleAnimation borderAnimationRotation1 = new DoubleAnimation
        //    //    {
        //    //        Duration = TimeSpan.FromMilliseconds(500),
        //    //        To = 0
        //    //    };
        //    //    //DoubleAnimation borderAnimation2 = new DoubleAnimation
        //    //    //{
        //    //    //    Duration = TimeSpan.FromMilliseconds(500),
        //    //    //    To = 0
        //    //    //};
        //    //    //DoubleAnimation borderAnimation3 = new DoubleAnimation
        //    //    //{
        //    //    //    Duration = TimeSpan.FromMilliseconds(500),
        //    //    //    To = 0
        //    //    //};

        //    //    Storyboard.SetTarget(borderAnimationRotation1, b1);
        //    //    Storyboard.SetTarget(borderAnimationRotation1, b2);
        //    //    Storyboard.SetTarget(borderAnimationRotation1, b3);

        //    //    Storyboard.SetTargetProperty(borderAnimationRotation1, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
        //    //    //Storyboard.SetTargetProperty(borderAnimation1, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));
        //    //    //Storyboard.SetTargetProperty(borderAnimationRotation1, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
        //    //    //Storyboard.SetTargetProperty(borderAnimation2, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));
        //    //    //Storyboard.SetTargetProperty(borderAnimationRotation1, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
        //    //    //Storyboard.SetTargetProperty(borderAnimation3, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));

        //    //    //Storyboard.SetTargetName(borderAnimationRotation1, "border");
        //    //    //Storyboard.SetTargetName(borderAnimationRotation1, "border");
        //    //    //Storyboard.SetTargetName(borderAnimationRotation1, "border");

        //    //    //Storyboard.SetTarget(borderAnimation1, b1);
        //    //    //Storyboard.SetTarget(borderAnimation2, b2);
        //    //    //Storyboard.SetTarget(borderAnimation3, b3);  

        //    //    sbBorderReverse.Children.Add(borderAnimationRotation1);
        //    //}
        //    //storybrdsReverse3D.Add(sbBorderReverse);
        //    //#endregion

        //    //#region 3DBorderRight
        //    //for (int i = 0; i < items.Count - 1; i++)
        //    //{
        //    //    DependencyObject container1 = albums.ItemContainerGenerator.ContainerFromIndex(0);
        //    //    Border b1 = (Border)VisualTreeHelper.GetChild(container1, 0);

        //    //    DependencyObject container2 = albums.ItemContainerGenerator.ContainerFromIndex(1);
        //    //    Border b2 = (Border)VisualTreeHelper.GetChild(container2, 0);

        //    //    DependencyObject container3 = albums.ItemContainerGenerator.ContainerFromIndex(2);
        //    //    Border b3 = (Border)VisualTreeHelper.GetChild(container3, 0);
                
        //    //    //        <Storyboard x:Name="_3dimage_right">
        //    //    //    <DoubleAnimation Duration="0:0:0.4" To="-20" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.RotationY)" Storyboard.TargetName="border5" d:IsOptimized="True"/>
        //    //    //    <DoubleAnimation Duration="0:0:0.4" To="-20" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.RotationY)" Storyboard.TargetName="border4" d:IsOptimized="True"/>
        //    //    //    <DoubleAnimation Duration="0:0:0.4" To="-21" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.RotationY)" Storyboard.TargetName="border3" d:IsOptimized="True"/>
        //    //    //    <DoubleAnimation Duration="0:0:0.4" To="-8" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.GlobalOffsetX)" Storyboard.TargetName="border4" d:IsOptimized="True"/>
        //    //    //    <DoubleAnimation Duration="0:0:0.4" To="-14" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.GlobalOffsetX)" Storyboard.TargetName="border3" d:IsOptimized="True"/>
        //    //    //</Storyboard>

        //    //    Storyboard sbBorder = new Storyboard();

        //    //    var planProjectionAnimation1 = new PlaneProjection
        //    //    {
        //    //        CenterOfRotationX = 0.5,
        //    //        CenterOfRotationY = 0.5,
        //    //        RotationY = -20,
        //    //        GlobalOffsetX = 20
        //    //    };

        //    //    var planProjectionAnimation2 = new PlaneProjection
        //    //    {
        //    //        CenterOfRotationX = 0.5,
        //    //        CenterOfRotationY = 0.5,
        //    //        RotationY = -20,
        //    //        GlobalOffsetX = 28
        //    //    };

        //    //    var planProjectionAnimation3 = new PlaneProjection
        //    //    {
        //    //        CenterOfRotationX = 0.5,
        //    //        CenterOfRotationY = 0.5,
        //    //        RotationY = -20,
        //    //        GlobalOffsetX = 36
        //    //    };

        //    //    b1.Projection = planProjectionAnimation1;
        //    //    b2.Projection = planProjectionAnimation2;
        //    //    b3.Projection = planProjectionAnimation3;

        //    //    //DoubleAnimation borderAnimation1 = new DoubleAnimation
        //    //    //{
        //    //    //    Duration = TimeSpan.FromMilliseconds(500),
        //    //    //    To = -5
        //    //    //};
        //    //    //DoubleAnimation borderAnimation2 = new DoubleAnimation
        //    //    //{
        //    //    //    Duration = TimeSpan.FromMilliseconds(500),
        //    //    //    To = 6
        //    //    //};
        //    //    //DoubleAnimation borderAnimation3 = new DoubleAnimation
        //    //    //{
        //    //    //    Duration = TimeSpan.FromMilliseconds(500),
        //    //    //    To = -13
        //    //    //};

        //    //    //Storyboard.SetTargetProperty(borderAnimation1, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
        //    //    //Storyboard.SetTargetProperty(borderAnimation1, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));
        //    //    //Storyboard.SetTargetProperty(borderAnimation2, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
        //    //    //Storyboard.SetTargetProperty(borderAnimation2, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));
        //    //    //Storyboard.SetTargetProperty(borderAnimation3, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
        //    //    //Storyboard.SetTargetProperty(borderAnimation3, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));

        //    //    //Storyboard.SetTargetName(borderAnimation1, "border");
        //    //    //Storyboard.SetTargetName(borderAnimation2, "border");
        //    //    //Storyboard.SetTargetName(borderAnimation3, "border");

        //    //    //Storyboard.SetTarget(borderAnimation1, b1);
        //    //    //Storyboard.SetTarget(borderAnimation2, b2);
        //    //    //Storyboard.SetTarget(borderAnimation3, b3);
        //    //    storybrdsRight3D.Add(sbBorder);
        //    //}
        //    //#endregion
        //    #endregion
        //}

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
            get { return ViewModelRoute.Create("MyAlbum", "MyAlbum"); }
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
        
        private void btnLeftScroll_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //storybrdsLeft3D.ForEach(delegate(Storyboard sb) { sb.Begin(); });
            var scrollHost = MyAlbumListBox.GetScrollHost();
            if (scrollHost.HorizontalOffset == 0.0)
            {                
                return;
            }
             
            ScrollViewerOffsetMediator mediator = FindVisualChildByName<ScrollViewerOffsetMediator>(MyAlbumListBox, "Mediator");
            
            if (sbleft.Children.Count == 1)
            {
                if (sbleft.GetCurrentState() == ClockState.Active)
                {
                    sbleft.Resume();
                    return;
                }
                else
                {
                    sbleft.Begin();
                    return;
                }                
            }

            var rightDoubleAnimation = new DoubleAnimation
            {
                BeginTime = TimeSpan.FromSeconds(0),
                Duration = TimeSpan.FromSeconds(2),
                From = 1,
                To = 0,
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };
            sbleft.Children.Add(rightDoubleAnimation);
            Storyboard.SetTargetProperty(rightDoubleAnimation, new PropertyPath("ScrollableHeightMultiplier"));
            //Storyboard.SetTargetName(revDoubleAnimation, "Mediator");
            Storyboard.SetTarget(rightDoubleAnimation, mediator);
            sbleft.Begin();            
        }       

        private void BorderPlanProj()
        {
            if (MyAlbumListBox.Items.Count == 0)
                return;
            List<UIElement> items = new List<UIElement>();
            GetChildren(MyAlbumListBox, typeof(BorderItemsControl), ref items);
            if (items.Count == 0) return;            

            for (int i = 0; i < items.Count-1; i++)
            {
                    BorderItemsControl albums = items[i] as BorderItemsControl;

                    DependencyObject container1 = albums.ItemContainerGenerator.ContainerFromIndex(0);
                    Border b1 = (Border)VisualTreeHelper.GetChild(container1, 0);

                    DependencyObject container2 = albums.ItemContainerGenerator.ContainerFromIndex(1);
                    Border b2 = (Border)VisualTreeHelper.GetChild(container2, 0);

                    DependencyObject container3 = albums.ItemContainerGenerator.ContainerFromIndex(2);
                    Border b3 = (Border)VisualTreeHelper.GetChild(container3, 0);
                                        
                    //<Storyboard x:Name="_3dimage">
                    //    <DoubleAnimation Duration="0:0:0.5" To="20" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.RotationY)" Storyboard.TargetName="border1" d:IsOptimized="True"/>
                    //    <DoubleAnimation Duration="0:0:0.5" To="-5" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.GlobalOffsetX)" Storyboard.TargetName="border1" d:IsOptimized="True"/>
                    //    <DoubleAnimation Duration="0:0:0.5" To="20" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.RotationY)" Storyboard.TargetName="border" d:IsOptimized="True"/>
                    //    <DoubleAnimation Duration="0:0:0.5" To="6" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.GlobalOffsetX)" Storyboard.TargetName="border" d:IsOptimized="True"/>
                    //    <DoubleAnimation Duration="0:0:0.5" To="20" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.RotationY)" Storyboard.TargetName="border2" d:IsOptimized="True"/>
                    //    <DoubleAnimation Duration="0:0:0.5" To="-13" Storyboard.TargetProperty="(UIElement.Projection).(PlaneProjection.GlobalOffsetX)" Storyboard.TargetName="border2" d:IsOptimized="True"/>
                    //</Storyboard>

                    Storyboard sbBorder = new Storyboard();

                    var planProjectionAnimation1 = new PlaneProjection
                    {
                        CenterOfRotationX = 0.5,
                        CenterOfRotationY = 0.5,
                        RotationY = 20,
                        GlobalOffsetX=-5                        
                    };

                    var planProjectionAnimation2 = new PlaneProjection
                    {
                        CenterOfRotationX = 0.5,
                        CenterOfRotationY = 0.5,
                        RotationY = 20,
                        GlobalOffsetX=6
                    };

                    var planProjectionAnimation3 = new PlaneProjection
                    {
                        CenterOfRotationX = 0.5,
                        CenterOfRotationY = 0.5,
                        RotationY = 20,
                        GlobalOffsetX=-13                        
                    };

                    b1.Projection = planProjectionAnimation1;
                    b2.Projection = planProjectionAnimation2;
                    b3.Projection = planProjectionAnimation3;

                    DoubleAnimation borderAnimation1 = new DoubleAnimation
                    {
                        BeginTime = TimeSpan.FromMilliseconds(0),
                        Duration = TimeSpan.FromMilliseconds(5000),
                        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
                    };
                    DoubleAnimation borderAnimation2 = new DoubleAnimation
                    {
                        BeginTime = TimeSpan.FromMilliseconds(0),
                        Duration = TimeSpan.FromMilliseconds(5000),
                        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
                    };
                    DoubleAnimation borderAnimation3 = new DoubleAnimation
                    {
                        BeginTime = TimeSpan.FromMilliseconds(0),
                        Duration = TimeSpan.FromMilliseconds(5000),
                        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
                    };

                    Storyboard.SetTargetProperty(borderAnimation1, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
                    Storyboard.SetTargetProperty(borderAnimation1, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));
                    Storyboard.SetTargetProperty(borderAnimation2, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
                    Storyboard.SetTargetProperty(borderAnimation2, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));
                    Storyboard.SetTargetProperty(borderAnimation3, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)"));
                    Storyboard.SetTargetProperty(borderAnimation3, new PropertyPath("(UIElement.Projection).(PlaneProjection.GlobalOffsetX)"));

                    Storyboard.SetTargetName(borderAnimation1, "border");
                    Storyboard.SetTargetName(borderAnimation2, "border");
                    Storyboard.SetTargetName(borderAnimation3, "border");

                    Storyboard.SetTarget(borderAnimation1, b1);
                    Storyboard.SetTarget(borderAnimation2, b2);
                    Storyboard.SetTarget(borderAnimation3, b3);
                    
                    sbBorder.Begin();                
            }
        }

        private void BorderPlanProjRight()
        {
            if (MyAlbumListBox.Items.Count == 0)
                return;
            List<UIElement> items = new List<UIElement>();
            GetChildren(MyAlbumListBox, typeof(BorderItemsControl), ref items);
            if (items.Count == 0) return;

            for (int i = 0; i < items.Count - 1; i++)
            {
                BorderItemsControl albums = items[i] as BorderItemsControl;

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
                    CenterOfRotationY = 0.5,
                    RotationY = -20,
                    GlobalOffsetX = 20
                };

                var planProjectionAnimation2 = new PlaneProjection
                {
                    CenterOfRotationX = 0.5,
                    CenterOfRotationY = 0.5,
                    RotationY = -20,
                    GlobalOffsetX = 28
                };

                var planProjectionAnimation3 = new PlaneProjection
                {
                    CenterOfRotationX = 0.5,
                    CenterOfRotationY = 0.5,
                    RotationY = -20,
                    GlobalOffsetX = 36
                };

                b1.Projection = planProjectionAnimation1;
                b2.Projection = planProjectionAnimation2;
                b3.Projection = planProjectionAnimation3;
                sbBorder.Begin();
            }
        }

        private void BorderPlanProjReverse()
        {
            if (MyAlbumListBox.Items.Count == 0)
                return;
            List<UIElement> items = new List<UIElement>();
            GetChildren(MyAlbumListBox, typeof(BorderItemsControl), ref items);
            if (items.Count == 0) return;

            for (int i = 0; i < items.Count-1; i++)
            {
                BorderItemsControl albums = items[i] as BorderItemsControl;

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
                    CenterOfRotationY = 0.5,
                    RotationY = 0,
                    GlobalOffsetX = 0
                };               

                b1.Projection = planProjectionAnimation1;
                b2.Projection = planProjectionAnimation1;
                b3.Projection = planProjectionAnimation1;
                sbBorder.Begin();
            }
        }

        Storyboard sbleft = new Storyboard();
        Storyboard sbright = new Storyboard();
        private void btnRightScroll_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            storybrdsRight3D.ForEach(delegate(Storyboard sb) { sb.Begin(); });
            var scrollHost = MyAlbumListBox.GetScrollHost();
            if (scrollHost.HorizontalOffset == scrollHost.ScrollableWidth)
            {                
                return;
            }
            ScrollViewerOffsetMediator mediator=FindVisualChildByName<ScrollViewerOffsetMediator>(MyAlbumListBox, "Mediator");
            if (sbright.Children.Count == 1)
            {
                if (sbright.GetCurrentState() == ClockState.Active)
                {
                    sbright.Resume();
                    return;
                }
                else
                {
                    sbright.Begin();
                    return;
                }
            }
            var rightDoubleAnimation = new DoubleAnimation
            {
                BeginTime = TimeSpan.FromSeconds(0),
                Duration = TimeSpan.FromSeconds(2),
                From = 0,
                To = 1,
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn }
            };
            sbright.Children.Add(rightDoubleAnimation);
            Storyboard.SetTargetProperty(rightDoubleAnimation, new PropertyPath("ScrollableHeightMultiplier"));
            //Storyboard.SetTargetName(revDoubleAnimation, "Mediator");
            Storyboard.SetTarget(rightDoubleAnimation, mediator);
            sbright.Begin();
        }
        
        private double GetVerticalOffset(FrameworkElement child, ScrollViewer scrollViewer)
        {
            // Ensure the control is scrolled into view in the ScrollViewer. 
            GeneralTransform focusedVisualTransform = child.TransformToVisual(scrollViewer);
            Point topLeft = focusedVisualTransform.Transform(new Point(child.Margin.Left, child.Margin.Top));
            Rect rectangle = new Rect(topLeft, child.RenderSize);
            double newOffset = scrollViewer.HorizontalOffset + (rectangle.Left - scrollViewer.ViewportWidth);
            return newOffset < 0 ? 0 : newOffset; // no use returning negative offset
        }

        private void btnRightScroll_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            sbright.Pause();
            //BorderPlanProjReverse();
            //storybrdsReverse3D.ForEach(delegate(Storyboard sb) { sb.Begin(); });
        }

        private void btnLeftScroll_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            sbleft.Pause();
            //BorderPlanProjectionReverse();
            //BorderPlanProjReverse();
            //storybrdsReverse3D.ForEach(delegate(Storyboard sb) { sb.Begin(); });
        }

        private void MyAlbumListBox_LayoutUpdated(object sender, EventArgs e)
        {
            if (MyAlbumListBox.Items.Count > 0)
                AnimateAlbums();
        }

        AlbumViewModel model;

        [Import]
        public IViewModelRouter Router { get; set; }

        public void OnImportsSatisfied()
        {
            //FluentRouter.RouteViewModelForView("AlbumViewModel", "Info");
            model = Router.ResolveViewModel<AlbumViewModel>("MyAlbum");
        }
    }
}
