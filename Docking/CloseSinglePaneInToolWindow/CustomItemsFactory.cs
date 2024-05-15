using Telerik.Windows.Controls.Docking;

namespace CloseSinglePaneInToolWindow
{
	public class CustomItemsFactory : DefaultGeneratedItemsFactory
	{
		public override ToolWindow CreateToolWindow()
		{
			return new CustomToolWindow();
		}
	}
}
