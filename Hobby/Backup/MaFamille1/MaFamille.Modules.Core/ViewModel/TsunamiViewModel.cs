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
//using MaFamille1.MaFamilleService;

namespace MaFamille.Modules.Core.ViewModel
{
    public class TsunamiViewModel : INotifyPropertyChanged
    {
        private TsunamiModel _tsunamiModel;
        public TsunamiModel TsunamiModel
        {
            get
            {
                return _tsunamiModel;
            }
            set
            {
                _tsunamiModel = value;
                NotifyPropertyChanged("TsunamiModel");
            }
        }

        private ObservableCollection<TsunamiModel> _pictureList;
        public ObservableCollection<TsunamiModel> PictureList
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

        public TsunamiViewModel()
        {
            //MaFamilleService.ServiceClient client = new MaFamilleService.ServiceClient();            
            //client.GetTsunamiPhotosCompleted += delegate(object sender, MaFamilleService.GetTsunamiPhotosCompletedEventArgs e)
            //{
            //    PictureList = new ObservableCollection<TsunamiModel>();
            //    if (e.Result != null)
            //    {
            //        ObservableCollection<MaFamilleService.TsunamiModel> lstPics = e.Result as ObservableCollection<MaFamilleService.TsunamiModel>;
            //        foreach (MaFamilleService.TsunamiModel pic in lstPics)
            //        {
            //            PictureList.Add(new TsunamiModel
            //            {
            //                ImageStream = pic.ImageStream,
            //                CanvasLeft=pic.CanvasLeft,
            //                CanvasTop=pic.CanvasTop,
            //                Rotation=pic.Rotation
            //            });
            //        }
            //    }
            //};
            //client.GetTsunamiPhotosAsync();
        }        
    }
}
