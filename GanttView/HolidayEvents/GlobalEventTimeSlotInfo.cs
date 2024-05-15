using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.Scheduling;
using Telerik.Windows.Core;

namespace HolidayEvents
{
    public class GlobalEventTimeSlotInfo: TimeSlotInfo
    {
        public GlobalEventTimeSlotInfo(Range<long> timeRange)
			: base(timeRange)
		{
		}

		public override bool Equals(object obj)
		{
            return this.Equals(obj as GlobalEventTimeSlotInfo);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
