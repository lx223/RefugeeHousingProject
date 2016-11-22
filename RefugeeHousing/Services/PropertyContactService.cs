using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using RefugeeHousing.Models;
using RefugeeHousing.ViewModels;
using SendGrid.Helpers.Mail;

namespace RefugeeHousing.Services
{
    public interface IPropertyContactService
    {
        Task ContactOwner(PropertyEnquiry enquiry);
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

        public async Task ContactOwner(PropertyEnquiry enquiry)
        {
            var listing = dbContext.Listings
                .Include(l => l.Owner)
                .First(l => l.Id == enquiry.PropertyId);

            var recipient = listing.Owner.Email;

            // TODO REF-42: Fill in real values
            var from = new Email(FromAddress);
            var to = new Email(recipient);
            var subject = "Email from Refugee Housing Project";
            var content = new Content("text/plain", ContentText(enquiry));

            var email = new Mail(from, subject, to, content);

            Logger.Info($"Sending email to '{recipient}' about property '{enquiry.PropertyId}'");

            await propertyEmailService.SendEmail(email);
        }

        private static string ContentText(PropertyEnquiry enquiry)
        {
            return "Hello,\n" +
                   $"You have a query from {enquiry.InquirerName} regarding your property.";
        }
    }
}