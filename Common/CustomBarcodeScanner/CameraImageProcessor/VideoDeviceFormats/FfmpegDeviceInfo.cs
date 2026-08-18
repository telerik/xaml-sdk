namespace CustomBarcodeReader
{
    public class FfmpegVideoDeviceInfo
    {
        public string Name { get; set; }
        public List<FfmpegVideoFormatInfo> Formats { get; set; } = new();
        public List<FfmpegVideoFormatInfo> OrderedFormats => Formats
            .OrderByDescending(f => f.MaxWidth)
            .ThenByDescending(f => f.MaxFps)
            .ToList();
    }
}
