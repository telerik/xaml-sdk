using System;

namespace AnnotationsZIndex
{
	public class RadTimelineAnnotationDataItem
	{
		public DateTime StartDate { get; set; }

		public TimeSpan Duration { get; set; }

		public string Content { get; set; }

		public int ZIndex { get; set; }
	}
}
