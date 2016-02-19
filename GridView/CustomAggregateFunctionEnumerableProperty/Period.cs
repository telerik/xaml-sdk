using System;
using System.Linq;

namespace CustomAggregateFunctionEnumerableProperty
{
    public class Period
    {
        public HalfSeason HalfSeason { get; set; }
        public double NumberOfGoals { get; set; }
    }

    public enum HalfSeason
    {
        First,
        Second
    }
}
