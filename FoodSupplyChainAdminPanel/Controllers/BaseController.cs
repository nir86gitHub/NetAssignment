using FoodSupplyChainAdminPanel.CustomAttributes;
using FoodSupplyChainAdminPanel.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace FoodSupplyChainAdminPanel.Controllers
{
    [Authorize]
    [FSCHasSession]
    [FSCHandleError(View = "FSCErrorInfo", Master = "~/Views/MasterPage/AdminPanelMaster.cshtml")]
    public class BaseController : Controller
    {
        public BaseController()
        {
            try
            {
                FSCUserSession objSession = GetSession.GetSessionFromContext();
                if (objSession != null)
                {
                    ViewBag.UserName = objSession.UserName;
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
    public static class GetSession
    {
        public static FSCUserSession GetSessionFromContext()
        {
            return HttpContext.Current.Session["UserDetails"] as FSCUserSession;
        }
    }
}