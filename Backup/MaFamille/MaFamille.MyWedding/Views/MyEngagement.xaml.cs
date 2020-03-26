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
using Jounce.Core.ViewModel;
using System.ComponentModel.Composition;
using System.Windows.Media.Imaging;
using MaFamille.MyWedding.Model;
using System.IO;
using System.Windows.Threading;
//using TransitionEffects;
using System.Collections.ObjectModel;
using MaFamille.MyWedding.ViewModel;
using System.Linq;
using MaFamille.Common.Controls;

namespace MaFamille.MyWedding.Views
{
    [ExportAsView("MyEngagement", Category = "Wedding", MenuName = "MyEngagement", ToolTip = "My Engagement")]
	public partial class MyEngagement
	{
        Storyboard FadeOutAnim;
        //Storyboard FadeInAnim;
        Storyboard FadeOutAnim2;
        //Storyboard FadeInAnim2;
        Storyboard WaitAnim;
        Storyboard WaitAnim2;


		public MyEngagement()
		{
			// Required to initialize variables
			InitializeComponent();
            MouseWheel += new MouseWheelEventHandler(MainPage_MouseWheel);
            Loaded += new RoutedEventHandler(MyEngagement_Loaded);
            flowControl.SelectedItemChanged += new Common.Controls.SelectedItemChangedEvent(flowControl_SelectedItemChanged);
            //flowControl.Loaded += new RoutedEventHandler(flowControl_Loaded);            
            flowControl.ItemsLoaded += new Common.Controls.ItemsLoadedEvent(flowControl_ItemsLoaded);

            FadeOutAnim = (Storyboard)this.FindName("FadeOutAnimation");
            FadeOutAnim2 = (Storyboard)this.FindName("FadeOutAnimation2");
            
            WaitAnim = (Storyboard)this.FindName("ZoomSlideShow");
            WaitAnim.Completed += new EventHandler(WaitAnim_Completed);
            
            WaitAnim2 = (Storyboard)this.FindName("ZoomSlideShow2");
            WaitAnim2.Completed+=new EventHandler(WaitAnim2_Completed);
		}

        Storyboard showFullImage = null;
        Storyboard hideFullImage = null;        
        void MyEngagement_Loaded(object sender, RoutedEventArgs e)
        {
            showFullImage = (Storyboard)this.FindName("ShowFullImage");
            hideFullImage = (Storyboard)this.FindName("HideFullImage"); 
        }

        void flowControl_ItemsLoaded()
        {
            btnSlideShow.IsEnabled = true;
            model = Router.ResolveViewModel<PhotoViewModel>("PhotoViewModel");            
        }

        void MainPage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            int i = -(e.Delta / 120);
            int index = flowControl.SelectedIndex + i;
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

        public ObservableCollection<PhotoModel> picList
        {
            get { return flowControl.ItemsSource as ObservableCollection<PhotoModel>; }
        }

        int loadedPicsCount = 0;
        void flowControl_SelectedItemChanged(Common.Controls.CoverFlowEventArgs e)
        {
            //LoadPhotos(e.Index);
            //CoverFlowItemControl item = flowControl.items[flowControl.SelectedIndex];
            //CoverFlowItemControl.AnimationCompletedEvent animHandler = null;
            //animHandler = () =>
            //{
            //    //ObservableCollection<PhotoModel> picList = flowControl.ItemsSource as ObservableCollection<PhotoModel>;
            //    model.DisplaySelectedPhoto(picList[flowControl.SelectedIndex]);

            //    if (showFullImage != null)
            //        showFullImage.Begin();
            //    item.AnimationCompleted -= animHandler;
            //};
            //item.AnimationCompleted += animHandler;            
            model.DisplaySelectedPhoto(picList[flowControl.SelectedIndex]);
        }

        void LoadPhotos(int i)
        {
            ObservableCollection<PhotoModel> picList = flowControl.ItemsSource as ObservableCollection<PhotoModel>;
            
            loadedPicsCount = picList.Count(delegate(PhotoModel p) { return p.IsLoaded == true; });
            if (loadedPicsCount < i + 1 && !model.IsFetching)
            {
                model.GetPhotos(model.SelectedAlbum);
            }
        }

