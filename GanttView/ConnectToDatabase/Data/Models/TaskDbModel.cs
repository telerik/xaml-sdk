extern alias DataAnnotations;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnectToDatabase_WPF.Data
{
    public class TaskDbModel
    {        
        [DataAnnotations.System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public long Duration { get; set; }
        public double Progress { get; set; }
        public DateTime Start { get; set; }
        public bool IsMilestone { get; set; }        
        public int? ParentId { get; set; }               

        [ForeignKey("ParentId")]
        public virtual ICollection<TaskDbModel> Children { get; set; }
        public virtual List<RelationDbModel> Relations { get; set; }
    }
}
