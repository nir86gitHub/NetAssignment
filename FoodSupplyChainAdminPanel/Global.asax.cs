using FoodSupplyChainAdminPanel.App_Code;
using FSC_BOL.Models.Common;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FoodSupplyChainAdminPanel
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ModelBinders.Binders.Add(typeof(DataTablesRequest), new DataTablesBinder());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
