var dataTable;
var apiDel = "CoverType/Delete/"; //скорее всего надо добавить area
var apiUpsert = "CoverType/Upsert/";

$(loadDataTable(), deleteRow(dataTable, apiDel));

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        ajax: {
            url: "/Admin/CoverType/GetAll"
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



