define(["jquery"], function ($) {

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
          showErrorMessage("Please use Location autocomplete to input a valid address");
          event.preventDefault();
        } else {
          hideErrorMessage();
        }
      });    }
  }
});