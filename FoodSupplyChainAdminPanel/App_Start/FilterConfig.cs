using FoodSupplyChainAdminPanel.CustomAttributes;
using System.Web.Mvc;

namespace FoodSupplyChainAdminPanel
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new FSCHandleErrorAttribute());
        }
    }
}
