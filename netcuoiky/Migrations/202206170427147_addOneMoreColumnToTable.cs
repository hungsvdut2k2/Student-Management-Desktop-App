namespace netcuoiky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addOneMoreColumnToTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseEducationalPrograms", "Semester", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourseEducationalPrograms", "Semester");
        }
    }
}
