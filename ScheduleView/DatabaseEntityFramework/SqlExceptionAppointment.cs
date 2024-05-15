namespace DatabaseEntityFramework
{
    extern alias DataAnnotations;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SqlExceptionAppointment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SqlExceptionAppointment()
        {
            SqlExceptionResources = new HashSet<SqlExceptionResource>();
        }

        [DataAnnotations.System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExceptionId { get; set; }

        [DataAnnotations.System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Subject { get; set; }

        [DataAnnotations.System.ComponentModel.DataAnnotations.StringLength(500)]
        public string Body { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public bool IsAllDayEvent { get; set; }

        [DataAnnotations.System.ComponentModel.DataAnnotations.StringLength(100)]
        public string TimeZoneString { get; set; }

        public int Importance { get; set; }

        public int? TimeMarkerID { get; set; }

        public int? CategoryID { get; set; }

        public virtual Category Category { get; set; }

        public virtual SqlExceptionOccurrence SqlExceptionOccurrence { get; set; }

        public virtual TimeMarker TimeMarker { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SqlExceptionResource> SqlExceptionResources { get; set; }
    }
}
