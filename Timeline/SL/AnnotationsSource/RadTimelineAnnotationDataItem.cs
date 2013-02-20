using System;

namespace AnnotationsSource
{
	public class RadTimelineAnnotationDataItem
	{
		public DateTime StartDate { get; set; }

		public TimeSpan Duration { get; set; }

		public string Content { get; set; }

		public int ZIndex { get; set; }
	}
}
