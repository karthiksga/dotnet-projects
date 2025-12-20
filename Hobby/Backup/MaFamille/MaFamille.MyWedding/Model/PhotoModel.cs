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
using Jounce.Core.Command;
using Jounce.Core.ViewModel;

namespace MaFamille.MyWedding.Model
{
    [ExportAsViewModel(typeof(PhotoModel))]
    public class PhotoModel:BaseViewModel
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
                RaisePropertyChanged(()=>AlbumName);
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        private byte[] _imageStream;

        public byte[] ImageStream
        {
            get { return this._imageStream; }
            set
            {
                this._imageStream = value;
                RaisePropertyChanged(() => ImageStream);
            }
        }

        private byte[] _thumbnailImageStream;
        public byte[] ThumbnailImageStream
        {
            get { return this._thumbnailImageStream; }
            set
            {
                this._thumbnailImageStream = value;
                RaisePropertyChanged(() => ThumbnailImageStream);
            }
        }

        public double Transform 
        {
            get { return this._transform; }
            set
            {
                this._transform = value;
                RaisePropertyChanged(() => Transform);
            }
        }private double _transform;

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

        public IActionCommand<object> ClickCommand { get; set; }        
    }
}
