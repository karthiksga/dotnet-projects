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
using MaFamille.Albums.ViewModel;
using MaFamille.Albums.Model;
using System.Collections.ObjectModel;
using MaFamille.Common.Controls;
using System.Windows.Threading;

namespace MaFamille.Albums.Views
{
    [ExportAsView("MyPhoto")]
    public partial class MyPhoto : IPartImportsSatisfiedNotification
    {        
        public MyPhoto()
        {
            InitializeComponent();
            //PhotoList.Items.Clear();
            Loaded += new RoutedEventHandler(MyPhoto_Loaded);
            MouseWheel += new MouseWheelEventHandler(MainPage_MouseWheel);
            flowControl.SelectedItemChanged += new Common.Controls.SelectedItemChangedEvent(flowControl_SelectedItemChanged);
        }

        void flowControl_SelectedItemChanged(Common.Controls.CoverFlowEventArgs e)
        {
            ObservableCollection<PhotoModel> picList = flowControl.ItemsSource as ObservableCollection<PhotoModel>;
            model = Router.ResolveViewModel<PhotoViewModel>("MyPhoto");
            model.DisplaySelectedPhoto(picList[e.Index]);
        }

        Storyboard showFullImage = null;
        Storyboard hideFullImage = null;        
        void MyPhoto_Loaded(object sender, RoutedEventArgs e)
        {   
            showFullImage = (Storyboard)this.FindName("ShowFullImage");
            hideFullImage = (Storyboard)this.FindName("HideFullImage");            
        }
                
        [Export]
        public ViewModelRoute Binding
        {
            get { return ViewModelRoute.Create("MyPhoto", "MyPhoto"); }
        }

        void MainPage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            int i = -(e.Delta / 120);
            int index = flowControl.SelectedIndex + i;
            if (index < 0)
                flowControl.First();
            else if (index > flowControl.Items.Count - 1)
                flowControl.Last();
            else
                flowControl.SelectedIndex = index;
        }

        #region Commented
        //private const int msLoadDelay = 200;
        //private int animationCounter;
        //private TimeSpan loadCounter = new TimeSpan(0, 0, 0, 0, msLoadDelay);        

        //private void StartAnimationFromResource(string animationName, FrameworkElement element)
        //{            
        //    Storyboard sb = element.Resources[animationName] as Storyboard;
        //    Storyboard.SetTarget(sb, element);
        //    sb.BeginTime = loadCounter;
        //    sb.Begin();

        //    loadCounter = loadCounter.Add(new TimeSpan(0, 0, 0, 0, msLoadDelay));
        //    animationCounter++;
        //    if (animationCounter >= PhotoList.Items.Count)
        //    {
        //        loadCounter = new TimeSpan(0, 0, 0, 0, msLoadDelay);
        //        animationCounter = 0;
        //    }
        //}

        //private void PhotoList_Loaded(object sender, System.Windows.RoutedEventArgs e)
        //{            
        //    this.StartAnimationFromResource("LoadAnimation", sender as FrameworkElement);

        //    // Do not unwire this event if you plan on filtering/reloading the ListBoxItems
        //    ((FrameworkElement)sender).Loaded -= this.PhotoList_Loaded;
        //}
        #endregion

        [Import]
        public IViewModelRouter Router { get; set; }

        PhotoViewModel model;
        public void OnImportsSatisfied()
        {
            //model = Router.ResolveViewModel<PhotoViewModel>("MyPhoto");    
        }

        private void btnGoRight_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            EventHandler handler = null;
            handler = (s, es) =>
            {
                hideFullImage.Completed -= handler;

                int index = flowControl.SelectedIndex + 1;
                if (index < 0)
                    flowControl.First();
                else if (index > flowControl.Items.Count - 1)
                    flowControl.Last();
                else
                    flowControl.SelectedIndex = index;
                                
                CoverFlowItemControl item = flowControl.items[flowControl.SelectedIndex];
                CoverFlowItemControl.AnimationCompletedEvent animHandler = null;
                animHandler =() =>
                    {
                        ObservableCollection<PhotoModel> picList = flowControl.ItemsSource as ObservableCollection<PhotoModel>;
                        model = Router.ResolveViewModel<PhotoViewModel>("MyPhoto");
                        model.DisplaySelectedPhoto(picList[flowControl.SelectedIndex]);

                        if (showFullImage != null)
                            showFullImage.Begin();
                        item.AnimationCompleted -= animHandler;
                    };
                item.AnimationCompleted += animHandler;
                //item.AnimationCompleted += new CoverFlowItemControl.AnimationCompletedEvent(item_AnimationCompleted);
                //item.AnimationCompleted -= item_AnimationCompleted;
            };
            hideFullImage.Begin();
            hideFullImage.Completed += handler;
        }       

        private void btnGoLeft_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            EventHandler handler = null;
            handler = (s, es) =>
            {
                hideFullImage.Completed -= handler;
                int index = flowControl.SelectedIndex - 1;
                if (index < 0)
                    flowControl.First();
                else if (index > flowControl.Items.Count - 1)
                    flowControl.Last();
                else
                    flowControl.SelectedIndex = index;
                
                CoverFlowItemControl item = flowControl.items[flowControl.SelectedIndex];
                CoverFlowItemControl.AnimationCompletedEvent animHandler = null;
                animHandler = () =>
                {
                    ObservableCollection<PhotoModel> picList = flowControl.ItemsSource as ObservableCollection<PhotoModel>;
                    model = Router.ResolveViewModel<PhotoViewModel>("MyPhoto");
                    model.DisplaySelectedPhoto(picList[flowControl.SelectedIndex]);

                    if (showFullImage != null)
                        showFullImage.Begin();
                    item.AnimationCompleted -= animHandler;
                };
                item.AnimationCompleted += animHandler;
                //item.AnimationCompleted += new CoverFlowItemControl.AnimationCompletedEvent(item_AnimationCompleted);
                //item.AnimationCompleted -= item_AnimationCompleted;
            };
            hideFullImage.Begin();
            hideFullImage.Completed += handler;            
        }
        
        //void item_AnimationCompleted()
        //{
        //    ObservableCollection<PhotoModel> picList = flowControl.ItemsSource as ObservableCollection<PhotoModel>;
        //    model = Router.ResolveViewModel<PhotoViewModel>("MyPhoto");
        //    model.DisplaySelectedPhoto(picList[flowControl.SelectedIndex]);

        //    if (showFullImage != null)
        //        showFullImage.Begin();
        //}

        private void btnLeftScroll_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            isContinueScroll = true;
            StartScrolling(false);
        }

        private void btnLeftScroll_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            isContinueScroll = false;
        }

        private void btnRightScroll_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            isContinueScroll = true;
            StartScrolling(true);
        }

        private void btnRightScroll_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            isContinueScroll = false;
        }

        bool isContinueScroll = true;
        void StartScrolling(bool isRight)
        {
            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                ScrollPhotos(isRight);
                if (isContinueScroll)
                    StartScrolling(isRight);
            };
            timer.Start();
        }
        void ScrollPhotos(bool isRight)
        {
            //int i = -(e.Delta / 120);
            int index = 0;
            if (isRight)
                index = flowControl.SelectedIndex + 1;
            else
                index = flowControl.SelectedIndex - 1;

            if (index < 0)
            {
                flowControl.First();
            }
            else if (index > flowControl.Items.Count - 1)
            {
                flowControl.Last();
            }
            else
            {
                flowControl.SelectedIndex = index;
                //LoadPhotos(index);
            }
        }
    }
}
