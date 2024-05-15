namespace DatabaseEntityFramework
{
    extern alias DataAnnotations;
    using System.Collections.Generic;

    public partial class SqlResourceType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SqlResourceType()
        {
            SqlResources = new HashSet<SqlResource>();
        }

        [DataAnnotations.System.ComponentModel.DataAnnotations.Key]
        public int SqlResourceTypeId { get; set; }

        [DataAnnotations.System.ComponentModel.DataAnnotations.Required]
        [DataAnnotations.System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Name { get; set; }

        [DataAnnotations.System.ComponentModel.DataAnnotations.StringLength(100)]
        public string DisplayName { get; set; }

        public bool AllowMultipleSelection { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SqlResource> SqlResources { get; set; }
    }
}
