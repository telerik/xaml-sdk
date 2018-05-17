using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectToDatabase_WPF.Data
{
    public static class DummyDataProvider
    {
        private static Random randomNumberGenerator = new Random(0);

        public static void PopuplateData(TasksDbContext context)
        {
            string[] descriptions = loremImpsum.Split('.').Select(x => x.Trim()).ToArray();

            int taskId = 0;
            int relationId = 0;

            for (int i = 0; i < 5; i++)
            {
                var start = DateTime.Today;

                var rootTask = new TaskDbModel()
                {
                    Id = taskId++,
                    Title = "Root element " + i,
                    Start = start,                                        
                    Duration = TimeSpan.FromDays(1).Ticks,
                    Description = descriptions[randomNumberGenerator.Next(0, descriptions.Length - 1)],
                    Children = new List<TaskDbModel>(),
                };

                for (int k = 0; k < 3; k++)
                {
                    var childTask = new TaskDbModel()
                    {
                        Id = taskId++,
                        ParentId = rootTask.Id,
                        Title = "Element " + i + "." + k,
                        Start = rootTask.Start.AddHours(2),
                        Duration = TimeSpan.FromHours(8).Ticks,
                        Children = new List<TaskDbModel>(),
                        Description = descriptions[randomNumberGenerator.Next(0, descriptions.Length - 1)],
                        Progress = randomNumberGenerator.Next(0, 100),                        
                    };
                                        
                    for (int j = 0; j < 2; j++)
                    {
                        childTask.Children.Add(new TaskDbModel()
                        {
                            Id = taskId++,
                            ParentId = childTask.Id,
                            Title = "Element " + i + "." + k + "." + j,
                            Start = childTask.Start.AddHours(2),
                            Description = descriptions[randomNumberGenerator.Next(0, descriptions.Length - 1)],
                            Duration = TimeSpan.FromHours(8).Ticks,
                        });
                    }
                    
                    rootTask.Children.Add(childTask);
                }

                context.Relations.Add(new RelationDbModel()
                {
                    Id = relationId++,
                    Type = 0,
                    Tasks = new List<TaskDbModel>()
                    {
                        rootTask.Children.ElementAt(0),
                        rootTask.Children.ElementAt(1),
                    },
                });

                context.Relations.Add(new RelationDbModel()
                {
                    Id = relationId++,
                    Type = 0,
                    Tasks = new List<TaskDbModel>()
                    {
                        rootTask.Children.ElementAt(1),
                        rootTask.Children.ElementAt(2),
                    },
                });               

                context.Tasks.Add(rootTask);
            }
        }

        private static string loremImpsum = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
                                            Nam ac tellus condimentum, tincidunt erat rutrum, scelerisque mi. 
                                            Nulla justo neque, vestibulum vel purus et, ultricies tristique purus. 
                                            Suspendisse eget nisl ex. Maecenas quis sapien vitae sapien feugiat tristique.
                                            Nam non maximus nunc. Praesent at massa non leo volutpat molestie. 
                                            Phasellus maximus, eros sit amet maximus tempus, mi orci sodales diam, ut tempus diam nibh a elit. 
                                            Nunc eu vestibulum sem, placerat laoreet lorem. 
                                            Vestibulum ut neque pellentesque quam laoreet dignissim non eu ligula. 
                                            Maecenas viverra, augue non iaculis rutrum, libero nisl auctor diam, sit amet auctor dui nulla a massa.
                                            Quisque maximus sapien in placerat malesuada.";
    }
}
