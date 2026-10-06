using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Spreadsheet.Layers;
using Telerik.Windows.Controls.Spreadsheet.Worksheets.Layers;

namespace CustomCellEditLayer
{
    public class CustomLayersBuilder : WorksheetUILayersBuilder
    {
        private readonly RadSpreadsheet radSpreadsheet;
        private CustomCellEditUILayer cellEditorLayer;

        public CustomLayersBuilder(RadSpreadsheet radSpreadsheet)
        {
            this.radSpreadsheet = radSpreadsheet;
        }

        public override void BuildUILayers(UILayerStack<WorksheetUILayerBase> uiLayers)
        {
            this.cellEditorLayer = new CustomCellEditUILayer(this.radSpreadsheet);
            base.BuildUILayers(uiLayers);
            uiLayers.Remove(this.cellEditorLayer);
            uiLayers.AddLast(this.cellEditorLayer);
        }

        public RadSpreadsheet RadSpreadsheet
        {
            get
            {
                return this.radSpreadsheet;
            }
        }
    }
}
