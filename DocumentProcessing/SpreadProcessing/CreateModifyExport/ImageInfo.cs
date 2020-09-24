using System.IO;
using Telerik.Documents.Primitives;
using Telerik.Windows.Documents.Core.Imaging;

namespace CreateModifyExport
{
    public class ImageInfo : ImagePropertiesResolverBase
    {
        public override Size GetImageSize(byte[] imageData)
        {
            MemoryStream stream = new MemoryStream(imageData);
            using (System.Drawing.Bitmap image = new System.Drawing.Bitmap(stream))
            {
                return new Size(image.Width, image.Height);
            }
        }
    }
}
