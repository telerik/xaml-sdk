using System.Windows.Media;
using Telerik.Windows.Controls.HeatMap;

namespace CustomHeatMapSourceAndDefinition
{
    public class CustomHeatMapDefinition : HeatMapDefinition
    {
        private CustomHeatMapSource source;

        public CustomHeatMapDefinition(CustomHeatMapSource source)
        {
            this.source = source;
        }     

        protected override IHeatMapSource Source
        {
            get { return this.source; }
        }
        
        protected override int GetColor(int rowIndex, int columnIndex)
        {
            int color = ToColorInt(this.source.GetColor(rowIndex, columnIndex));
            return color;
        }

        protected override object GetColumnHeader(int index)
        {
            return "Column " + index;
        }

        protected override object GetRowHeader(int index)
        {
            return "Row " + index;
        }

        protected override void OnItemsSourceChanged()
        {            
        }

        private static int ToColorInt(Color color)
        {
            var scaleApha = color.A / 255d;
            return (color.A << 24) | ((byte)(color.R * scaleApha) << 16) | ((byte)(color.G * scaleApha) << 8) | (byte)(color.B * scaleApha);
        }
    }
}
