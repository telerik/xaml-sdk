using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Telerik.Windows.Persistence.Services;

namespace ZipIntegration
{
	public class ImageTypeProvider : ITypeConverterProvider
	{
		public Type GetTypeConverterType()
		{
			return typeof(ImageTypeConverter);
		}
	}

	public class ImageTypeConverter : System.ComponentModel.TypeConverter
	{
		public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			string resultValue = string.Empty;
			BitmapFrame frame = value as BitmapFrame;
			JpegBitmapEncoder encoder = new JpegBitmapEncoder();
			encoder.Frames.Add(frame);
			Stream stream = new MemoryStream();
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

		public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			string[] stringBytes = value.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			byte[] bytes = new byte[stringBytes.Length];
			for (int i = 0; i < stringBytes.Length; i++)
			{
				bytes[i] = byte.Parse(stringBytes[i]);
			}
			MemoryStream stream = new MemoryStream(bytes);
			stream.Position = 0L;
			BitmapFrame frame = BitmapFrame.Create(stream);
			return frame;
		}
	}
}
