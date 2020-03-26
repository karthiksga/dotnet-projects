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
using MaFamille.MyWedding.Model;
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
using MaFamille.MyWedding.ViewModel;
using System.IO;
using System.Windows.Media.Imaging;
using System.ServiceModel;
using System.Collections.Generic;

namespace MaFamille.MyWedding.ViewModel
{
    [ExportAsViewModel("PhotoViewModel")]
    public class PhotoViewModel : BaseViewModel, IEventSink<AlbumModel>,IPartImportsSatisfiedNotification
    {
        public int picCount=5;
        public string SelectedAlbum
        {
            get
            {
                return _selectedAlbum;
            }
            set
            {
                _selectedAlbum = value;
                RaisePropertyChanged(() => SelectedAlbum);
            }
        }private string _selectedAlbum;
                
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

        public ObservableCollection<PhotoModel> pics { get; set; }
        //public string[] pics { get; set; }

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

        public ObservableCollection<PhotoModel> FilteredPictureList
        {
            get
            {
                return _filteredPictureList;
            }
            set
            {
                _filteredPictureList = value;
                this.Dispatcher.BeginInvoke(delegate()
                {
                    RaisePropertyChanged(() => FilteredPictureList);
                });
            }
        }private ObservableCollection<PhotoModel> _filteredPictureList;

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

        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                _isLoaded = value;
                RaisePropertyChanged(() => IsLoaded);
            }
        }private bool _isLoaded;

        public bool IsSlideShowStarted
        {
            get { return _isSlideShowStarted; }
            set
            {
                _isSlideShowStarted = value;
                RaisePropertyChanged(() => IsSlideShowStarted);
            }
        }private bool _isSlideShowStarted;

        public string PhotosCount
        {
            get { return _photosCount; }
            set
            {
                _photosCount = value;
                RaisePropertyChanged(() => PhotosCount);
            }
        }private string _photosCount;

        public bool IsFetching
        {
            get { return _isFetching; }
            set
            {
                _isFetching = value;
                RaisePropertyChanged(() => IsLoaded);
            }
        }private bool _isFetching;

        private Dispatcher Dispatcher;        
        public ICommand Click { get; set; }
        private ModalDialogService modalDialogService;

        public PhotoViewModel()
        {
            Dispatcher = Application.Current.RootVisual.Dispatcher;
            PictureList = new ObservableCollection<PhotoModel>();
            TempPictureList = new ObservableCollection<PhotoModel>();
            FilteredPictureList = new ObservableCollection<PhotoModel>();
            pics = new ObservableCollection<PhotoModel>();
            modalDialogService = new ModalDialogService();
        }

        //protected override void ActivateView(string viewName, IDictionary<string, object> parameters)
        //{
        //    if (PictureList.Count > 0)
        //        return;
        //    TempPictureList.Clear();
        //    LoadPhoto(viewName);
        //}

        public void LoadPhoto(string albumName)
        {
            IsLoaded = false;
            FilteredPictureList.Clear();            
            //if (PictureList.Any(delegate(PhotoModel photo) { return photo.AlbumName == albumName; }))
            //{
            //    this.Dispatcher.BeginInvoke(delegate()
            //    {
            //        IsLoaded = true;
            //        var items = from o in PictureList where o.AlbumName == albumName select o;
            //        TempPictureList = new ObservableCollection<PhotoModel>(items);                    
            //    });
            //    return;
            //}            
            if (TempPictureList.Any(delegate(PhotoModel photo) { return photo.AlbumName == albumName; }))
            {
                this.Dispatcher.BeginInvoke(delegate()
                {                    
                    FilteredPictureList = new ObservableCollection<PhotoModel>(from o in TempPictureList where o.AlbumName == albumName select o);
                    IsLoaded = true;
                    //TempPictureList = new ObservableCollection<PhotoModel>(items);
                    //GetPhotos(albumName);
                });
                //return;
            }
            else
            {
                MaFamilleService.ServiceClient client = new MaFamilleService.ServiceClient();
                client.GetWeddingPhotosCompleted += delegate(object sender, MaFamilleService.GetWeddingPhotosCompletedEventArgs e)
                {
                    if (e.Error is FaultException<MaFamilleService.CustomException>)
                    {
                        MessageBox.Show(e.Error.Message);
                        return;
                    }
                    if (e.Error == null)
                    {
                        PhotoModel file = null;
                        for (int i = 0; i < e.Result.Count(); i++)
                        {
                            file = new PhotoModel();
                            file.Name = e.Result[i];
                            file.AlbumName = albumName;
                            pics.Add(file);
                        }
                        GetPhotos(albumName);
                    }
                };
                client.GetWeddingPhotosAsync(albumName);
            }            
        }

        public void GetPhotos(string albumName)
        {
            if (PictureList.Count(delegate(PhotoModel p) { return p.AlbumName == albumName; }) < pics.Count(delegate(PhotoModel p) { return p.AlbumName == albumName; }))
            {
                string[] picsArray = (from pic in pics
                                      where pic.AlbumName == albumName
                                      select pic.Name).ToArray();

                MaFamilleService.ServiceClient client = new MaFamilleService.ServiceClient();
                client.GetWeddingPhotosListCompleted += delegate(object sender, MaFamilleService.GetWeddingPhotosListCompletedEventArgs e)
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
                                ImageStream = pic.ImageStream,
                                //ClickCommand = new ActionCommand<object>(obj =>
                                //{
                                //    if (obj != null)
                                //    {
                                //        DisplaySelectedPhoto(obj as PhotoModel);
                                //    }
                                //}),
                                IsLoaded = true
                            });
                        }                        
                        IsFetching = false;
                        PhotosCount = string.Format("{0} of {1} Photos downloaded", PictureList.Count(delegate(PhotoModel p) { return p.AlbumName == albumName; }), pics.Count(delegate(PhotoModel p) { return p.AlbumName == albumName; }));
                        var items = from o in PictureList where o.AlbumName == albumName select o;
                        if (TempPictureList.Count(delegate(PhotoModel p) { return p.AlbumName == albumName; }) == 0)
                        {
                            ObservableCollection<PhotoModel> newCollection = new ObservableCollection<PhotoModel>(items);
                            foreach (PhotoModel photo in newCollection)
                            {
                                TempPictureList.Add(photo);
                            } 
                            //TempPictureList = new ObservableCollection<PhotoModel>(items);
                            PhotoModel loadingPhoto = null;
                            for (int i = 0; i < picsArray.Count() - PictureList.Count(delegate(PhotoModel p) { return p.AlbumName == albumName; }); i++)
                            {
                                loadingPhoto = new PhotoModel();
                                loadingPhoto.AlbumName = albumName;
                                loadingPhoto.ImageStream = AddLoadingImage();
                                loadingPhoto.Name = "default.png";
                                loadingPhoto.IsLoaded = false;
                                //loadingPhoto.ClickCommand = new ActionCommand<object>(obj =>
                                //{
                                //    if (obj != null)
                                //    {
                                //        DisplaySelectedPhoto(obj as PhotoModel);
                                //    }
                                //});
                                TempPictureList.Add(loadingPhoto);
                            }
                            FilteredPictureList = new ObservableCollection<PhotoModel>(from o in TempPictureList where o.AlbumName == albumName select o);
                            IsLoaded = true;
                        }
                        else
                        {
                            foreach (var item in items)
                            {
                                if (!TempPictureList.Any(delegate(PhotoModel p) { return p.AlbumName == albumName && p.Name == (item as PhotoModel).Name; }))
                                {
                                    if (TempPictureList.Any(delegate(PhotoModel p) { return p.AlbumName == albumName && p.Name == "default.png"; }))
                                    {
                                        PhotoModel tempphoto = TempPictureList.Where(p => p.AlbumName == albumName && p.Name == "default.png").First();
                                        PhotoModel photo = item as PhotoModel;
                                        if (photo == null)
                                            return;
                                        tempphoto.ImageStream = photo.ImageStream;
                                        tempphoto.Name = photo.Name;
                                        tempphoto.IsLoaded = true;
                                    }
                                }
                            }
                        }
                    }                                        
                    //if (IsSlideShowStarted)
                    GetPhotos(albumName);
                };
                var tempPics = from pic in PictureList
                               where pic.AlbumName == albumName
                               select pic;
                var totalPics = from pic in pics
                                where pic.AlbumName == albumName
                                select pic;
                int getPhotosCount = (totalPics.Count() - tempPics.Count() >= picCount) ? picCount : totalPics.Count() - tempPics.Count();
                string[] result = new string[getPhotosCount];
                Array.Copy(picsArray, tempPics.Count(), result, 0, getPhotosCount);
                if (result.Count() > 0)
                {
                    client.GetWeddingPhotosListAsync(albumName, new ObservableCollection<string>(result));
                    IsFetching = true;
                }
            }
        }

        private byte[] AddLoadingImage()
        {
            Uri uri = new Uri("/MaFamille.MyWedding;component/Image/LoadingPhoto.png", UriKind.Relative);
            Stream stream = System.Windows.Application.GetResourceStream(uri).Stream;
            return new BinaryReader(stream).ReadBytes((int)stream.Length);            
        }

        public void DisplaySelectedPhoto(PhotoModel photo)
        {
            if (photo.ImageStream != null)
            {
                SelectedItem = photo;
            }
        }

        public void HandleEvent(AlbumModel publishedEvent)
        {
            if (publishedEvent.AlbumName == "")
                return;
            SelectedAlbum = publishedEvent.AlbumName;
            if (publishedEvent.AlbumName != null && publishedEvent.AlbumName != "")
                Deployment.Current.Dispatcher.BeginInvoke(() => LoadPhoto(publishedEvent.AlbumName));
        }

        public void OnImportsSatisfied()
        {
            EventAggregator.Subscribe<AlbumModel>(this);
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
