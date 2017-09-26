using RBAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using RBAC.ViewModels;

namespace RBAC.Controllers
{
    public class UserRoleController : Controller
    {
        Model1 db = new Model1 ();
        // GET: UserRole
        public ActionResult Index()
        {
            return View(db.Users.Include(u=>u.Roles));
        }
        public ActionResult Create()
        {
            ViewBag.useroption = db.Users.Select(s => new SelectListItem { Text = s.Username, Value = s.Id.ToString() });
            ViewBag.roleoption = db.Roles.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() });
            return View();
        }
        public ActionResult Insert(UserRoleViewModel userrole)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code=400});
            }
            var user = db.Users.FirstOrDefault(u => u.Id == userrole.UserId);
            var role = new Role { Id = userrole.RoleId };
            db.Roles.Attach(role);
            user.Roles.Add(role);
            if (db.SaveChanges()==0)
            {
                return Json(new { code=400});
            }
            return Json(new { code=200});
        }
        public ActionResult Edit(UserRoleViewModel userrole)
        {
            ViewBag.RoleOption = db.Roles.Select(s=>new SelectListItem { Text=s.Name,Value=s.Id.ToString()});
            userrole.UserName = db.Users.FirstOrDefault(r=>r.Id==userrole.UserId).Username;
            return View(userrole);
        }
        public ActionResult Update(UserRoleViewModel userrole)
        {
            if (userrole.RoleId==userrole.UpdateRoleId)
            {
                return Json(new { code=400});
            }
            var user = db.Users.FirstOrDefault(u=>u.Id==userrole.UserId);
            var role = new Role { Id=userrole.RoleId};
            var updaterole = new Role { Id=userrole.UpdateRoleId};
            db.Roles.Attach(role);
            db.Roles.Attach(updaterole);
            user.Roles.Remove(role);
            user.Roles.Add(updaterole);
            if (db.SaveChanges()==0)
            {
                return Json(new { code=400});
            }
            return Json(new { code=200});
        }
        public ActionResult Delete(UserRoleViewModel userrole)
        {
            var user = db.Users.FirstOrDefault(u=>u.Id==userrole.UserId);
            var role = new Role { Id=userrole.RoleId};
            db.Roles.Attach(role);
            user.Roles.Remove(role);
            if (db.SaveChanges()==0)
            {
                return Json(new { code=400});
            }
            return Json(new {code=200 });
        }
    }
}