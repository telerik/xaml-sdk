using System.Windows;
using System.Windows.Controls;

namespace Axis
{
	public class BinaryTemplateSelector : DataTemplateSelector
	{
		public DataTemplate Template1 { get; set; }

		public DataTemplate Template2 { get; set; }
		
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			DataTemplate tmp = this.Template1;
			this.Template1 = this.Template2;
			this.Template2 = tmp;
			return tmp;
		}
	}
}