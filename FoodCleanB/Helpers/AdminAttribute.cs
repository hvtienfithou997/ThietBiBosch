using System;
using System.Web.Mvc;
using FoodCleanB.Database;

namespace FoodCleanB.Helpers
{
    public class AdminAttribute : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.Session.Contents["User"] as TaiKhoan;

            if (!string.Equals(user?.TenDangNhap, "admin", StringComparison.InvariantCultureIgnoreCase))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(new { controller = "Home", action = "Index" })

                );
            }
        }
    }
}