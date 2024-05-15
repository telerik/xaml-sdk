using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using Telerik.Windows.Media.Imaging;

namespace CustomImageFormatProvider.Utilities
{
    public static class Extensions
    {
        public static RadBitmap ToRadBitmap(this Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                ms.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            return new RadBitmap(bitmapImage);
        }

        public static Bitmap ToBitmap(this RadBitmap radBitmap)
        {
            Bitmap bitmap = null;
            using (MemoryStream ms = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create((BitmapSource)radBitmap.Bitmap));
                enc.Save(ms);
                bitmap = new Bitmap(ms);
            }

            return bitmap;
        }
    }
}
