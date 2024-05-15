using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseFirstNorthwind
{
    public partial class EmployeeTerritory
    {
        [Key]
        [Column("EmployeeID")]
        public int EmployeeId { get; set; }
        [Key]
        [Column("TerritoryID")]
        [StringLength(20)]
        public string TerritoryId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(DatabaseFirstNorthwind.Employee.EmployeeTerritories))]
        public virtual Employee Employee { get; set; }
        [ForeignKey(nameof(TerritoryId))]
        [InverseProperty(nameof(DatabaseFirstNorthwind.Territory.EmployeeTerritories))]
        public virtual Territory Territory { get; set; }
    }
}
