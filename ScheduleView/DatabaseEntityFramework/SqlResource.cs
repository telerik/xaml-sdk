namespace DatabaseEntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SqlResource
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SqlResource()
        {
            SqlAppointmentResources = new HashSet<SqlAppointmentResource>();
            SqlExceptionResources = new HashSet<SqlExceptionResource>();
        }

        [Key]
        public int SqlResourceId { get; set; }

        public int? SqlResourceTypeId { get; set; }

        [StringLength(100)]
        public string ResourceName { get; set; }

        [StringLength(100)]
        public string DisplayName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SqlAppointmentResource> SqlAppointmentResources { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SqlExceptionResource> SqlExceptionResources { get; set; }

        public virtual SqlResourceType SqlResourceType { get; set; }
    }
}
