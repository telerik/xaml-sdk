using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MultipleAccessKeys_KeyTips
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = new MainViewModel();
		}

		public class MainViewModel
		{
			ObservableCollection<KeyGesture> accessKeys;
			public MainViewModel()
			{
				this.AccessKeys = this.GenerateAccessKeys();
			}

			private ObservableCollection<KeyGesture> GenerateAccessKeys()
			{
				var keys = new ObservableCollection<KeyGesture>();
				keys.Add(new KeyGesture(Key.Q,ModifierKeys.Control));
				keys.Add(new KeyGesture(Key.LeftAlt));
				keys.Add(new KeyGesture(Key.RightAlt));
				return keys;
			}

			public ObservableCollection<KeyGesture> AccessKeys
			{
				get
				{
					return this.accessKeys;
				}
				set
				{
					this.accessKeys = value;
				}
			}
		}
	}
}
