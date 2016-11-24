var autocomplete;

var placeChangedEvent = "place_changed"; // Event fired by Google API when the place changed

function initAutocomplete() {
    var options = {
        componentRestrictions: { "country": "gr"}
    }
    autocomplete = new google.maps.places.Autocomplete(document.getElementById("location"), options);
    autocomplete.addListener(placeChangedEvent, placeChanged);
}

function placeChanged() {
    var place = autocomplete.getPlace();

    if (!place.geometry) {
        showErrorMessage("No details available for input: '" + place.name + "'");
    } else if (!isPlaceSpecific(place)) {
        showErrorMessage("Please input a more specific address.");
    } else {
        hideErrorMessage();
        document.getElementById("place_id").value = place.place_id;
    }
}

function showErrorMessage(message) {
    $("#location-form-group").addClass("has-error");
    $("#location-help-block").text(message);
}

function hideErrorMessage() {
    $("#location-form-group").removeClass("has-error");
    $("#location-help-block").text("");
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