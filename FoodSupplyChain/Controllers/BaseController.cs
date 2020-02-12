using FoodSupplyChain.CustomAttributes;
using FoodSupplyChain.Models;
using FSC_BLL.AdminPanel;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace FoodSupplyChain.Controllers
{
    [FSCErrorHandler(View = "FSCErrorInfo", Master = "~/Views/Master.cshtml")]
    public class BaseController : Controller
    {
        public BaseController()
        {
            try
            {
                SessionManager.SetSessionId();
                ViewBag.FSCCategoriesMenu = new ClsCommon().GetCategory(0);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }

    public static class SessionManager
    {
        public static string GetSessionId()
        {
            return HttpContext.Current.Session["UserSession"] as string;
        }
        public static void SetSessionId()
        {
            string sessionId = GetSessionId();
            if (sessionId == null || sessionId == string.Empty)
            {
                sessionId = HttpContext.Current.Session.SessionID;
                HttpContext.Current.Session["UserSession"] = sessionId;
            }
        }
        public static List<ProductCart> GetCartSession()
        {
            return HttpContext.Current.Session["UserCart"] as List<ProductCart>;
        }
        public static void SetCartSession(List<ProductCart> productCart)
        {
            HttpContext.Current.Session["UserCart"] = productCart;
        }
    }
}