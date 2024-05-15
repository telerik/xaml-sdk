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
using Telerik.Windows.Controls;

namespace DataBinding_WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}
	}

	public class Page : ViewModelBase
	{
		private string content;

		public Page(string content)
		{
			this.content = content;
		}

		public string Content
		{
			get
			{
				return this.content;
			}
			set
			{
				if (this.content != value)
				{
					this.content = value;
					this.OnPropertyChanged("Content");
				}
			}
		}
	}

	public class PageCollection : ObservableCollection<Page>
	{
		public PageCollection()
		{
			this.Add(new Page("Page 1"));
			this.Add(new Page("Page 2"));
			this.Add(new Page("Page 3"));
			this.Add(new Page("Page 4"));
			this.Add(new Page("Page 5"));
			this.Add(new Page("Page 6"));
			this.Add(new Page("Page 7"));
			this.Add(new Page("Page 8"));
			this.Add(new Page("Page 9"));
			this.Add(new Page("Page 10"));
		}
	}
}
