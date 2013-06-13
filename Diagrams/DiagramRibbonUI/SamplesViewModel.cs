using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace DiagramFirstLookDemo
{
	public class SamplesViewModel : ViewModelBase
	{
		private SampleItem selectedSample;
		private readonly RadDiagram diagram;

		public SamplesViewModel(RadDiagram diagram)
		{
			this.diagram = diagram;
			this.Samples = SamplesFactory.GetSamples(diagram);
			this.SelectedSample = this.Samples.Last();
		}

		public IEnumerable<SampleItem> Samples
		{
			get;
			private set;
		}

		public SampleItem SelectedSample
		{
			get { return selectedSample; }
			set
			{
				if (selectedSample != value)
				{
					selectedSample = value;
					OnPropertyChanged("SelectedSample");

					if (selectedSample != null)
					{
						selectedSample.Run(this.diagram);
					}
				}
			}
		}
	}
}
