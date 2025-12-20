using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Web;
using System.Security.Principal;
using System.ServiceModel.Configuration;
using System.ServiceModel.Activation;
using MaFamilleService.Model;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Serialization.Formatters.Binary;

namespace MaFamilleService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.    
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service : IService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }       

        public List<PhotoModel> GetPhotos(string album)
        {
            List<PhotoModel> photos = new List<PhotoModel>();
            var files = Directory.GetFiles(Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"\Images", album)));
            PhotoModel photo = null;
            foreach (var file in files)
            {
                photo = new PhotoModel();
                //FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                //BinaryReader reader = new BinaryReader(stream);
                //photo.ImageStream= reader.ReadBytes((int)stream.Length);
                //photo.Name = Path.GetFileName(file).Substring(0, Path.GetFileName(file).IndexOf('.'));
                photo.Name = Path.GetFileName(file);
                photo.AlbumName = album;
                photos.Add(photo);
            }
            return photos;
        }

        public List<AlbumModel> GetAlbums()
        {
            List<AlbumModel> albums = new List<AlbumModel>();
            foreach (DirectoryInfo folder in new DirectoryInfo(Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Images"))).GetDirectories())
	        {
                AlbumModel album = new AlbumModel();
                album.AlbumName = folder.Name;
                albums.Add(album);
	        }
            return albums;
        }

        public List<TsunamiModel> GetTsunamiPhotos()
        {
            List<TsunamiModel> photos = new List<TsunamiModel>();            
            //var files = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Thumbnails"));
            var files = Directory.GetFiles("C:\\inetpub\\wwwroot\\MaFamille1\\WcfService1\\Thumbnails");
            //var files = Directory.GetFiles("C:\\inetpub\\wwwroot\\MaFamille1\\WcfService1\\Images");
            TsunamiModel photo = null;
            foreach (var file in files)
            {
                photo = new TsunamiModel();
                FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(stream);
                photo.ImageStream = reader.ReadBytes((int)stream.Length);
                photos.Add(photo);

                //Image fullSizeImg = Image.FromStream(stream);
                //Image.GetThumbnailImageAbort dummyCallBack = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                //Image thumbNailImg = fullSizeImg.GetThumbnailImage(fullSizeImg.Height, fullSizeImg.Height, dummyCallBack, IntPtr.Zero);
                                
                //MemoryStream ms = new MemoryStream();                
                //BinaryFormatter formatter = new BinaryFormatter();
                //formatter.Serialize(ms, thumbNailImg);
                //photo.ImageStream = ms.ToArray();
                //photos.Add(photo);
            }
            //photos[0].CanvasLeft = 239M;
            //photos[0].CanvasTop = 150M;
            //photos[0].Rotation = 37.523M;

            //photos[1].CanvasLeft = 427M;
            //photos[1].CanvasTop = 116M;
            //photos[1].Rotation = -16.515M;

            return photos;
        }

        public byte[] GetPhoto(string albumName, string fileName)
        {
            //var files = Directory.GetFiles("C:\\inetpub\\wwwroot\\MaFamille1\\WcfService1\\Images");
            //var files = Directory.GetFiles(Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"\Images", albumName)));
            //FileStream stream = new FileStream(files[new Random().Next(files.Length)], FileMode.Open, FileAccess.Read);
            FileStream stream = new FileStream(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"Images\" + albumName) + @"\" + fileName, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);
            return reader.ReadBytes((int)stream.Length);
        }

        public AlbumModel GetPhotoObject(string fileName)
        {
            //Image fullSizeImg = Image.FromFile("C:\\inetpub\\wwwroot\\MaFamille1\\WcfService1\\Images" + "\\" + fileName + ".jpg");
            FileStream fs = new FileStream("C:\\inetpub\\wwwroot\\MaFamille1\\WcfService1\\Images" + "\\" + fileName + ".jpg", FileMode.Open, FileAccess.Read);
            return new AlbumModel { ImageStream = new BinaryReader(fs).ReadBytes((int)fs.Length), AlbumName = fileName };            
        }

        //public void UploadPhoto(AlbumModel picture)
        //{
        //    try
        //    {
        //        var fs = new BinaryWriter(new FileStream("C:\\inetpub\\wwwroot\\MaFamille1\\WcfService1\\Images" + "\\" + picture.AlbumName + ".jpg", FileMode.Create));
        //        fs.Write(picture.ImageStream);
        //        fs.Close();

        //        //You may even specify a standard thumbnail size
        //        //int imageWidth = 188;
        //        //int imageHeight = 132;                
        //        Image fullSizeImg = Image.FromFile("C:\\inetpub\\wwwroot\\MaFamille1\\WcfService1\\Images" + "\\" + picture.AlbumName + ".jpg");
        //        Image.GetThumbnailImageAbort dummyCallBack = new Image.GetThumbnailImageAbort(ThumbnailCallback);
        //        Image thumbNailImg  = fullSizeImg.GetThumbnailImage(fullSizeImg.Height, fullSizeImg.Height,dummyCallBack, IntPtr.Zero);

        //        //Save the thumbnail in PNG format. 
        //        //You may change it to a diff format with the ImageFormat property
        //        thumbNailImg.Save("C:\\inetpub\\wwwroot\\MaFamille1\\WcfService1\\Thumbnails" + "\\" + picture.AlbumName+".png", ImageFormat.Png);
        //        thumbNailImg.Dispose();

        //    }
        //    catch (FaultException<CustomException> ex)
        //    {                
        //        throw ex;
        //    }            
        //}

        public bool ThumbnailCallback()
        {
            return false;
        }

        public FileUploadModel UploadPhoto(string albumName, string fileName, byte[] content,int id, bool append)
        {
            try
            {
                //string folder = Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Images"));
                string folder = Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"\Images", albumName));
                if (!System.IO.Directory.Exists(folder))
                    System.IO.Directory.CreateDirectory(folder);

                FileMode m = FileMode.Create;
                if (append)
                    m = FileMode.Append;

                using (FileStream fs = new FileStream(folder + @"\" + fileName, m, FileAccess.Write))
                {
                    fs.Write(content, 0, content.Length);
                }

                FileUploadModel file = new FileUploadModel { Name = fileName, ID = id };
                return file;
            }            
            catch (FaultException<CustomException> ex)
            {
                throw ex;
            }            
        }

        public int RemovePhoto(string fileName,int id)
        {
            try
            {
                string folder = Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Images"));
                File.Delete(folder+ @"\" + fileName);
                return id;
            }
            catch (FaultException<CustomException> ex)
            {
                throw ex;
            }
        }

        public bool IsFileExists(string albumName,string fileName, long length)
        {
            if (File.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, albumName) + @"\" + fileName))
            {
                using (FileStream fs = new FileStream(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, albumName) + @"\" + fileName, FileMode.Open, FileAccess.Read))
                {
                    return fs.Length == length;
                }
            }
            else
            {
                return false;
            }            
        }

        public bool IsAlbumExists(string albumName,string oldAlbumName)
        {

            if (Directory.Exists(Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"\Images", albumName))))
            {
                return true;
            }
            else
            {
                if (oldAlbumName == "New Album")
                {
                    new DirectoryInfo(Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"\Images", albumName))).Create();
                }
                else
                {
                    //DirectoryInfo folder = new DirectoryInfo(Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"\Images", oldAlbumName)));
                    Directory.Move(Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"\Images", oldAlbumName)), Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"\Images", albumName)));
                }
                return false;
            }
        }
    }
}
