namespace DataBindingWithAutomaticSeriesMappings
{
	public class Manufacturer
	{
		public Manufacturer(string name, int sales, int turnover)
		{
			this.Name = name;
			this.Sales = sales;
			this.Turnover = turnover;
		}
		public string Name
		{
			get;
			set;
		}
		public int Sales
		{
			get;
			set;
		}
		public int Turnover
		{
			get;
			set;
		}
	}
}
