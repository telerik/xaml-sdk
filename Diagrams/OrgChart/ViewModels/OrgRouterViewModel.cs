using System;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Diagrams.Core;

namespace OrgChart.ViewModels
{
	public class OrgRouterViewModel : ViewModelBase
	{
		private TreeLayoutType currentTreeLayoutType;
		private double connectionOuterSpacing;

		public OrgRouterViewModel(OrgTreeRouter router)
		{
			if (router == null)
				throw new ArgumentException("router");
			this.Router = router;
			this.Router.TreeLayoutType = TreeLayoutType.TreeDown;
			this.ConnectionOuterSpacing = 20d;
		}

		public event EventHandler RouterSettingChanged;

		public OrgTreeRouter Router { get; private set; }

		public TreeLayoutType CurrentTreeLayoutType
		{
			get { return this.currentTreeLayoutType; }
			set
			{
				if (this.currentTreeLayoutType != value)
				{
					this.currentTreeLayoutType = value;
					this.Router.TreeLayoutType = value;
					this.OnPropertyChanged("CurrentTreeLayoutType");
				}
			}
		}

		public double ConnectionOuterSpacing
		{
			get { return this.connectionOuterSpacing; }
			set
			{
				this.connectionOuterSpacing = value;
				this.Router.ConnectionOuterSpacing = value;
				this.OnPropertyChanged("ConnectionOuterSpacing");
			}
		}
	}
}
