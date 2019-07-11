namespace DatabaseEntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SqlExceptionResource
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SqlExceptionAppointment_ExceptionId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SqlResource_SqlResourceId { get; set; }

        public bool? ManyToManyWorkaround { get; set; }

        public virtual SqlExceptionAppointment SqlExceptionAppointment { get; set; }

        public virtual SqlResource SqlResource { get; set; }
    }
}
