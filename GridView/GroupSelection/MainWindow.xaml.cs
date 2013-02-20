using System;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace WpfApplication1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
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
