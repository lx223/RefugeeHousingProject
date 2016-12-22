using System.Threading.Tasks;
using RefugeeHousing.Helpers;
using Resources;

namespace RefugeeHousing.Services
{
    public interface IEmailService
    {
        Task<EmailDeliveryStatus> SendPasswordReset(string userEmail, string callbackUrl);
    }

    public class EmailService : IEmailService
    {
        private readonly IEmailClient client;

        public EmailService(IEmailClient client)
        {
            this.client = client;
        }

        public async Task<EmailDeliveryStatus> SendPasswordReset(string userEmail, string callbackUrl)
        {
            var subject = LocalizedText.ResetPassword;
            var body = string.Format(LocalizedText.ResetPasswordLinkText, callbackUrl);
            return await client.Send(subject, userEmail, body, EmailContentType.Html);
        }
    }
}
