using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using RefugeeHousing.Models;
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
        private readonly IApplicationDbContext dbContext;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private const string FromAddress = "refugeehousingproject@example.com";

        public PropertyContactService(IPropertyEmailService propertyEmailService, IApplicationDbContext dbContext)
        {
            this.propertyEmailService = propertyEmailService;
            this.dbContext = dbContext;
        }

        public async Task ContactOwner(int propertyId)
        {
            var listing = dbContext.Listings
                .Include(l => l.Owner)
                .First(l => l.Id == propertyId);

            var owner = listing.Owner;
            var recipient = owner.Email;

            // TODO REF-42: Fill in real values
            var from = new Email(FromAddress);
            var subject = "This is a test email sent via SendGrid";
            var to = new Email(recipient);
            var content = new Content("text/plain", "Hello, Email!");
            var email = new Mail(@from, subject, to, content);

            Logger.Info($"Sending email to '{recipient}' about property '{propertyId}'");

            await propertyEmailService.SendEmail(email);
        }
    }
}