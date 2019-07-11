namespace DatabaseEntityFramework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Migrations;

    public partial class ScheduleViewEntities : DbContext
    {
        public ScheduleViewEntities()
            : base("name=ScheduleViewEntities")
        {
            Database.SetInitializer(new CustomCreateDatabaseIfNotExists<ScheduleViewEntities>());
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<SqlAppointmentResource> SqlAppointmentResources { get; set; }
        public virtual DbSet<SqlAppointment> SqlAppointments { get; set; }
        public virtual DbSet<SqlExceptionAppointment> SqlExceptionAppointments { get; set; }
        public virtual DbSet<SqlExceptionOccurrence> SqlExceptionOccurrences { get; set; }
        public virtual DbSet<SqlExceptionResource> SqlExceptionResources { get; set; }
        public virtual DbSet<SqlResource> SqlResources { get; set; }
        public virtual DbSet<SqlResourceType> SqlResourceTypes { get; set; }
        public virtual DbSet<TimeMarker> TimeMarkers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.DisplayName)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.CategoryBrushName)
                .IsUnicode(false);

            modelBuilder.Entity<SqlAppointment>()
                .Property(e => e.RecurrencePattern)
                .IsUnicode(false);

            modelBuilder.Entity<SqlAppointment>()
                .Property(e => e.TimeZoneString)
                .IsUnicode(false);

            modelBuilder.Entity<SqlAppointment>()
                .HasMany(e => e.SqlAppointmentResources)
                .WithRequired(e => e.SqlAppointment)
                .HasForeignKey(e => e.SqlAppointment_SqlAppointmentId);

            modelBuilder.Entity<SqlAppointment>()
                .HasMany(e => e.SqlExceptionOccurrences)
                .WithRequired(e => e.SqlAppointment)
                .HasForeignKey(e => e.MasterSqlAppointmentId);

            modelBuilder.Entity<SqlExceptionAppointment>()
                .Property(e => e.TimeZoneString)
                .IsUnicode(false);

            modelBuilder.Entity<SqlExceptionAppointment>()
                .HasMany(e => e.SqlExceptionResources)
                .WithRequired(e => e.SqlExceptionAppointment)
                .HasForeignKey(e => e.SqlExceptionAppointment_ExceptionId);

            modelBuilder.Entity<SqlExceptionOccurrence>()
                .HasOptional(e => e.SqlExceptionAppointment)
                .WithRequired(e => e.SqlExceptionOccurrence)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SqlResource>()
                .HasMany(e => e.SqlAppointmentResources)
                .WithRequired(e => e.SqlResource)
                .HasForeignKey(e => e.SqlResource_SqlResourceId);

            modelBuilder.Entity<SqlResource>()
                .HasMany(e => e.SqlExceptionResources)
                .WithRequired(e => e.SqlResource)
                .HasForeignKey(e => e.SqlResource_SqlResourceId);

            modelBuilder.Entity<TimeMarker>()
                .HasMany(e => e.SqlAppointments)
                .WithOptional(e => e.TimeMarker)
                .HasForeignKey(e => e.TimeMarkerID);

            modelBuilder.Entity<TimeMarker>()
                .HasMany(e => e.SqlExceptionAppointments)
                .WithOptional(e => e.TimeMarker)
                .HasForeignKey(e => e.TimeMarkerID);
        }
    }
}
