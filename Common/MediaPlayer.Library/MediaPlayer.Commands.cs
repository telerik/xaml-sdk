using System;
using System.Windows.Input;

namespace MediaPlayer.Library
{
    public partial class MediaPlayer
    {
        private void RegisterCommandBindings()
        {
            CommandBinding playCommandBinding = new CommandBinding(MediaCommands.Play, OnPlayCommandExecuted, OnPlayCommandCanExecute);
            this.CommandBindings.Add(playCommandBinding);

            CommandBinding stopCommandBinding = new CommandBinding(MediaCommands.Stop, OnStopCommandExecuted, OnStopCommandCanExecute);
            this.CommandBindings.Add(stopCommandBinding);

            CommandBinding togglePlayPauseCommandBinding = new CommandBinding(MediaCommands.TogglePlayPause, OnTogglePlayPauseCommandExecuted);
            this.CommandBindings.Add(togglePlayPauseCommandBinding);

            CommandBinding muteVolumeCommandBinding = new CommandBinding(MediaCommands.MuteVolume, OnMuteVolumeCommandExecuted);
            this.CommandBindings.Add(muteVolumeCommandBinding);
                        
            CommandBinding selectCommandBinding = new CommandBinding(MediaCommands.Select, OnSelectCommandExecuted);
            this.CommandBindings.Add(selectCommandBinding);
        }
        
        private void OnPlayCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !this.IsPlaying;
        }

        private void OnPlayCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Play();
        }

        private void OnStopCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.IsPlaying;
        }

        private void OnStopCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Stop();
        }

        private void OnTogglePlayPauseCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.TogglePlayPause();
        }

        private void OnMuteVolumeCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.IsMuted ^= true;
        }

        private void OnSelectCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            MediaItem selectedItem = e.Parameter as MediaItem;
            if (selectedItem != null)
            {
                this.CurrentPlaylistItem = selectedItem;
                this.Position = TimeSpan.Zero;
                this.Play();
            }
        }
    }
}
