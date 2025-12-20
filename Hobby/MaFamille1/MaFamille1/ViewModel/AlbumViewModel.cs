using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MaFamille1.MaFamilleService;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace MaFamille1
{
    public class AlbumViewModel:INotifyPropertyChanged
    {
        private string _albumName;
        public string AlbumName
        {
            get
            {
                return _albumName;
            }
            set
            {
                _albumName = value;
                NotifyPropertyChanged("AlbumName");
            }
        }
        private AlbumModel _albumModel;
        public AlbumModel AlbumModel
        {
            get
            {
                return _albumModel;
            }
            set
            {
                _albumModel = value;
                NotifyPropertyChanged("AlbumModel");
            }
        }

        private AlbumModel _selectedItem;
        public AlbumModel SelectedItem
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

        private ObservableCollection<AlbumModel> _pictureList;
        public ObservableCollection<AlbumModel> PictureList
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

        public AlbumViewModel()
        {
            ServiceClient client = new ServiceClient();
            client.GetPhotosCompleted += delegate(object sender, GetPhotosCompletedEventArgs e)
            {
                PictureList = new ObservableCollection<AlbumModel>();
                if (e.Result != null)
                {
                    ObservableCollection<MaFamilleService.AlbumModel> lstPics = e.Result as ObservableCollection<MaFamilleService.AlbumModel>;
                    foreach (MaFamilleService.AlbumModel pic in lstPics)
                    {
                        PictureList.Add(new AlbumModel
                        {
                            ImageStream=pic.ImageStream,
                            Click=new DelegateCommand<object>(obj=>{
                                if (obj != null)
                                    SelectedItem = obj as AlbumModel;
                            })
                        });
                    }
                }

            };
            client.GetPhotosAsync();            
        }

        public void GetPhoto(AlbumModel albumModel)
        {
            
        }

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
