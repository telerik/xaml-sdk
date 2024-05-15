using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using Telerik.Windows.Controls.GanttView;

namespace ConnectToDatabase_WPF.Data
{
    public class TasksDataManager
    {
        private const double TimelineWidth = 1000;
        private readonly TimeSpan RangeOffset = TimeSpan.FromHours(1);
        
        private TasksDbContext context = new TasksDbContext();
        private Dictionary<TaskDbModel, CustomGanttTask> dbTaskToGanttTaskDict = new Dictionary<TaskDbModel, CustomGanttTask>();
        private Dictionary<Dependency, RelationDbModel> ganttDependencyToDbRelationDict = new Dictionary<Dependency, RelationDbModel>();
       
        public ObservableCollection<CustomGanttTask> GetAllTasks()
        {
            var dbTasks = this.context.Tasks.Where(x => x.ParentId == null).ToList();

            this.ganttDependencyToDbRelationDict.Clear();
            this.dbTaskToGanttTaskDict.Clear();

            var ganttTasks = new ObservableCollection<CustomGanttTask>();
            foreach (var dbTask in dbTasks)
            {
                var ganttTask = CreateGanttTaskFromDbModel(dbTask);
                ganttTasks.Add(ganttTask);
            }

            var dbRelations = this.context.Relations.ToList();
            foreach (var dbRelation in dbRelations)
            {
                var taskFrom = this.dbTaskToGanttTaskDict[dbRelation.Tasks[0]];
                var taskTo = this.dbTaskToGanttTaskDict[dbRelation.Tasks[1]];

                var dependency = new Dependency() { FromTask = taskFrom, Type = (DependencyType)dbRelation.Type };
                taskTo.Dependencies.Add(dependency);

                ganttDependencyToDbRelationDict[dependency] = dbRelation;
            }

            return ganttTasks;
        }

        public void UpdateDatabase()
        {
            foreach (var item in this.dbTaskToGanttTaskDict)
            {
                var ganttTask = item.Value;
                var dbModel = UpdateTaskDbModel(item.Key, item.Value);

                foreach (Dependency dependency in ganttTask.Dependencies)
                {
                    RelationDbModel dbRelation = CreateOrUpdateRelationDbModel(dbModel, dependency);
                    this.context.Entry(dbRelation).State = EntityState.Modified;
                }
                this.context.Entry(dbModel).State = EntityState.Modified;
            }

            context.SaveChanges();
        }       
        
        public DateTime GetRangeStart()
        {
            return this.dbTaskToGanttTaskDict.Min(t => t.Value.Start) - RangeOffset;
        }

        public DateTime GetRangeEnd()
        {
            return this.dbTaskToGanttTaskDict.Max(t => t.Value.End) + RangeOffset;
        }      
        
        public bool IsDatabaseInitialized()
        {
            return this.context.Database.Exists() && this.context.Tasks.Count() > 0;
        }  

        public void InitializeDatabase()
        {
            this.context.InitializeDatabase();
        }

        private RelationDbModel CreateOrUpdateRelationDbModel(TaskDbModel dbTask, Dependency ganttDependency)
        {
            RelationDbModel dbRelation = this.ganttDependencyToDbRelationDict[ganttDependency];
            if (dbRelation == null)
            {
                dbRelation = new RelationDbModel();
                dbRelation.Id = this.context.Relations.Max(r => r.Id) + 1;
                dbRelation.Tasks = new List<TaskDbModel>() { null, null };
                this.context.Relations.Add(dbRelation);
            }
            dbRelation.Tasks[0] = this.dbTaskToGanttTaskDict.FirstOrDefault(t => t.Value == (CustomGanttTask)ganttDependency.FromTask).Key;
            dbRelation.Tasks[1] = dbTask;
            dbRelation.Type = (int)ganttDependency.Type;

            return dbRelation;
        }

        private TaskDbModel UpdateTaskDbModel(TaskDbModel dbTask, CustomGanttTask ganttTask)
        {
            dbTask.Id = ganttTask.Id;
            dbTask.Title = ganttTask.Title;
            dbTask.Description = ganttTask.Description;
            dbTask.Start = ganttTask.Start;
            dbTask.Duration = ganttTask.Duration.Ticks;
            dbTask.ParentId = ganttTask.ParentId;
            dbTask.Progress = ganttTask.Progress;
            dbTask.Deadline = ganttTask.Deadline;
            dbTask.IsMilestone = ganttTask.IsMilestone;
            return dbTask;
        }

        private CustomGanttTask CreateGanttTaskFromDbModel(TaskDbModel dbTask)
        {
            var ganttTask = new CustomGanttTask()
            {
                Id = dbTask.Id,
                ParentId = dbTask.ParentId,
                Title = dbTask.Title,
                Description = dbTask.Description,
                Start = dbTask.Start,
                End = dbTask.Start.AddTicks(dbTask.Duration),
                Duration = TimeSpan.FromTicks(dbTask.Duration),
                IsMilestone = dbTask.IsMilestone,
                Deadline = dbTask.Deadline,
                Progress = dbTask.Progress,
            };
            this.dbTaskToGanttTaskDict.Add(dbTask, ganttTask);

            if (dbTask.Children != null && dbTask.Children.Count > 0)
            {
                foreach (var item in dbTask.Children)
                {
                    var childGanttTask = CreateGanttTaskFromDbModel(item);
                    ganttTask.Children.Add(childGanttTask);
                }
            }

            return ganttTask;
        }
    }
}
