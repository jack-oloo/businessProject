using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TM.BusinessCourses.Models;


namespace TM.BusinessCourses.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private TMBusinessCoursesContext db = new TMBusinessCoursesContext();

        
        public ActionResult Index(string sortOrder, string search, string deleted)
        {
            ViewBag.deleted = deleted;
            ViewBag.search = search;
            ViewBag.NumberSortParm = String.IsNullOrEmpty(sortOrder) ? "num_desc" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.CoordSortParm = sortOrder == "Coord" ? "coord_desc" : "Coord";
            if (search != null)
            {
                var fa = db.Faculties.Where(x => x.email.Contains(search) || x.first_name.Contains(search) || x.last_name.Contains(search));
                var courses = db.Courses.Include(c => c.coordinator).Where(x => x.course_name.Contains(search) || x.course_no.Contains(search) || fa.Contains(x.coordinator));
                if (deleted == "" || deleted == null)
                    courses = courses.Where(x => x.deleted == false);
                else
                    courses = courses.Where(x => x.deleted == true);
                switch (sortOrder)
                {
                    case "num_desc":
                        courses = courses.OrderByDescending(s => s.course_no);
                        break;
                    case "Name":
                        courses = courses.OrderBy(s => s.course_name);
                        break;
                    case "name_desc":
                        courses = courses.OrderByDescending(s => s.course_name);
                        break;
                    case "Coord":
                        courses = courses.OrderBy(s => s.coordinator.first_name);
                        break;
                    case "coord_desc":
                        courses = courses.OrderByDescending(s => s.coordinator.first_name);
                        break;
                    default:
                        courses = courses.OrderBy(s => s.course_no);
                        break;
                }
                ViewBag.courses = courses.ToList();
                return View();
            }
            else
            {
                var courses = db.Courses.Include(c=>c.coordinator);
                if (deleted == "" || deleted == null)
                    courses = courses.Where(x => x.deleted == false);
                else
                    courses = courses.Where(x => x.deleted == true);
                switch (sortOrder)
                {
                    case "num_desc":
                        courses = courses.OrderByDescending(s => s.course_no);
                        break;
                    case "Name":
                        courses = courses.OrderBy(s => s.course_name);
                        break;
                    case "name_desc":
                        courses = courses.OrderByDescending(s => s.course_name);
                        break;
                    case "Coord":
                        courses = courses.OrderBy(s => s.coordinator.first_name);
                        break;
                    case "coord_desc":
                        courses = courses.OrderByDescending(s => s.coordinator.first_name);
                        break;
                    default:
                        courses = courses.OrderBy(s => s.course_no);
                        break;
                }
                ViewBag.courses = courses.ToList();
                return View();
            }
                //courses.AddRange(db.Courses.Where(x => db.Faculties.Where(y=>y.id == x.coordinator_id == search));            
        }

        public ActionResult Sections(string id, string sortOrder, string deleted)
        {
            if (id == null)
                id = "1";
            ViewBag.CourseSortParm = String.IsNullOrEmpty(sortOrder) ? "course_desc" : "";
            ViewBag.InstructorSortParm = sortOrder == "Instr" ? "instr_desc" : "Instr";
            ViewBag.LocationSortParm = sortOrder == "Locat" ? "locat_desc" : "Locat";
            ViewBag.course = db.Courses.Include(c => c.coordinator).Where(x => x.id.ToString() == id).First();
            var section = db.Sections.Include(s => s.course).Include(s => s.course.coordinator).Include(s => s.instructor).Include(s => s.location).Where(x => x.course_id.ToString() == id);
            if (deleted == "" || deleted == null)
                section = section.Where(x => x.deleted == false);
            else
                section = section.Where(x => x.deleted == true);
            switch (sortOrder)
            {
                case "course_desc":
                    section = section.OrderByDescending(x => x.course.course_name);
                    break;
                case "Instr":
                    section = section.OrderBy(x => x.instructor.last_name);
                    break;
                case "instr_desc":
                    section = section.OrderByDescending(x => x.instructor.last_name);
                    break;
                case "Locat":
                    section = section.OrderBy(x => x.location.name);
                    break;
                case "locat_desc":
                    section = section.OrderByDescending(x => x.location.name);
                    break;
                default:
                    section = section.OrderBy(x => x.course.course_name);
                    break;
            }
            ViewBag.deleted = deleted;
            return View(section.ToList());
        }
    }
}