using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace DifferentToolWindowStyles
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }


        private void OnToolWindowCreated(object sender, Telerik.Windows.Controls.Docking.ElementCreatedEventArgs e)
        {
            var toolWindow = e.CreatedElement as ToolWindow;
            var pane = e.SourceElement as RadPane;
            var paneGroup = e.SourceElement as RadPaneGroup;
            object tag = null;
            if (pane == null && paneGroup != null)
            {
                pane = paneGroup.EnumeratePanes().FirstOrDefault();
            }
            else
            {
                if (pane != null)
                {
                    tag = pane.Tag;
                }
                else
                {
                    var splitContainer = e.SourceElement as RadSplitContainer;
                    if (splitContainer != null)
                    {
                        tag = splitContainer.Tag;
                    }
                }
            }

            SetToolWindowStyle(toolWindow, tag != null ? tag : pane.Tag);
        }

        private static void SetToolWindowStyle(ToolWindow toolWindow, object tag)
        {
            if (tag != null)
            {
                var styleName = string.Format("{0}ToolWindowStyle", tag);
                var style = Application.Current.Resources[styleName] as Style;
                if (style != null)
                {
                    toolWindow.Style = style;
                }
            }
        }

        private void OnPaneGroupCreated(object sender, ElementCreatedEventArgs e)
        {
            // Add any logic here for related to when the auto created RadPaneGrup instances are created.
        }

        private void OnSplitContainerCreated(object sender, ElementCreatedEventArgs e)
        {
            // Presists the Tag property of RadSplitContainer.
            var oldSplitContainer = e.SourceElement as RadSplitContainer;
            var newSplitContainer = e.CreatedElement as RadSplitContainer;
            if (oldSplitContainer != null && newSplitContainer != null)
            {
                newSplitContainer.Tag = oldSplitContainer.Tag;
            }
        }
    }
}
