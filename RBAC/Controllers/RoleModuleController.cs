using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using RBAC.Models;
using RBAC.ViewModels;
namespace RBAC.Controllers
{
    public class RoleModuleController : Controller
    {
        Model1 db = new Model1();
        // GET: RoleModule
        public ActionResult Index()
        {
            return View(db.Roles.Include(r=>r.Modules));
        }
        public ActionResult Create()
        {
            ViewBag.useroption = db.Roles.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() });
            ViewBag.roleoption = db.Modules.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() });
            return View();
        }
        public ActionResult Insert(UserRoleViewModel userrole)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            var role = db.Roles.FirstOrDefault(u => u.Id == userrole.RoleId);
            var mode = new Module { Id = userrole.ModuleId };
            db.Modules.Attach(mode);
            role.Modules.Add(mode);
            if (db.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
        }
        public ActionResult Edit(UserRoleViewModel userrole)
        {
            var a = db.Modules.FirstOrDefault(r => r.Id == userrole.ModuleId).Name;
            ViewBag.RoleOption = db.Modules.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() ,Selected=true});
            userrole.RoleName = db.Roles.FirstOrDefault(r => r.Id == userrole.RoleId).Name;
            return View(userrole);
        }
        public ActionResult Update(UserRoleViewModel userrole)
        {
            if (userrole.ModuleId == userrole.UpdateModule)
            {
                return Json(new { code = 400 });
            }
            var role = db.Roles.FirstOrDefault(u => u.Id == userrole.RoleId);
            var mode = new Module { Id = userrole.ModuleId };
            var updaterole = new Module { Id = userrole.UpdateModule };
            db.Modules.Attach(mode);
            db.Modules.Attach(updaterole);
            role.Modules.Remove(mode);
            role.Modules.Add(updaterole);
            if (db.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
        }
        public ActionResult Delete(UserRoleViewModel userrole)
        {
            var role = db.Roles.FirstOrDefault(u => u.Id == userrole.RoleId);
            var mode = new Module { Id = userrole.ModuleId };
            db.Modules.Attach(mode);
            role.Modules.Remove(mode);
            if (db.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
        }
    }
}