using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaFamilleService.Model
{
    public class TsunamiModel
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

        public decimal CanvasTop { get; set; }
        public decimal CanvasLeft { get; set; }
        public decimal Rotation { get; set; }

    }
}
