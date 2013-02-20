using System;

namespace Diagrams.Layout
{
	public sealed class SampleItem
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public Action<Telerik.Windows.Controls.RadDiagram, GraphGenerationSpecifications> Run { get; set; }
	}
}