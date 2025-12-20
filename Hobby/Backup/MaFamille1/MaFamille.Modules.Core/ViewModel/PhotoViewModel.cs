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
using MaFamille.Modules.Core.Model;
using System.Collections.ObjectModel;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace MaFamille.Modules.Core.ViewModel
{
    public class PhotoViewModel : INotifyPropertyChanged
    {
       private PhotoModel _selectedItem;
        public PhotoModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged("SelectedItem");
            }
        }

        private ObservableCollection<PhotoModel> _pictureList;
        public ObservableCollection<PhotoModel> PictureList
        {
            get
            {
                return _pictureList;
            }
            set
            {
                _pictureList = value;
                NotifyPropertyChanged("PictureList");
            }
        }

        public ICommand Click { get; set; }

        public PhotoViewModel(string albumName)
        {
            MaFamilleService.ServiceClient client = new MaFamilleService.ServiceClient();
            PictureList = new ObservableCollection<PhotoModel>();
            PictureList.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(PictureList_CollectionChanged);
            client.GetPhotosCompleted += delegate(object sender, MaFamilleService.GetPhotosCompletedEventArgs e)
            {                
                if (e.Error == null)
                {
                    ObservableCollection<MaFamilleService.PhotoModel> lstPics = e.Result as ObservableCollection<MaFamilleService.PhotoModel>;
                    foreach (MaFamilleService.PhotoModel pic in lstPics)
                    {
                        PictureList.Add(new PhotoModel
                        {
                            Name=pic.Name,                            
                            ImageStream = pic.ImageStream,
                            AlbumName=pic.AlbumName,
                            Click = new DelegateCommand<object>(obj =>
                            {
                                if (obj != null)
                                    SelectedItem = obj as PhotoModel;
                            })
                        });                        
                    }
                    PictureList.Add(new PhotoModel { AlbumName = albumName, Click = new DelegateCommand<object>(obj => { UploadPhoto(obj as PhotoModel); }) });
                }
            };
            client.GetPhotosAsync(albumName);            
        }

        void PictureList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {            
            PhotoModel photo = e.NewItems[0] as PhotoModel;
            if (photo.Name == null)
                return;
            MaFamilleService.ServiceClient client = new MaFamilleService.ServiceClient();
            client.GetPhotoCompleted += delegate(object s, MaFamilleService.GetPhotoCompletedEventArgs es)
            {
                if (es.Error == null)
                {
                    photo.ImageStream = es.Result as byte[];
                }
            };
            client.GetPhotoAsync(photo.AlbumName,photo.Name);
        }

        public void UploadPhoto(PhotoModel photo)
        {         
            //App.uploadVM.AlbumName = photo.AlbumName;
            //App.uploadVM.IsUploadEnabled = true;         
            //App.uploadWindow.Show();
        }

        //void child_SubmitClicked(object sender, EventArgs e)
        //{
        //    SelectedItem.AlbumName = (sender as MyAlbumChild).AlbumName;
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
