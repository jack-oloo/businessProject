using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TM.BusinessCourses.Models;

namespace TM.BusinessCourses.Controllers
{
    public class ReportsController : Controller
    {
        private TMBusinessCoursesContext db = new TMBusinessCoursesContext();
        // GET: Reports
        public ActionResult Index()
        {            
            return View();
        }
        public ActionResult Instructor()
        {
            List<Section> ls = db.Sections.Include(i => i.instructor).Include(i=>i.location).Include(i=>i.course).Where(x => x.instructor == null||x.instructor.deleted).ToList();
            return View(ls);
        }
        public ActionResult Coordinator()
        {
            List<Course> lc = db.Courses.Include(c => c.coordinator).Where(x => x.coordinator == null || x.coordinator.deleted).ToList();
            return View(lc);
        }
    }
}