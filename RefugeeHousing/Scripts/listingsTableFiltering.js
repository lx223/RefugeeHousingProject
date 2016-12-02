define(["jquery", "datatables"], function($, datatables) {
  $.fn.dataTable.ext.search.push(
    // ReSharper disable once UnusedParameter
    function(settings, data, dataIndex) {

      const minBedroomsFilterText = parseInt($('#filter-min-bedrooms-text').val(), 10);
      const maxPriceFilterText = parseInt($('#filter-max-price-text').val(), 10);
      const shouldBeFurnishedFilterText = $('#filter-furnished-text').val();
      const locationFilterText = $('#filter-location-text').val().toLowerCase();

      const listingBedrooms = parseFloat(data[1]);
      const listingIsFurnished = data[2] === "Yes" ? "True" : "False";
      const listingLocation = data[3].toLowerCase();
      const listingPrice = parseInt(data[4].substring(1));


      if (!isNaN(minBedroomsFilterText) && listingBedrooms < minBedroomsFilterText) {
        return false;
      }

      if (!isNaN(maxPriceFilterText) && listingPrice > maxPriceFilterText) {
        return false;
      }

      if (shouldBeFurnishedFilterText !== null && shouldBeFurnishedFilterText !== "") {
        if (shouldBeFurnishedFilterText !== listingIsFurnished) {
          return false;
        }
      }

      if (locationFilterText !== null && locationFilterText !== "") {
        if (!listingLocation.includes(locationFilterText)) {
          return false;
        }
      }

      return true;
    }
  );

    function initListingsTableFiltering(language) {
        if (language == 'el') {
            var table = $('#listings-table').DataTable({
                language: {
                    url: '//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Greek.json'
                }
            }
            );
        } else {
            var table = $('#listings-table').DataTable();
        }

        $('#filter-min-bedrooms-text, #filter-max-price-text, #filter-furnished-text').change(function () {
            table.draw();
        });

        $('#filter-location-text').on('input', function () {
            table.draw();
        });
    }

    return {
        init: function () {
            initListingsTableFiltering();
        }
    }

});
