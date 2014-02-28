using System.Linq;
using Telerik.Windows.Controls;

namespace ScheduleViewDB.Web
{
	public partial class SqlResourceType : IResourceType
	{
		public System.Collections.IList Resources
		{
			get { return this.SqlResources.ToList(); }
		}
	}
}
