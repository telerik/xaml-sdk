using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls.Map;

namespace FileSystemMapProvider
{
    /// <summary>
    /// Tile source which read map tiles from the file system.
    /// </summary>
    public class FileSystemTileSource : TiledMapSource
	{
        internal bool IsGrayScale;
        internal bool IsInverted;
        internal bool IsCustomColor;
		private string tilePathFormat;

		/// <summary>
		/// Initializes a new instance of the FileSystemTileSource class.
		/// </summary>
		/// <param name="tilePathFormat">Format string to access tiles in file system.</param>
		public FileSystemTileSource(string tilePathFormat)
			: base(1, 3, 256, 256)
		{
			this.tilePathFormat = tilePathFormat;
		}

        public override void Initialize()
        {
            this.RaiseInitializeCompleted();
        }

        protected override void GetTileLayers(int tileLevel, int tilePositionX, int tilePositionY, IList<object> tileImageLayerSources)
        {
            base.GetTileLayers(tileLevel, tilePositionX, tilePositionY, tileImageLayerSources);
        }

        /// <summary>
        /// Gets the image URI.
        /// </summary>
        /// <param name="tileLevel">Tile level.</param>
        /// <param name="tilePositionX">Tile X.</param>
        /// <param name="tilePositionY">Tile Y.</param>
        /// <returns>URI of image.</returns>
        protected override Uri GetTile(int tileLevel, int tilePositionX, int tilePositionY)
		{
			int zoomLevel = ConvertTileToZoomLevel(tileLevel);

			string tileFileName = this.tilePathFormat.Replace("{zoom}", zoomLevel.ToString(CultureInfo.InvariantCulture));
			tileFileName = tileFileName.Replace("{x}", tilePositionX.ToString(CultureInfo.InvariantCulture));
			tileFileName = tileFileName.Replace("{y}", tilePositionY.ToString(CultureInfo.InvariantCulture));

			if (File.Exists(tileFileName))
			{
                string result = (this.IsGrayScale || this.IsInverted || IsCustomColor) ? this.ProcessFile(tileFileName) : tileFileName;
                return new Uri(result);
			}

			return null;
		}       

        // Invert Colors
        private string ProcessFile(string fileName)
        {
            BitmapImage bmp = new BitmapImage(new Uri(fileName));
            Bitmap bitmap = this.BitmapImage2Bitmap(bmp);

            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMatrix matrix = this.GetColorMatrix();
            if (matrix != null)
            {
                imageAttributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap); 
            }
            
            if (IsCustomColor)
            {
                imageAttributes.SetRemapTable(this.CreateColorMap());
            }
            Graphics g = Graphics.FromImage(bitmap);

            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            g.DrawImage(bitmap, rect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttributes);
            g.Dispose();

            BitmapImage grayBmp = this.Bitmap2BitmapImage(bitmap);
            bitmap.Dispose();
            bmp = null;

            int index = fileName.LastIndexOf("\\");
            string newFileName = fileName.Substring(0, index + 1) + "gray" + fileName.Substring(index + 1);
            using (FileStream filestream = new FileStream(newFileName, FileMode.OpenOrCreate))
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(grayBmp));
                encoder.Save(filestream);
            }           

            return newFileName;
        }


        private ColorMap[] CreateColorMap()
        {
            List<ColorMap> colorMap = new List<ColorMap>();

            //Change the water to blue color
            ColorMap waterColorMap = new ColorMap();
            waterColorMap.OldColor = Color.FromArgb(255, 170, 211, 223);
            waterColorMap.NewColor = Color.FromArgb(255, 44, 192, 255);
            colorMap.Add(waterColorMap);

            ColorMap landColorMap = new ColorMap();
            landColorMap.OldColor = Color.FromArgb(255, 242, 239, 233);
            landColorMap.NewColor = Color.FromArgb(255, 255, 255, 255);
            colorMap.Add(landColorMap);

            return colorMap.ToArray();
        }

        private ColorMatrix GetColorMatrix()
        {
            if (this.IsGrayScale)
            {
                return GreyScaleMatrix;
            }
            else if (this.IsInverted)
            {
                return InvertScaleMatrix;
            }
            return null;
        }

        public static ColorMatrix InvertScaleMatrix = new ColorMatrix(
             new float[][]
                {
                   new float[] {-1, 0, 0, 0, 0},
                   new float[] {0, -1, 0, 0, 0},
                   new float[] {0, 0, -1, 0, 0},
                   new float[] {0, 0, 0, 1, 0},
                   new float[] {1, 1, 1, 0, 1}
                });

        public static ColorMatrix GreyScaleMatrix = new ColorMatrix(
              new float[][]
                {
                    new float[] {.3f, .3f, .3f, 0, 0},
                    new float[] {.59f, .59f, .59f, 0, 0},
                    new float[] {.11f, .11f, .11f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });

        private BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        private Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {          

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }
	}
}
