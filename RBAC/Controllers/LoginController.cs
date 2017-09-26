using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RBAC.Models;
using RBAC.Filters;

namespace RBAC.Controllers
{
    [CustomAutorization(type = AuthorizationType.None)]
    public class LoginController : Controller
    {
        Model1 db = new Model1();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(User loginuser)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            var result = db.Users.FirstOrDefault(u => u.Username == loginuser.Username && u.Userpass == loginuser.Userpass);
            if (result == null) return Json(new { code = 404 });
            Session["user"] = result;

            var roles = result.Roles.ToList();
            Session["roles"] = roles;

            var rolesModules = result.Roles.ToDictionary(r => r.Id, r => r.Modules);
            Session["rolesModules"] = rolesModules;
            Session["role"] = roles[0];

            return Json(new { code = 200 });
        }
        public ActionResult LoginOut()
        {
            Session.Clear();
            return RedirectToAction("index", "login");
        }
    }
}