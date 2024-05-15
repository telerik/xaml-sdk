using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseFirstNorthwind
{
    public partial class CustomerAndSuppliersByCity
    {
        [StringLength(15)]
        public string City { get; set; }
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }
        [StringLength(30)]
        public string ContactName { get; set; }
        [Required]
        [StringLength(9)]
        public string Relationship { get; set; }
    }
}
