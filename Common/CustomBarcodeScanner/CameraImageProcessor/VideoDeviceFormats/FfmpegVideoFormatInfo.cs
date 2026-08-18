using System.Text.RegularExpressions;

namespace CustomBarcodeReader
{
    public class FfmpegVideoFormatInfo
    {
        public string Type { get; set; } // "pixel_format" or "vcodec"
        public string Format { get; set; }
        public int MinWidth { get; set; }
        public int MinHeight { get; set; }
        public int MinFps { get; set; }
        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }
        public int MaxFps { get; set; }
        public string OrignalFormatInfo { get; set; }

        public override string ToString()
        {
            return Regex.Replace(OrignalFormatInfo, @"\[dshow @ [^\]]+\]", "").Trim();
        }
    }
}
