using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using MaFamille1.MaFamilleService;
using Microsoft.Practices.Composite.Presentation.Commands;
using System.IO;
using System.ServiceModel;
namespace MaFamille1
{
    public class ImageUploadViewModel : INotifyPropertyChanged
	{
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public ICommand CommandClick { get; set; }
        public ICommand Click { get; set; }

        public ImageUploadViewModel()
        {
            //AlbumModel = new AlbumModel
            //{
            //    AlbumName="Image not selected",
            //    ImageStream=null
            //};
            PictureList = new ObservableCollection<AlbumModel>();
            MaFamilleService.ServiceClient client = new MaFamilleService.ServiceClient();
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
                            ImageStream = pic.ImageStream,
                            Click = new DelegateCommand<object>(obj =>
                            {
                                if (obj != null)
                                    SelectedItem = obj as AlbumModel;
                            })
                        });
                    }
                }

            };
            client.GetPhotosAsync();
            CommandClick = new DelegateCommand<object>(argument =>
            {
                switch (argument.ToString())
                {
                    case "Browse":
                        BrowseFile();
                        break;
                    case "Upload":
                        UploadFile();
                        break;
                    default:
                        break;
                }
            });
        }

        private void BrowseFile()
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog { Filter = "JPEG Files|*.jpg" };
                if (fileDialog.ShowDialog() == true)
                {
                    byte[] bytes = null;
                    using (Stream stream = (Stream)fileDialog.File.OpenRead())
                    {
                        bytes = new byte[stream.Length];
                        stream.Read(bytes, 0, (int)stream.Length);
                        //AlbumModel.ImageStream = bytes;            
                    }
                    MaFamilleService.ServiceClient client = new MaFamilleService.ServiceClient();
                    client.UploadPhotoCompleted += delegate(object sender, AsyncCompletedEventArgs e)
                    {
                        PictureList.Add(new AlbumModel { ImageStream = bytes });
                    };
                    //List<MaFamilleService.AlbumModel> picList = new List<MaFamilleService.AlbumModel>();
                    //foreach (AlbumModel pic in PictureList)
                    //{
                    //    picList.Add(new MaFamilleService.AlbumModel { ImageStream=pic.ImageStream});
                    //}
                    client.UploadPhotoAsync(new MaFamilleService.AlbumModel { ImageStream = bytes, AlbumName = DateTime.Now.ToString("dd-mm-yy-hh-mm-ss") });
                }
            }
            catch (FaultException<MaFamilleService.CustomException> ex)
            {
                MessageBox.Show(ex.Detail.Message);
            }            
        }        

        private void UploadFile()
        {
        }
	}
}