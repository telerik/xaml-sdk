using System.Collections.Generic;
using System.Linq;

namespace StackedBars3D
{
    public class PlotInfo
    {
        public string XCategory { get; set; }
        public string YCategory { get; set; }
        public List<double> StackedZValues { get; set; }

        public double ZValue
        {
            get { return this.StackedZValues.Sum(); }
        }
    }
}
