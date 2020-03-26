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
using System.ComponentModel;
using System.Collections.ObjectModel;
using MaFamille.Albums.Model;
using Jounce.Core.ViewModel;
using Jounce.Framework.Command;
using Jounce.Core.Event;
using System.ComponentModel.Composition;
using Jounce.Framework;
using Jounce.Framework.View;
using System.Linq;
using System.Windows.Data;
using System.Windows.Threading;
using System.Threading;
using MaFamille.Common;
using MaFamille.Albums.Views;
using MaFamille.Albums.ViewModels;
using System.IO;
using System.Windows.Media.Imaging;
using System.ServiceModel;

namespace MaFamille.Albums.ViewModel
{
    [ExportAsViewModel("MyPhoto")]
    public class PhotoViewModel : BaseViewModel,IEventSink<AlbumModel>,IEventSink<PhotoModel>, IPartImportsSatisfiedNotification
    {
        
        public PhotoModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
            }
        }private PhotoModel _selectedItem;
                
        public ObservableCollection<PhotoModel> PictureList
        {
            get
            {
                return _pictureList;
            }
            set
            {
                _pictureList = value;
                this.Dispatcher.BeginInvoke(delegate()
                {
                    RaisePropertyChanged(() => PictureList);
                });
                
            }
        }private ObservableCollection<PhotoModel> _pictureList;

        public ObservableCollection<PhotoModel> TempPictureList
        {
            get
            {
                return _tempPictureList;
            }
            set
            {
                _tempPictureList = value;
                RaisePropertyChanged(() => TempPictureList);
            }
        }private ObservableCollection<PhotoModel> _tempPictureList;
        
        public ObservableCollection<PhotoModel> LoadedPictureList
        {
            get
            {
                return _loadedPictureList;
            }
            set
            {
                _loadedPictureList = value;
                this.Dispatcher.BeginInvoke(delegate()
                {
                    RaisePropertyChanged(() => LoadedPictureList);
                });                
            }
        }private ObservableCollection<PhotoModel> _loadedPictureList;
                
        public PagedCollectionView FilteredPictureList
        {
            get { return _filteredPictureList; }
            set 
            { 
                _filteredPictureList = value;
                this.Dispatcher.BeginInvoke(delegate()
                {
                    RaisePropertyChanged(() => FilteredPictureList);
                }); 
            }
        }private PagedCollectionView _filteredPictureList;

        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                _isLoaded = value;
                RaisePropertyChanged(() => IsLoaded);
            }
        }private bool _isLoaded;
        
        private Dispatcher Dispatcher;
        ObservableCollection<PhotoModel> Temp;
        public ICommand Click { get; set; }
        private ModalDialogService modalDialogService;

        public PhotoViewModel()
        {
            Dispatcher = Application.Current.RootVisual.Dispatcher;
            PictureList = new ObservableCollection<PhotoModel>();
            TempPictureList = new ObservableCollection<PhotoModel>();
            FilteredPictureList = new PagedCollectionView(TempPictureList);
            modalDialogService = new ModalDialogService();
        }

        public void LoadPhoto(string albumName)
        {
            IsLoaded = false;
            TempPictureList.Clear();
            SelectedItem = null;
            if (PictureList.Any(delegate(PhotoModel photo) { return photo.AlbumName == albumName; }))
            {
                this.Dispatcher.BeginInvoke(delegate()
                {
                    IsLoaded = true;
                    var items = from o in PictureList where o.AlbumName == albumName select o;
                    //TempPictureList = new ObservableCollection<PhotoModel>(items);
                    LoadedPictureList = new ObservableCollection<PhotoModel>(items);
                    AddToTempPictureList(0);
                });
                return;
            }
            MaFamilleService.ServiceClient client = new MaFamilleService.ServiceClient();
            client.GetPhotosCompleted += delegate(object sender, MaFamilleService.GetPhotosCompletedEventArgs e)
            {
                if (e.Error is FaultException<MaFamilleService.CustomException>)
                {
                    MessageBox.Show(e.Error.Message);
                    return;
                }
                if (e.Error == null)
                {
                    ObservableCollection<MaFamilleService.PhotoModel> lstPics = e.Result as ObservableCollection<MaFamilleService.PhotoModel>;
                    foreach (MaFamilleService.PhotoModel pic in lstPics)
                    {
                        PictureList.Add(new PhotoModel
                        {
                            Name = pic.Name,
                            AlbumName = pic.AlbumName,
                            Transform = pic.Transform,
                            ThumbnailImageStream = pic.ThumbnailImageStream,//CreateThumbnailImage(new MemoryStream(pic.ThumbnailImageStream),180),
                            ImageStream=pic.ImageStream,
                            IsLoaded = true,
                            ClickCommand = new ActionCommand<object>(obj =>
                            {
                                if (obj != null)
                                {
                                    DisplaySelectedPhoto(obj as PhotoModel);
                                }
                            })
                        });
                    }
                    IsLoaded = true;
                    //AddToTempPictureList(albumName, 0);
                    var items = from o in PictureList where o.AlbumName == albumName select o;
                    LoadedPictureList = new ObservableCollection<PhotoModel>(items);
                    AddToTempPictureList(0);
                }
            };
            client.GetPhotosAsync(albumName);                        
        }

        private byte[] CreateThumbnailImage(Stream stream, int width)
        {
            BitmapImage bi = new BitmapImage();
            bi.SetSource(stream);
            double cx = width;
            double cy = bi.PixelHeight * (cx / bi.PixelWidth);
            Image image = new Image();
            image.Source = bi;
            WriteableBitmap wb1 = new WriteableBitmap((int)cx, (int)cy);
            ScaleTransform transform = new ScaleTransform();
            transform.ScaleX = cx / bi.PixelWidth;
            transform.ScaleY = cy / bi.PixelHeight;
            wb1.Render(image, transform);
            wb1.Invalidate();
            WriteableBitmap wb2 = new WriteableBitmap((int)cx, (int)cy);
            for (int i = 0; i < wb2.Pixels.Length; i++)
                wb2.Pixels[i] = wb1.Pixels[i];
            wb2.Invalidate();

            int[] p = wb2.Pixels;
            int len = p.Length * 4;
            byte[] result = new byte[len]; // ARGB
            Buffer.BlockCopy(p, 0, result, 0, len);
            return result;

            //Image thumbnail = new Image();
            //thumbnail.Width = cx;
            //thumbnail.Height = cy;
            //thumbnail.Source = wb2;            
            //return thumbnail;
        }

        void AddToTempPictureList(int _index) //(string _albumName,int _index)
        {
            #region
            //var item = _pictureList.First(delegate(PhotoModel photo) { return photo.AlbumName == _albumName && photo.Name == _photoName; });
            //TempPictureList.Add(item);
            //FilteredPictureList.Filter = (o) => (o as PhotoModel).AlbumName == _albumName;
            //FilteredPictureList.Refresh(); 
            
            //var items = from o in PictureList where o.AlbumName == _albumName select o;
            //Temp = new ObservableCollection<PhotoModel>(items);

            //if (_index == Temp.Count)
            //{
            //    return;
            //}
            ////var item = PictureList[_index];            
            //TempPictureList.Add(Temp[_index]);            

            //var timer = new DispatcherTimer();
            //timer.Interval = new TimeSpan(0, 0, 0, 0, 400);
            //timer.Tick += (t, ev) =>
            //{
            //    timer.Stop();
            //    AddToTempPictureList(_albumName, _index + 1);
            //};
            //timer.Start(); 
            #endregion
            //var items = from o in PictureList where o.AlbumName == _albumName select o;            
            //TempPictureList = new ObservableCollection<PhotoModel>(items);

            if (_index == LoadedPictureList.Count)
            {                
                return;
            }

            var item = LoadedPictureList[_index];
            TempPictureList.Add(LoadedPictureList[_index]);

            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 250);
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                AddToTempPictureList(_index + 1);
            };
            timer.Start();
        }

        #region 'Comment
        //void PictureList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    if (e.NewItems == null)
        //        return;
        //    PhotoModel photo = e.NewItems[0] as PhotoModel;
        //    if (photo.Name == null || photo.ThumbnailImageStream != null)
        //        return;
        //    GetThumbnail(photo);
        //    //MaFamilleService.ServiceClient client = new MaFamilleService.ServiceClient();
        //    //client.GetThumbnailPhotoCompleted += delegate(object s, MaFamilleService.GetThumbnailPhotoCompletedEventArgs es)
        //    //{
        //    //    if (es.Error == null)
        //    //    {
        //    //        photo.ThumbnailImageStream = es.Result as byte[];
        //    //    }
        //    //};
        //    //client.GetThumbnailPhotoAsync(photo.AlbumName, photo.Name); 
        //}
        #endregion

        public void GetThumbnail(PhotoModel photo)
        {
            // Events used during background processing
            DoWorkEventHandler workHandler = null;
            RunWorkerCompletedEventHandler doneHandler = null;
            Action<PhotoModel> longRunProgress = null;
            Action<PhotoModel> longRunCompleted = null;

            // Implementation of the BackgroundWorker
            var wrkr = new BackgroundWorker();
            wrkr.DoWork += workHandler =
                delegate(object oDoWrk, DoWorkEventArgs eWrk)
                {
                    // Unwire the workHandler to prevent memory leaks
                    wrkr.DoWork -= workHandler;
                    Photo LongRun = new Photo();
                    LongRun.WorkProgress += longRunProgress =
                        delegate(PhotoModel result)
                        {
                            // Call the method on the UI thread so that we can get 
                            // updates and avoid cross-threading exceptions.
                            Deployment.Current.Dispatcher.BeginInvoke(
                                new Action<PhotoModel>(ReceiveThumbnail), result);                            
                        };
                    LongRun.WorkCompleted += longRunCompleted =
                        delegate(PhotoModel result)
                        {
                            // Unwire all events for this instance 
                            // of the LongRunningObject
                            LongRun.WorkProgress -= longRunProgress;
                            LongRun.WorkCompleted -= longRunCompleted;
                            //currentDispatcher.BeginInvoke(
                            //    new Action<ProcessItem>(AddToken), result);
                        };
                    // Events are wired for the business object, 
                    // this where we start the actual work.
                    LongRun.RunWorkAsync(photo);
                };
            wrkr.RunWorkerCompleted += doneHandler =
                delegate(object oDone, RunWorkerCompletedEventArgs eDone)
                {
                    // Work is complete, decrement the counter
                    // and kill references to teh donHandler.
                    wrkr.RunWorkerCompleted -= doneHandler;                    
                };
            // This is where the actual asynchronous process will 
            // start performing the work that is wired up in the 
            // previous statements.
            wrkr.RunWorkerAsync();
        }

        public void ReceiveThumbnail(PhotoModel photo)
        {
            var item = TempPictureList.First(delegate(PhotoModel p) { return p.AlbumName == photo.AlbumName && p.Name == photo.Name; });
            (item as PhotoModel).ThumbnailImageStream = photo.ThumbnailImageStream;
        }

        public void DisplaySelectedPhoto(PhotoModel photo)
        {
            if (photo.ImageStream != null)
            {
                SelectedItem = photo;
                #region 'Commented
                //EventAggregator.Publish<PhotoModel>(SelectedItem);
                //modalDialogService.ShowDialog(new MyPhotoChild(), new MyPhotoChildViewModel(
                //    previousClick =>
                //    {
                //        MessageBox.Show("Previous Clicked");
                //    },
                //    nextClick =>
                //    {
                //        MessageBox.Show("Next Clicked");
                //    }
                //), r => { });
                #endregion
                return;
            }
            MaFamilleService.ServiceClient client = new MaFamilleService.ServiceClient();
            client.GetPhotoCompleted += delegate(object s, MaFamilleService.GetPhotoCompletedEventArgs es)
            {
                if (es.Error is FaultException<MaFamilleService.CustomException>)
                {
                    MessageBox.Show(es.Error.Message);
                    return;
                }
                if (es.Error == null)
                {
                    photo.ImageStream = es.Result as byte[];                    
                }
            };
            client.GetPhotoAsync(photo.AlbumName, photo.Name);
            SelectedItem = photo;

            #region 'Commented
            //modalDialogService.ShowDialog(new MyPhotoChild(), new MyPhotoChildViewModel(
            //    previousClick => 
            //    {
            //        MessageBox.Show("Previous Clicked");
            //    }, 
            //    nextClick => 
            //    {
            //        MessageBox.Show("Next Clicked");
            //    }
            //), r => { });
            //EventAggregator.Publish("MyPhotoChild".AsViewNavigationArgs());
            //EventAggregator.Publish<PhotoModel>(SelectedItem);
            #endregion
        }

        public void GetUploadedPhoto(PhotoModel uploadedPhoto)
        {
            MaFamilleService.ServiceClient client = new MaFamilleService.ServiceClient();
            client.GetUploadedPhotoCompleted += delegate(object s, MaFamilleService.GetUploadedPhotoCompletedEventArgs es)
            {
                if (es.Error is FaultException<MaFamilleService.CustomException>)
                {
                    MessageBox.Show(es.Error.Message);
                    return;
                }
                if (es.Error == null)
                {
                    MaFamilleService.PhotoModel servicePhoto=es.Result as MaFamilleService.PhotoModel;
                    uploadedPhoto.ThumbnailImageStream = servicePhoto.ThumbnailImageStream;
                    uploadedPhoto.Transform = servicePhoto.Transform;
                    uploadedPhoto.ClickCommand = new ActionCommand<object>(obj =>
                            {
                                if (obj != null)
                                {
                                    DisplaySelectedPhoto(obj as PhotoModel);
                                }
                            });                    
                    PictureList.Insert(PictureList.Count, uploadedPhoto);
                    TempPictureList.Insert(TempPictureList.Count, uploadedPhoto);                    
                }
            };
            client.GetUploadedPhotoAsync(uploadedPhoto.AlbumName, uploadedPhoto.Name);            
        }

        public void HandleEvent(AlbumModel publishedEvent)
        {
            TempPictureList.Clear();
            
            if (publishedEvent.AlbumName != null && publishedEvent.AlbumName != "")
               Deployment.Current.Dispatcher.BeginInvoke(()=> LoadPhoto(publishedEvent.AlbumName));
        }

        public void HandleEvent(PhotoModel publishedPhoto)
        {
            GetUploadedPhoto(publishedPhoto);
        }

        public void OnImportsSatisfied()
        {
            //EventAggregator.SubscribeOnDispatcher(new AlbumModel());
            EventAggregator.Subscribe<AlbumModel>(this);
            EventAggregator.Subscribe<PhotoModel>(this);
        }
    }

    public class Photo
    {
        public event Action<PhotoModel> WorkCompleted;
        public event Action<PhotoModel> WorkProgress;

        public Photo()
        {            
        }

        public void RunWorkAsync(PhotoModel photo)
        {            
            // Throw starting progress event
            MaFamilleService.ServiceClient client = new MaFamilleService.ServiceClient();
            client.GetThumbnailPhotoCompleted += delegate(object s, MaFamilleService.GetThumbnailPhotoCompletedEventArgs es)
            {
                if (es.Error is FaultException<MaFamilleService.CustomException>)
                {
                    MessageBox.Show(es.Error.Message);
                    return;
                }
                if (es.Error == null)
                {
                    WorkProgress(new PhotoModel{ AlbumName=photo.AlbumName,Name=photo.Name, ThumbnailImageStream=es.Result as byte[]});                    
                }
            };
            client.GetThumbnailPhotoAsync(photo.AlbumName, photo.Name);

            // Throw the completed event
            WorkCompleted+=delegate(PhotoModel p){
            };
        }
    }
}
