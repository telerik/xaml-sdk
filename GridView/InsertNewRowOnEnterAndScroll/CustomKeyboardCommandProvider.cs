using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace InsertNewRowOnEnterAndScroll
{
	public class CustomKeyboardCommandProvider : DefaultKeyboardCommandProvider
	{
		private GridViewDataControl parentGrid;

		public CustomKeyboardCommandProvider(GridViewDataControl grid)
		 : base(grid)
		{
			this.parentGrid = grid;
		}

		public override IEnumerable<ICommand> ProvideCommandsForKey(Key key)
		{
			List<ICommand> commandsToExecute = base.ProvideCommandsForKey(key).ToList();

			if (key == Key.Enter && this.parentGrid.RowInEditMode is GridViewNewRow)
			{
				commandsToExecute.Clear();
				commandsToExecute.Add(RadGridViewCommands.CommitEdit);
				commandsToExecute.Add(RadGridViewCommands.BeginInsert);
				this.parentGrid.ChildrenOfType<GridViewScrollViewer>().First().ScrollToEnd();
			}

			return commandsToExecute;
		}
	}
}
