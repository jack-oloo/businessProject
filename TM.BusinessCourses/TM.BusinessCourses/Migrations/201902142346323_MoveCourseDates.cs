namespace TM.BusinessCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveCourseDates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sections", "start_date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Sections", "end_date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Courses", "start_date");
            DropColumn("dbo.Courses", "end_date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "end_date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Courses", "start_date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Sections", "end_date");
            DropColumn("dbo.Sections", "start_date");
        }
    }
}
