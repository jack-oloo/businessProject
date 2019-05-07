using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TM.BusinessCourses.Models
{
    public class Faculty
    {
        [Key]
        public int id { get; set; }

        [Display(Name ="First Name"), Required(ErrorMessage = "First Name is required.")]
        public string first_name { get; set; }

        [Display(Name ="Last Name"), Required(ErrorMessage = "Last Name is required.")]
        public string last_name { get; set; }

        [Display(Name ="Email"), Required(ErrorMessage = "Email is required.")]
        public string email { get; set; }

        [Display(Name = "Deleted")]
        public bool deleted { get; set; }

        [NotMapped]
        public string DropDown => first_name + " " + last_name + " - " + email;
    }
}