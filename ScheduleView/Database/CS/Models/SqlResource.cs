using System.ComponentModel;
using Telerik.Windows.Controls;

namespace ScheduleViewDB
{
	public partial class SqlResource : IResource
	{
		public string ResourceType
		{
			get
			{
				return this.SqlResourceType.Name;
			}
			set
			{
				if (this.SqlResourceType.Name != value)
				{
					this.SqlResourceType.Name = value;
					this.OnPropertyChanged("ResourceType");
				}
			}
		}

		public override string ToString()
		{
			return this.DisplayName;
		}

		public bool Equals(IResource other)
		{
			return other != null && other.ResourceName == this.ResourceName && other.ResourceType == this.ResourceType;
		}
	}
}
