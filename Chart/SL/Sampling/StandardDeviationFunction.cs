using System;
using Telerik.Windows.Data;

namespace Sampling
{
	public class StandardDeviationFunction : EnumerableSelectorAggregateFunction
	{
		protected override string AggregateMethodName
		{
			get
			{
				return "StdDev";
			}
		}

		protected override Type ExtensionMethodsType
		{
			get
			{
				return typeof(Statistics);
			}
		}
	}
}
