var dataTable;
var getAll = "/Admin/Company/GetAll";
var apiDel = "Company/Delete/";
var apiUpsert = "Company/Upsert/";

$(loadDataTable(), deleteRow(dataTable, apiDel));

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        ajax: {
            url: getAll
        },
        columns: [
            { data: "name", width: "15%" },
            { data: "streetAddress", width: "15%" },
            { data: "city", width: "12%" },
            { data: "state", width: "12%" },
            { data: "phoneNumber", width: "15%" },
            {
                data: "isAuthorizedCompany",
                render: function (data) {
                    if (data) {
                        return /*html*/ `<div class="text-center h5">
                            <i class="far fa-check-square"></i>
                        </div >`;
                    } else {
                        return /*html*/ `<div class="text-center h5">
                            <i class="far fa-square"></i>
                        </div >`;
                    }
                },
                width: "5%"
            },
            {
                data: "id",
                render: data => {
                    return renderEditDeleteButtons(data, apiUpsert);
                }, width: "20%"
            }
        ]
    });
}



