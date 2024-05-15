namespace DatabaseEntityFramework
{
    extern alias DataAnnotations;

    using System;
    using System.Collections.Generic;

    public partial class SqlAppointment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SqlAppointment()
        {
            SqlAppointmentResources = new HashSet<SqlAppointmentResource>();
            SqlExceptionOccurrences = new HashSet<SqlExceptionOccurrence>();
        }

        [DataAnnotations.System.ComponentModel.DataAnnotations.Key]
        public int SqlAppointmentId { get; set; }

        [DataAnnotations.System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Subject { get; set; }

        [DataAnnotations.System.ComponentModel.DataAnnotations.StringLength(500)]
        public string Body { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public bool IsAllDayEvent { get; set; }

        [DataAnnotations.System.ComponentModel.DataAnnotations.StringLength(100)]
        public string RecurrencePattern { get; set; }

        [DataAnnotations.System.ComponentModel.DataAnnotations.StringLength(100)]
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
