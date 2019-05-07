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
    public class CoursesController : Controller
    {
        private TMBusinessCoursesContext db = new TMBusinessCoursesContext();

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.coordinator_id = new SelectList(db.Faculties, "id", "DropDown");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,course_name,course_no,coordinator_id")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Sections", "Home", new { id = course.id });
            }

            ViewBag.coordinator_id = new SelectList(db.Faculties, "id","DropDown", course.coordinator_id);
            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.coordinator_id = new SelectList(db.Faculties, "id", "DropDown", course.coordinator_id);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,course_name,course_no,coordinator_id")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Sections", "Home", new { id = course.id });
            }
            ViewBag.coordinator_id = new SelectList(db.Faculties, "id","DropDown", course.coordinator_id);
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Include(c=>c.coordinator).Where(x=>x.id == id).First();
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Courses.Find(id).deleted = true;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Restore(int id)
        {
            db.Courses.Find(id).deleted = false;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
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
