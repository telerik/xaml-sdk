using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;

namespace ToolBarMVVM
{
	public class ButtonViewModel : ViewModelBase
	{
		public ButtonViewModel(string content, string tooltip)
		{
			this.ToolTipContent = tooltip;
			this.Content = content;
			this.InfoCommand = new DelegateCommand(x => MessageBox.Show("Colors Saved!"));
		}

		private string tooltip;
		public string ToolTipContent
		{
			get { return this.tooltip; }
			set
			{
				if (this.tooltip != value)
				{
					this.tooltip = value;
					this.OnPropertyChanged("ToolTipContent");
				}
			}
		}


		private string content;

		public string Content
		{
			get { return this.content; }
			set
			{
				if (this.content != value)
				{
					this.content = value;
					this.OnPropertyChanged("Content");
				}
			}
		}

		public DelegateCommand InfoCommand { get; set; }

	}
}
