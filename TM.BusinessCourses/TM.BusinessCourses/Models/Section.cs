using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TM.BusinessCourses.Models
{
    public class Section
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Course"), ForeignKey("course"), Required(ErrorMessage = "Course is required.")]
        public int? course_id { get; set; }

        [Display(Name = "Course")]
        public Course course { get; set; }

        [Display(Name ="Section"), Required(ErrorMessage = "Course Section is required.")]
        public int course_section { get; set; }   
        
        [ForeignKey("instructor"), Display(Name = "Instructor")]
        public int? instructor_id { get; set; }

        public Faculty instructor { get; set; }

        [ForeignKey("location"), Display(Name = "Location")]
        public int? location_id { get; set; }

        public Location location { get; set; }

        [Display(Name ="Meeting Days"), Required(ErrorMessage = "Meeting Days is required.")]
        public string days { get; set; }//"MTWRF, None"

        [Display(Name = "Start Date"), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true), Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date)]
        public DateTime start_date { get; set; }

        [Display(Name = "End Date"), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true), Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        public DateTime end_date { get; set; }

        [Display(Name ="Start Time"), DisplayFormat(DataFormatString = @"{0:hh\:mm}"), Required(ErrorMessage = "Start Time is required.")]
        [DataType(DataType.Time)]
        public TimeSpan start_time { get; set; }

        [Display(Name ="End Time"), DisplayFormat(DataFormatString = @"{0:hh\:mm}"), Required(ErrorMessage = "End Time is required.")]
        [DataType(DataType.Time)]
        public TimeSpan end_time { get; set; }

        [Display(Name ="Class Type"), Required(ErrorMessage = "Class Type is required.")]
        public string class_type { get; set; }//TAP, Undergrad

        [Display(Name ="Class Style"), Required(ErrorMessage = "Class Style is required.")]
        public string class_style { get; set; }//Online, Hybrid, Traditional

        [Display(Name = "Deleted")]
        public bool deleted { get; set; }
    }
}