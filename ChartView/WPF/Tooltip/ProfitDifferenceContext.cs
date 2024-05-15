namespace Tooltip
{
	public class ProfitDifferenceContext
	{
		public string Quarter { get; set; }

		public double Profit { get; set; }

		public object PreviousQuarter { get; set; }

		public double PreviousDifference { get; set; }

		public object NextQuarter { get; set; }

		public double NextDifference { get; set; }
	}
}