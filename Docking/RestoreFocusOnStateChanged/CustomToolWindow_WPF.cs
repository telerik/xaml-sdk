using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls.Docking;

namespace RestoreFocusOnStateChanged
{
    public class CustomToolWindow_WPF : ToolWindow
    {
        private IInputElement elementToFocus;

        public CustomToolWindow_WPF(IInputElement elementTofocus)
        {
            this.elementToFocus = elementTofocus;
        }

        protected override void OnDragEnd(Point globalMousePosition, bool isCancel, bool isResize)
        {
            base.OnDragEnd(globalMousePosition, isCancel, isResize);
            Keyboard.Focus(elementToFocus);
            elementToFocus = null;
        }
    }
}
