namespace CustomBarcodeReader
{
    public class BarcodeProcessedEventArgs : EventArgs
    {
        public string Text { get; private set; }
        
        public BarcodeProcessedEventArgs(string text)
        {
            Text = text;
        }
    }
}
