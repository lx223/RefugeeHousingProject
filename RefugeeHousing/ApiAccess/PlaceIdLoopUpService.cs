using System.Linq;
using RefugeeHousing.ApiClient;
using RestSharp;

namespace RefugeeHousing.ApiAccess
{
    public class PlaceIdLoopUpService
    {
        public enum Languages
        {
            English,
            Greek
        };

        public static string FindLocalityNameByLocationId(string locationId, Languages language)
        {
            var client = new RestClient("https://maps.googleapis.com/maps/api/place/details/json?");
            var request = new RestRequest(Method.GET);
            request.AddParameter("key", "AIzaSyAi0qtOthsQrOMs_IrfghpmlBKqaSUHmI0");
            request.AddParameter("placeid", locationId);

            if (language == Languages.Greek)
            {
                request.AddParameter("language", "el");
            }

            var response = client.Execute<PlaceIdLookUpResult>(request);
            var addressComponents = response.Data.Result.AddressComponents;
            var localityComponent = addressComponents.Single(s => s.Types.Contains("locality"));
            return localityComponent.LongName;
        }
    }
}