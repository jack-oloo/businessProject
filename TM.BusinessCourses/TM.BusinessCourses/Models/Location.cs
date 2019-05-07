using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TM.BusinessCourses.Models
{
    public class Location
    {
        [Key]
        public int id { get; set; }

        [Display(Name="Location Name"), Required(ErrorMessage = "Location Name is required.")]
        public string name { get; set; }

        [Display(Name ="Description"), Required(ErrorMessage = "Description is required.")]
        public string description { get; set; }

        [Display(Name = "Deleted")]
        public bool deleted { get; set; }
    }
}