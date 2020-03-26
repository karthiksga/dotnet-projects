using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaFamilleService.Model
{
    public class PhotoModel
    {
        public string AlbumName { get; set; }
        public string Name { get; set; }
        public byte[] ImageStream { get; set; }  
    }
}