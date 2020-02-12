using System.Web.Mvc;

namespace FoodSupplyChainAdminPanel.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Home()
        {
            return View("Home");
        }
    }
}