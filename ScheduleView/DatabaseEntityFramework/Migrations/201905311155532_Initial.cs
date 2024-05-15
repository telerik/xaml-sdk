namespace DatabaseEntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(maxLength: 100, unicode: false),
                        DisplayName = c.String(maxLength: 100, unicode: false),
                        CategoryBrushName = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.SqlAppointments",
                c => new
                    {
                        SqlAppointmentId = c.Int(nullable: false, identity: true),
                        Subject = c.String(maxLength: 100),
                        Body = c.String(maxLength: 500),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        IsAllDayEvent = c.Boolean(nullable: false),
                        RecurrencePattern = c.String(maxLength: 100, unicode: false),
                        TimeZoneString = c.String(maxLength: 100, unicode: false),
                        Importance = c.Int(nullable: false),
                        TimeMarkerID = c.Int(),
                        CategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.SqlAppointmentId)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .ForeignKey("dbo.TimeMarkers", t => t.TimeMarkerID)
                .Index(t => t.TimeMarkerID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.SqlAppointmentResources",
                c => new
                    {
                        SqlAppointment_SqlAppointmentId = c.Int(nullable: false),
                        SqlResource_SqlResourceId = c.Int(nullable: false),
                        ManyToManyWorkaround = c.Boolean(),
                    })
                .PrimaryKey(t => new { t.SqlAppointment_SqlAppointmentId, t.SqlResource_SqlResourceId })
                .ForeignKey("dbo.SqlResources", t => t.SqlResource_SqlResourceId, cascadeDelete: true)
                .ForeignKey("dbo.SqlAppointments", t => t.SqlAppointment_SqlAppointmentId, cascadeDelete: true)
                .Index(t => t.SqlAppointment_SqlAppointmentId)
                .Index(t => t.SqlResource_SqlResourceId);
            
            CreateTable(
                "dbo.SqlResources",
                c => new
                    {
                        SqlResourceId = c.Int(nullable: false, identity: true),
                        ResourceType = c.String(),
                        SqlResourceTypeId = c.Int(),
                        ResourceName = c.String(maxLength: 100),
                        DisplayName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.SqlResourceId)
                .ForeignKey("dbo.SqlResourceTypes", t => t.SqlResourceTypeId)
                .Index(t => t.SqlResourceTypeId);
            
            CreateTable(
                "dbo.SqlExceptionResources",
                c => new
                    {
                        SqlExceptionAppointment_ExceptionId = c.Int(nullable: false),
                        SqlResource_SqlResourceId = c.Int(nullable: false),
                        ManyToManyWorkaround = c.Boolean(),
                    })
                .PrimaryKey(t => new { t.SqlExceptionAppointment_ExceptionId, t.SqlResource_SqlResourceId })
                .ForeignKey("dbo.SqlExceptionAppointments", t => t.SqlExceptionAppointment_ExceptionId, cascadeDelete: true)
                .ForeignKey("dbo.SqlResources", t => t.SqlResource_SqlResourceId, cascadeDelete: true)
                .Index(t => t.SqlExceptionAppointment_ExceptionId)
                .Index(t => t.SqlResource_SqlResourceId);
            
            CreateTable(
                "dbo.SqlExceptionAppointments",
                c => new
                    {
                        ExceptionId = c.Int(nullable: false),
                        Subject = c.String(maxLength: 100),
                        Body = c.String(maxLength: 500),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        IsAllDayEvent = c.Boolean(nullable: false),
                        TimeZoneString = c.String(maxLength: 100, unicode: false),
                        Importance = c.Int(nullable: false),
                        TimeMarkerID = c.Int(),
                        CategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.ExceptionId)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .ForeignKey("dbo.SqlExceptionOccurrences", t => t.ExceptionId, cascadeDelete: true)
                .ForeignKey("dbo.TimeMarkers", t => t.TimeMarkerID)
                .Index(t => t.ExceptionId)
                .Index(t => t.TimeMarkerID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.SqlExceptionOccurrences",
                c => new
                    {
                        ExceptionId = c.Int(nullable: false, identity: true),
                        MasterSqlAppointmentId = c.Int(nullable: false),
                        ExceptionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ExceptionId)
                .ForeignKey("dbo.SqlAppointments", t => t.MasterSqlAppointmentId, cascadeDelete: true)
                .Index(t => t.MasterSqlAppointmentId);
            
            CreateTable(
                "dbo.TimeMarkers",
                c => new
                    {
                        TimeMarkerId = c.Int(nullable: false, identity: true),
                        TimeMarkerName = c.String(maxLength: 50),
                        TimeMarkerBrushName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.TimeMarkerId);
            
            CreateTable(
                "dbo.SqlResourceTypes",
                c => new
                    {
                        SqlResourceTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        DisplayName = c.String(maxLength: 100),
                        AllowMultipleSelection = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SqlResourceTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SqlExceptionOccurrences", "MasterSqlAppointmentId", "dbo.SqlAppointments");
            DropForeignKey("dbo.SqlAppointmentResources", "SqlAppointment_SqlAppointmentId", "dbo.SqlAppointments");
            DropForeignKey("dbo.SqlResources", "SqlResourceTypeId", "dbo.SqlResourceTypes");
            DropForeignKey("dbo.SqlExceptionResources", "SqlResource_SqlResourceId", "dbo.SqlResources");
            DropForeignKey("dbo.SqlExceptionAppointments", "TimeMarkerID", "dbo.TimeMarkers");
            DropForeignKey("dbo.SqlAppointments", "TimeMarkerID", "dbo.TimeMarkers");
            DropForeignKey("dbo.SqlExceptionResources", "SqlExceptionAppointment_ExceptionId", "dbo.SqlExceptionAppointments");
            DropForeignKey("dbo.SqlExceptionAppointments", "ExceptionId", "dbo.SqlExceptionOccurrences");
            DropForeignKey("dbo.SqlExceptionAppointments", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.SqlAppointmentResources", "SqlResource_SqlResourceId", "dbo.SqlResources");
            DropForeignKey("dbo.SqlAppointments", "CategoryID", "dbo.Categories");
            DropIndex("dbo.SqlExceptionOccurrences", new[] { "MasterSqlAppointmentId" });
            DropIndex("dbo.SqlExceptionAppointments", new[] { "CategoryID" });
            DropIndex("dbo.SqlExceptionAppointments", new[] { "TimeMarkerID" });
            DropIndex("dbo.SqlExceptionAppointments", new[] { "ExceptionId" });
            DropIndex("dbo.SqlExceptionResources", new[] { "SqlResource_SqlResourceId" });
            DropIndex("dbo.SqlExceptionResources", new[] { "SqlExceptionAppointment_ExceptionId" });
            DropIndex("dbo.SqlResources", new[] { "SqlResourceTypeId" });
            DropIndex("dbo.SqlAppointmentResources", new[] { "SqlResource_SqlResourceId" });
            DropIndex("dbo.SqlAppointmentResources", new[] { "SqlAppointment_SqlAppointmentId" });
            DropIndex("dbo.SqlAppointments", new[] { "CategoryID" });
            DropIndex("dbo.SqlAppointments", new[] { "TimeMarkerID" });
            DropTable("dbo.SqlResourceTypes");
            DropTable("dbo.TimeMarkers");
            DropTable("dbo.SqlExceptionOccurrences");
            DropTable("dbo.SqlExceptionAppointments");
            DropTable("dbo.SqlExceptionResources");
            DropTable("dbo.SqlResources");
            DropTable("dbo.SqlAppointmentResources");
            DropTable("dbo.SqlAppointments");
            DropTable("dbo.Categories");
        }
    }
}
