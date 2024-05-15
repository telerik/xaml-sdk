using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace MediaPlayer.Library
{
    public partial class MediaPlayer : ContentControl
    {
        private const string DefaultTrackInfoValueFormatString = "hh\\:mm\\:ss";
        private const string DefaultTrackInfoLayoutFormatString = "{0} / {1}";

        private DispatcherTimer trackBarMediaPositionSyncTimer;
        private MediaElement mediaElement;
        private TextBlock  trackInfo;
        private RadSlider trackBar;

        private double cachedVolume = -1;
        private bool isTrackbarValueUpdating = false;
        private bool shouldNormalizeTrackBarValue = false;

        static MediaPlayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MediaPlayer), new FrameworkPropertyMetadata(typeof(MediaPlayer)));
        }

        public MediaPlayer()
        {            
            this.RegisterCommandBindings();
            this.Unloaded += OnUnloaded;
            this.Loaded += OnLoaded;
        }
        
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.mediaElement = GetTemplateChild("PART_MediaElement") as MediaElement;
            this.trackInfo = GetTemplateChild("PART_TrackInfo") as TextBlock;
            this.trackBar = GetTemplateChild("PART_ProgressTrackBar") as RadSlider;           
            this.UpdateTrackBarInfo();

            this.shouldNormalizeTrackBarValue = true;
        }

        public void Play()
        {
            if (this.mediaElement != null && this.CurrentPlaylistItem != null)
            {
                this.mediaElement.Play();
                this.trackBarMediaPositionSyncTimer.Start();
                this.IsPlaying = true;
            }
        }      

        public void Pause()
        {
            if (this.mediaElement != null && this.mediaElement.CanPause)
            {
                this.mediaElement.Pause();                
                this.trackBarMediaPositionSyncTimer.Stop();
                this.IsPlaying = false;
            }
        }

        public void TogglePlayPause()
        {
            if (this.IsPlaying)
            {
                this.Pause();
            }
            else
            {
                this.Play();
            }
        }

        public void Stop()
        {
            if (this.mediaElement != null)
            {
                this.mediaElement.Stop();
                this.trackBarMediaPositionSyncTimer.Stop();
                this.Position = TimeSpan.Zero;
                this.IsPlaying = false;
            }
        }
        
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {            
            this.Focus();                 
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                this.TogglePlayPause();
            }
        }

        protected virtual string GetProgressTrackInfo(Duration naturalDuration, TimeSpan currentPosition, TrackInfoMode mode)
        {
            if (mode == TrackInfoMode.CurrentAndEndTime)
            {
                if (naturalDuration == Duration.Automatic)
                {
                    return string.Format(DefaultTrackInfoLayoutFormatString, TimeSpan.Zero.ToString(DefaultTrackInfoValueFormatString), TimeSpan.Zero.ToString(DefaultTrackInfoValueFormatString));
                }
                return string.Format(DefaultTrackInfoLayoutFormatString, currentPosition.ToString(DefaultTrackInfoValueFormatString), naturalDuration.TimeSpan.ToString(DefaultTrackInfoValueFormatString));
            }
            else if (mode == TrackInfoMode.CurrentTime)
            {
                if (naturalDuration == Duration.Automatic)
                {
                    return TimeSpan.Zero.ToString(DefaultTrackInfoValueFormatString);
                }
                return currentPosition.ToString(DefaultTrackInfoValueFormatString);
            }
            else
            {
                if (naturalDuration == Duration.Automatic)
                {
                    return TimeSpan.Zero.ToString(DefaultTrackInfoValueFormatString);
                }
                return (naturalDuration.TimeSpan - currentPosition).ToString(DefaultTrackInfoValueFormatString);
            }            
        }

        protected virtual void OnMediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message);
        }

        private void OnTrackBarMediaPositionSyncTimerTick(object sender, EventArgs e)
        {
            if (this.trackBarMediaPositionSyncTimer.IsEnabled)
            {
                this.Position = this.mediaElement.Position;
            }
        }
        
        private void OnMediaElementMediaOpened(object sender, RoutedEventArgs e)
        {
            this.UpdateTrackBarMaximum();
            this.TryNormalizeTrackBarValue();
        }        

        private void OnTrackBarValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!this.isTrackbarValueUpdating)
            {                
                this.Position = TimeSpan.FromSeconds(e.NewValue);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.SetupSyncTimer();

            if (this.mediaElement != null)
            {
                this.mediaElement.MediaOpened += OnMediaElementMediaOpened;
                this.mediaElement.MediaFailed += OnMediaFailed;
            }

            if (this.trackBar != null)
            {
                this.trackBar.ValueChanged += OnTrackBarValueChanged;
            }

            if (this.trackInfo != null)
            {
                this.trackInfo.MouseLeftButtonDown += OnTrackInfoMouseLeftButtonDown;
            }

            if (this.CurrentPlaylistItem == null && this.Playlist.Count > 0)
            {
                this.CurrentPlaylistItem = this.Playlist[0];
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            this.Stop();
            this.trackBarMediaPositionSyncTimer.Tick -= OnTrackBarMediaPositionSyncTimerTick;
            this.mediaElement.MediaOpened -= OnMediaElementMediaOpened;
            this.mediaElement.MediaFailed -= OnMediaFailed;
            this.trackBar.ValueChanged -= OnTrackBarValueChanged;
            this.trackInfo.MouseLeftButtonDown -= OnTrackInfoMouseLeftButtonDown;
            this.Unloaded -= OnUnloaded;
        }

        private void OnTrackInfoMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var values = (TrackInfoMode[])Enum.GetValues(typeof(TrackInfoMode));
            var currentIndex = Array.IndexOf(values, this.TrackInfoMode) + 1;
            TrackInfoMode newMode = (values.Length == currentIndex) ? values[0] : values[currentIndex];
            this.TrackInfoMode = newMode;
        }

        private void SetupSyncTimer()
        {
            this.trackBarMediaPositionSyncTimer = new DispatcherTimer();
            this.trackBarMediaPositionSyncTimer.Interval = TimeSpan.FromSeconds(1);
            this.trackBarMediaPositionSyncTimer.Tick += OnTrackBarMediaPositionSyncTimerTick;
        }

        private void UpdateTrackBarValue(double value)
        {
            this.isTrackbarValueUpdating = true;
            this.trackBar.Value = value;
            this.isTrackbarValueUpdating = false;
        }
        
        private void UpdateTrackBarInfo()
        {
            if (this.trackInfo != null && this.mediaElement != null)
            {
                this.trackInfo.Text = GetProgressTrackInfo(this.mediaElement.NaturalDuration, this.mediaElement.Position, this.TrackInfoMode);
            }
        }    

        private void UpdateTrackBarMaximum()
        {
            if (this.mediaElement.NaturalDuration != Duration.Automatic)
            {
                this.trackBar.Maximum = this.mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
            }
        }

        private void TryNormalizeTrackBarValue()
        {
            if (this.shouldNormalizeTrackBarValue)
            {
                double normalizedValue = this.trackBar.Value * this.trackBar.Maximum;
                this.Position = TimeSpan.FromSeconds(normalizedValue);
                this.shouldNormalizeTrackBarValue = false;
            }
        }
    }
}
