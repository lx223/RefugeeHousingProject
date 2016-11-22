using FakeItEasy;
using NUnit.Framework;
using RefugeeHousing.Models;
using RefugeeHousing.Services;
using RefugeeHousing.ViewModels;
using SendGrid.Helpers.Mail;

namespace RefugeeHousing.Tests.Services
{
    [TestFixture]
    class PropertyContactServiceTest
    {
        private IPropertyEmailService propertyEmailService;
        private IApplicationDbContext dbContext;
        private IEmailBuilder emailBuilder;

        private PropertyContactService propertyContactService;

        [SetUp]
        public void SetUp()
        {
            propertyEmailService = A.Fake<IPropertyEmailService>();
            emailBuilder = A.Fake<IEmailBuilder>();

            dbContext = A.Fake<IApplicationDbContext>();
            dbContext.Listings = new FakeDbSet.InMemoryDbSet<Listing>();

            propertyContactService = new PropertyContactService(propertyEmailService, dbContext, emailBuilder);
        }

        [Test]
        public void ContactingOwnerSendsEmail()
        {
            // Arrange
            const int propertyId = 6;
            const string ownerEmailAddress = "email address of owner";

            var owner = new ApplicationUser { Email = ownerEmailAddress };
            dbContext.Listings.Add(new Listing { Id = propertyId, Owner = owner });

            var enquiry = new PropertyEnquiry { PropertyId = propertyId };

            var expectedEmail = new Mail {Subject = "Some email"};
            A.CallTo(() => emailBuilder.Build(enquiry, owner)).Returns(expectedEmail);

            // Act
            propertyContactService.ContactOwner(enquiry);

            // Assert
            A.CallTo(() => propertyEmailService.SendEmail(expectedEmail)).MustHaveHappened();
        }

        [Test]
        public void ContactingOwnerSendsEmailOnlyToOwnerOfProperty()
        {
            // Arrange
            var propertyOwner = new ApplicationUser { Email = "some email address" };

            dbContext.Listings.Add(new Listing { Id = 1, Owner = new ApplicationUser() });
            dbContext.Listings.Add(new Listing { Id = 2, Owner = propertyOwner });
            dbContext.Listings.Add(new Listing { Id = 3, Owner = new ApplicationUser () });

            var enquiry = new PropertyEnquiry { PropertyId = 2 };

            var expectedEmail = new Mail { Subject = "Some email" };
            A.CallTo(() => emailBuilder.Build(enquiry, propertyOwner)).Returns(expectedEmail);

            // Act
            propertyContactService.ContactOwner(enquiry);

            // Assert
            A.CallTo(() => propertyEmailService.SendEmail(expectedEmail)).MustHaveHappened();
            A.CallTo(() => propertyEmailService.SendEmail(A<Mail>.That.Matches(e => e != expectedEmail))).MustNotHaveHappened();
        }
    }
}
