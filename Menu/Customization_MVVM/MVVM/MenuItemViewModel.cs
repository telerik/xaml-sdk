
namespace Customization_MVVM
{
	public class MenuItemViewModel : MenuViewModel
	{
		public enum MenuItemTypes : uint { Link, SpecialLink, TopLevel, TopLevelSection, Title, Paragraph, Gallery, Image };

		public string Content { get; set; }
		public MenuItemTypes Type { get; set; }

		private bool isSeparator = false;
		public bool IsSeparator
		{
			get
			{
				return isSeparator;
			}
			set
			{
				isSeparator = value;
			}
		}

		public MenuItemViewModel()
		{
		}
	}
}
