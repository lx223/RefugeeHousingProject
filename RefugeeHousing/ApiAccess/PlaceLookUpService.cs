﻿using System.IO;
using System.Linq;
using RestSharp;

namespace RefugeeHousing.ApiAccess
{
    public class PlaceLookUpService
    {
        public enum Languages
        {
            English,
            Greek
        };

        public string FindLocalityNameByLocationId(string locationId, Languages language)
        {
            var client = new RestClient("https://maps.googleapis.com/maps/api/place/details/json");
            var request = new RestRequest(Method.GET);
            request.AddParameter("key", Resources.ApiKeys.GoogleMapsJavaScriptApi);
            request.AddParameter("placeid", locationId);

            if (language == Languages.Greek)
            {
                request.AddParameter("language", "el");
            }

            var response = client.Execute<PlaceLookUpResult>(request);

            if (response.ResponseStatus == ResponseStatus.Error)
            {
                throw new IOException("Error in Google Place API result. Status code returned: " + response.StatusCode);
            }
            var addressComponents = response.Data.Result.AddressComponents;
            var localityComponent = addressComponents.Single(s => s.Types.Contains("administrative_area_level_5"));
            return localityComponent.LongName;
        }
    }
}