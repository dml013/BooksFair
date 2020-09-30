var dataTable;
var getAll = "Product/GetAll";
var apiDel = "Product/Delete/";
var apiUpsert = "Product/Upsert/";

$(loadDataTable(), deleteRow(dataTable, apiDel));

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        ajax: {
            url: getAll
        },
        columns: [
            { data: "title", width: "" },
            { data: "isbn", width: "" },
            { data: "price", width: "" },
            { data: "author", width: "" },
            { data: "category.name", width: "" },
            {
                data: "id",
                render: data => {
                    return renderEditDeleteButtons(data, apiUpsert);
                }, width: "15%"
            }
        ]
    });
}



