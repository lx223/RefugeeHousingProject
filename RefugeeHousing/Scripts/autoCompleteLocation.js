﻿define(["googlemaps", "jquery", "translationConfig"], function (googleMaps, $, translationConfig) {
    var autocomplete;

    var placeChangedEvent = "place_changed"; // Event fired by Google API when the place changed

    function showErrorMessage(message) {
        $("#location-form-group").addClass("has-error");
        $("#location-help-block").text(message);
    }

    function hideErrorMessage() {
        $("#location-form-group").removeClass("has-error");
        $("#location-help-block").text("");
    }

    function getAdministrativeAreaLevel5Address(place) {
        var addressComponents = place["address_components"];
        for (var i = 0; i < addressComponents.length; i++) {
            var types = addressComponents[i]["types"];
            if (types.indexOf("administrative_area_level_5") !== -1) {
                return addressComponents[i]["long_name"];
            }
        }
        return null;
    }

    function placeChanged() {
        var place = autocomplete.getPlace();

        if (!place.geometry) {
            showErrorMessage(translationConfig.noDetailsAvailableForInput + " '" + place.name + "'");
        } else {
            var locality = getAdministrativeAreaLevel5Address(place);
            if (locality == null) {
                showErrorMessage(translationConfig.needMoreSpecificAddress);
            } else {
                hideErrorMessage();
                document.getElementById("place_id").value = place.place_id;
                document.getElementById("locality").value = locality;
            }
        }
    }

    function initAutocomplete() {
        var options = {
            componentRestrictions: { "country": "gr" }
        }
        // ReSharper disable once UseOfImplicitGlobalInFunctionScope
        autocomplete = new google.maps.places.Autocomplete(document.getElementById("location"), options);
        autocomplete.addListener(placeChangedEvent, placeChanged);
    }

    return {
        init: function() {
            initAutocomplete();
        }
    }
});


