namespace DatabaseEntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            SqlAppointments = new HashSet<SqlAppointment>();
            SqlExceptionAppointments = new HashSet<SqlExceptionAppointment>();
        }

        [Key]
        public int CategoryID { get; set; }

        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(100)]
        public string DisplayName { get; set; }

        [StringLength(100)]
        public string CategoryBrushName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SqlAppointment> SqlAppointments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SqlExceptionAppointment> SqlExceptionAppointments { get; set; }
    }
}
