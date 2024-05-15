using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using RowDetailsTemplateSelector;
using Telerik.Windows.Controls;

namespace RowDetailsTemplateSelector
{
	public class MyCustomRowDetailsTemplateSelector : DataTemplateSelector
	{
		public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
		{
			if (item is Club)
			{
				Club club = item as Club;
				if (club.StadiumCapacity > 50000)
				{
					return bigStadium;
				}
				else
				{
					return smallStadium;
				}
			}
			return null;
		}
		public DataTemplate bigStadium { get; set; }
		public DataTemplate smallStadium { get; set; }
	}
}
