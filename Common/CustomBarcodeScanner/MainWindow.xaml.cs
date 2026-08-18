using System.Collections.Concurrent;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace CustomBarcodeReader
{
    public partial class MainWindow : Window
    {
        private BlockingCollection<FrameInfo> frameQueue;
        private VideoFeedReader frameReader;
        private BarcodeProcessor barcodeProcessor;

        public MainWindow()
        {
            StyleManager.ApplicationTheme = new Windows11Theme();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            InitializeComponent();

            frameReader = new VideoFeedReader();
            frameReader.FrameReady += FrameReader_FrameReady;
            frameReader.VideoFeedError += FrameReader_VideoFeedError;

            barcodeProcessor = new BarcodeProcessor();
            barcodeProcessor.BarcodeProcessed += BarcodeProcessor_BarcodeProcessed;

            var devices = FfmpegHelper.ListVideoDevicesAndFormats();
            videoDevicesComboBox.ItemsSource = devices;
            videoDevicesComboBox.SelectedIndex = 0;
            videoFormatsComboBox.SelectedIndex = 0;
            videoDevicesComboBox.SelectionChanged += (s, e) => { videoFormatsComboBox.SelectedIndex = 0; };
        }

        private void BarcodeProcessor_BarcodeProcessed(object? sender, BarcodeProcessedEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                QrResultTextBox.Text = e.Text;

                StopCapture();

                DoubleAnimation animation = new DoubleAnimation
                {
                    From = 12,
                    To = 20,
                    Duration = TimeSpan.FromSeconds(0.5),
                    AutoReverse = true
                };
                QrResultTextBox.BeginAnimation(TextBlock.FontSizeProperty, animation);                
            }));
        }

        private void FrameReader_VideoFeedError(object? sender, VideoFeedErrorEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() => MessageBox.Show(this, "ffmpeg reader failed: " + e.Exception.Message)));
        }

        private void FrameReader_FrameReady(object? sender, FrameReadyEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                try
                {
                    var bmp = BitmapSource.Create(e.Width, e.Height, 96, 96, System.Windows.Media.PixelFormats.Bgr24, null, e.FrameData, e.Width * e.BytesPerPixel);
                    VideoImage.Source = bmp;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("UI update error: " + ex);
                }
            }), DispatcherPriority.Render);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            StopButton.IsEnabled = true;
            videoDevicesComboBox.IsEnabled = false;
            videoFormatsComboBox.IsEnabled = false;
            QrResultTextBox.Text = string.Empty;

            frameQueue = new BlockingCollection<FrameInfo>(boundedCapacity: 8);

            string deviceName = ((FfmpegVideoDeviceInfo)videoDevicesComboBox.SelectedItem).Name;
            var videoFormat = (FfmpegVideoFormatInfo)videoFormatsComboBox.SelectedItem;

            frameReader?.StartCapture(deviceName, videoFormat.MaxWidth, videoFormat.MaxHeight, videoFormat.MaxFps, videoFormat.Format, frameQueue);
            barcodeProcessor.StartProcessing(frameQueue);
        }

        private void StopCapture()
        {
            frameReader?.StopCapture();
            barcodeProcessor?.StopProcessing();

            frameQueue?.CompleteAdding();

            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
            videoDevicesComboBox.IsEnabled = true;
            videoFormatsComboBox.IsEnabled = true;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            StopCapture();
        }

        protected override void OnClosed(EventArgs e)
        {
            StopCapture();
            base.OnClosed(e);
        }
    }
}