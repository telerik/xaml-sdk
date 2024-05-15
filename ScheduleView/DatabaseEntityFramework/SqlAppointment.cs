namespace DatabaseEntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Telerik.Windows.Controls.ScheduleView;

    public partial class SqlAppointment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SqlAppointment()
        {
            SqlAppointmentResources = new HashSet<SqlAppointmentResource>();
            SqlExceptionOccurrences = new HashSet<SqlExceptionOccurrence>();
        }

        [Key]
        public int SqlAppointmentId { get; set; }

        [StringLength(100)]
        public string Subject { get; set; }

        [StringLength(500)]
        public string Body { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public bool IsAllDayEvent { get; set; }

        [StringLength(100)]
        public string RecurrencePattern { get; set; }

        [StringLength(100)]
        public string TimeZoneString { get; set; }

        public int Importance { get; set; }

        public int? TimeMarkerID { get; set; }

        public int? CategoryID { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SqlAppointmentResource> SqlAppointmentResources { get; set; }

        public virtual TimeMarker TimeMarker { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SqlExceptionOccurrence> SqlExceptionOccurrences { get; set; }
    }
}
