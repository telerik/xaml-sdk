using System;
using System.Linq;
using System.Windows.Media;
using Telerik.Windows.Controls.Diagrams.Extensions.ViewModels;

namespace CustomSettingsPane.ViewModels
{
	public class ShapeViewModel : NodeViewModelBase
	{
		private Geometry geometry;
		public Geometry Geometry
		{
			get { return this.geometry; }
			set
			{
				if (this.geometry != value)
				{
					this.geometry = value;
					this.OnPropertyChanged("Geometry");
				}
			}
		}
		
		private string shapeName;
		public string ShapeName
		{
			get { return this.shapeName; }
			set
			{
				if (this.shapeName != value)
				{
					this.shapeName = value;
					this.OnPropertyChanged("ShapeName");
				}
			}
		}		

		/// <summary>
		/// Prevents displaying the ViewModel Name in the Shape's Content.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return "";
		}

		public string ID { get; set; }
	}
}