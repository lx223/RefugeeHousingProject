using System;
using System.Linq.Expressions;
using FakeItEasy;
using NUnit.Framework;
using RefugeeHousing.Controllers;
using RefugeeHousing.Models;
using RefugeeHousing.Services;
using RefugeeHousing.ViewModels;
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

            const string enquirerName = "name of the enquirer";
            var propertyEnquiry = new PropertyEnquiry { PropertyId = propertyId, InquirerName = enquirerName };

            // Act
            await propertiesController.ContactOwner(propertyEnquiry);

            // Assert
            A.CallTo(() => propertyEmailService.SendEmail(A<Mail>.That.Matches(EmailMatching(ownerEmailAddress, enquirerName)))).MustHaveHappened();
        }

        private static Expression<Func<Mail, bool>> EmailMatching(string emailAddress, string enquirerName)
        {
            return m => 
                m.Personalization.Count == 1
                && m.Personalization[0].Tos.Count == 1
                && m.Personalization[0].Tos[0].Address == emailAddress
                // This is not a strict match, but it ensures that the email mentions the enquirer's name somewhere
                && m.Contents[0].Value.Contains(enquirerName);
        }
    }
}
