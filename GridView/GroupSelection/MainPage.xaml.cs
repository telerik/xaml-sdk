using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace GroupSelection_SL
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			RadGridView1.GroupRenderMode = Telerik.Windows.Controls.GridView.GroupRenderMode.Nested;
			List<TestClass> items = new List<TestClass>();
			for (int i = 0; i < 100; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					for (int k = 0; k < 2; k++)
					{
						items.Add(new TestClass()
						{
							MyProperty1 = string.Format("Prop1_{0}", i),
							MyProperty2 = string.Format("Prop2_{0}", j),
							MyProperty3 = string.Format("Prop3_{0}", k),
						});
					}
				}
			}
			DataContext = items;
		}
	}

	public class TestClass
	{
		public string MyProperty1 { get; set; }
		public string MyProperty2 { get; set; }
		public string MyProperty3 { get; set; }
	}
}
