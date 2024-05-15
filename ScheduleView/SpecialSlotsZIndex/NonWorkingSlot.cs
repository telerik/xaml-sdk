using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;

namespace SpecialSlotsZIndex
{
	public class NonWorkingSlot : Slot
	{
		public NonWorkingSlot(DateTime start, DateTime end)
			: base(start, end)
		{
			
		}

		public override Slot Copy()
		{
			Slot slot = new NonWorkingSlot(this.Start, this.End);
			slot.CopyFrom(this);
			return slot;
		}

        private int zValueIndex = 1;

        public int ZValueIndex
        {
            get 
            {
                return zValueIndex; 
            }
            set 
            { 
                zValueIndex = value;
                this.OnPropertyChanged(() => this.ZValueIndex);
            }
        }
	}
}
