namespace DatabaseEntityFramework
{
    extern alias DataAnnotations;
    using System.Collections.Generic;

    public partial class TimeMarker
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimeMarker()
        {
            SqlAppointments = new HashSet<SqlAppointment>();
            SqlExceptionAppointments = new HashSet<SqlExceptionAppointment>();
        }
        
        [DataAnnotations.System.ComponentModel.DataAnnotations.Key]
        public int TimeMarkerId { get; set; }

        [DataAnnotations.System.ComponentModel.DataAnnotations.StringLength(50)]
        public string TimeMarkerName { get; set; }

        [DataAnnotations.System.ComponentModel.DataAnnotations.StringLength(50)]
        public string TimeMarkerBrushName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SqlAppointment> SqlAppointments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SqlExceptionAppointment> SqlExceptionAppointments { get; set; }
    }
}
