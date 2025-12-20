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
using MaFamille.Modules.Core;
using MaFamille.Modules.Core.MaFamilleService;

namespace MaFamille.Modules.Core.ViewModel
{    
    public class EditAlbumViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<AlbumModel> _albumList;
        public ObservableCollection<AlbumModel> AlbumList
        {
            get
            {
                return _albumList;
            }
            set
            {
                _albumList = value;
                NotifyPropertyChanged("PictureList");
            }
        }

        public EditAlbumViewModel()
        {            
            ServiceClient client = new ServiceClient();
            client.GetAlbumsCompleted += delegate(object sender, GetAlbumsCompletedEventArgs e)
            {
                AlbumList = new ObservableCollection<AlbumModel>();
                if (e.Result != null)
                {
                    ObservableCollection<MaFamilleService.AlbumModel> lstPics = e.Result as ObservableCollection<MaFamilleService.AlbumModel>;
                    foreach (MaFamilleService.AlbumModel pic in lstPics)
                    {
                        AlbumList.Add(new AlbumModel
                        {                           
                             AlbumName=pic.AlbumName
                        });
                    }
                }
            };
            client.GetAlbumsAsync();
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
