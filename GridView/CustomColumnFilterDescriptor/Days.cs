using System;

namespace CustomColumnFilterDescriptor
{
	[Flags]
	public enum Days
	{
		None = 0x0,
		Sunday = 0x1,
		Monday = 0x2,
		Tuesday = 0x4,
		Wednesday = 0x8,
		Thursday = 0x10,
		Friday = 0x20,
		Saturday = 0x40
	}
}
