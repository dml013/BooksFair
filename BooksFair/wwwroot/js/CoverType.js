var dataTable;
var getAll = "/Admin/CoverType/GetAll";
var apiDel = "CoverType/Delete/";
var apiUpsert = "CoverType/Upsert/";

$(loadDataTable(), deleteRow(dataTable, apiDel));

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        ajax: {
            url: getAll
        },
        columns: [
            { data: "name", width: "60%" },
            {
                data: "id",
                render: data => {
                    return renderEditDeleteButtons(data, apiUpsert);
                }, width: "40%"
            }
        ]
    });
}



