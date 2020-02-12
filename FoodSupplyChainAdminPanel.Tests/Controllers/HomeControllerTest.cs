using FoodSupplyChainAdminPanel.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace FoodSupplyChainAdminPanel.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Home() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
