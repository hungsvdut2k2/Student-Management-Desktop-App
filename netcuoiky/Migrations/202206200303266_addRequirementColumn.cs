namespace netcuoiky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRequirementColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "requirementId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "requirementId");
        }
    }
}
