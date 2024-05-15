using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace CloseSinglePaneInToolWindow
{
	public class CustomToolWindow : ToolWindow
	{
		protected override bool OnClosing()
		{
			if (this.Content != null)
			{
				var group = (this.Content as RadSplitContainer).Items[0] as RadPaneGroup;
				if (group.Items.Count > 1)
				{
					group.SelectedPane.IsHidden = true;
					return false;
				}
			}

			return true;
		}
	}
}
