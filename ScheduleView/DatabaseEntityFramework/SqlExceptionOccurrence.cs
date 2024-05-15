namespace DatabaseEntityFramework
{
    extern alias DataAnnotations;

    using System;

    public partial class SqlExceptionOccurrence
    {
        [DataAnnotations.System.ComponentModel.DataAnnotations.Key]
        public int ExceptionId { get; set; }

        public int MasterSqlAppointmentId { get; set; }

        public DateTime ExceptionDate { get; set; }

        public virtual SqlAppointment SqlAppointment { get; set; }

        public virtual SqlExceptionAppointment SqlExceptionAppointment { get; set; }
    }
}
