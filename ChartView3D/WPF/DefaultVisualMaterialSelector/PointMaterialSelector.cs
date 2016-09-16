using System.Windows.Media;
using System.Windows.Media.Media3D;
using Telerik.Charting;
using Telerik.Windows.Controls.ChartView;

namespace DefaultVisualMaterialSelector
{
    public class PointMaterialSelector : MaterialSelector
    {
        public override Material SelectMaterial(object context)
        {
            XyzDataPoint3D dp = (XyzDataPoint3D)context;
            PlotInfo info = (PlotInfo)dp.DataItem;
            Material material = CreateMaterialByCategory(info.ZValue);
            return material;            
        }

        public Material CreateMaterialByCategory(string category)
        {
            Color color;
            switch (category)
            {
                case "A":
                    color = (Color)ColorConverter.ConvertFromString("#007ACC");
                    break;
                case "B":
                    color = (Color)ColorConverter.ConvertFromString("#68217A");
                    break;                
                default:
                    color = (Color)ColorConverter.ConvertFromString("#DD4C40"); ;
                    break;
            }

            DiffuseMaterial material = new DiffuseMaterial(new SolidColorBrush(color));            
            return material;
        }
    }
}
