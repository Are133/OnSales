
let idDeleteEstate;


let DeleteEstate = function (entity, element, id) {
    idDeleteEstate = id;

    console.log(idDeleteEstate)

    document.getElementById('cmdDelete').addEventListener('click', () => {
        console.log(id);
        ConfirmDeleteEstate();
    })

    event.stopPropagation();
    document.getElementById('deleteTitle').innerHTML = `Eliminar ${entity}`;
    document.getElementById('deleteMessage').innerHTML = `¿Estas seguro de borrar el registro ${element} ?`;

    let elem = document.getElementById('deleteModal');
    let instance = M.Modal.init(elem, { dismissible: false });

    instance.open();
    
}

let urlApoyo = "https://localhost:44310/Countries";

let ConfirmDeleteEstate = function () {
    $.ajax({
        type: "POST",
        url: "https://localhost:44310/Countries/DeleteConfirmedEstate",
        data: { id: idDeleteEstate }
    }).done(function (data) {
        document.getElementById(`row_${data.id}`).remove();
        var toastHTML = '<span>Registro eliminado</span><button class="btn-flat toast-action"><i class="material-icons">check</i></button>';
        M.toast({ html: toastHTML });
    }).fail(function () {
        var toastHTML = '<span>No elimino el registrp</span><button class="btn-flat toast-action"><i class="material-icons">error</i></button>';
        M.toast({ html: toastHTML });
    }).always(function () {

    });
}
