using System;
using System.Collections.Generic;
using System.Linq;

namespace MaFamille.CommonModel
{
    public class AlbumModel
    {
        public string AlbumName { get; set; }
        public string OldAlbumName { get; set; }
        public List<AlbumImage> AlbumImage { get; set; }
        public bool IsAlbumExists { get; set; }
    }

    public class AlbumImage
    {        
        public byte[] ImageStream { get; set; }        
        public double Transform { get; set; }        
    }
}