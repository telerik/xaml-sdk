namespace DatabaseEntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SqlExceptionOccurrence
    {
        [Key]
        public int ExceptionId { get; set; }

        public int MasterSqlAppointmentId { get; set; }

        public DateTime ExceptionDate { get; set; }

        public virtual SqlAppointment SqlAppointment { get; set; }

        public virtual SqlExceptionAppointment SqlExceptionAppointment { get; set; }
    }
}
