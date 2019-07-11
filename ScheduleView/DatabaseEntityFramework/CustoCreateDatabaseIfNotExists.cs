namespace DatabaseEntityFramework
{
    using System.Data.Entity;

    public class CustomCreateDatabaseIfNotExists<TContext> : CreateDatabaseIfNotExists<TContext>
    where TContext : ScheduleViewEntities
    {
        protected override void Seed(TContext context)
        {
            context.Categories.Add(new Category()
            {
                CategoryName = "Yellow Category",
                DisplayName = "Yellow Category",
                CategoryBrushName = "Yellow"
            });

            context.Categories.Add(new Category()
            {
                CategoryName = "Red Category",
                DisplayName = "Red Category",
                CategoryBrushName = "Red"
            });

            context.Categories.Add(new Category()
            {
                CategoryName = "Green Category",
                DisplayName = "Green Category",
                CategoryBrushName = "Green"
            });

            context.TimeMarkers.Add(new TimeMarker()
            {
                TimeMarkerName = "Free",
                TimeMarkerBrushName = "Green"
            });

            context.TimeMarkers.Add(new TimeMarker()
            {
                TimeMarkerName = "Busy",
                TimeMarkerBrushName = "Red"
            });

            context.TimeMarkers.Add(new TimeMarker()
            {
                TimeMarkerName = "Out of Office",
                TimeMarkerBrushName = "Yellow"
            });

            base.Seed(context);
        }
    }
}
