using FluentAssertions;
using NUnit.Framework;
using RefugeeHousing.Models;
using RefugeeHousing.Services;
using RefugeeHousing.Translations;
using RefugeeHousing.ViewModels;

namespace RefugeeHousing.Tests.Services
{
    [TestFixture]
    public class EmailBuilderTest
    {

        private EmailBuilder emailBuilder;

        private static Language[] languages = { Language.English, Language.Greek};

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

        [TestCase(Language.English, "Hello", "Χαίρετε")]
        [TestCase(Language.Greek, "Χαίρετε", "Hello")]
        public void EmailIsTranslated(Language language, string expectedContentFragment, string incorrectContent)
        {
            // Arrange
            var owner = new ApplicationUser { PreferredLanguage = language };

            // Act
            var email = emailBuilder.Build(new PropertyEnquiry(), owner);

            // Assert
            email.Contents[0].Value.Should().Contain(expectedContentFragment);
            email.Contents[0].Value.Should().NotContain(incorrectContent);
        }

        [Test, TestCaseSource(nameof(languages))]
        public void EmailIncludesNameOfEnquirer(Language language)
        {
            // Arrange
            const string nameOfEnquirer = "name of enquirer";
            var enquiry = new PropertyEnquiry { EnquirerName = nameOfEnquirer };

            // Act
            var email = emailBuilder.Build(enquiry, new ApplicationUser {PreferredLanguage = language});

            // Assert
            email.Contents[0].Value.Should().Contain(nameOfEnquirer);
        }

        [Test, TestCaseSource(nameof(languages))]
        public void EmailIncludesOrganizationName(Language language)
        {
            // Arrange
            const string organizationName = "name of the NGO";
            var enquiry = new PropertyEnquiry { OrganizationName = organizationName };

            // Act
            var email = emailBuilder.Build(enquiry, new ApplicationUser { PreferredLanguage = language });

            // Assert
            email.Contents[0].Value.Should().Contain(organizationName);
        }

        [Test, TestCaseSource(nameof(languages))]
        public void EmailIncludesLinkToOrganizationWebsite(Language language)
        {
            // Arrange
            const string organizationWebsite = "some organization website";
            var enquiry = new PropertyEnquiry { OrganizationWebsite = organizationWebsite };

            // Act
            var email = emailBuilder.Build(enquiry, new ApplicationUser { PreferredLanguage = language });

            // Assert
            email.Contents[0].Value.Should().Contain(organizationWebsite);
        }

        [TestCase(true, false, Language.English, "Languages spoken by name of property owner: English")]
        [TestCase(false, true, Language.English, "Languages spoken by name of property owner: Greek")]
        [TestCase(true, true, Language.English, "Languages spoken by name of property owner: English, Greek")]
        [TestCase(true, false, Language.Greek, "Γλώσσες που ομιλούνται από name of property owner: Αγγλικά")]
        [TestCase(false, true, Language.Greek, "Γλώσσες που ομιλούνται από name of property owner: Eλληνικά")]
        [TestCase(true, true, Language.Greek, "Γλώσσες που ομιλούνται από name of property owner: Αγγλικά, Eλληνικά")]
        public void EmailIncludesLanguagesSpokenByNgoWorkerTranslatedIntoPropertyOwnersLanguage(
            bool english, bool greek, Language preferredLanguageOfOwner, string expectedLanguageText)
        {
            // Arrange
            var enquiry = new PropertyEnquiry { EnquirerName = "name of property owner", EnquirerSpeaksEnglish = english, EnquirerSpeaksGreek = greek};

            // Act
            var email = emailBuilder.Build(enquiry, new ApplicationUser {PreferredLanguage = preferredLanguageOfOwner});

            // Assert
            email.Contents[0].Value.Should().Contain(expectedLanguageText);
        }
    }
}