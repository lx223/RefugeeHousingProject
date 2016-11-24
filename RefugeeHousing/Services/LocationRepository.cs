using System.Linq;
using RefugeeHousing.ApiAccess;
using RefugeeHousing.Models;

namespace RefugeeHousing.Services
{
    public class LocationRepository
    {
        public static Location GetOrCreateLocation(string locationId)
        {
            using (var db = new ApplicationDbContext())
            {
                Location location;
                if (db.Locations.Any(l => l.Id == locationId))
                {
                    location = db.Locations.Single(l => l.Id == locationId);
                }
                else
                {
                    var placeLookUpService = new PlaceLookUpService();
                    var englishName = placeLookUpService.FindLocalityNameByLocationId(locationId,
                        PlaceLookUpService.Languages.English);
                    var greekName = placeLookUpService.FindLocalityNameByLocationId(locationId, PlaceLookUpService.Languages.Greek);
                    location = new Location { EnglishName = englishName, GreekName = greekName, Id = locationId };
                    db.Locations.Add(location);
                    db.SaveChanges();
                }
                return location;
            }    
        }
    }
}