namespace DatabaseEntityFramework
{
    extern alias DataAnnotations;

    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SqlAppointmentResource
    {
        [DataAnnotations.System.ComponentModel.DataAnnotations.Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SqlAppointment_SqlAppointmentId { get; set; }

        [DataAnnotations.System.ComponentModel.DataAnnotations.Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SqlResource_SqlResourceId { get; set; }

        public bool? ManyToManyWorkaround { get; set; }

        public virtual SqlAppointment SqlAppointment { get; set; }

        public virtual SqlResource SqlResource { get; set; }
    }
}
