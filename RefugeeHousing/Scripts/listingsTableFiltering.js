$.fn.dataTable.ext.search.push(
    function (settings, data, dataIndex) {
        
        const minBedrooms = parseInt($('#filter-min-bedrooms-text').val(), 10);
        const maxPrice = parseInt($('#filter-max-price-text').val(), 10);
        const shouldBeFurnished = $('#filter-furnished-text').val();

        const bedrooms = parseFloat(data[1]);
        const furnished = data[2] === "Yes" ? "True" : "False";
        const price = parseInt(data[3].substring(1));

        
        if (!isNaN(minBedrooms) && bedrooms < minBedrooms) {
            return false;
        }

        if (!isNaN(maxPrice) && price > maxPrice) {
            return false;
        }

        if (shouldBeFurnished !== null && shouldBeFurnished !== "") {
            if (shouldBeFurnished !== furnished) {
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