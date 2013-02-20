using System;
using System.Linq;
using Telerik.Windows.Media.Imaging.Tools.UI;

namespace CustomWatermarkTool
{
    public partial class WatermarkToolSettings : ToolSettingsHeader
    {
        public WatermarkToolSettings()
        {
            InitializeComponent();

            this.opacity.Value = WatermarkTool.DefaultOpacity;
            this.scale.Value = WatermarkTool.DefaultScale;
            this.rotation.Value = WatermarkTool.DefaultRotation;
        }
    }
}
