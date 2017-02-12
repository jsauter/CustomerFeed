using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace CustomerFeed
{
    public class ImageUtilites
    {
        public static byte[] ResizeImage(byte[] imgBytes)
        {
            using (System.Drawing.Image oldImage = System.Drawing.Image.FromStream(new MemoryStream(imgBytes)))
            {
                if(Resize(oldImage))
                {
                    Size newSize = GetNewSize(oldImage);

                    using (Bitmap newImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format24bppRgb))
                    {
                        using (Graphics gps = Graphics.FromImage(newImage))
                        {
                            gps.SmoothingMode = SmoothingMode.AntiAlias;

                            gps.InterpolationMode = InterpolationMode.HighQualityBicubic;

                            gps.PixelOffsetMode = PixelOffsetMode.HighQuality;

                            gps.DrawImage(oldImage, new Rectangle(new Point(0, 0), newSize));
                            MemoryStream MyStream = new MemoryStream();
                            newImage.Save(MyStream, ImageFormat.Jpeg);

                            return MyStream.GetBuffer();
                        }
                    }    
                }                
            }

            return imgBytes;
        }

        private static bool Resize(Image bmp)
        {
            bool resize = false;
            int height = bmp.Height; ;
            int width = bmp.Width;
            

            width = bmp.Width;
            height = bmp.Height;

            if(height > (float)(System.Convert.ToInt32(ConfigurationManager.AppSettings["maxImageHeight"])) && width > (float)(System.Convert.ToInt32(ConfigurationManager.AppSettings["maxImageWidth"])))
            {
                resize = true;
            }
          
            return resize;
        }

        private static Size GetNewSize(Image bmp)
        {
            int height = bmp.Height;;
            int width = bmp.Width;
            
            float maxImageHeight = (float)(System.Convert.ToInt32(ConfigurationManager.AppSettings["maxImageHeight"]));
            float maxImageWidth = (float)(System.Convert.ToInt32(ConfigurationManager.AppSettings["maxImageWidth"]));

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)maxImageWidth / (float)width);
            nPercentH = ((float)maxImageHeight / (float)height);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int newWidth = (int)(width * nPercent);
            int newHeight = (int)(height * nPercent);

            return new Size(newWidth, newHeight);
        }
    }
}
