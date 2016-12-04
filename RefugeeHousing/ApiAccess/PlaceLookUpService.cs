using System;
using System.IO;
using System.Linq;
using RestSharp;

namespace RefugeeHousing.ApiAccess
{
    public interface IPlaceLookUpService
    {
        string FindLocalityNameByLocationId(string locationId, PlaceLookUpService.Languages language);
    }

    public class PlaceLookUpService : IPlaceLookUpService
    {
        private const string GoogleApiBaseUrl = "https://maps.googleapis.com/maps/api/place/details/json";
        private const string LanguageGreek = "el";
        private const string AddressTypes = "administrative_area_level_5";

        private const string ApiKeyEnvironmentVariable = "REFUGEE_HOUSING_GOOGLE_API_KEY";

        public enum Languages
        {
            English,
            Greek
        };

        public string FindLocalityNameByLocationId(string locationId, Languages language)
        {
            var apiKey = Environment.GetEnvironmentVariable(ApiKeyEnvironmentVariable);

            if (apiKey == null)
            {
                throw new InvalidOperationException(
                    $"Could not find the '{ApiKeyEnvironmentVariable}' environment variable, so cannot look up location in the Google Maps API");
            }

            var client = new RestClient(GoogleApiBaseUrl);
            var request = new RestRequest(Method.GET);
            request.AddParameter("key", apiKey);
            request.AddParameter("placeid", locationId);

            if (language == Languages.Greek)
            {
                request.AddParameter("language", LanguageGreek);
            }

            var response = client.Execute<PlaceLookUpResult>(request);

            if (response.ResponseStatus == ResponseStatus.Error)
            {
                throw new IOException("Error in Google Place API when fetching location details for location id: " +
                    locationId + ". Status code returned: " + response.StatusCode);
            }
            var addressComponents = response.Data.Result.AddressComponents;
            var localityComponent = addressComponents.Single(s => s.Types.Contains(AddressTypes));
            return localityComponent.LongName;
        }
    }
}