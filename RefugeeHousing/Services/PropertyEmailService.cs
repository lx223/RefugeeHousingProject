using System;
using System.IO;
using System.Linq;
using System.Net;
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
            try
            {
                var result = await Send(mail);
                if (result.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new IOException($"Could not send email. SendGrid returned status code '{result.StatusCode}'");
                }
            }
            catch (EmailNotConfiguredException e)
            {
                // Only log the message to avoid noisy logs, because we know this exception only has one cause
                Logger.Warn(e.Message);
                Logger.Info("Here is the email that would have been sent: " + string.Join("\r\n", mail.Contents.Select(c => c.Value)));
            }
        }

        private static async Task<dynamic> Send(Mail email)
        {
            var apiKey = Environment.GetEnvironmentVariable(ApiKeyEnvironmentVariable);

            if (apiKey == null)
            {
                throw new EmailNotConfiguredException($"Could not find '{ApiKeyEnvironmentVariable}' environment variable, so cannot send email");
            }

            var sendGridClient = new SendGridAPIClient(apiKey);

            return await sendGridClient.client.mail.send.post(requestBody: email.Get());
        }
    }

    class EmailNotConfiguredException : InvalidOperationException
    {
        public EmailNotConfiguredException(string message) : base(message) {}
    }
}