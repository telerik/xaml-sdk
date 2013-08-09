using System;
using System.Linq;
using Telerik.Windows.Controls;

namespace ToolBarMVVM
{
	public class TextBlockViewModel : ViewModelBase
	{
		public TextBlockViewModel(string text)
		{
			this.Text = text;
		}

		private string text;
		public string Text
		{
			get { return this.text; }
			set
			{
				if (this.text != value)
				{
					this.text = value;
					this.OnPropertyChanged("Text");
				}
			}
		}

	}
}
