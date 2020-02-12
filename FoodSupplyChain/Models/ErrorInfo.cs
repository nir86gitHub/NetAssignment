using System;
using System.Web.Mvc;

namespace FoodSupplyChain.Models
{
    public class FSCErrorInfo: HandleErrorInfo
    {
        public FSCErrorInfo(Exception exception, string controllerName, string actionName)
            : base(exception, controllerName, actionName)
        {
        }
        public string ErrorDate { get; set; }
        public string UserCode { get; set; }
        public string IpAdd { get; set; }
    }
}