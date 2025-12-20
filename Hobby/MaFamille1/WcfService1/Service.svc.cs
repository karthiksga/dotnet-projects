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

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }


        public List<AlbumModel> GetPhotos()
        {
            List<AlbumModel> photos = new List<AlbumModel>();
            var files = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Images"));
            AlbumModel photo = null;
            foreach (var file in files)
            {
                photo = new AlbumModel();
                FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(stream);
                photo.ImageStream= reader.ReadBytes((int)stream.Length);                
                photos.Add(photo);
            }
            return photos;
        }

        public List<AlbumModel> GetAlbums()
        {
            List<AlbumModel> photos = new List<AlbumModel>();
            AlbumModel model = new AlbumModel();
            model.AlbumName = "Album1";
            photos.Add(model);
            return photos;
        }

        public List<TsunamiModel> GetTsunamiPhotos()
        {
            List<TsunamiModel> photos = new List<TsunamiModel>();
            var files = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Thumbnails"));
            TsunamiModel photo = null;
            foreach (var file in files)
            {
                photo = new TsunamiModel();
                FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(stream);
                photo.ImageStream = reader.ReadBytes((int)stream.Length);
                photos.Add(photo);
            }
            photos[0].CanvasLeft = 239M;
            photos[0].CanvasTop = 150M;
            photos[0].Rotation = 37.523M;

            //photos[1].CanvasLeft = 427M;
            //photos[1].CanvasTop = 116M;
            //photos[1].Rotation = -16.515M;

            return photos;
        }

        public byte[] GetPhoto()
        {
            var files = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Images"));
            FileStream stream = new FileStream(files[new Random().Next(files.Length)], FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);
            return reader.ReadBytes((int)stream.Length);
        }

        public void UploadPhoto(AlbumModel picture)
        {
            try
            {
                var fs = new BinaryWriter(new FileStream(HttpContext.Current.Server.MapPath("~/Images") + "\\" + picture.AlbumName + ".jpg", FileMode.Create));
                fs.Write(picture.ImageStream);
                fs.Close();

                //You may even specify a standard thumbnail size
                int imageWidth = 188;
                int imageHeight = 132;

                Image fullSizeImg = Image.FromFile(HttpContext.Current.Server.MapPath("~/Images") + "\\" + picture.AlbumName + ".jpg");
                Image.GetThumbnailImageAbort dummyCallBack = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                Image thumbNailImg  = fullSizeImg.GetThumbnailImage(imageWidth, imageHeight,dummyCallBack, IntPtr.Zero);

                //Save the thumbnail in PNG format. 
                //You may change it to a diff format with the ImageFormat property
                thumbNailImg.Save(HttpContext.Current.Server.MapPath("~/Thumbnails") + "\\" + picture.AlbumName+".png", ImageFormat.Png);
                thumbNailImg.Dispose();

            }
            catch (FaultException<CustomException> ex)
            {                
                throw ex;
            }            
        }

        public bool ThumbnailCallback()
        {
            return false;
        }

        public void png()
        {
            //try
            //{                
            //    //Read in the width and height
            //    //int imageHeight = Convert.ToInt32(h.Text);
            //    //int imageWidth = Convert.ToInt32(w.Text);

            //    //You may even specify a standard thumbnail size
            //    //int imageWidth  = 70; 
            //    //int imageHeight = 70;

            //    if (imageUrl.IndexOf("/") >= 0 || imageUrl.IndexOf("\\") >= 0)
            //    {
            //        //We found a / or \
            //        Response.End();
            //    }

            //    //the uploaded image will be stored in the Pics folder.
            //    //to get resize the image, the original image has to be
            //    //accessed from the Pics folder
            //    imageUrl = "pics/" + imageUrl;

            //    System.Drawing.Image fullSizeImg
            //            = System.Drawing.Image.FromFile(Server.MapPath(imageUrl));
            //    System.Drawing.Image.GetThumbnailImageAbort dummyCallBack
            //            = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
            //    System.Drawing.Image thumbNailImg
            //            = fullSizeImg.GetThumbnailImage(imageWidth, imageHeight,
            //                                            dummyCallBack, IntPtr.Zero);

            //    //We need to create a unique filename for each generated image
            //    DateTime MyDate = DateTime.Now;

            //    String MyString = MyDate.ToString("ddMMyyhhmmss") + ".png";

            //    //Save the thumbnail in PNG format. 
            //    //You may change it to a diff format with the ImageFormat property
            //    thumbNailImg.Save(Request.PhysicalApplicationPath
            //                        + "pics\\" + MyString, ImageFormat.Png);
            //    thumbNailImg.Dispose();

            //    //Display the original & the newly generated thumbnail

            //    Image1.AlternateText = "Original image";
            //    Image1.ImageUrl = "pics\\" + UploadedFileName;
            //    Image2.AlternateText = "Thumbnail";
            //    Image2.ImageUrl = "pics\\" + MyString;
            //}
            //catch (Exception ex)
            //{
            //    Response.Write("An error occurred - " + ex.ToString());
            //}
        }
    }
}
