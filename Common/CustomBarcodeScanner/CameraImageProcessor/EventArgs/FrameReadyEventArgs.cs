namespace CustomBarcodeReader
{
    public class FrameReadyEventArgs : EventArgs
    {
        public byte[] FrameData { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int BytesPerPixel { get; private set; }

        public FrameReadyEventArgs(byte[] frameData, int width, int height, int bytesPerPixel)
        {
            FrameData = frameData;
            Width = width;
            Height = height;
            BytesPerPixel = bytesPerPixel;
        }
    }
}
