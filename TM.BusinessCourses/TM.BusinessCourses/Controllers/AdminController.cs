using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TM.BusinessCourses.Models;

namespace TM.BusinessCourses.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        static ApplicationDbContext db = new ApplicationDbContext(); //This is in the IdentityModels model and creates a connection to the user database
        string adminId = db.Roles.Where(x => x.Name == "Admin").First().Id;
        // GET: Admin
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ManageUsers(string search = "")
        {
            //ApplicationUserManager um = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            
            var users = db.Users.Include(u => u.Roles).Where(x=>x.Id != "e00f0bee-76ef-4fa5-acd3-35c908b2f913" && (x.Email.Contains(search) || x.UserName.Contains(search)));//ApplicationUser is a model in the IdentityModels model and represents a user object
            //ApplicationUser user = new ApplicationUser();
            ViewBag.adminID = adminId;
            ViewBag.search = search;
            return View(users);
        }


        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            if (applicationUser.Roles.Where(x => x.RoleId == adminId).Count() > 0)
                ViewBag.isAdmin = true;
            else
                ViewBag.isAdmin = false;
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,UserName")] ApplicationUser applicationUser, bool checkAdmin = false)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = db.Users.Where(x => x.Id == applicationUser.Id).First();//Populates the ApplicationUser object

                user.Email = applicationUser.Email;
                user.UserName = applicationUser.UserName;

                if (user.Roles.Where(x => x.RoleId == adminId).Count() == 0 && checkAdmin)
                {
                    IdentityUserRole role = new IdentityUserRole();

                    role.RoleId = adminId;

                    role.UserId = user.Id;
                    user.Roles.Add(role);
                }
                else if (!checkAdmin && user.Roles.Where(x=>x.RoleId == adminId).Count() > 0)
                {
                    user.Roles.Remove(user.Roles.FirstOrDefault(x => x.RoleId == adminId));
                }
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageUsers");
            }
            return View(applicationUser);
        }
    }
}