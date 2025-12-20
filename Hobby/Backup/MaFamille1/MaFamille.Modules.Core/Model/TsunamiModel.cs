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

namespace MaFamille.Modules.Core.Model
{
    public class TsunamiModel : INotifyPropertyChanged
    {
        private byte[] _imageStream;

        public byte[] ImageStream
        {
            get { return this._imageStream; }
            set
            {
                this._imageStream = value;                
            }
        }
        
        public decimal CanvasTop
        {
            get { return this._canvasTop; }
            set
            {
                this._canvasTop = value;
                PropertyChange("CanvasTop");
            } 
        } private decimal _canvasTop;

        public decimal CanvasLeft 
        { 
            get{ return this._canvasLeft;} 
            set
            {
                this._canvasLeft=value;
                PropertyChange("CanvasLeft");
            }
        } private decimal _canvasLeft;

        public decimal Rotation 
        {
            get { return this._rotation; }
            set
            {
                this._rotation = value;
                PropertyChange("Rotation");
            }  
        } private decimal _rotation;

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
