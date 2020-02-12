using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FoodSupplyChainAdminPanel.CustomAttributes
{
    public class FSCHasSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                string sessionCookie = filterContext.HttpContext.Request.Headers["Cookie"];
                if ((null != sessionCookie) && (sessionCookie.IndexOf("ASP.NET_SessionId") >= 0))
                {
                    if (filterContext.HttpContext.Session["UserDetails"] == null)
                    {
                        filterContext.HttpContext.Response.StatusCode = 504;
                        ctx.Response.End();
                    }
                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = 504;
                    ctx.Response.End();
                }
            }
            else if (filterContext.HttpContext.Session["UserDetails"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "LogOff", controller = "Account", Msg = "Session Has Expired Please Login Again!" }));
            }
        }
    }
}