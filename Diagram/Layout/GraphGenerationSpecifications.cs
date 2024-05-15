using System.Windows.Media;
using Telerik.Windows.Diagrams.Core;

namespace Diagrams.Layout
{
	/// <summary>
	/// Graph generation specifications.
	/// </summary>
	public   class GraphGenerationSpecifications
	{
		/// <summary>
		/// Gets or sets whether the generated graph should be a tree.
		/// </summary>
		public bool TreeGraph { get; set; }

		/// <summary>
		/// Gets or sets whether the generates shapes should have a random with and height.
		/// </summary>
		public bool RandomShapeSize { get; set; }

		/// <summary>
		/// Gets or sets whether the graph should be connected (i.e. not have multiple components).
		/// </summary>
		public bool Connected { get; set; }

		/// <summary>
		/// Gets or sets whether the graph should have (random) connections.
		/// </summary>
		public bool Connections { get; set; }

		/// <summary>
		/// Gets or sets how many nodes should be generated.
		/// </summary>
		public int NodeCount { get; set; }

		/// <summary>
		/// Gets or sets the horizontal separation between siblings.
		/// </summary>
		public double HorizontalSeparation { get; set; }

		/// <summary>
		/// Gets or sets the vertical separation between parents and children.
		/// </summary>
		public double VerticalSeparation { get; set; }

		/// <summary>
		/// Gets or sets the horizontal offset between a parent and its first child (if tip-over tree layout is applied).
		/// </summary>
		public double UnderneathHorizontalOffset { get; set; }

		/// <summary>
		/// Gets or sets the vertical offset between a parent and its first child (if tip-over tree layout is applied).
		/// </summary>
		public double UnderneathVerticalSeparation { get; set; }

		/// <summary>
		/// Gets or sets whether the components are connected when applying radial tree layout.
		/// </summary>
		public bool KeepComponentsInOneRadialLayout { get; set; }

		/// <summary>
		/// Gets or sets the sub-type of the tree (if tree-layout is applied).
		/// </summary>
		public TreeLayoutType TreeType { get; set; }

		/// <summary>
		/// Gets or sets the way shapes are being created.
		/// </summary>
		public GraphExtensions.CreateShapeDelegate CreateShape { get; set; }

		/// <summary>
		/// Gets or sets the highlighting brush.
		/// </summary>
		public Brush HighlightBrush { get; set; }

		/// <summary>
		/// Gets or sets the radial separation.
		/// </summary>
		/// <remarks>This property is only meaningful when use the radial tree layout.</remarks>
		public double RadialSeparation { get; set; }

		/// <summary>
		/// Gets or sets the separation between the root and the first level.
		/// </summary>
		public double RadialFirstLevelSeparation { get; set; }
	}


}