namespace ControlShape
{
	/// <summary>
	/// Taken from Telerik's help pages http://www.telerik.com/help/wpf/radchart-getting-started-create-data-bound-chart.html.
	/// </summary>
	public class ProductSales
	{
		public ProductSales(int quantity, int month, string monthName)
		{
			this.Quantity = quantity;
			this.Month = month;
			this.MonthName = monthName;
		}

		public int Quantity { get; set; }
		public int Month { get; set; }
		public string MonthName { get; set; }
	}
}