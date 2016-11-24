using System.IO;
using System.Linq;
using System.Web.Configuration;
using RestSharp;

namespace RefugeeHousing.ApiAccess
{
    public class PlaceLookUpService
    {
        private const string GoogleApiBaseUrl = "https://maps.googleapis.com/maps/api/place/details/json";
        private const string LanguageGreek = "el";
        private const string AddressTypes = "administrative_area_level_5";
        public enum Languages
        {
            English,
            Greek
        };

        public string FindLocalityNameByLocationId(string locationId, Languages language)
        {
            var client = new RestClient(GoogleApiBaseUrl);
            var request = new RestRequest(Method.GET);
            request.AddParameter("key", WebConfigurationManager.AppSettings["GoogleApiKey"]);
            request.AddParameter("placeid", locationId);

            if (language == Languages.Greek)
            {
                request.AddParameter("language", LanguageGreek);
            }

            var response = client.Execute<PlaceLookUpResult>(request);

            if (response.ResponseStatus == ResponseStatus.Error)
            {
                throw new IOException("Error in Google Place API result. Status code returned: " + response.StatusCode);
            }
            var addressComponents = response.Data.Result.AddressComponents;
            var localityComponent = addressComponents.Single(s => s.Types.Contains(AddressTypes));
            return localityComponent.LongName;
        }
    }
}