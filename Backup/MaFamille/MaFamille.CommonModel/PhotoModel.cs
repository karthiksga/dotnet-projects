using System;
using System.Collections.Generic;
using System.Linq;


namespace MaFamille.CommonModel
{
    public class PhotoModel
    {
        public string AlbumName { get; set; }
        public string Name { get; set; }
        public byte[] ImageStream { get; set; }
        public double Transform { get; set; }
        public byte[] ThumbnailImageStream { get; set; }
    }
}