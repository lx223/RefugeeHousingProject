using System;
using System.Threading.Tasks;
using NLog;
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

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task SendEmail(Mail mail)
        {
            await Send(mail);

            // TODO REF-42: Handle exceptions and unexpected status codes (i.e. not 202 Accepted)
        }

        private static async Task<dynamic> Send(Mail email)
        {
            var apiKey = Environment.GetEnvironmentVariable(ApiKeyEnvironmentVariable);

            if (apiKey == null)
            {
                Logger.Warn($"Could not find environment variable {ApiKeyEnvironmentVariable}, so cannot send email.");
                return Task.CompletedTask;
            }

            var sendGridClient = new SendGridAPIClient(apiKey);

            return await sendGridClient.client.mail.send.post(requestBody: email.Get());
        }
    }
}