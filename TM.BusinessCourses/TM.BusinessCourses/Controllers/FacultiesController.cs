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
    public class FacultiesController : Controller
    {
        private TMBusinessCoursesContext db = new TMBusinessCoursesContext();

        // GET: Faculties
        public ActionResult Index(string sortOrder, string deleted, string search)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.FirstSortParm = sortOrder == "First" ? "first_desc" : "First";
            List<Faculty> fac;
            if (deleted == null || deleted == "")
            {
                var faculty = db.Faculties.Where(x => x.deleted != true);
                if (search != "" && search != null)
                    faculty = faculty.Where(x => x.first_name.Contains(search) || x.last_name.Contains(search) || x.email.Contains(search));
                switch (sortOrder)
                {
                    case "name_desc":
                        faculty = faculty.OrderByDescending(s => s.last_name);
                        break;
                    case "First":
                        faculty = faculty.OrderBy(s => s.first_name);
                        break;
                    case "first_desc":
                        faculty = faculty.OrderByDescending(s => s.first_name);
                        break;
                    default:
                        faculty = faculty.OrderBy(s => s.last_name);
                        break;
                }
                fac = faculty.ToList();
            }
            else
            {
                var faculty = db.Faculties.Where(x => x.deleted == true);
                if (search != "" && search != null)
                    faculty = faculty.Where(x => x.first_name.Contains(search) || x.last_name.Contains(search) || x.email.Contains(search));
                switch (sortOrder)
                {
                    case "name_desc":
                        faculty = faculty.OrderByDescending(s => s.last_name);
                        break;
                    case "First":
                        faculty = faculty.OrderBy(s => s.first_name);
                        break;
                    case "first_desc":
                        faculty = faculty.OrderByDescending(s => s.first_name);
                        break;
                    default:
                        faculty = faculty.OrderBy(s => s.last_name);
                        break;
                }
                fac = faculty.ToList();
            }
            ViewBag.deleted = deleted;
            ViewBag.search = search;
            return View(fac);
        }

        // GET: Faculties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Faculties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,first_name,last_name,email")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                db.Faculties.Add(faculty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(faculty);
        }

        // GET: Faculties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // POST: Faculties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,first_name,last_name,email,deleted")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faculty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faculty);
        }

        // GET: Faculties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Faculties.Find(id).deleted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeletedUsers()
        {
            return View(db.Faculties.Where(x => x.deleted == true));
        }
        

        public ActionResult Restore(int id)
        {
            db.Faculties.Find(id).deleted = false;
            db.SaveChanges();
            return RedirectToAction("Index");
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
