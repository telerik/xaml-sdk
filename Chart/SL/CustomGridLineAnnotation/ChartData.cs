using System;

namespace CustomGridLineAnnotation
{
    public class ChartData
    {
        public DateTime Date { get; set; }
        public int Year
        {
            get
            {
                return Date.Year;
            }
        }
        public int Value { get; set; }
    }
}