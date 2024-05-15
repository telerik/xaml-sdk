using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace MultipleAccessKeys_KeyTipsSL
{
	public partial class MainPage : UserControl
	{
		public MainPage()
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
				keys.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
				keys.Add(new KeyGesture(Key.Shift));
				keys.Add(new KeyGesture(Key.Ctrl));
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
