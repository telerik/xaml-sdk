using FreeImageAPI;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Filters;

namespace CustomDecoder_WPF
{
    public class JpxDecoder : IPdfFilter
    {
        public string Name
        {
            get
            {
                return PdfFilterNames.JPXDecode;
            }
        }

        public byte[] Decode(PdfObject decodedObject, byte[] inputData, DecodeParameters decodeParameters)
        {
            FIBITMAP myImage = new FIBITMAP();
            using (MemoryStream stream = new MemoryStream(inputData))
            {
                myImage = FreeImage.LoadFromStream(stream);
            }

            Bitmap bitmap = FreeImage.GetBitmap(myImage);
            byte[] result;

            if (decodedObject.ColorSpace == ColorSpace.Gray)
            {
                result = new byte[decodedObject.Width * decodedObject.Height];

                for (int i = 0; i < decodedObject.Width; i++)
                {
                    for (int j = 0; j < decodedObject.Height; j++)
                    {
                        Color pixel = bitmap.GetPixel(i, j);
                        int index = j * decodedObject.Width + i;
                        byte grayColor = (byte)(0.2126 * pixel.R + 0.7152 * pixel.G + 0.0722 * pixel.B);

                        result[index] = grayColor;
                    }
                }
            }
            else
            {
                result = new byte[decodedObject.Width * decodedObject.Height * 3];

                for (int i = 0; i < decodedObject.Width; i++)
                {
                    for (int j = 0; j < decodedObject.Height; j++)
                    {
                        Color pixel = bitmap.GetPixel(i, j);

                        int index = j * decodedObject.Width + i;
                        result[index * 3] = pixel.R;
                        result[index * 3 + 1] = pixel.G;
                        result[index * 3 + 2] = pixel.B;
                    }
                }
            }

            return result;
        }

        public byte[] Encode(PdfObject encodedObject, byte[] inputData)
        {
            throw new NotImplementedException();
        }
    }
}