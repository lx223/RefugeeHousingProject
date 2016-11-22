using System;
using System.Linq.Expressions;
using FakeItEasy;
using NUnit.Framework;
using RefugeeHousing.Controllers;
using RefugeeHousing.Models;
using RefugeeHousing.Services;
using SendGrid.Helpers.Mail;

namespace RefugeeHousing.Tests.Integration
{
    [TestFixture]
    class PropertiesControllerIntegrationTest
    {
        private IPropertyEmailService propertyEmailService;
        private IPropertyContactService propertyContactService;
        private IApplicationDbContext dbContext;

        private PropertiesController propertiesController;

        [SetUp]
        public void SetUp()
        {
            dbContext = A.Fake<IApplicationDbContext>();
            dbContext.Listings = new FakeDbSet.InMemoryDbSet<Listing>();

            propertyEmailService = A.Fake<IPropertyEmailService>();
            propertyContactService = new PropertyContactService(propertyEmailService, dbContext);

            propertiesController = new PropertiesController(propertyContactService);
        }

        [Test]
        public async void ContactingPropertyOwnerEmailsOwner()
        {
            // Arrange
            const int propertyId = 6;
            const string ownerEmailAddress = "email address of owner";

            dbContext.Listings.Add(new Listing {Id = propertyId, Owner = new ApplicationUser {Email = ownerEmailAddress}});

            // Act
            await propertiesController.ContactOwner(propertyId);

            // Assert
            A.CallTo(() => propertyEmailService.SendEmail(A<Mail>.That.Matches(IsEmailToSingleRecipient(ownerEmailAddress)))).MustHaveHappened();
        }

        private static Expression<Func<Mail, bool>> IsEmailToSingleRecipient(string emailAddress)
        {
            return m => 
                m.Personalization.Count == 1
                && m.Personalization[0].Tos.Count == 1
                && m.Personalization[0].Tos[0].Address == emailAddress;
        }
    }
}
