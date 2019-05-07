namespace TM.BusinessCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeleteFaculty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Faculties", "deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Faculties", "deleted");
        }
    }
}
