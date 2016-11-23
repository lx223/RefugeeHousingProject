using System.Collections.Generic;

namespace RefugeeHousing.ApiClient
{
    public class PlaceIdLookUpResult
    {
        public ResultWrapper Result { get; set; }
        public class ResultWrapper
        {
            public List<AddressComponent> AddressComponents { get; set; } 
        }

        public class AddressComponent
        {
            public string LongName { get; set; }
            public string ShortName { get; set; }
            public List<string> Types { get; set; } 
        }
    }
}