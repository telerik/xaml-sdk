namespace DatabaseEntityFramework
{
    extern alias DataAnnotations;
    using System.Collections.Generic;

    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            SqlAppointments = new HashSet<SqlAppointment>();
            SqlExceptionAppointments = new HashSet<SqlExceptionAppointment>();
        }

        [DataAnnotations.System.ComponentModel.DataAnnotations.Key]
        public int CategoryID { get; set; }

        [DataAnnotations.System.ComponentModel.DataAnnotations.StringLength(100)]
        public string CategoryName { get; set; }

        [DataAnnotations.System.ComponentModel.DataAnnotations.StringLength(100)]
        public string DisplayName { get; set; }

        [DataAnnotations.System.ComponentModel.DataAnnotations.StringLength(100)]
        public string CategoryBrushName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SqlAppointment> SqlAppointments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SqlExceptionAppointment> SqlExceptionAppointments { get; set; }
    }
}
