define(["jquery", "datatables"], function ($) {
    return {
        init: function (language) {
            if (language === 'el') {
                $('#listings-table').DataTable({
                        language: {
                            url: '//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Greek.json'
                        }
                    }
                );
            } else {
                $('#listings-table').DataTable();
            }
        }
    }
});