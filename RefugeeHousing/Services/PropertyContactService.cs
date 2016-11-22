using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using RefugeeHousing.Models;
using RefugeeHousing.ViewModels;

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
        private readonly IEmailBuilder emailBuilder;

        public PropertyContactService(IPropertyEmailService propertyEmailService, IApplicationDbContext dbContext, IEmailBuilder emailBuilder)
        {
            this.propertyEmailService = propertyEmailService;
            this.dbContext = dbContext;
            this.emailBuilder = emailBuilder;
        }

        public async Task ContactOwner(PropertyEnquiry enquiry)
        {
            var listing = dbContext.Listings
                .Include(l => l.Owner)
                .First(l => l.Id == enquiry.PropertyId);

            var email = emailBuilder.Build(enquiry, listing.Owner);

            Logger.Info($"Sending email to '{listing.Owner.Email}' about property '{enquiry.PropertyId}'");

            await propertyEmailService.SendEmail(email);
        }
    }
}