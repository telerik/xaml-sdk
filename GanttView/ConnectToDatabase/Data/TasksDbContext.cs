using System.Data.Entity;

namespace ConnectToDatabase_WPF.Data
{
    public class TasksDbContext : DbContext
    {
        public TasksDbContext()
            : base("TasksDbContext")
        {
        }

        public DbSet<TaskDbModel> Tasks { get; set; }
        public DbSet<RelationDbModel> Relations { get; set; }

        public void InitializeDatabase()
        {
            if (!this.Database.Exists())
            {
                this.Database.CreateIfNotExists();
                DummyDataProvider.PopuplateData(this);
                this.SaveChanges();
            }
        }        
    }
}
