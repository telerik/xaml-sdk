using System.Windows.Media;
using Telerik.Windows.Controls;
using ScheduleViewDB.Helpers;

namespace ScheduleViewDB
{
	public partial class Category : ICategory
	{
		private Brush categoryBrush;
		public Brush CategoryBrush
		{
			get
			{
				if (this.categoryBrush == null)
				{
					this.categoryBrush = SolidColorBrushHelper.FromNameString(this.CategoryBrushName);
				}

				return this.categoryBrush;
			}
			set
			{
				this.CategoryBrushName = (this.categoryBrush as SolidColorBrush).Color.ToString().Substring(1);
				this.categoryBrush = value;
			}
		}

		public bool Equals(ICategory other)
		{
			return this.DisplayName == other.DisplayName &&
				this.CategoryName == other.CategoryName;
		}
	}
}
