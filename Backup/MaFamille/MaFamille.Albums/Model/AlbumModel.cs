using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections;
using Jounce.Core.Command;
using Jounce.Core.ViewModel;
using System.Collections.ObjectModel;

namespace MaFamille.Albums.Model
{
    public class AlbumModel : BaseViewModel
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
                RaisePropertyChanged(() => AlbumName);
                //PropertyChange("AlbumName");
                if (AlbumNameChanged != null)
                    AlbumNameChanged(this, null);
            }
        }
        public event EventHandler AlbumNameChanged;        

        private string _oldAlbumName;
        public string OldAlbumName
        {
            get
            {
                return _oldAlbumName;
            }
            set
            {
                _oldAlbumName = value;
            }
        }

        private ObservableCollection<AlbumImage> _albumImage;
        public ObservableCollection<AlbumImage> AlbumImage
        {
            get { return this._albumImage; }
            set
            {
                this._albumImage = value;
                //PropertyChange("AlbumImage");
                RaisePropertyChanged(() => AlbumImage);
            }
        }

        public bool IsLoaded
        {
            get { return this._isLoaded; }
            set
            {
                this._isLoaded = value;
                //PropertyChange("IsLoaded");
                RaisePropertyChanged(() => IsLoaded);
            }
        }private bool _isLoaded;

        public string ClickParameter
        {
            get { return "MyPhoto"; }         
        }

        public IActionCommand<object> ClickCommand { get; set; }
        public IActionCommand<object> EditCommand { get; set; }
        public IActionCommand<object> DeleteCommand { get; set; }

        //public void PropertyChange(string property)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(property));
        //    }
        //}        

        public AlbumModel()
        {            
            
        }

        //public event PropertyChangedEventHandler PropertyChanged;
    }

    public class AlbumImage:BaseViewModel
    {
        private byte[] _imageStream;
        public byte[] ImageStream
        {
            get { return this._imageStream; }
            set
            {
                this._imageStream = value;
                //PropertyChange("ImageStream");
                RaisePropertyChanged(() => ImageStream);
            }
        }

        private double _transform;
        public double Transform
        {
            get { return _transform; }
            set { 
                    _transform = value; 
                    //PropertyChange("Transform"); 
                    RaisePropertyChanged(() => Transform);
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //public void PropertyChange(string property)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(property));
        //    }
        //}   
    }
}
