extern alias DataAnnotations;

using System.Collections.Generic;

namespace ConnectToDatabase_WPF.Data
{
    public class RelationDbModel
    {
        [DataAnnotations.System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public int Type { get; set; }

        // this.Tasks[0] - from task
        // this.Tasks[1] - to task (or task owner)
        public virtual List<TaskDbModel> Tasks { get; set; }
    }
}
