using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls.Docking;

namespace RestoreFocusOnStateChanged
{
    public class CustomGeneratedItemsFactory_SL : DefaultGeneratedItemsFactory
    {
        public override ToolWindow CreateToolWindow()
        {
            var focusedElement = FocusManager.GetFocusedElement() as Control;
            var toolWindow = new CustomToolWindow_SL(focusedElement);

            return toolWindow;
        }
    }
}
