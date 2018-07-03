using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorBars
{
    public class PlotInfo
    {
        public string Category { get; set; }
        public double Value { get; set; }
        public double ErrorHigh { get; set; }
        public double ErrorLow { get; set; }
    }
}
