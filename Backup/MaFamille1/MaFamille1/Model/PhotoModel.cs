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

namespace MaFamille1.Model
{
    public class PhotoModel : INotifyPropertyChanged
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
                PropertyChange("Name");
            }
        }

        private byte[] _imageStream;

        public byte[] ImageStream
        {
            get { return this._imageStream; }
            set
            {
                this._imageStream = value;
                PropertyChange("ImageStream");
            }
        }

        public ICommand Click { get; set; }

        public void PropertyChange(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
