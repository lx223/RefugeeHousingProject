using System.Web.Mvc;
using FluentAssertions;
using NUnit.Framework;
using RefugeeHousing.Controllers;

namespace RefugeeHousing.Tests.Controllers
{
    // TODO REF-42: Delete once there are some meaningful tests
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            result.Should().NotBeNull();
        }
    }
}
