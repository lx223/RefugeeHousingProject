var autocomplete;

function initAutocomplete() {
    var options = {
        componentRestrictions: { "country": "gr"}
    }
    autocomplete = new google.maps.places.Autocomplete(document.getElementById("location"), options);
    autocomplete.addListener("place_changed", placeChanged);
}

function placeChanged() {
    var place = autocomplete.getPlace();
    if (!place.geometry) {
        window.alert("No details available for input: '" + place.name + "'");
    } else if (!isPlaceSpecific(place)) {
        window.alert("Please input a more specific address.");
    } else {
        document.getElementById("place_id").value = place.place_id;
    }
}

function isPlaceSpecific(place) {
    var addressComponents = place["address_components"];
    if (containsAdministrativeAreaLevel5Address(addressComponents)) {
        return true;
    } else {
        return false;
    }
}

function containsAdministrativeAreaLevel5Address(addressComponents) {
    for (var i = 0; i < addressComponents.length; i++) {
        var types = addressComponents[i]["types"];
        if (types.indexOf("administrative_area_level_5") != -1) {
            return true;
        }
    }
    return false;
}