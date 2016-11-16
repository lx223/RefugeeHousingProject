﻿$.fn.dataTable.ext.search.push(
    // ReSharper disable once UnusedParameter
    function (settings, data, dataIndex) {
        
        const minBedroomsFilterText = parseInt($('#filter-min-bedrooms-text').val(), 10);
        const maxPriceFilterText = parseInt($('#filter-max-price-text').val(), 10);
        const shouldBeFurnishedFilterText = $('#filter-furnished-text').val();

        const listingBedrooms = parseFloat(data[1]);
        const listingIsFurnished = data[2] === "Yes" ? "True" : "False";
        const listingPrice = parseInt(data[3].substring(1));

        
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

        return true;
    }
);

$(document).ready(function () {
    var table = $('#listings-table').DataTable();

    // Event listener to the two range filtering inputs to redraw on input
    $('#filter-min-bedrooms-text, #filter-max-price-text, #filter-furnished-text').change(function () {
        table.draw();
    });
});