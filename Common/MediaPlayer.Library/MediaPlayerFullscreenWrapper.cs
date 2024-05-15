using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace MediaPlayer.Library
{
    /// <summary>
    /// A control that allows you to display the MediaPlayer control in a fullscreen mode.
    /// </summary>
    [ContentProperty("MediaPlayer")]
    public class MediaPlayerFullscreenWrapper : Control
    {
        private const double ShowVideoControlsPanelOffset = 50;

        private bool requestToggleFullscreen = false;
        private Window fullscreenPresenter;
        private ContentControl contentPresenter;        

        static MediaPlayerFullscreenWrapper()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MediaPlayerFullscreenWrapper), new FrameworkPropertyMetadata(typeof(MediaPlayerFullscreenWrapper)));
        }

        public static readonly DependencyProperty MediaPlayerProperty =
            DependencyProperty.Register(
                "MediaPlayer", 
                typeof(MediaPlayer), 
                typeof(MediaPlayerFullscreenWrapper), 
                new PropertyMetadata(null, OnMediaPlayerChanged));
        
        public static readonly DependencyProperty IsFullscreenProperty =
            DependencyProperty.Register(
                "IsFullscreen",
                typeof(bool),
                typeof(MediaPlayerFullscreenWrapper),
                new PropertyMetadata(false, OnIsFullscreenChanged));
        
        public MediaPlayer MediaPlayer
        {
            get { return (MediaPlayer)GetValue(MediaPlayerProperty); }
            set { SetValue(MediaPlayerProperty, value); }
        }

        public bool IsFullscreen
        {
            get { return (bool)GetValue(IsFullscreenProperty); }
            set { SetValue(IsFullscreenProperty, value); }
        }

        public MediaPlayerFullscreenWrapper()
        {         
            this.Unloaded += OnUnloaded;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.contentPresenter = this.GetTemplateChild("PART_MediaPlayerPresenter") as ContentControl;

            if (this.requestToggleFullscreen)
            {
                this.GoToFullscreen();
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            this.CloseFullscreen();
        }

        private static void OnMediaPlayerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                MediaPlayerFullscreenWrapper wrapper = (MediaPlayerFullscreenWrapper)d;
                MediaPlayer mediaPlayer = (MediaPlayer)e.NewValue;

                CommandBinding fullscreenCommandBinding = new CommandBinding(AdditionalMediaCommands.ToggleFullscreenCommand, wrapper.OnToggleFullscreenExecuted);
                mediaPlayer.CommandBindings.Add(fullscreenCommandBinding);
            }            
        }

        private static void OnIsFullscreenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var wrapper = (MediaPlayerFullscreenWrapper)d;
            if (wrapper. MediaPlayer == null)
            {
                wrapper.requestToggleFullscreen = true;
                return;
            }

            if ((bool)e.NewValue)
            {
                wrapper.GoToFullscreen();
            }
            else
            {
                wrapper.CloseFullscreen();
            }
        }
        
        private void OnToggleFullscreenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.IsFullscreen = (this.fullscreenPresenter == null);
        }
        
        private void OnFullscreenPresenterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.IsFullscreen = false;
            }
        }

        private void OnFullscreenPresenterMouseMove(object sender, MouseEventArgs e)
        {
            this.UpdateVideoControlsPanelVisibility(e.GetPosition(this.fullscreenPresenter));
        }

        private Window CreateFullscreenPresenter(MediaPlayer mediaPlayer)
        {
            var presenter = new Window();
            presenter.Content = mediaPlayer;
            presenter.WindowStartupLocation = WindowStartupLocation.Manual;
            presenter.WindowStyle = WindowStyle.None;
            presenter.KeyDown += OnFullscreenPresenterKeyDown;
            presenter.MouseMove += OnFullscreenPresenterMouseMove;
            

            return presenter;
        }
        
        private void MaximizeFullscreenPresenter()
        {
            if (this.fullscreenPresenter != null && this.fullscreenPresenter.IsLoaded)
            {
                this.fullscreenPresenter.Left = Window.GetWindow(this).Left;
                this.fullscreenPresenter.Top = Window.GetWindow(this).Top;
                this.fullscreenPresenter.Topmost = true;
                this.fullscreenPresenter.WindowState = WindowState.Maximized;
            }
        }

        private void GoToFullscreen()
        {
            var isPlaying = this.MediaPlayer.IsPlaying;
            var playerPosition = this.MediaPlayer.Position;
            this.contentPresenter.Content = null;
            this.fullscreenPresenter = this.CreateFullscreenPresenter(this.MediaPlayer);
            this.fullscreenPresenter.Show();
            this.fullscreenPresenter.Activate();
            this.MaximizeFullscreenPresenter();
            this.MediaPlayer.Position = playerPosition;
            if (isPlaying)
            {
                this.MediaPlayer.Play();
            }
        }

        private void CloseFullscreen()
        {
            this.fullscreenPresenter.Content = null;
            this.fullscreenPresenter.Close();
            this.contentPresenter.Content = this.MediaPlayer;
            this.fullscreenPresenter.KeyDown -= OnFullscreenPresenterKeyDown;
            this.fullscreenPresenter.MouseMove -= OnFullscreenPresenterMouseMove;
            this.fullscreenPresenter = null;

            this.MediaPlayer.VideoControlsPanelVisibility = Visibility.Visible;
        }

        private void UpdateVideoControlsPanelVisibility(Point mousePosition)
        {
            double delta = (this.fullscreenPresenter.ActualHeight - mousePosition.Y);
            if (delta <= ShowVideoControlsPanelOffset)
            {
                this.MediaPlayer.VideoControlsPanelVisibility = Visibility.Visible;
            }
            else if (delta > ShowVideoControlsPanelOffset && this.MediaPlayer.VideoControlsPanelVisibility == Visibility.Visible)
            {
                this.MediaPlayer.VideoControlsPanelVisibility = Visibility.Collapsed;
            }
        }        
    }
}
