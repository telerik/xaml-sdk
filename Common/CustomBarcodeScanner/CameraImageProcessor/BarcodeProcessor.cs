using System.Collections.Concurrent;
using System.Diagnostics;
using ZXing;
using ZXing.Common;

namespace CustomBarcodeReader
{
    /// <summary>
    /// Process video frames to detect and decode QR and barcodes.
    /// </summary>
    public class BarcodeProcessor
    {
        private CancellationTokenSource? cts;
        private Task? qrTask;

        public event EventHandler<BarcodeProcessedEventArgs> BarcodeProcessed;

        private void RunQrProcessor(BlockingCollection<FrameInfo> queue, CancellationToken ct)
        {
            var reader = new BarcodeReaderGeneric 
            { 
                AutoRotate = true,
                Options = new DecodingOptions
                {
                    TryInverted = true, 
                    TryHarder = true
                }
            };

            while (!queue.IsCompleted && !ct.IsCancellationRequested)
            {
                try
                {
                    if (!queue.TryTake(out FrameInfo frameInfo, 200))
                        continue;

                    if (frameInfo == null) continue;

                    byte[] frame = frameInfo.ImageData;

                    var rgb = new byte[frame.Length];
                    for (int i = 0; i < frame.Length; i += 3)
                    {
                        byte b = frame[i + 0];
                        byte g = frame[i + 1];
                        byte r = frame[i + 2];
                        rgb[i + 0] = r; // R
                        rgb[i + 1] = g; // G
                        rgb[i + 2] = b; // B
                    }

                    var luminance = new RGBLuminanceSource(rgb, frameInfo.Width, frameInfo.Height, ZXing.RGBLuminanceSource.BitmapFormat.BGR24);
                    try
                    {
                        ZXing.Result result = reader.Decode(luminance);
                        if (result != null)
                        {
                            var text = result.Text;
                            BarcodeProcessed?.Invoke(this, new BarcodeProcessedEventArgs(result.Text));
                        }
                    }
                    catch (NullReferenceException ex)
                    {
                        Debug.WriteLine("BarcodeProcessor error: " + ex);
                    }
                    
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("BarcodeProcessor error: " + ex);
                }
            }
        }

        public void StartProcessing(BlockingCollection<FrameInfo> frameQueue)
        {
            cts = new CancellationTokenSource();
            qrTask = Task.Run(() => RunQrProcessor(frameQueue, cts.Token));
        }

        public void StopProcessing()
        {
            if (cts != null)
            {
                cts.Cancel();
                try
                {
                    qrTask?.Wait(2000);
                }
                catch { }
                cts.Dispose();
                cts = null;
            }
        }
    }
}
