using FakeItEasy;
using NUnit.Framework;
using RefugeeHousing.Services;
using SendGrid.Helpers.Mail;

namespace RefugeeHousing.Tests.Services
{
    [TestFixture]
    class PropertyContactServiceTest
    {
        private IPropertyEmailService propertyEmailService;

        private PropertyContactService propertyContactService;

        [SetUp]
        public void SetUp()
        {
            propertyEmailService = A.Fake<IPropertyEmailService>();

            propertyContactService = new PropertyContactService(propertyEmailService);
        }

        [Test]
        public void ContactingOwnerSendsEmail()
        {
            const int propertyId = 6;
            propertyContactService.ContactOwner(propertyId);

            A.CallTo(() => propertyEmailService.SendEmail(A<Mail>._)).MustHaveHappened();
        }
    }
}
