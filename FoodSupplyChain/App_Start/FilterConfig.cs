using FoodSupplyChain.CustomAttributes;
using System.Web.Mvc;

namespace FoodSupplyChain.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new FSCErrorHandlerAttribute());
        }
    }
}