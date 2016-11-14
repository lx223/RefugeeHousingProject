using System.Web.Mvc;
using FluentAssertions;
using NUnit.Framework;
using RefugeeHousing.Controllers;

namespace RefugeeHousing.Tests.Controllers
{
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

        [Test]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            ((string) result.ViewBag.Message).Should().Be("Your application description page.");
        }

        [Test]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            result.Should().NotBeNull();
        }
    }
}
