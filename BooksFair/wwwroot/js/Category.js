﻿var dataTable;
var apiDel = "/test/del/";
var apiUpsert = "Category/Upsert/";

$(loadDataTable(), deleteRow(dataTable, apiDel));

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        ajax: {
            url: "/Admin/Category/GetAll"
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



