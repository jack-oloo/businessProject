﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TM.BusinessCourses.Models
{
    public class TMBusinessCoursesContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public TMBusinessCoursesContext() : base("name=TMBusinessCoursesContext")
        {
        }

        public System.Data.Entity.DbSet<TM.BusinessCourses.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<TM.BusinessCourses.Models.Faculty> Faculties { get; set; }

        public System.Data.Entity.DbSet<TM.BusinessCourses.Models.Location> Locations { get; set; }

        public System.Data.Entity.DbSet<TM.BusinessCourses.Models.Section> Sections { get; set; }
    }
}
