using System.Windows.Input;

namespace MediaPlayer.Library
{
    public static class AdditionalMediaCommands
    {
        public static RoutedUICommand ToggleFullscreenCommand { get; set; }

        static AdditionalMediaCommands()
        {
            ToggleFullscreenCommand = new RoutedUICommand("Toggle full screen", "ToggleFullscreenCommand", typeof(AdditionalMediaCommands));
        }
    }
}
