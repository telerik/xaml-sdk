using System;
using System.Windows.Data;
using System.Windows.Media;
using OrgChart.ViewModels;

namespace OrgChart.Converters
{
	public class ShapeBackgroundSelector : IValueConverter
	{
		public Brush TopManagementBrush
		{
			get;
			set;
		}

		public Brush DevelopmentBrush
		{
			get;
			set;
		}

		public Brush QABrush
		{
			get;
			set;
		}

		public Brush SalesBrush
		{
			get;
			set;
		}

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			Branch branch = (Branch)value;
			switch (branch)
			{
				case Branch.TopManagement:
					return this.TopManagementBrush;
				case Branch.Development:
					return this.DevelopmentBrush;
				case Branch.Sales:
					return this.SalesBrush;
				case Branch.QA:
					return this.QABrush;
				default:
					return null;
			}	
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
