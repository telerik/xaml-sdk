using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Diagrams.Core;

namespace CustomTools_SL
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            var toolService = this.diagram.ServiceLocator.GetService<IToolService>() as ToolService;
            if (toolService != null)
            {
                toolService.ToolList[9] = new ForbidNWSEResizing();
                toolService.ToolList.Add(new ShapeTool());
                toolService.ToolList.Add(new ConnectShapesTool());
            }
        }
    }
}
