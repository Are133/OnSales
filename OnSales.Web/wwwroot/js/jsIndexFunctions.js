$(document).ready(function () {
    $("#example").DataTable({
        autoWidth: false,
        language: {
            url: "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json",
        },
        columnDefs: [
            {
                targets: ["_all"],
                className: "mdc-data-table__cell",
                paging: true,
                pageLength: 5,
                lengthChange: true,
                lengthMenu: [
                    [100, 250, -1],
                    [100, 250, "All"],
                ],
                order: [5, "asc"],
                dom: "Bfrtip",
            },
        ],
    });
});

let idDelete;

let Delete = function (entity, element, id) {

    idDelete = id;

    document.getElementById('cmdDelete').addEventListener('click', () => {
        console.log(id);
        ConfirmDelete();
    })

    event.stopPropagation();
    document.getElementById('deleteTitle').innerHTML = `Eliminar ${entity}`;
    document.getElementById('deleteMessage').innerHTML = `¿Estas seguro de borrar el registro ${element} ?`;

    
    let elem = document.getElementById('deleteModal');
    let instance = M.Modal.init(elem, { dismissible: false });

    instance.open();
}

let ConfirmDelete = function () {
    $.ajax({
        type: "POST",
        url: "Countries/DeleteConfirmed",
        data: { id: idDelete }
    }).done(function (data) {
        document.getElementById(`row_${data.id}`).remove();
        var toastHTML = '<span>Registro eliminado</span><button class="btn-flat toast-action"><i class="material-icons">check</i></button>';
        M.toast({ html: toastHTML });
    }).fail(function () {
        var toastHTML = '<span>Registro eliminado</span><button class="btn-flat toast-action"><i class="material-icons">error</i></button>';
        M.toast({ html: toastHTML });
    }).always(function () {
       
    });
}
