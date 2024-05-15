using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.GridView;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace WpfApplication1
{
    class KeyboardCommandProvider : DefaultKeyboardCommandProvider
    {
        private GridViewDataControl parentGrid;
        private DefaultKeyboardCommandProvider defaultKeyboardProvider;
        private KeyboardCommandProvider customKeyboardProvider;
        public KeyboardCommandProvider(GridViewDataControl grid)
            : base(grid)
        {
            this.parentGrid = grid;
        }
        public override IEnumerable<ICommand> ProvideCommandsForKey(Key key)
        {
            List<ICommand> commandsToExecute = base.ProvideCommandsForKey(key).ToList();
            if (key == Key.Enter)
            {
                commandsToExecute.Clear();
                commandsToExecute.Add(RadGridViewCommands.CommitEdit);
                commandsToExecute.Add(RadGridViewCommands.MoveNext);
                commandsToExecute.Add(RadGridViewCommands.BeginEdit);
            }


            return commandsToExecute;
        }
    }
}
