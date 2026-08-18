using System.Collections.Concurrent;
using System.Diagnostics;

namespace CustomBarcodeReader
{
    public class VideoFeedReader
    {
        private const int BytesPerPixel = 3; // bgr24
        private CancellationTokenSource? _cts;
        private Task? ffmpegTask;

        public event EventHandler<FrameReadyEventArgs> FrameReady;        
        public event EventHandler<VideoFeedErrorEventArgs> VideoFeedError;

        public void StartCapture(string deviceName, int width, int height, int fps, string pixelFormat, BlockingCollection<FrameInfo> frameQueue)
        {
            _cts = new CancellationTokenSource();
            ffmpegTask = Task.Run(() => RunFfmpegReader(deviceName, width, height, fps, pixelFormat, frameQueue, _cts.Token));
        }

        public void StopCapture()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                try
                {
                    ffmpegTask?.Wait(2000);                    
                }
                catch { }
                _cts.Dispose();
                _cts = null;
            }            
        }

        private void RunFfmpegReader(string deviceString, int width, int height, int fps, string pixelFormat, BlockingCollection<FrameInfo> queue, CancellationToken ct)
        {
            int frameSize = width * height * BytesPerPixel;

            var commandArgs = $"-hide_banner -loglevel error -f dshow " +
                            $"-video_size {width}x{height} " +
                            $"-framerate {fps} " +
                            $"-i \"video={deviceString}\" " +
                            $"-pix_fmt bgr24 " +
                            $"-vcodec rawvideo -f rawvideo -";

            var startInfo = new ProcessStartInfo
            {
                FileName = "ffmpeg.exe",
                Arguments = commandArgs,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
            };

            try
            {
                using var process = new Process { StartInfo = startInfo, EnableRaisingEvents = true };

                process.ErrorDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                        Debug.WriteLine("ffmpeg: " + e.Data);
                };

                process.Start();
                process.BeginErrorReadLine();

                using var stdout = process.StandardOutput.BaseStream;
                var buffer = new byte[frameSize];

                while (!ct.IsCancellationRequested)
                {
                    int read = 0;
                    while (read < frameSize)
                    {
                        int r = stdout.Read(buffer, read, frameSize - read);
                        if (r == 0)
                        {
                            return;
                        }
                        read += r;
                    }

                    var frameCopy = new byte[frameSize];
                    Buffer.BlockCopy(buffer, 0, frameCopy, 0, frameSize);

                    if (!queue.TryAdd(new FrameInfo { ImageData = frameCopy, Width = width, Height = height }))
                    {
                        //Debug.WriteLine("Dropping frame: queue full");
                    }

                    FrameReady?.Invoke(this, new FrameReadyEventArgs(frameCopy, width, height, BytesPerPixel));
                }

                try { if (!process.HasExited) process.Kill(); } catch { }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ffmpeg reader failed: " + ex);
                VideoFeedError?.Invoke(this, new VideoFeedErrorEventArgs(ex));
            }
        }
    }
}
