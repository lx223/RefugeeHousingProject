using FakeItEasy;
using NUnit.Framework;
using RefugeeHousing.Controllers;
using RefugeeHousing.Services;
using SendGrid.Helpers.Mail;

namespace RefugeeHousing.Tests.Integration
{
    [TestFixture]
    class PropertiesControllerIntegrationTest
    {
        private IPropertyEmailService propertyEmailService;
        private IPropertyContactService propertyContactService;

        private PropertiesController propertiesController;

        [SetUp]
        public void SetUp()
        {
            propertyEmailService = A.Fake<IPropertyEmailService>();
            propertyContactService = new PropertyContactService(propertyEmailService);

            propertiesController = new PropertiesController(propertyContactService);
        }

        [Test]
        public void ContactingPropertyOwnerEmailsOwner()
        {
            const int propertyId = 6;
            propertiesController.ContactOwner(propertyId);

            A.CallTo(() => propertyEmailService.SendEmail(A<Mail>._)).MustHaveHappened();
        }
    }
}
