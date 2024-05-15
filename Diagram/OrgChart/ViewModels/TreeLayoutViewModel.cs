using System;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Diagrams.Core;

namespace OrgChart.ViewModels
{
	public class TreeLayoutViewModel : ViewModelBase
	{
		private TreeLayoutType currentTreeLayoutType = TreeLayoutType.TreeDown;
		private TreeLayoutSettings currentLayoutSetting;

		public TreeLayoutViewModel()
		{
			this.CurrentLayoutSettings = new TreeLayoutSettings()
			{
				HorizontalSeparation = 50d,
				UnderneathVerticalTopOffset = 50d,
				UnderneathHorizontalOffset = 80d,
				ComponentsGridWidth = 2000d,
				UnderneathVerticalSeparation = 100,
				AnimateTransitions = true,
			};
		}

		public event EventHandler LayoutSettingsChanged;

		public TreeLayoutSettings CurrentLayoutSettings
		{
			get
			{
				return this.currentLayoutSetting;
			}
			set
			{
				if (this.currentLayoutSetting != value)
				{
					this.currentLayoutSetting = value;
					this.OnPropertyChanged("CurrentLayoutSettings");
				}
			}
		}

		public double UnderneathHorizontalOffset
		{
			get
			{
				return this.CurrentLayoutSettings.UnderneathHorizontalOffset;
			}
			set
			{
				if (this.CurrentLayoutSettings.UnderneathHorizontalOffset != value)
				{
					this.CurrentLayoutSettings.UnderneathHorizontalOffset = value;
					this.OnPropertyChanged("UnderneathHorizontalOffset");
					this.OnLayoutSettingsChanged();
				}
			}
		}

		public double UnderneathVerticalTopOffset
		{
			get
			{
				return this.CurrentLayoutSettings.UnderneathVerticalTopOffset;
			}
			set
			{
				if (this.CurrentLayoutSettings.UnderneathVerticalTopOffset != value)
				{
					this.CurrentLayoutSettings.UnderneathVerticalTopOffset = value;
					this.OnPropertyChanged("UnderneathVerticalTopOffset");
					this.OnLayoutSettingsChanged();
				}
			}
		}

		public double HorizontalSeparation
		{
			get
			{
				return this.CurrentLayoutSettings.HorizontalSeparation;
			}
			set
			{
				if (this.CurrentLayoutSettings.HorizontalSeparation != value)
				{
					this.CurrentLayoutSettings.HorizontalSeparation = value;
					this.OnPropertyChanged("HorizontalSeparation");
					this.OnLayoutSettingsChanged();
				}
			}
		}

		public double VerticalSeparation
		{
			get
			{
				return this.CurrentLayoutSettings.VerticalSeparation;
			}
			set
			{
				if (this.CurrentLayoutSettings.VerticalSeparation != value)
				{
					this.CurrentLayoutSettings.VerticalSeparation = value;
					this.OnPropertyChanged("VerticalSeparation");
					this.OnLayoutSettingsChanged();
				}
			}
		}

		public TreeLayoutType CurrentTreeLayoutType
		{
			get
			{
				return this.currentTreeLayoutType;
			}
			set
			{
				if (this.currentTreeLayoutType != value)
				{
					this.currentTreeLayoutType = value;
					this.CurrentLayoutSettings.TreeLayoutType = value;
					this.OnPropertyChanged("CurrentTreeLayoutType");
				}
			}
		}

		public bool ShouldAnimateTransitions
		{
			get
			{
				return this.CurrentLayoutSettings.AnimateTransitions;
			}
			set
			{
				if (this.CurrentLayoutSettings.AnimateTransitions != value)
				{
					this.CurrentLayoutSettings.AnimateTransitions = value;
					this.OnPropertyChanged("ShouldAnimateTransitions");
					this.OnLayoutSettingsChanged();
				}
			}
		}

		private void OnLayoutSettingsChanged()
		{
			if (this.LayoutSettingsChanged != null)
			{
				this.LayoutSettingsChanged(this, EventArgs.Empty);
			}
		}
	}
}
