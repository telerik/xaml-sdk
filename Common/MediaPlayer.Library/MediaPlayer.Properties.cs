using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediaPlayer.Library
{
    public partial class MediaPlayer 
    {
        private ObservableCollection<MediaItem> playlist;

        public static readonly DependencyProperty CurrentPlaylistItemProperty =
            DependencyProperty.Register(
                "CurrentPlaylistItem", 
                typeof(MediaItem), 
                typeof(MediaPlayer), 
                new PropertyMetadata(null, OnCurrentPlaylistItemChanged));

        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register(
                "Position",
                typeof(TimeSpan),
                typeof(MediaPlayer),
                new PropertyMetadata(TimeSpan.Zero, OnPositionChanged));

        public static readonly DependencyProperty VolumeProperty =
           DependencyProperty.Register(
               "Volume",
               typeof(double),
               typeof(MediaPlayer),
               new PropertyMetadata(0.5, OnVolumeChanged));     

        public static readonly DependencyProperty IsPlayingProperty =
            DependencyProperty.Register(
                "IsPlaying",
                typeof(bool),
                typeof(MediaPlayer),
                new PropertyMetadata(false));

        public static readonly DependencyProperty IsMutedProperty =
            DependencyProperty.Register(
                "IsMuted",
                typeof(bool),
                typeof(MediaPlayer),
                new PropertyMetadata(false, OnIsMutedChanged));

        public static readonly DependencyProperty IsPlaylistOpenProperty =
           DependencyProperty.Register(
               "IsPlaylistOpen",
               typeof(bool),
               typeof(MediaPlayer),
               new PropertyMetadata(false));

        public static readonly DependencyProperty PlaylistItemTemplateProperty =
            DependencyProperty.Register(
                "PlaylistItemTemplate",
                typeof(DataTemplate),
                typeof(MediaPlayer),
                new PropertyMetadata(null));       
                
        public static readonly DependencyProperty AdditionalMediaControlContentProperty =
            DependencyProperty.Register(
                "AdditionalMediaControlContent", 
                typeof(object), 
                typeof(MediaPlayer), 
                new PropertyMetadata(null));
        
        public static readonly DependencyProperty AdditionalSettingsContentProperty =
            DependencyProperty.Register(
                "AdditionalSettingsContent", 
                typeof(object), 
                typeof(MediaPlayer), 
                new PropertyMetadata(null));
                
        public static readonly DependencyProperty TrackInfoModeProperty =
            DependencyProperty.Register(
                "TrackInfoMode", 
                typeof(TrackInfoMode), 
                typeof(MediaPlayer),
                new PropertyMetadata(TrackInfoMode.CurrentAndEndTime, OnTrackInfoModeChanged));

        public static readonly DependencyProperty VideoControlsPanelVisibilityProperty =
            DependencyProperty.Register(
                "VideoControlsPanelVisibility",
                typeof(Visibility),
                typeof(MediaPlayer),
                new PropertyMetadata(Visibility.Visible));
        
        public static readonly DependencyProperty MediaStretchProperty =
            DependencyProperty.Register(
                "MediaStretch", 
                typeof(Stretch), 
                typeof(MediaPlayer), 
                new PropertyMetadata(Stretch.Uniform));       
        
        public static readonly DependencyProperty MediaStretchDirectionProperty =
            DependencyProperty.Register(
                "MediaStretchDirection", 
                typeof(StretchDirection), 
                typeof(MediaPlayer), 
                new PropertyMetadata(StretchDirection.Both));

        public MediaItem CurrentPlaylistItem
        {
            get { return (MediaItem)GetValue(CurrentPlaylistItemProperty); }
            set { SetValue(CurrentPlaylistItemProperty, value); }
        }

        public TimeSpan Position
        {
            get { return (TimeSpan)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        public double Volume
        {
            get { return (double)GetValue(VolumeProperty); }
            set { SetValue(VolumeProperty, value); }
        }

        public bool IsPlaying
        {
            get { return (bool)GetValue(IsPlayingProperty); }
            private set { SetValue(IsPlayingProperty, value); }
        }

        public bool IsMuted
        {
            get { return (bool)GetValue(IsMutedProperty); }
            set { SetValue(IsMutedProperty, value); }
        }

        public bool IsPlaylistOpen
        {
            get { return (bool)GetValue(IsPlaylistOpenProperty); }
            set { SetValue(IsPlaylistOpenProperty, value); }
        }
        
        public DataTemplate PlaylistItemTemplate
        {
            get { return (DataTemplate)GetValue(PlaylistItemTemplateProperty); }
            set { SetValue(PlaylistItemTemplateProperty, value); }
        }

        public object AdditionalMediaControlContent
        {
            get { return (object)GetValue(AdditionalMediaControlContentProperty); }
            set { SetValue(AdditionalMediaControlContentProperty, value); }
        }

        public object AdditionalSettingsContent
        {
            get { return (object)GetValue(AdditionalSettingsContentProperty); }
            set { SetValue(AdditionalSettingsContentProperty, value); }
        }

        public TrackInfoMode TrackInfoMode
        {
            get { return (TrackInfoMode)GetValue(TrackInfoModeProperty); }
            set { SetValue(TrackInfoModeProperty, value); }
        }

        public Visibility VideoControlsPanelVisibility
        {
            get { return (Visibility)GetValue(VideoControlsPanelVisibilityProperty); }
            set { SetValue(VideoControlsPanelVisibilityProperty, value); }
        }

        public Stretch MediaStretch
        {
            get { return (Stretch)GetValue(MediaStretchProperty); }
            set { SetValue(MediaStretchProperty, value); }
        }

        public StretchDirection MediaStretchDirection
        {
            get { return (StretchDirection)GetValue(MediaStretchDirectionProperty); }
            set { SetValue(MediaStretchDirectionProperty, value); }
        }

        public ObservableCollection<MediaItem> Playlist
        {
            get
            {
                if (this.playlist == null)
                {
                    this.playlist = new ObservableCollection<MediaItem>();
                }
                return this.playlist;
            }
        }

        private static void OnPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mediaPlayer = (MediaPlayer)d;
            if (mediaPlayer.mediaElement != null)
            {
                var newPosition = (TimeSpan)e.NewValue;
                mediaPlayer.UpdateTrackBarValue(newPosition.TotalSeconds);
                mediaPlayer.mediaElement.Position = newPosition;
                mediaPlayer.UpdateTrackBarInfo();

                if (mediaPlayer.mediaElement.NaturalDuration.HasTimeSpan && 
                    newPosition == mediaPlayer.mediaElement.NaturalDuration.TimeSpan)
                {
                    mediaPlayer.Stop();
                }
            }
        }

        private static void OnCurrentPlaylistItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mediaPlayer = (MediaPlayer)d;
            if (e.NewValue != null)
            {
                if (mediaPlayer.IsPlaying)
                {
                    mediaPlayer.Stop();
                }                
                mediaPlayer.IsPlaylistOpen = false;                
            }
        }

        private static void OnVolumeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mediaPlayer = (MediaPlayer)d;
            double newVolume = (double)e.NewValue;
            if (newVolume > 0)
            {
                mediaPlayer.IsMuted = false;
            }            
        }

        private static void OnIsMutedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mediaPlayer = (MediaPlayer)d;
            bool isMuted = (bool)e.NewValue;
            if (isMuted)
            {
                mediaPlayer.cachedVolume = mediaPlayer.Volume;
                mediaPlayer.Volume = 0;
            }
            else
            {
                if (mediaPlayer.Volume == 0)
                {
                    mediaPlayer.Volume = mediaPlayer.cachedVolume;
                }                
                mediaPlayer.cachedVolume = -1;
            }
        }

        private static void OnTrackInfoModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mediaPlayer = (MediaPlayer)d;
            mediaPlayer.UpdateTrackBarInfo();
        }
    }
}
