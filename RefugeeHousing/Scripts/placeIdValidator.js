define(["jquery", "translationConfig"], function ($, translationConfig) {

  function showErrorMessage(message) {
    $("#location-form-group").addClass("has-error");
    $("#location-help-block").text(message);
  }

  function hideErrorMessage() {
    $("#location-form-group").removeClass("has-error");
    $("#location-help-block").text("");
  }

  return {
    init: function() {
      $("#add-property-form").submit(function (event) {
        var placeId = $("#place_id").val();
        if (placeId === "") {
          showErrorMessage(translationConfig.useLocationAutocompleteForInput);
          event.preventDefault();
        } else {
          hideErrorMessage();
        }
      });    }
  }
});