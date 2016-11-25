using System.Collections.Generic;
using System.Globalization;
using RefugeeHousing.Models;
using RefugeeHousing.Translations;
using RefugeeHousing.ViewModels;
using Resources;
using SendGrid.Helpers.Mail;

namespace RefugeeHousing.Services
{
    public interface IEmailBuilder
    {
        Mail Build(PropertyEnquiry enquiry, ApplicationUser owner);
    }

    public class EmailBuilder : IEmailBuilder
    {
        private const string FromAddress = "refugeehousingproject@example.com";

        public Mail Build(PropertyEnquiry enquiry, ApplicationUser owner)
        {
            var from = new Email(FromAddress);
            var to = new Email(owner.Email);
            var subject = "Email from Refugee Housing Project";
            var content = new Content("text/plain", ContentText(enquiry, owner.PreferredLanguage));

            var email = new Mail(@from, subject, to, content) {ReplyTo = new Email(enquiry.EnquirerEmail)};

            return email;
        }

        private static string ContentText(PropertyEnquiry enquiry, Language languageSpokenByPropertyOwner)
        {
            var cultureInfo = new CultureInfo(languageSpokenByPropertyOwner.GetCode());

            var languagesSpoken = GetLanguagesSpokenByNgoWorker(enquiry, languageSpokenByPropertyOwner);

            return string.Format(
                // ReSharper disable once AssignNullToNotNullAttribute
                LocalizedText.ResourceManager.GetString("EnquiryEmail", cultureInfo),
                enquiry.EnquirerName,
                enquiry.OrganizationName,
                enquiry.OrganizationWebsite,
                languagesSpoken);
        }

        private static string GetLanguagesSpokenByNgoWorker(PropertyEnquiry enquiry, Language languageSpokenByPropertyOwner)
        {
            var cultureInfo = new CultureInfo(languageSpokenByPropertyOwner.GetCode());

            var languages = new List<string>();
            if (enquiry.EnquirerSpeaksEnglish)
            {
                languages.Add(LocalizedText.ResourceManager.GetString("EnglishLanguage", cultureInfo));
            }
            if (enquiry.EnquirerSpeaksGreek)
            {
                languages.Add(LocalizedText.ResourceManager.GetString("GreekLanguage", cultureInfo));
            }

            return string.Join(", ", languages);
        }
    }
}