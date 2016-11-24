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
            const string ownerEmailAddress = "email address of owner";

            var owner = new ApplicationUser {Email = ownerEmailAddress};

            // Act
            var email = emailBuilder.Build(new PropertyEnquiry(), owner);

            // Assert
            email.Personalization.Should().HaveCount(1);
            email.Personalization[0].Tos.Should().OnlyContain(t => t.Address == ownerEmailAddress);
        }

        [Test]
        public void ReplyToAddressIsEnquirerEmailAddress()
        {
            // Arrange
            var owner = new ApplicationUser();

            const string enquirerEmail = "some email address";
            var enquiry = new PropertyEnquiry { EnquirerEmail = enquirerEmail };

            // Act
            var email = emailBuilder.Build(enquiry, owner);

            // Assert
            email.ReplyTo.Address.Should().Be(enquirerEmail);
        }

        // The following tests of email content are not very strict because we don't want them to be
        // fragile, but they do ensure that we haven't missed out any important data from the email:

        [Test]
        public void EmailIncludesNameOfEnquirer()
        {
            // Arrange
            const string nameOfEnquirer = "name of enquirer";
            var enquiry = new PropertyEnquiry { EnquirerName = nameOfEnquirer };

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
            var enquiry = new PropertyEnquiry { OrganizationName = organizationName };

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
            var enquiry = new PropertyEnquiry { OrganizationWebsite = organizationWebsite };

            // Act
            var email = emailBuilder.Build(enquiry, new ApplicationUser());

            // Assert
            email.Contents[0].Value.Should().Contain(organizationWebsite);
        }

        [TestCase(true, false, "English")]
        [TestCase(false, true, "Greek")]
        [TestCase(true, true, "English, Greek")]
        public void EmailIncludesLanguagesSpokenByNgoWorker(bool english, bool greek, string expectedLanguages)
        {
            // Arrange
            const string enquirerName = "name of property owner";
            var enquiry = new PropertyEnquiry { EnquirerName = enquirerName, EnquirerSpeaksEnglish = english, EnquirerSpeaksGreek = greek};

            // Act
            var email = emailBuilder.Build(enquiry, new ApplicationUser());

            // Assert
            email.Contents[0].Value.Should().Contain($"Languages spoken by {enquirerName}: {expectedLanguages}");
        }
    }
}