using System.Collections.Generic;
using System.Windows.Controls;

namespace StylesAndTemplates_TreeMap
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			var datasource = new List<IDiskItem>()
			{
				new Folder("Windows", new List<IDiskItem>()
				{
					new Folder("Fonts", new List<IDiskItem>()
					{
						new File("Arial", 256),
						new File("Tahoma", 246)
					}),
					new Folder("Logs", new List<IDiskItem>()
					{
						new File("Log1.log", 112),
						new File("Log2.log", 156)
					})
				}),
               new Folder ("My Documents", new List<IDiskItem>()
			   {
				   new File("Document1.txt", 88),
				   new File("Document2.txt", 55)
			   }),
               new File("pagefile.sys", 114)
			};

			treeMap1.ItemsSource = datasource;
		}
	}
}
