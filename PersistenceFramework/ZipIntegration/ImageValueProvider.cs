using System;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;
using Telerik.Windows.Media.Imaging;
using Telerik.Windows.Persistence.Services;

namespace ZipIntegration
{
    public class ImageValueProvider : IValueProvider
    {
        public string ProvideValue(object context)
        {
            string resultValue = string.Empty;
            BitmapImage image = context as BitmapImage;
            RadBitmap radImage = new RadBitmap(image);
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            Stream stream = new MemoryStream();
            encoder.Frames.Add(radImage.Bitmap);
            encoder.Save(stream);
            stream.Position = 0L;
            StringBuilder builder = new StringBuilder();
            int readByte = 0;
            while ((readByte = stream.ReadByte()) != -1)
            {
                builder.Append(readByte + ",");
            }
            resultValue = builder.ToString();
            return resultValue;
        }

        public void RestoreValue(object context, string savedValue)
        {
            BitmapImage image = context as BitmapImage;
            string[] stringBytes = savedValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] bytes = new byte[stringBytes.Length];
            for (int i = 0; i < stringBytes.Length; i++)
            {
                bytes[i] = byte.Parse(stringBytes[i]);
            }
            MemoryStream stream = new MemoryStream(bytes);
            stream.Position = 0L;
            RadBitmap radBitmap = new RadBitmap(stream);
            image.SetSource(stream);
        }
    }
}
