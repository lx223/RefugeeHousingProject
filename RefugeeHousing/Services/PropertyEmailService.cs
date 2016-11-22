using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace RefugeeHousing.Services
{
    public interface IPropertyEmailService
    {
        Task SendEmail(Mail mail);
    }

    public class PropertyEmailService : IPropertyEmailService
    {
        private const string ApiKeyEnvironmentVariable = "REFUGEE_HOUSING_SENDGRID_API_KEY";

        public async Task SendEmail(Mail mail)
        {
            await Send(mail);

            // TODO REF-42: Handle exceptions and unexpected status codes (i.e. not 202 Accepted)
        }

        // TODO REF-42: Can return something more specific?
        private static async Task<object> Send(Mail email)
        {
            // TODO REF-42: Log warning if API key is null
            // TODO REF-42: Log info if email is being sent

            var apiKey = Environment.GetEnvironmentVariable(ApiKeyEnvironmentVariable);
            var sendGridClient = new SendGridAPIClient(apiKey);

            return await sendGridClient.client.mail.send.post(requestBody: email.Get());
        }
    }
}