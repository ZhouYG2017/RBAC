using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RBAC.Models;
using System.Data.Entity.Migrations;

namespace RBAC.Controllers
{
    public class UserController : Controller
    {
        Model1 db = new Model1();
        // GET: User
        public ActionResult Index()
        {
            return View(db.Users);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var role = db.Users.FirstOrDefault(f => f.Id == id);
            if (role == null) return Json(new { code = 400 });
            return View(role);
        }
        public ActionResult Save(User user)
        {
            db.Users.AddOrUpdate(user);
            db.SaveChanges();
            return Json(new { code = 200 });
        }
        public ActionResult Delete(int id)
        {
            User user = new User { Id = id };
            db.Users.Attach(user);
            db.Users.Remove(user);
            db.SaveChanges();
            return Json(new { code = 200 });
        }
    }
}