namespace TM.BusinessCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adddeletedfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Locations", "deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sections", "deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sections", "deleted");
            DropColumn("dbo.Locations", "deleted");
            DropColumn("dbo.Courses", "deleted");
        }
    }
}
