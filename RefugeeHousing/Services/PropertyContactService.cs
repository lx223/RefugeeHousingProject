using System.Threading.Tasks;
using SendGrid.Helpers.Mail;

namespace RefugeeHousing.Services
{
    public interface IPropertyContactService
    {
        Task ContactOwner(int propertyId);
    }

    public class PropertyContactService : IPropertyContactService
    {
        private readonly IPropertyEmailService propertyEmailService;

        public PropertyContactService(IPropertyEmailService propertyEmailService)
        {
            this.propertyEmailService = propertyEmailService;
        }

        public async Task ContactOwner(int propertyId)
        {
            // TODO REF-42: Fill in real values
            var from = new Email("suzanne.hamilton@softwire.com");
            var subject = "This is a test email sent via SendGrid";
            var to = new Email("suzanne.hamilton@softwire.com");
            var content = new Content("text/plain", "Hello, Email!");
            var email = new Mail(@from, subject, to, content);

            await propertyEmailService.SendEmail(email);
        }
    }
}