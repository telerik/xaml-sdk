using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InteractivityEffects
{
	public class DataObject
	{
		private static Random rand = new Random(0);
		private static Random adjrand = new Random(rand.Next());
		private double _y1;
		private double _y2;
		private double _y3;
		private double _y4;
		private double _y5;

		public double Y1
		{
			get
			{
				return _y1;
			}
			set
			{
				_y1 = value;
			}
		}

		public double Y2
		{
			get
			{
				return _y2;
			}
			set
			{
				_y2 = value;
			}
		}

		public double Y3
		{
			get
			{
				return _y3;
			}
			set
			{
				_y3 = value;
			}
		}

		public double Y4
		{
			get
			{
				return _y4;
			}
			set
			{
				_y4 = value;
			}
		}

		public double Y5
		{
			get
			{
				return _y5;
			}
			set
			{
				_y5 = value;
			}
		}

		public DataObject(double y1, double y2, double y3, double y4, double y5)
		{
			this.Y1 = y1;
			this.Y2 = y2;
			this.Y3 = y3;
			this.Y4 = y4;
			this.Y5 = y5;
		}

		public static List<DataObject> GetData()
		{
			List<DataObject> list = new List<DataObject>();

			for (int i = 0; i < 17; i++)
			{
				double y1 = rand.Next(10, 100);
				list.Add(new DataObject(y1, y1 + adjrand.Next(-10, 10), y1 + adjrand.Next(-5, 5), y1 + adjrand.Next(40, 75), y1 + adjrand.Next(20, 50)));
			}
			return list;
		}
	}
}
