using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RBAC.Filters;
using RBAC.Models;

namespace RBAC.Controllers
{
    [CustomAutorization(type =AuthorizationType.Identity)]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Header()
        {
            var user = Session["user"] as User;
            var role = Session["role"] as Role;
            var roles = Session["roles"] as List<Role>;

            var roseSelect = new List<SelectListItem>();

            foreach (var item in roles)
            {
                roseSelect.Add(new SelectListItem { Text=item.Name,Value=item.Id.ToString(),Selected=item.Id==role.Id});
            }
            ViewBag.RoleSelect = roseSelect;
            return PartialView(user);
        }
        public ActionResult Nav(int id=0)
        {
            var roleModules = Session["rolesModules"] as Dictionary<int, ICollection<Module>>;
            var roles = Session["roles"] as List<Role>;
            ICollection<Module> modules;
            if (roleModules.ContainsKey(id))
            {
                modules = roleModules[id];
                Session["role"] = roles.FirstOrDefault(r=>r.Id==id);
            }
            else
            {
                var role = Session["role"] as Role;
                modules = role.Modules;
            }
            return PartialView(modules);
        }
    }
}