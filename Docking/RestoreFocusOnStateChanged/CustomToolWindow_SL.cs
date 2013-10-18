using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace RestoreFocusOnStateChanged
{
    class CustomToolWindow_SL : ToolWindow
    {
        private Control elementToFocus;

        public CustomToolWindow_SL(Control elementTofocus)
        {
            this.elementToFocus = elementTofocus;
        }

        protected override void OnDragEnd(Point globalMousePosition, bool isCancel, bool isResize)
        {
            base.OnDragEnd(globalMousePosition, isCancel, isResize);
            if (elementToFocus != null)
            {
                HtmlPage.Plugin.Focus();
                elementToFocus.Focus();
                elementToFocus = null;
            }
        }
    }
}
