namespace CustomBarcodeReader
{
    public class VideoFeedErrorEventArgs : EventArgs
    {
        public Exception Exception { get; private set; }

        public VideoFeedErrorEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}
