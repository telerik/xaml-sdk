using System.Windows.Forms;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Diagrams;

namespace WinForm
{
	public partial class Form1 : Form
	{
		private RadDiagram diagram;
		public Form1()
		{
			StyleManager.ApplicationTheme = new Office_SilverTheme();
			InitializeComponent();

			diagram = new RadDiagram() { Background = System.Windows.Media.Brushes.White, IsBackgroundSurfaceVisible = false };
			elementHost1.Child = diagram;
			var s1 = diagram.AddShape(new RadDiagramShape { Position = new System.Windows.Point(120, 50) });
			var s2 = diagram.AddShape(new RadDiagramShape { Position = new System.Windows.Point(320, 50) });
			var con = diagram.AddConnection(s1, s2) as RadDiagramConnection;
			con.Content = "Connected";
			con.Stroke = System.Windows.Media.Brushes.OrangeRed;

			var info = diagram.AddShape(
				"This is not a XAML form, but a Windows Form hosting RadDiagram.",
				ShapeFactory.GetShapeGeometry(CommonShapeType.RectangleShape),
				new System.Windows.Point(280, 150)) as RadDiagramShape;
			info.Background = System.Windows.Media.Brushes.Transparent;
			info.Stroke = System.Windows.Media.Brushes.Silver;
		}
	}
}
