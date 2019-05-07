using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TM.BusinessCourses.Models
{
    public class Course
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Course Name"), Required(ErrorMessage = "Course Name is required.")]
        public string course_name { get; set; }

        [Display(Name ="Course Number"), Required(ErrorMessage = "Course Number is required.")]
        public string course_no { get; set; }

        [ForeignKey("coordinator"), Display(Name = "Coordinator")]
        public int? coordinator_id { get; set; }

        public Faculty coordinator { get; set; }

        [Display(Name = "Deleted")]
        public bool deleted { get; set; }

    }
}