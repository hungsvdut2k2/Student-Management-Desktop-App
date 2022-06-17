namespace netcuoiky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class configCourseTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseClassrooms", "Credits", c => c.Int(nullable: false));
            DropColumn("dbo.CourseClassrooms", "TeacherName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourseClassrooms", "TeacherName", c => c.String());
            DropColumn("dbo.CourseClassrooms", "Credits");
        }
    }
}
