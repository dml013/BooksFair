var dataTable;

$(loadDataTable(), deleteRow());

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        ajax: {
            url: "/Admin/Category/GetAll"
        },
        columns: [
            { data: "name", width: "60%" },
            {
                data: "id",
                render: function (data) {
                    return /*html*/`
                    <div class="text-center">
                        <a class="btn btn-success text-white" 
                        href="Category/Upsert/${data}">
                            <i class="fas fa-edit"></i>
                        </a>
                        <button class="btn btn-danger text-white js-delete" data-category-id="${data}">
                            <i class="fas fa-trash-alt"></i>
                        </button>
                    </div>
                    `;
                }, width: "40%"
            }
        ]
    });
}

function deleteRow() {
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
                    url: "/test/del/" + button.attr("data-category-id"),
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

