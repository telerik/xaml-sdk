using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Animation;

namespace ShowingAndHidingUsingAnimationGroup
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public RadDesktopAlertManager AnimationGroupManager { get; set; }
        public RadDesktopAlertManager SingleAnimationManager { get; set; }
        public RadDesktopAlert Alert { get; set; }

        public Example()
        {
            InitializeComponent();

            this.Unloaded += Example_Unloaded;
            this.SingleAnimationManager = new RadDesktopAlertManager(AlertScreenPosition.TopRight);
            this.AnimationGroupManager = new RadDesktopAlertManager(AlertScreenPosition.BottomLeft);
        }

        private void Example_Unloaded(object sender, RoutedEventArgs e)
        {
            this.AnimationGroupManager.CloseAllAlerts();
            this.SingleAnimationManager.CloseAllAlerts();
        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            this.CreateAlert("AnimationGroup", "This DesktopAlert is shown and hidden using AnimationGroup!");

            AnimationGroup groupIn = new AnimationGroup();
            groupIn.Children.Add(new FadeAnimation() { Direction = AnimationDirection.In });
            groupIn.Children.Add(new ScaleAnimation() { Direction = AnimationDirection.In, MinScale = 0.9 });

            AnimationGroup groupOut = new AnimationGroup();
            groupOut.Children.Add(new FadeAnimation() { Direction = AnimationDirection.Out });
            groupOut.Children.Add(new ScaleAnimation() { Direction = AnimationDirection.Out, MinScale = 0.9 });

            this.AnimationGroupManager.ShowAnimation = groupIn;
            this.AnimationGroupManager.HideAnimation = groupOut;

            this.AnimationGroupManager.ShowAlert(this.Alert);
        }

        private void RadButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.CreateAlert("SlideAnimation", "This DesktopAlert is shown and hidden using SlideAnimation!");

            var slideInAnimation = new SlideAnimation
            {
                Direction = AnimationDirection.In,
                SlideMode = SlideMode.Bottom,
                Orientation = Orientation.Horizontal,
                SpeedRatio = 0.5d
            };

            var slideOutAnimation = new SlideAnimation
            {
                Direction = AnimationDirection.Out,
                SlideMode = SlideMode.Bottom,
                Orientation = Orientation.Horizontal,
                SpeedRatio = 0.5d
            };

            this.SingleAnimationManager.ShowAnimation = slideInAnimation;
            this.SingleAnimationManager.HideAnimation = slideOutAnimation;

            this.SingleAnimationManager.ShowAlert(this.Alert);
        }

        private void CreateAlert(string header, string content)
        {
            this.Alert = new RadDesktopAlert();
            this.Alert.Header = header;
            this.Alert.Content = content;
            this.Alert.ShowDuration = 3000;
        }
    }
}
