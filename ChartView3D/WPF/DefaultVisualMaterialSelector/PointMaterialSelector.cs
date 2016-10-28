using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Telerik.Charting;
using Telerik.Windows.Controls.ChartView;

namespace DefaultVisualMaterialSelector
{
    public class PointMaterialSelector : MaterialSelector
    {
        private Dictionary<Color, Material> colorToMaterialDict = new Dictionary<Color, Material>();

        public override Material SelectMaterial(object context)
        {
            XyzDataPoint3D dp = (XyzDataPoint3D)context;
            PlotInfo info = (PlotInfo)dp.DataItem;
            Material material = GetOrCreateMaterial(info.ZValue);
            return material;            
        }

        public Material GetOrCreateMaterial(string category)
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

            return GetOrCreateMaterial(color);
        }
                
        private Material GetOrCreateMaterial(Color color)
        {
            // Reuse and freeze material for better performance.
            Material material;
            if (!this.colorToMaterialDict.TryGetValue(color, out material))
            {
                material = new DiffuseMaterial(new SolidColorBrush(color));
                material.Freeze();
                this.colorToMaterialDict[color] = material;
            }

            return material;
        }
    }
}
