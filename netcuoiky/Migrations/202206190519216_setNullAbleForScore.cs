namespace netcuoiky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setNullAbleForScore : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Scores", "excerciseScore", c => c.Double());
            AlterColumn("dbo.Scores", "midTermScore", c => c.Double());
            AlterColumn("dbo.Scores", "finalTermScore", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Scores", "finalTermScore", c => c.Double(nullable: false));
            AlterColumn("dbo.Scores", "midTermScore", c => c.Double(nullable: false));
            AlterColumn("dbo.Scores", "excerciseScore", c => c.Double(nullable: false));
        }
    }
}
