using System;
using System.Net;
using System.Threading.Tasks;
using RefugeeHousing.Helpers;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace RefugeeHousing.Services
{
    public interface IEmailClient
    {
        Task<EmailDeliveryStatus> Send(string from, string to, string subject, string body, EmailContentType contentType);
        Task<EmailDeliveryStatus> Send(string subject, string toEmail, string body, EmailContentType contentType);
    }

    public class EmailClient : IEmailClient
    {
        private const string ApiKeyEnvironmentVariable = "REFUGEE_HOUSING_SENDGRID_API_KEY";
        private const string DefaultFromEmail = "refugeehousingproject@example.com";
        private readonly SendGridAPIClient client;

        public EmailClient()
        {
            var apiKey = Environment.GetEnvironmentVariable(ApiKeyEnvironmentVariable);

            if (apiKey == null)
                throw new EmailNotConfiguredException($"'{ApiKeyEnvironmentVariable}' environment variable missing");

            client = new SendGridAPIClient(apiKey);
        }

        public async Task<EmailDeliveryStatus> Send(string fromEmail, string subject, string toEmail, string body, EmailContentType contentType)
        {
            var from = new Email(fromEmail);
            var to = new Email(toEmail);
            var content = new Content(contentType.ToString(), body);

            var email = new Mail(from, subject, to, content);

            var result = await client.client.mail.send.post(requestBody: email.Get());
            return MapToDelieveryStatus(result);
        }

        public async Task<EmailDeliveryStatus> Send(string subject, string toEmail, string body, EmailContentType contentType)
        {
            return await Send(DefaultFromEmail, subject, toEmail, body, contentType);
        }

        private EmailDeliveryStatus MapToDelieveryStatus(dynamic result)
        {
            dynamic statusCode = result.StatusCode;
            return statusCode == HttpStatusCode.Accepted ? EmailDeliveryStatus.Sent : EmailDeliveryStatus.Failed;
        }
    }

    public enum EmailDeliveryStatus
    {
        Sent,
        Failed
    }
}
