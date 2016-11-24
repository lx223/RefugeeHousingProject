window.onload = function() {
    $("#add-property-form").submit(function(event) {
        var placeId = $("#place_id").val();
        if (placeId == "") {
            alert("Please use Location autocomplete to input a valid address");
            event.preventDefault();
        } 
    });
};
