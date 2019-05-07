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
    [Authorize(Roles = "Admin")]
    public class SectionsController : Controller
    {
        private TMBusinessCoursesContext db = new TMBusinessCoursesContext();

        // GET: Sections/Create
        public ActionResult Create(string id)
        {
            ViewBag.course_id = new SelectList(db.Courses.Where(x=>x.id.ToString() == id), "id", "course_name");
            ViewBag.instructor_id = new SelectList(db.Faculties, "id", "DropDown");
            ViewBag.location_id = new SelectList(db.Locations, "id", "name");
            ViewBag.id = id;
            return View();
        }

        // POST: Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,course_id,course_section,instructor_id,location_id,start_date,end_date,days,start_time,end_time,class_type,class_style")] Section section)
        {
            if (ModelState.IsValid)
            {
                db.Sections.Add(section);
                db.SaveChanges();
                return RedirectToAction("Sections", "Home", new { id = section.course_id });
            }

            ViewBag.course_id = new SelectList(db.Courses.Where(x=>x.id == section.course_id), "id", "course_name", section.course_id);
            ViewBag.instructor_id = new SelectList(db.Faculties, "id", "DropDown", section.instructor_id);
            ViewBag.location_id = new SelectList(db.Locations, "id", "name", section.location_id);
            return View(section);
        }

        // GET: Sections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            ViewBag.course_id = new SelectList(db.Courses.Where(x=>x.id == section.course_id), "id", "course_name");
            ViewBag.instructor_id = new SelectList(db.Faculties, "id", "DropDown", section.instructor_id);
            ViewBag.location_id = new SelectList(db.Locations, "id", "name", section.location_id);
            return View(section);
        }

        // POST: Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,course_id,course_section,instructor_id,location_id,start_date,end_date,days,start_time,end_time,class_type,class_style")] Section section)
        {
            if (ModelState.IsValid)
            {
                db.Entry(section).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Sections", "Home", new { id = section.course_id });
            }
            ViewBag.course_id = new SelectList(db.Courses.Where(x=>x.id == section.course_id.Value), "id", "course_name", section.course_id);
            ViewBag.instructor_id = new SelectList(db.Faculties, "id", "DropDown", section.instructor_id);
            ViewBag.location_id = new SelectList(db.Locations, "id", "name", section.location_id);
            return View(section);
        }

        // GET: Sections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Include(s=>s.course).Include(s=>s.instructor).Include(s=>s.course.coordinator).Include(s=>s.location).Where(x=>x.id == id).First();
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Sections.Find(id).deleted = true;
            db.SaveChanges();
            return RedirectToAction("Sections", "Home", new { @id = db.Sections.Find(id).course_id });
        }

        public ActionResult Restore(int id)
        {
            db.Sections.Find(id).deleted = false;
            db.SaveChanges();
            return RedirectToAction("Sections", "Home", new { @id = db.Sections.Find(id).course_id });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
