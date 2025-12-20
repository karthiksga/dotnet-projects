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
using WcfService1.Model;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Drawing2D;

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
                photo.Transform = (new Random().NextDouble() * 2.0 - 1.0) * 6.5;
                photo.ThumbnailImageStream = GetThumbnailPhoto(photo.AlbumName, photo.Name);
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
                album.AlbumImage = new List<AlbumImage>();
                album.AlbumName = folder.Name;
                var files = folder.GetFiles();
                int count = 0;
                if (files.Count() < 3)
                {
                    int j = files.Count();
                    Image fullSizeImg = Image.FromFile(Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Images") + @"\default.png");
                    //Image thumbNailImg = fullSizeImg.GetThumbnailImage(180, 100, null, new IntPtr());
                    fullSizeImg = resizeImage(fullSizeImg, 180, 100);
                    MemoryStream ms = new MemoryStream();
                    fullSizeImg.Save(ms, ImageFormat.Jpeg);
                    AlbumImage albumImage;
                    for (int i = 0; i < 3 - j; i++)
                    {
                        albumImage = new AlbumImage();
                        albumImage.ImageStream = ms.ToArray();
                        albumImage.Transform = (new Random().NextDouble() * 2.0 - 1.0)*6.5;
                        album.AlbumImage.Add(albumImage);                        
                        j += 1;
                    }                    
                }
                foreach (var file in files)
                {
                    if (count == 3)
                        break;
                    Image fullSizeImg = Image.FromFile(file.FullName);
                    //Image thumbNailImg = fullSizeImg.GetThumbnailImage(180, 100, null, new IntPtr());
                    fullSizeImg = resizeImage(fullSizeImg, 180, 100);
                    MemoryStream ms = new MemoryStream();
                    fullSizeImg.Save(ms, ImageFormat.Jpeg);
                    AlbumImage albumImage = new AlbumImage();
                    albumImage.ImageStream = ms.ToArray();
                    albumImage.Transform = (new Random().NextDouble() * 2.0 - 1.0) * 6.5;
                    album.AlbumImage.Add(albumImage);
                    count += 1;
                }                
                albums.Add(album);
	        }
            return albums;
        }

        public void DeleteAlbum(string albumName)
        {
            new DirectoryInfo(Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Images/"+albumName))).Delete();
        }

        public byte[] GetDefaultAlbumImage()
        {
            Image fullSizeImg = Image.FromFile(Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Images") + @"\Add Album.png");
            //Image thumbNailImg = fullSizeImg.GetThumbnailImage(180, 100, null, new IntPtr());
            fullSizeImg = resizeImage(fullSizeImg, 180, 100);
            MemoryStream ms = new MemoryStream();
            fullSizeImg.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
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
            //FileStream stream = new FileStream(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"Images\" + albumName) + @"\" + fileName, FileMode.Open, FileAccess.Read);
            //BinaryReader reader = new BinaryReader(stream);
            //return reader.ReadBytes((int)stream.Length);

            Image fullSizeImg = Image.FromFile(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"Images\" + albumName) + @"\" + fileName);
            fullSizeImg = resizeImage(fullSizeImg, 640, 480);
            MemoryStream ms = new MemoryStream();
            fullSizeImg.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }

        private static Image resizeImage(Image imgToResize, int x, int y)
        {
            int destWidth = x;
            int destHeight = y;
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (Image)b;
        }

        public Bitmap GetImage(string albumName, string fileName)
        {
            FileStream stream = new FileStream(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"Images\" + albumName) + @"\" + fileName, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);
            return new Bitmap(new MemoryStream(reader.ReadBytes((int)stream.Length)));
        }


        public byte[] GetThumbnailPhoto(string albumName, string fileName)
        {            
            Image fullSizeImg = Image.FromFile(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"Images\" + albumName) + @"\" + fileName);
            //Image thumbNailImg = fullSizeImg.GetThumbnailImage(206, 136, null, new IntPtr());
            fullSizeImg = resizeImage(fullSizeImg, 206, 136);
            MemoryStream ms = new MemoryStream();
            fullSizeImg.Save(ms, ImageFormat.Jpeg);         
            return ms.ToArray();
        }

        //public AlbumModel GetPhotoObject(string fileName)
        //{
        //    //Image fullSizeImg = Image.FromFile("C:\\inetpub\\wwwroot\\MaFamille1\\WcfService1\\Images" + "\\" + fileName + ".jpg");
        //    FileStream fs = new FileStream("C:\\inetpub\\wwwroot\\MaFamille1\\WcfService1\\Images" + "\\" + fileName + ".jpg", FileMode.Open, FileAccess.Read);
        //    return new AlbumModel { ImageStream = new BinaryReader(fs).ReadBytes((int)fs.Length), AlbumName = fileName };            
        //}

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

        public AlbumModel IsAlbumExists(string albumName,string oldAlbumName, bool isThumbnailsLoaded)
        {

            if (Directory.Exists(Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"\Images", albumName))))
            {
                return new AlbumModel{ IsAlbumExists=true};
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
                if (!isThumbnailsLoaded)
                {
                    AlbumModel newAlbum = new AlbumModel();
                    newAlbum.AlbumName = albumName;
                    newAlbum.OldAlbumName = oldAlbumName;
                    newAlbum.IsAlbumExists = false;
                    newAlbum.AlbumImage = new List<AlbumImage>();
                    Image fullSizeImg = Image.FromFile(Path.GetFullPath(System.AppDomain.CurrentDomain.BaseDirectory + @"\Images") + @"\default.png");
                    //Image thumbNailImg = fullSizeImg.GetThumbnailImage(180, 100, null, new IntPtr());
                    fullSizeImg = resizeImage(fullSizeImg, 180, 100);
                    MemoryStream ms = new MemoryStream();
                    fullSizeImg.Save(ms, ImageFormat.Jpeg);
                    AlbumImage albumImage;
                    for (int i = 0; i < 3; i++)
                    {
                        albumImage = new AlbumImage();
                        albumImage.ImageStream = ms.ToArray();
                        albumImage.Transform = (new Random().NextDouble() * 2.0 - 1.0) * 6.5;
                        newAlbum.AlbumImage.Add(albumImage);                                                
                    }                    
                    return newAlbum;
                }
                else
                {
                    return new AlbumModel { IsAlbumExists=false};
                }
                
            }
        }
    }
}
