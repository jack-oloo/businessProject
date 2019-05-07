namespace TM.BusinessCourses.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TM.BusinessCourses.Models.TMBusinessCoursesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TM.BusinessCourses.Models.TMBusinessCoursesContext";
        }

        protected override void Seed(TM.BusinessCourses.Models.TMBusinessCoursesContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
