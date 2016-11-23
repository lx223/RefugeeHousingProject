function autoCompleteLocation() {
    var places = new google.maps.places.Autocomplete(document.getElementById("location"), {
        componentRestrictions: {"country": "gr"}
    });
    google.maps.event.addListener(places, "place_changed", function () {
        document.getElementById("place_id").value = places.getPlace().place_id;
    });
}