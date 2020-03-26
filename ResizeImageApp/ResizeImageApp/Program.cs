using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;
using ExifLib;
using System.Drawing.Drawing2D;

namespace ResizeImageApp
{
    class Program
    {
        static void Main(string[] args)
        {            
            ImageCompress imgCompress = new ImageCompress();
            DirectoryInfo folder = new DirectoryInfo(@"D:\My Photos\My Marriage Facebook\My Marriage original\");
            var files = folder.GetFiles();
            foreach (var file in files)
            {
                imgCompress.ResizeImageWithAspect(file.FullName, file.Name, 800);
            }            
        }        
    }    

    public class ImageCompress
    {
        #region[PrivateData]
        private static volatile ImageCompress imageCompress;
        private Bitmap bitmap;
        private int width;
        private int height;
        private Image img;
        #endregion[Privatedata]

        #region[Constructor]
        /// <summary>
        /// It is used to restrict to create the instance of the      ImageCompress
        /// </summary>
        public ImageCompress()
        {
        }
        #endregion[Constructor]

        public void ResizeImageWithAspect(string fileName, string outputFileName, int newWidth)
        {
            string outputpath = @"D:\My Photos\My Marriage Facebook\" + outputFileName;
            //string newoutputpath = @"C:\inetpub\wwwroot\MaFamille1\WcfService1\Images\test\" + outputFileName;
            Image original = Image.FromFile(fileName);

            //Find the aspect ratio between the height and width.

            float aspect = (float)original.Height / (float)original.Width;

            //Calculate the new height using the aspect ratio

            // and the desired new width.

            int newHeight = (int)(newWidth * aspect);

            //Create a bitmap of the correct size.

            Bitmap temp = new Bitmap(newWidth, newHeight, original.PixelFormat);

            //Get a Graphics object from the bitmap.

            Graphics newImage = Graphics.FromImage(temp);
            newImage.InterpolationMode = InterpolationMode.HighQualityBicubic;
            newImage.SmoothingMode = SmoothingMode.HighQuality;
            newImage.PixelOffsetMode = PixelOffsetMode.HighQuality;
            newImage.CompositingQuality = CompositingQuality.HighQuality;

            //Draw the image with the new width/height

            newImage.DrawImage(original, 0, 0, newWidth, newHeight);

            try
            {
                ExifReader reader = new ExifReader(fileName);
                object val;
                reader.GetTagValue((ushort)ExifTags.Orientation, out val);
                if (val.ToString() == "8")
                {                    
                    temp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    //PropertyItem props = original.PropertyItems[0];
                    //SetProperty(ref props, 771, "8");
                    //temp.SetPropertyItem(props);
                    //temp.Tag = "8";                    
                    //tests.Tag = val;
                }
                //tests.Save(newoutputpath);
            }
            catch (Exception ex)
            { }

            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 100);          // 100% Percent Compression
            temp.Save(outputpath, ImageCodecInfo.GetImageEncoders()[1], encoderParameters);   // jpg format

            //Save the bitmap  
            //temp.Save(outputpath);      
            
            //Dispose of our objects.

            temp.Dispose();

            original.Dispose();            

            newImage.Dispose();

            
            //Image tests = Image.FromFile(outputpath);
            //try
            //{
            //    ExifReader reader = new ExifReader(fileName);
            //    object val;
            //    reader.GetTagValue((ushort)ExifTags.Orientation, out val);
            //    if (val.ToString() == "8")
            //    {
            //        //PropertyItem props = original.PropertyItems[0];
            //        //SetProperty(ref props, 771, "8");
            //        //temp.SetPropertyItem(props);
            //        //temp.Tag = "8";                    
            //        tests.Tag = val;                                         
            //    }
            //    tests.Save(newoutputpath);                
            //}
            //catch (Exception ex)
            //{}

            //Image testsagain = Image.FromFile(newoutputpath);
            //Console.WriteLine(testsagain.Tag);            
        }

        public void SetProperty(ref System.Drawing.Imaging.PropertyItem prop, int iId, string sTxt)
        {
            int iLen = sTxt.Length + 1;
            byte[] bTxt = new Byte[iLen];
            for (int i = 0; i < iLen - 1; i++)
                bTxt[i] = (byte)sTxt[i];
            bTxt[iLen - 1] = 0x00;
            prop.Id = iId;
            prop.Type = 2;
            prop.Value = bTxt;
            prop.Len = iLen;
        }

    }
}
