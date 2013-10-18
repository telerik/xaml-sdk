using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls.Docking;

namespace RestoreFocusOnStateChanged
{
    public class CustomGeneratedItemsFactory_WPF : DefaultGeneratedItemsFactory
    {
        public override ToolWindow CreateToolWindow()
        {
            var focusedElement = FocusManager.GetFocusedElement(Application.Current.MainWindow);

            var toolWindow = new CustomToolWindow_WPF(focusedElement);
            return toolWindow;
        }
    }
}
