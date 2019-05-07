namespace TM.BusinessCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        course_name = c.String(nullable: false),
                        course_no = c.String(nullable: false),
                        start_date = c.DateTime(nullable: false),
                        end_date = c.DateTime(nullable: false),
                        coordinator_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Faculties", t => t.coordinator_id)
                .Index(t => t.coordinator_id);
            
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        first_name = c.String(nullable: false),
                        last_name = c.String(nullable: false),
                        email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        course_id = c.Int(nullable: false),
                        course_section = c.Int(nullable: false),
                        instructor_id = c.Int(),
                        location_id = c.Int(),
                        days = c.String(nullable: false),
                        start_time = c.Time(nullable: false, precision: 7),
                        end_time = c.Time(nullable: false, precision: 7),
                        class_type = c.String(nullable: false),
                        class_style = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Courses", t => t.course_id, cascadeDelete: true)
                .ForeignKey("dbo.Faculties", t => t.instructor_id)
                .ForeignKey("dbo.Locations", t => t.location_id)
                .Index(t => t.course_id)
                .Index(t => t.instructor_id)
                .Index(t => t.location_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sections", "location_id", "dbo.Locations");
            DropForeignKey("dbo.Sections", "instructor_id", "dbo.Faculties");
            DropForeignKey("dbo.Sections", "course_id", "dbo.Courses");
            DropForeignKey("dbo.Courses", "coordinator_id", "dbo.Faculties");
            DropIndex("dbo.Sections", new[] { "location_id" });
            DropIndex("dbo.Sections", new[] { "instructor_id" });
            DropIndex("dbo.Sections", new[] { "course_id" });
            DropIndex("dbo.Courses", new[] { "coordinator_id" });
            DropTable("dbo.Sections");
            DropTable("dbo.Locations");
            DropTable("dbo.Faculties");
            DropTable("dbo.Courses");
        }
    }
}
