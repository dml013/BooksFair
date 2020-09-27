// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {

});

function deleteRow(dataTable, apiDel) {
    $("#tblData").on("click", ".js-delete", function () {
        let button = $(this);
        swal({
            title: "Are you sure you want to Delete",
            text: "You will not be able to restore the data!",
            icon: "warning",
            buttons: true,
            dangerMode: true
        }).then(willDelete => {
            if (willDelete) {
                $.ajax({
                    url: apiDel + button.attr("data-id"),
                    method: "DELETE",
                    success: function (responce) {
                        if (responce.success) {
                            toastr.success(responce.message);
                            dataTable.row(button.parents("tr")).remove().draw();
                        } else {
                            toastr.error("something goes wrong " + responce.message);
                        }
                    },
                    error: function (data) {
                        let responce = data.responseJSON;
                        if (!responce.success) {
                            toastr.error(responce.message);
                        } else {
                            toastr.error("something goes wrong " + responce.message);
                        }
                    }
                });
            }
        });
    });
}


function renderEditDeleteButtons(id, link) {
    return /*html*/`
    <div class="text-center">
        <a class="btn btn-success text-white" 
        href="${link}${id}">
            <i class="fas fa-edit"></i>
        </a>
        <button class="btn btn-danger text-white js-delete" data-id="${id}">
            <i class="fas fa-trash-alt"></i>
        </button>
    </div>
    `;
}