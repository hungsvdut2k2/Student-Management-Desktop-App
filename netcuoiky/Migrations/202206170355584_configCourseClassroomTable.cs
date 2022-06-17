namespace netcuoiky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class configCourseClassroomTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Credits", c => c.Int(nullable: false));
            AddColumn("dbo.CourseClassrooms", "TeacherName", c => c.String());
            DropColumn("dbo.Courses", "teacherName");
            DropColumn("dbo.CourseClassrooms", "Credits");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourseClassrooms", "Credits", c => c.Int(nullable: false));
            AddColumn("dbo.Courses", "teacherName", c => c.String());
            DropColumn("dbo.CourseClassrooms", "TeacherName");
            DropColumn("dbo.Courses", "Credits");
        }
    }
}
