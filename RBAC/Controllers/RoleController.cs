using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RBAC.Models;
using System.Data.Entity.Migrations;

namespace RBAC.Controllers
{
    public class RoleController : Controller
    {
        Model1 db = new Model1();
        // GET: Role
        public ActionResult Index()
        {
            return View(db.Roles);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var role = db.Roles.FirstOrDefault(f=>f.Id==id);
            if (role == null) return Json(new { code=400});
            return View(role);
        }
        public ActionResult Save(Role role)
        {
            db.Roles.AddOrUpdate(role);
            db.SaveChanges();
            return Json(new { code = 200 });
        }
        public ActionResult Delete(int id)
        {
            Role role = new Role {Id=id};
            db.Roles.Attach(role);
            db.Roles.Remove(role);
            db.SaveChanges();
            return Json(new { code=200});
        }
    }
}