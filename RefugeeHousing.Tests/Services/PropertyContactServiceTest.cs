using System;
using System.Linq.Expressions;
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

        private PropertyContactService propertyContactService;

        [SetUp]
        public void SetUp()
        {
            propertyEmailService = A.Fake<IPropertyEmailService>();

            dbContext = A.Fake<IApplicationDbContext>();
            dbContext.Listings = new FakeDbSet.InMemoryDbSet<Listing>();

            propertyContactService = new PropertyContactService(propertyEmailService, dbContext);
        }

        [Test]
        public void ContactingOwnerSendsEmail()
        {
            // Arrange
            const int propertyId = 6;
            const string ownerEmailAddress = "email address of owner";

            dbContext.Listings.Add(new Listing { Id = propertyId, Owner = new ApplicationUser { Email = ownerEmailAddress } });

            // Act
            propertyContactService.ContactOwner(new PropertyEnquiry {PropertyId = propertyId});

            // Assert
            A.CallTo(() => propertyEmailService.SendEmail(A<Mail>.That.Matches(IsEmailToSingleRecipient(ownerEmailAddress)))).MustHaveHappened();
        }

        [Test]
        public void ContactingOwnerSendsEmailOnlyToOwnerOfProperty()
        {
            // Arrange
            const string ownerEmailAddress1 = "property owner 1 email";
            const string ownerEmailAddress2 = "property owner 2 email";
            const string ownerEmailAddress3 = "property owner 3 email";

            dbContext.Listings.Add(new Listing { Id = 1, Owner = new ApplicationUser { Email = ownerEmailAddress1 } });
            dbContext.Listings.Add(new Listing { Id = 2, Owner = new ApplicationUser { Email = ownerEmailAddress2 } });
            dbContext.Listings.Add(new Listing { Id = 3, Owner = new ApplicationUser { Email = ownerEmailAddress3 } });

            // Act
            propertyContactService.ContactOwner(new PropertyEnquiry {PropertyId = 2});

            // Assert
            A.CallTo(() => propertyEmailService.SendEmail(A<Mail>.That.Matches(IsEmailToSingleRecipient(ownerEmailAddress1)))).MustNotHaveHappened();
            A.CallTo(() => propertyEmailService.SendEmail(A<Mail>.That.Matches(IsEmailToSingleRecipient(ownerEmailAddress2)))).MustHaveHappened();
            A.CallTo(() => propertyEmailService.SendEmail(A<Mail>.That.Matches(IsEmailToSingleRecipient(ownerEmailAddress1)))).MustNotHaveHappened();
        }

        [Test]
        public void ContactingOwnerIncludesNameOfEnquirer()
        {
            // Arrange
            // TODO REF-42: Tidy up unnecessary stuff
            const int propertyId = 6;
            const string ownerEmailAddress = "email address of owner";

            dbContext.Listings.Add(new Listing { Id = propertyId, Owner = new ApplicationUser { Email = ownerEmailAddress } });

            const string nameOfEnquirer = "name of enquirer";
            var enquiry = new PropertyEnquiry {PropertyId = propertyId, InquirerName = nameOfEnquirer};

            // Act
            propertyContactService.ContactOwner(enquiry);

            // Assert
            A.CallTo(() => propertyEmailService.SendEmail(A<Mail>.That.Matches(m => m.Contents[0].Value.Contains(nameOfEnquirer)))).MustHaveHappened();
        }

        // TODO REF-42: Combine all matchers with integration test
        private static Expression<Func<Mail, bool>> IsEmailToSingleRecipient(string emailAddress)
        {
            return m =>
                m.Personalization.Count == 1
                && m.Personalization[0].Tos.Count == 1
                && m.Personalization[0].Tos[0].Address == emailAddress;
        }
    }
}
