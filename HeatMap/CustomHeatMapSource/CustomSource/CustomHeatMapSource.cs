using System.Collections;
using System.Windows.Media;
using Telerik.Windows.Controls.HeatMap;

namespace CustomHeatMapSourceAndDefinition
{
    public class CustomHeatMapSource : IHeatMapSource
    {
        private CustomHeatMapItem[,] array;

        public CustomHeatMapSource(CustomHeatMapItem[,] array)
        {
            this.array = array;
        }

        public IEnumerable ItemsSource
        {
            get
            {
                return this.array;
            }
            set
            {
                this.array = (CustomHeatMapItem[,])value;
            }
        }

        public int RowsCount
        {
            get
            {
                return this.array != null ? this.array.GetLength(0) : 0;
            }
        }

        public int ColumnsCount
        {
            get
            {
                return this.array != null ? this.array.GetLength(1) : 0;
            }
        }

        public object GetDataItem(int rowIndex, int columnIndex)
        {
            return this.array != null ? this.array[rowIndex, columnIndex] : null;
        }

        public double GetValue(int rowIndex, int columnIndex)
        {
            return this.array[rowIndex, columnIndex].Value;
        }

        public Color GetColor(int rowIndex, int columnIndex)
        {
            return this.array[rowIndex, columnIndex].Color;
        }

        public void Dispose()
        {
            this.array = null;
        }
    }
}
