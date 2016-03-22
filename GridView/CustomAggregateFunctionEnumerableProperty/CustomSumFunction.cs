using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Data;

namespace CustomAggregateFunctionEnumerableProperty
{
    public class CustomSumFunction : EnumerableAggregateFunction
    {
        protected override string AggregateMethodName
        {
            get { return "SumGoalsPerHalfSeason"; }
        }

        protected override Type ExtensionMethodsType
        {
            get
            {
                return typeof(Aggregates);
            }
        }
    }

    public static class Aggregates
    {
        public static string SumGoalsPerHalfSeason<TSource>(IEnumerable<Club> clubs)
        {
            StringBuilder sb = new StringBuilder();
            double sum0 = 0, sum1 = 0;

            foreach (var club in clubs)
            {
                if (club.Period != null )
                {
                    sum0 += club.Period[0].NumberOfGoals;
                    sum1 += club.Period[1].NumberOfGoals;
                }
            }

            sb.Append("1st Half Season Total Goals: ");
            sb.Append(sum0);
            sb.Append(" ");
            sb.AppendLine();
            sb.Append("2nd Half Season Total Goals: ");
            sb.Append(sum1);

            return sb.ToString();
        }
    }
}
