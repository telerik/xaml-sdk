using System;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.BulletGraph;

namespace CreatingABulletGraphProgrammatically
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();

			RadHorizontalBulletGraph horizontalBulletGraph = new RadHorizontalBulletGraph();

			horizontalBulletGraph.AutoRange = false;
			horizontalBulletGraph.Width = 300;
			horizontalBulletGraph.Height = 60;
			horizontalBulletGraph.Minimum = 0;
			horizontalBulletGraph.Maximum = 100;
			horizontalBulletGraph.FeaturedMeasure = 60d;
			horizontalBulletGraph.ComparativeMeasure = 65d;
			horizontalBulletGraph.ProjectedValue = 75d;
			QualitativeRange range = new QualitativeRange();
			range.Value = 50;
			range.Brush = CombineAlphaAndColorInSolidColorBrush(1, "#A8A8A8");
			horizontalBulletGraph.QualitativeRanges.Add(range);
			QualitativeRange range1 = new QualitativeRange();
			range1.Value = 70;
			range1.Brush = CombineAlphaAndColorInSolidColorBrush(1, "#C6C8C8");
			horizontalBulletGraph.QualitativeRanges.Add(range1);
			QualitativeRange range2 = new QualitativeRange();
			range2.Brush = CombineAlphaAndColorInSolidColorBrush(1, "#E8E8E8");
			horizontalBulletGraph.QualitativeRanges.Add(range2);
			this.LayoutRoot.Children.Add(horizontalBulletGraph);
		}

		/// <summary>
		/// adds the alpha (opacity) value to the front of the color string
		/// </summary>
		/// <param name="opacity"></param>
		/// <param name="color"></param>
		/// <returns></returns>
		protected static SolidColorBrush CombineAlphaAndColorInSolidColorBrush(double opacity, string color)
		{
			SolidColorBrush theAnswer = new SolidColorBrush();
			// deal with opacity

			if (opacity > 1.0)
				opacity = 1.0;

			if (opacity < 0.0)
				opacity = 0.0;

			// get the hex value of the alpha chanel (opacity):
			byte a = (byte)(Convert.ToInt32(255 * opacity));
			// deal with the color

			try
			{
				byte r = (byte)(Convert.ToUInt32(color.Substring(1, 2), 16));
				byte g = (byte)(Convert.ToUInt32(color.Substring(3, 2), 16));
				byte b = (byte)(Convert.ToUInt32(color.Substring(5, 2), 16));

				theAnswer.Color = Color.FromArgb(a, r, g, b);
			}
			catch
			{
				theAnswer.Color = Color.FromArgb(255, 255, 0, 0);
			}

			return theAnswer;
		}
	}
}
