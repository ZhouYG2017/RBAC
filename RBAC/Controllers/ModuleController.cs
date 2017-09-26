using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RBAC.Models;
using RBAC.Filters;
using System.Data.Entity.Migrations;

namespace RBAC.Controllers
{
    public class ModuleController : Controller
    {
        Model1 db = new Model1();
        // GET: Module
        public ActionResult Index()
        {
            return View(db.Modules);
        }
        public ActionResult Edit(int id)
        {
            var module = db.Modules.FirstOrDefault(r=>r.Id==id);
            return View(module);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Save(Module module)
        {
            db.Modules.AddOrUpdate(module);
            db.SaveChanges();
            return Json(new { code = 200 });
        }
        public ActionResult Delete(int id)
        {
            Module m = new Module { Id = id };
            db.Modules.Attach(m);
            db.Modules.Remove(m);
            db.SaveChanges();
            return Json(new { code=200});
        }
    }
}