namespace netcuoiky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addScheduleTableAndUserCourseRelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateInWeek = c.Int(nullable: false),
                        StartPeriod = c.Int(nullable: false),
                        EndPeriod = c.Int(nullable: false),
                        CourseClassId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourseClassrooms", t => t.CourseClassId)
                .Index(t => t.CourseClassId);
            
            CreateTable(
                "dbo.UserCourses",
                c => new
                    {
                        CourseId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CourseId, t.UserId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Courses", "IsAvailable", c => c.Boolean(nullable: false));
            AddColumn("dbo.CourseClassrooms", "IsComplete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCourses", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Schedules", "CourseClassId", "dbo.CourseClassrooms");
            DropIndex("dbo.UserCourses", new[] { "UserId" });
            DropIndex("dbo.UserCourses", new[] { "CourseId" });
            DropIndex("dbo.Schedules", new[] { "CourseClassId" });
            DropColumn("dbo.CourseClassrooms", "IsComplete");
            DropColumn("dbo.Courses", "IsAvailable");
            DropTable("dbo.UserCourses");
            DropTable("dbo.Schedules");
        }
    }
}