        [Export]
        public ViewModelRoute Binding
        {
            get { return ViewModelRoute.Create("PhotoViewModel", "MyEngagement"); }
        }

        [Import]
        public IViewModelRouter Router { get; set; }

        PhotoViewModel model;

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!Application.Current.Host.Content.IsFullScreen)
            {
                Application.Current.Host.Content.IsFullScreen = true;                
            }
            else
            {
                Application.Current.Host.Content.IsFullScreen = false;                
            }                        
        }        
                
        private void WaitAnim_Completed(object sender, EventArgs e)
        {   
            SetCurrentImage();            
            FadeOutAnim.Begin();   
            WaitAnim2.Begin();
        }

        private void WaitAnim2_Completed(object sender, EventArgs e)
        {                    
            SetCurrentImage();            
            FadeOutAnim2.Begin();
            WaitAnim.Begin();
        }
        bool isEvenImage=true;
        private void SetCurrentImage()
        {
            if (model.IsSlideShowStarted)
            {
                CurrentJpg();
            }
            PhotoModel photo=flowControl.items[currenti].Content as PhotoModel;
            MemoryStream stream1 = new MemoryStream(photo.ImageStream);
            BitmapImage b1 = new BitmapImage();
            b1.SetSource(stream1);
            if (isEvenImage)
            {
                imgFullScreen.Source = b1;
                isEvenImage = false;
            }
            else
            {                
                imgFullScreen2.Source = b1;
                isEvenImage = true;
            }

            //if (model.PictureList.Count() == model.TempPictureList.Count())
            //{
            //    if(model.PictureList.Count()==currenti)
            //    return;
            //}

            //if (flowControl.items.Count > model.picCount)
            //{
            //    if (currenti % model.picCount == 0)
            //    {
            //        int nextIndex = currenti + model.picCount;
            //        if (flowControl.items.Count > nextIndex)
            //        {
            //            PhotoModel nextPhoto = flowControl.items[nextIndex].Content as PhotoModel;
            //            if(!nextPhoto.IsLoaded)
            //                model.GetPhotos(model.SelectedAlbum);
            //        }
            //    }
            //}

            //if (model.IsSlideShowStarted)
            //{                
            //    model.GetPhotos(model.SelectedAlbum);                
            //}
            
        }

        int currenti = 0;        

        private void CurrentJpg()
        {
            currenti++;
            if (currenti == flowControl.items.Count)
            {
                currenti = 0;                
            }            
        }        

        //bool isSlideShowStarted;
        private void btnSlideShow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!model.IsSlideShowStarted)
            {
                model.IsSlideShowStarted = true;
                Storyboard StartSlideShowAnim = (Storyboard)this.FindName("StartSlideShow");
                StartSlideShow.Begin();                
                SetCurrentImage();                
                WaitAnim.Begin();
                //isSlideShowStarted = true;  
                //model.GetPhotos(model.SelectedAlbum);
            }
            else
            {                
                WaitAnim.Stop();
                FadeOutAnim.Stop();
                WaitAnim2.Stop();                
                FadeOutAnim2.Stop();
                Storyboard StopSlideShowAnim = (Storyboard)this.FindName("StopSlideShow");
                StopSlideShowAnim.Begin();
                //isSlideShowStarted = false;
                model.IsSlideShowStarted = false;
            }            
        }

        private void imgFullScreen_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //SetCurrentImage();
        }

        private void imgFullScreen2_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //SetCurrentImage();
        }

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

        bool isContinueScroll=true;
        void StartScrolling(bool isRight)
        {
            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                ScrollPhotos(isRight);
                if(isContinueScroll)
                    StartScrolling(isRight);
            };
            timer.Start();
        }
        void ScrollPhotos(bool isRight)
        {
            //int i = -(e.Delta / 120);
            int index=0;
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