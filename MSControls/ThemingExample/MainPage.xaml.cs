using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;

namespace Theming
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
			InitializeComponent();
			this.listBox.ItemsSource = Conference.GetConferences();
			this.listBox.SelectedIndex = 2;
		}
		private void Office_Black_Checked(object sender, RoutedEventArgs e)
		{
			Application.Current.Resources.MergedDictionaries.Clear();
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Office_Black;component/Themes/System.Windows.xaml", UriKind.RelativeOrAbsolute)
			});
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Office_Black;component/Themes/Telerik.Windows.Controls.xaml", UriKind.RelativeOrAbsolute)
			});
			this.container.Background = new SolidColorBrush(Colors.White);
		}

		private void Office_Silver_Checked(object sender, RoutedEventArgs e)
		{
			Application.Current.Resources.MergedDictionaries.Clear();
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Office_Silver;component/Themes/System.Windows.xaml", UriKind.RelativeOrAbsolute)
			});
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Office_Silver;component/Themes/Telerik.Windows.Controls.xaml", UriKind.RelativeOrAbsolute)
			});
			this.container.Background = new SolidColorBrush(Colors.White);
		}

		private void Office_Blue_Checked(object sender, RoutedEventArgs e)
		{
			Application.Current.Resources.MergedDictionaries.Clear();
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Office_Blue;component/Themes/System.Windows.xaml", UriKind.RelativeOrAbsolute)
			});
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Office_Blue;component/Themes/Telerik.Windows.Controls.xaml", UriKind.RelativeOrAbsolute)
			});
			this.container.Background = new SolidColorBrush(Colors.White);
		}

		private void Summer_Checked(object sender, RoutedEventArgs e)
		{
			Application.Current.Resources.MergedDictionaries.Clear();
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Summer;component/Themes/System.Windows.xaml", UriKind.RelativeOrAbsolute)
			});
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Summer;component/Themes/Telerik.Windows.Controls.xaml", UriKind.RelativeOrAbsolute)
			});
			this.container.Background = new SolidColorBrush(Colors.White);
		}

		private void Vista_Checked(object sender, RoutedEventArgs e)
		{
			Application.Current.Resources.MergedDictionaries.Clear();
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Vista;component/Themes/System.Windows.xaml", UriKind.RelativeOrAbsolute)
			});
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Vista;component/Themes/Telerik.Windows.Controls.xaml", UriKind.RelativeOrAbsolute)
			});
			this.container.Background = new SolidColorBrush(Colors.White);
		}

		private void Windows8_Checked(object sender, RoutedEventArgs e)
		{
			Application.Current.Resources.MergedDictionaries.Clear();
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Windows8;component/Themes/System.Windows.xaml", UriKind.RelativeOrAbsolute)
			});
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Windows8;component/Themes/Telerik.Windows.Controls.xaml", UriKind.RelativeOrAbsolute)
			});

		}

		private void Windows8Touch_Checked(object sender, RoutedEventArgs e)
		{
			Application.Current.Resources.MergedDictionaries.Clear();
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Windows8Touch;component/Themes/System.Windows.xaml", UriKind.RelativeOrAbsolute)
			});
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Windows8Touch;component/Themes/Telerik.Windows.Controls.xaml", UriKind.RelativeOrAbsolute)
			});
			this.container.Background = new SolidColorBrush(Colors.White);
		}

		private void Transparent_Checked(object sender, RoutedEventArgs e)
		{
			Application.Current.Resources.MergedDictionaries.Clear();
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Transparent;component/Themes/System.Windows.xaml", UriKind.RelativeOrAbsolute)
			});
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Transparent;component/Themes/Telerik.Windows.Controls.xaml", UriKind.RelativeOrAbsolute)
			});
			ImageBrush ib = new ImageBrush();
			ib.ImageSource = new BitmapImage(new Uri(@"../../Images/bg04.jpg", UriKind.RelativeOrAbsolute));
			this.container.Background = ib;
		}

		private void Windows7_Checked(object sender, RoutedEventArgs e)
		{
			Application.Current.Resources.MergedDictionaries.Clear();
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Windows7;component/Themes/System.Windows.xaml", UriKind.RelativeOrAbsolute)
			});
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Windows7;component/Themes/Telerik.Windows.Controls.xaml", UriKind.RelativeOrAbsolute)
			});
			this.container.Background = new SolidColorBrush(Colors.White);
		}

		private void Expression_Dark_Checked(object sender, RoutedEventArgs e)
		{
			Application.Current.Resources.MergedDictionaries.Clear();
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Expression_Dark;component/Themes/System.Windows.xaml", UriKind.RelativeOrAbsolute)
			});
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Expression_Dark;component/Themes/Telerik.Windows.Controls.xaml", UriKind.RelativeOrAbsolute)
			});
			this.container.Background = new SolidColorBrush() { Color = Color.FromArgb(255, 28, 28, 28) };
		}

		private void Office2013_Checked(object sender, RoutedEventArgs e)
		{
			Application.Current.Resources.MergedDictionaries.Clear();
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Office2013;component/Themes/System.Windows.xaml", UriKind.RelativeOrAbsolute)
			});
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Office2013;component/Themes/Telerik.Windows.Controls.xaml", UriKind.RelativeOrAbsolute)
			});
			Office2013Palette.LoadPreset(Office2013Palette.ColorVariation.White);
			this.container.Background = new SolidColorBrush(Colors.White);
		}

		private void Office2013_LightGray_Checked(object sender, RoutedEventArgs e)
		{
			Application.Current.Resources.MergedDictionaries.Clear();
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Office2013;component/Themes/System.Windows.xaml", UriKind.RelativeOrAbsolute)
			});
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Office2013;component/Themes/Telerik.Windows.Controls.xaml", UriKind.RelativeOrAbsolute)
			});
			Office2013Palette.LoadPreset(Office2013Palette.ColorVariation.LightGray);
			this.container.Background = new SolidColorBrush(Colors.White);
		}

		private void Office2013_DarkGray_Checked(object sender, RoutedEventArgs e)
		{
			Application.Current.Resources.MergedDictionaries.Clear();
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Office2013;component/Themes/System.Windows.xaml", UriKind.RelativeOrAbsolute)
			});
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.Office2013;component/Themes/Telerik.Windows.Controls.xaml", UriKind.RelativeOrAbsolute)
			});
			Office2013Palette.LoadPreset(Office2013Palette.ColorVariation.DarkGray);
			this.container.Background = new SolidColorBrush(Colors.White);
		}

		private void VisualStudio2013_Checked(object sender, RoutedEventArgs e)
		{
			Application.Current.Resources.MergedDictionaries.Clear();
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.VisualStudio2013;component/Themes/System.Windows.xaml", UriKind.RelativeOrAbsolute)
			});
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.VisualStudio2013;component/Themes/Telerik.Windows.Controls.xaml", UriKind.RelativeOrAbsolute)
			});
			VisualStudio2013Palette.LoadPreset(VisualStudio2013Palette.ColorVariation.Light);
			this.container.Background = new SolidColorBrush(Colors.White);
		}

		private void VisualStudio2013_Dark_Checked(object sender, RoutedEventArgs e)
		{
			Application.Current.Resources.MergedDictionaries.Clear();
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.VisualStudio2013;component/Themes/System.Windows.xaml", UriKind.RelativeOrAbsolute)
			});
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.VisualStudio2013;component/Themes/Telerik.Windows.Controls.xaml", UriKind.RelativeOrAbsolute)
			});
			VisualStudio2013Palette.LoadPreset(VisualStudio2013Palette.ColorVariation.Dark);
			this.container.Background = new SolidColorBrush() { Color = Color.FromArgb(255, 45, 45, 48) };
		}

		private void VisualStudio2013_Blue_Checked(object sender, RoutedEventArgs e)
		{
			Application.Current.Resources.MergedDictionaries.Clear();
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.VisualStudio2013;component/Themes/System.Windows.xaml", UriKind.RelativeOrAbsolute)
			});
			Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
			{
				Source = new Uri("/Telerik.Windows.Themes.VisualStudio2013;component/Themes/Telerik.Windows.Controls.xaml", UriKind.RelativeOrAbsolute)
			});
			VisualStudio2013Palette.LoadPreset(VisualStudio2013Palette.ColorVariation.Blue);
			this.container.Background = new SolidColorBrush(Colors.White);
		}
	}
}
