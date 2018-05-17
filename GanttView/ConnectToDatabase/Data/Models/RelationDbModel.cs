using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConnectToDatabase_WPF.Data
{
    public class RelationDbModel
    {
        [Key]
        public int Id { get; set; }                
        public int Type { get; set; }

        // this.Tasks[0] - from task
        // this.Tasks[1] - to task (or task owner)
        public virtual List<TaskDbModel> Tasks { get; set; }
    }
}
