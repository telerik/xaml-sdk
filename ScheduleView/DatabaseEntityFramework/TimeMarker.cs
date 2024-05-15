namespace DatabaseEntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TimeMarker
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimeMarker()
        {
            SqlAppointments = new HashSet<SqlAppointment>();
            SqlExceptionAppointments = new HashSet<SqlExceptionAppointment>();
        }
        
        [Key]
        public int TimeMarkerId { get; set; }

        [StringLength(50)]
        public string TimeMarkerName { get; set; }

        [StringLength(50)]
        public string TimeMarkerBrushName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SqlAppointment> SqlAppointments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SqlExceptionAppointment> SqlExceptionAppointments { get; set; }
    }
}
