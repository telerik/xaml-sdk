using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.Data.PropertyGrid;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Data;
using System.Windows.Input;

namespace KeyboardCommandProvider
{
   public class CustomKeyboardCommandProvider : PropertyGridCommandProvider
    {
        public CustomKeyboardCommandProvider()
            : base(null)
        {

        }

        public CustomKeyboardCommandProvider(RadPropertyGrid propertyGrid)
            : base(propertyGrid)
        {
            this.PropertyGrid = propertyGrid;
        }
        public override List<DelegateCommandWrapper> ProvideCommandsForKey(KeyEventArgs args)
        {
            List<DelegateCommandWrapper> actionsToExecute = base.ProvideCommandsForKey(args);
            if (args.Key == Key.Tab)
            {
                actionsToExecute.Clear();
                actionsToExecute.Add(new PropertyGridDelegateCommandWrapper(RadPropertyGridCommands.MoveToNext, this.PropertyGrid));
                if (this.PropertyGrid.SelectedPropertyDefinition != null)
                {
                    if (!this.PropertyGrid.SelectedPropertyDefinition.IsExpanded)
                    {
                        actionsToExecute.Add(new PropertyGridDelegateCommandWrapper(RadPropertyGridCommands.ExpandCurrentField, this.PropertyGrid));
                    }
                }
            }
            if (args.Key == Key.Tab && Keyboard.Modifiers == ModifierKeys.Shift)
            {
                actionsToExecute.Clear();
                actionsToExecute.Add(new PropertyGridDelegateCommandWrapper(RadPropertyGridCommands.MoveToPrevious, this.PropertyGrid));
                if (this.PropertyGrid.SelectedPropertyDefinitions != null)
                {
                    if (!this.PropertyGrid.SelectedPropertyDefinition.IsExpanded)
                    {
                        actionsToExecute.Add(new PropertyGridDelegateCommandWrapper(RadPropertyGridCommands.ExpandCurrentField, this.PropertyGrid));
                    }
                }
            }

            return actionsToExecute;
        }
    }
}
