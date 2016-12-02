define(["jquery", "datatables"], function ($, dataTables) {
    return {
        init: function (language) {
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
        }
    }
});