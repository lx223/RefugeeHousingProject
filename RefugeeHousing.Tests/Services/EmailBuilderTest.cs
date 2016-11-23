using FluentAssertions;
using NUnit.Framework;
using RefugeeHousing.Models;
using RefugeeHousing.Services;
using RefugeeHousing.ViewModels;

namespace RefugeeHousing.Tests.Services
{
    [TestFixture]
    public class EmailBuilderTest
    {

        private EmailBuilder emailBuilder;

        [SetUp]
        public void SetUp()
        {
            emailBuilder = new EmailBuilder();
        }

        [Test]
        public void EmailRecipientIsPropertyOwner()
        {
            // Arrange
            const int propertyId = 6;
            const string ownerEmailAddress = "email address of owner";

            var owner = new ApplicationUser {Email = ownerEmailAddress};

            // Act
            var email = emailBuilder.Build(new PropertyEnquiry {PropertyId = propertyId}, owner);

            // Assert
            email.Personalization.Should().HaveCount(1);
            email.Personalization[0].Tos.Should().OnlyContain(t => t.Address == ownerEmailAddress);
        }

        [Test]
        public void EmailIncludesNameOfEnquirer()
        {
            // Arrange
            const string nameOfEnquirer = "name of enquirer";
            var enquiry = new PropertyEnquiry { PropertyId = 2, EnquirerName = nameOfEnquirer };

            // Act
            var email = emailBuilder.Build(enquiry, new ApplicationUser());

            // Assert
            email.Contents[0].Value.Should().Contain(nameOfEnquirer);
        }

        [Test]
        public void EmailIncludesOrganizationName()
        {
            // Arrange
            const string organizationName = "name of the NGO";
            var enquiry = new PropertyEnquiry { PropertyId = 2, OrganizationName = organizationName };

            // Act
            var email = emailBuilder.Build(enquiry, new ApplicationUser());

            // Assert
            email.Contents[0].Value.Should().Contain(organizationName);
        }

        [Test]
        public void EmailIncludesLinkToOrganizationWebsite()
        {
            // Arrange
            const string organizationWebsite = "some organization website";
            var enquiry = new PropertyEnquiry { PropertyId = 2, OrganizationWebsite = organizationWebsite };

            // Act
            var email = emailBuilder.Build(enquiry, new ApplicationUser());

            // Assert
            email.Contents[0].Value.Should().Contain(organizationWebsite);
        }

        [Test]
        public void ReplyToAddressIsEnquirerEmailAddress()
        {
            // Arrange
            var owner = new ApplicationUser();

            const string enquirerEmail = "some email address";
            var enquiry = new PropertyEnquiry { PropertyId = 2, EnquirerEmail = enquirerEmail };

            // Act
            var email = emailBuilder.Build(enquiry, owner);

            // Assert
            email.ReplyTo.Address.Should().Be(enquirerEmail);
        }
    }
}