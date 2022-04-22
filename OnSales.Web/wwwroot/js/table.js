$(document).ready(function () {
    var table = $('#live-datatable-example').DataTable({
        "order": [],
        "dom": 'Bfrtip',
        // Button class
        "buttons": {
            "dom": {
                "button": {
                    "tag": "button",
                    "className": "waves-effect waves-light btn mrm"
                }
            },
            // like csv Buttons,print buttons,pdfHtml5 button
            "buttons": ['copyHtml5', 'excelHtml5', 'csvHtml5', 'pdfHtml5']
        }
    });
});