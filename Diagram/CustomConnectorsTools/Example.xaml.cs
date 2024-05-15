using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Diagrams.Core;

namespace CustomConnectorsTool
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();
            ToolService toolService = this.xDiagram.ServiceLocator.GetService<IToolService>() as ToolService;
            if (toolService != null)
            {
                // Adding additional ConnectorAddTool to add connectors on Shift + Ctrl + MouseDown over shape.
                toolService.ToolList.Add(new CustomConnectorAddTool());

                // Replacing the default Connection Tool.
                toolService.ToolList[5] = new CustomConnectionTool();
            }
        }
    }
}
