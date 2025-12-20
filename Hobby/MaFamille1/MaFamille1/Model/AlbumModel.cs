using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;

namespace MaFamille1
{
    public class AlbumModel:INotifyPropertyChanged
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
                PropertyChange("AlbumName");
            }
        }

        private byte[] _imageStream;

        public byte[] ImageStream
        {
            get { return this._imageStream; }
            set {
                this._imageStream = value;
                PropertyChange("ImageStream");
                }
        }

        public ICommand Click { get; set; }

        public void PropertyChange(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(property));
            }
        }
    
        public event PropertyChangedEventHandler  PropertyChanged;
}
}
