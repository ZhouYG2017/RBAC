using RBAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RBAC.Filters
{
    public enum AuthorizationType { None,Identity,Authorize}
    public class CustomAutorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        public AuthorizationType type = AuthorizationType.Authorize;
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (type == AuthorizationType.None) return;

            if (filterContext.HttpContext.Session["user"] == null)
            {
                RedirectToLogin(filterContext);
                return;
            }
            if (type == AuthorizationType.Identity) return;
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var role = filterContext.HttpContext.Session["role"] as Role;
            var module = role.Modules.FirstOrDefault(m => m.Controller == controller);
            if (module == null)
            {
                RedirectToLogin(filterContext);
            }
            //if (role.Modules.All(m=>m.Controller!=controller))
            //{
            //    RedirectToLogin(filterContext);
            //}
        }
        public void RedirectToLogin(AuthorizationContext filterContext)
        {
            var url = new UrlHelper(filterContext.RequestContext);
            filterContext.Result = new RedirectResult(url.Action("index", "login"));
        }
    }
}