using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RBAC.Models;
using RBAC.Filters;

namespace RBAC.Controllers
{
    [CustomAutorization(type =AuthorizationType.None)]
    public class RegController : Controller
    {
        Model1 db = new Model1();
        // GET: Reg
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Reg(RBAC.Models.User user)
        {
            if (!ModelState.IsValid)
            {
                return Json(new{code=400 });
            }
            try
            {
                var role = db.Roles.FirstOrDefault(r=>r.Id==3);
                user.Roles.Add(role);
                db.Users.Add(user);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(new { code = 400 });
            }
            var result = db.Users.Include("Roles").FirstOrDefault(u => u.Username == user.Username && u.Userpass == user.Userpass);
            if (result == null) return Json(new { code = 404 });
            Session["user"] = result;

            var roles = result.Roles.ToList();
            Session["roles"] = roles;

            var rolesModules = result.Roles.ToDictionary(r => r.Id, r => r.Modules);
            Session["rolesModules"] = rolesModules;
            Session["role"] = roles[0];
            return Json(new { code=200});
        }
    }
}