using System;
using System.Collections.Generic;
using System.Linq;

namespace AnnotationsSource
{
    public class RadTimelineAnnotationsViewModel
    {
        public RadTimelineAnnotationsViewModel()
        {
            this.PeriodStart = new DateTime(2011, 1, 1);
            this.PeriodEnd = new DateTime(2012, 1, 1);

            this.GenerateTimelineData();
            this.GenerateTimelineAnnotationsData();
        }

        public DateTime PeriodStart { get; set; }

        public DateTime PeriodEnd { get; set; }

        public List<RadTimelineDataItem> TimelineItems { get; set; }

        public List<RadTimelineAnnotationDataItem> TimelineAnnotationItems { get; set; }

        private void GenerateTimelineData()
        {
            Random r = new Random();
            List<RadTimelineDataItem> items = new List<RadTimelineDataItem>();

            for (DateTime date = this.PeriodStart; date < this.PeriodEnd; date = date.AddDays(1))
            {
                items.Add(new RadTimelineDataItem() { StartDate = date, Duration = TimeSpan.FromDays(r.Next(5, 10)) });
            }

            this.TimelineItems = items;
        }

        private void GenerateTimelineAnnotationsData()
        {
            Random r = new Random();
            List<RadTimelineAnnotationDataItem> items = new List<RadTimelineAnnotationDataItem>();

            for (DateTime date = this.PeriodStart; date < this.PeriodEnd; date = date.AddDays(7))
            {
                items.Add(new RadTimelineAnnotationDataItem(){
                    StartDate = date,
                    Duration = TimeSpan.FromDays(r.Next(2, 5)),
                    Content = date.ToShortDateString(),
                    ZIndex = r.Next(0, 300)
                });
            }

            this.TimelineAnnotationItems = items;
        }
    }
}
