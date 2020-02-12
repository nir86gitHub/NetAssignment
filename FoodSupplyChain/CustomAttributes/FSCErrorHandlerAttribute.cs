using FoodSupplyChain.App_Code;
using FoodSupplyChain.Models;
using FSC_Logging;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace FoodSupplyChain.CustomAttributes
{
    public class FSCErrorHandlerAttribute: HandleErrorAttribute
    {
        public FSCErrorHandlerAttribute()
        {
        }
        //Function Handles Exception and Logs to Logging Media if Logging is Enabled
        #region Handle Exception And Log

        public override void OnException(ExceptionContext filterContext)
        {

            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            {
                return;
            }

            if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
            {
                return;
            }

            FSCErrorInfo objErrorInfo = null;
            // if the request is AJAX return JSON else view.
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        error = true,
                        message = filterContext.Exception.Message
                    }
                };
            }
            else
            {
                if (filterContext.Exception is HttpAntiForgeryException &&
                filterContext.Exception.Message.ToLower().StartsWith("the provided anti-forgery token was meant for user \"\", but the current user is"))
                {
                    var controllerName = (string)filterContext.RouteData.Values["controller"];
                    var actionName = (string)filterContext.RouteData.Values["action"];
                    var model = new FSCErrorInfo(filterContext.Exception, controllerName, actionName);

                    model.ErrorDate = System.DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss");
                    model.UserCode = string.Empty;
                    model.IpAdd = FSCSecurity.GetIPAddress();

                    objErrorInfo = model;

                    var isAjaxCall = string.Equals("XMLHttpRequest", filterContext.HttpContext.Request.Headers["x-requested-with"], StringComparison.OrdinalIgnoreCase);
                    var returnUrl = !string.IsNullOrWhiteSpace(filterContext.HttpContext.Request["returnUrl"]) ? filterContext.HttpContext.Request["returnUrl"] : "/";
                    var response = HttpContext.Current.Response;
                    if (isAjaxCall)
                    {
                        response.Clear();
                        response.StatusCode = 200;
                        response.ContentType = "application/json; charset=utf-8";
                        response.Write(JsonConvert.SerializeObject(new { success = 1, returnUrl = returnUrl }));
                        response.End();
                    }
                    else
                    {
                        response.StatusCode = 200;
                        response.Redirect(returnUrl);
                    }
                }
                else
                {
                    var controllerName = (string)filterContext.RouteData.Values["controller"];
                    var actionName = (string)filterContext.RouteData.Values["action"];
                    var model = new FSCErrorInfo(filterContext.Exception, controllerName, actionName);

                    model.ErrorDate = System.DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss");
                    model.UserCode = string.Empty;
                    model.IpAdd = FSCSecurity.GetIPAddress();

                    objErrorInfo = model;


                    TempDataDictionary objDataDictionary = new TempDataDictionary();

                    ViewDataDictionary data = filterContext.Controller.ViewData;

                    foreach (string str in data.Keys)
                    {
                        objDataDictionary.Add(str, data[str]);
                    }

                    filterContext.Result = new ViewResult
                    {
                        ViewName = View,
                        MasterName = Master,
                        ViewData = new ViewDataDictionary<FSCErrorInfo>(model),
                        TempData = objDataDictionary
                    };

                    filterContext.ExceptionHandled = true;
                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.StatusCode = 500;
                    filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                }
            }

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["LogErrors"]))
            {
                if (objErrorInfo != null)
                {
                    new FSC_ErrorLog().LogError(new ErrorLogType(), objErrorInfo.ControllerName, objErrorInfo.ActionName, objErrorInfo.Exception.GetType().FullName, objErrorInfo.Exception.Message
                                                , objErrorInfo.ErrorDate, objErrorInfo.UserCode, objErrorInfo.IpAdd);
                }
                else
                    new FSC_ErrorLog().LogError(new ErrorLogType(), filterContext.Controller.ToString(), filterContext.Exception.GetType().FullName, filterContext.Exception.Message
                                                                    , System.DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss"), string.Empty, FSCSecurity.GetIPAddress());
            }
        }
        #endregion
    }
}