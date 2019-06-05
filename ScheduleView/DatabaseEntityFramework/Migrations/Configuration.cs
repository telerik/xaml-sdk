namespace DatabaseEntityFramework.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseEntityFramework.ScheduleViewEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    }
}
