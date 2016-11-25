using System.Linq;
using RefugeeHousing.ApiAccess;
using RefugeeHousing.Models;

namespace RefugeeHousing.Services
{
    public interface ILocationRepository
    {
        Location GetOrCreateLocation(ApplicationDbContext db, string locationId);
    }

    public class LocationRepository : ILocationRepository
    {
        private readonly IPlaceLookUpService placeLookUpService;

        public LocationRepository(IPlaceLookUpService placeLookUpService)
        {
            this.placeLookUpService = placeLookUpService;
        }

        public Location GetOrCreateLocation(ApplicationDbContext db, string locationId)
        {
            Location location;
            if (db.Locations.Any(l => l.Id == locationId))
            {
                location = db.Locations.Single(l => l.Id == locationId);
            }
            else
            {
                var englishName = placeLookUpService.FindLocalityNameByLocationId(locationId,
                    PlaceLookUpService.Languages.English);
                var greekName = placeLookUpService.FindLocalityNameByLocationId(locationId,
                    PlaceLookUpService.Languages.Greek);
                location = new Location {EnglishName = englishName, GreekName = greekName, Id = locationId};
                db.Locations.Add(location);
                db.SaveChanges();
            }
            return location;
        }
    }
}