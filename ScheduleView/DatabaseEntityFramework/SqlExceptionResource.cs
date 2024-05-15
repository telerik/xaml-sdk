namespace DatabaseEntityFramework
{
    extern alias DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SqlExceptionResource
    {
        [DataAnnotations.System.ComponentModel.DataAnnotations.Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SqlExceptionAppointment_ExceptionId { get; set; }

        [DataAnnotations.System.ComponentModel.DataAnnotations.Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SqlResource_SqlResourceId { get; set; }

        public bool? ManyToManyWorkaround { get; set; }

        public virtual SqlExceptionAppointment SqlExceptionAppointment { get; set; }

        public virtual SqlResource SqlResource { get; set; }
    }
}
