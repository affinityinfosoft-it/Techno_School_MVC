var DataList = [];
$(document).ready(function () {
    //validateNumeric();
   
    });
function BindList() {
    $('#update-panel').html('Loading data...');

    $.ajax({
        url: '/JQuery/BookCopyMasterList',
        type: 'GET',
        dataType: 'json',
        success: function (res) {
            if (res.Data && res.Data.length > 0) {
                let tableHtml = '';
                tableHtml += '<table id="tblList" class="table table-bordered table-striped">';
                tableHtml += '<thead><tr>';
                tableHtml += '<th>SL</th>';
                tableHtml += '<th style="display:none">Id</th>';
                tableHtml += '<th>Book Copy code</th>';
                tableHtml += '<th>Book Copy Name</th>';
                tableHtml += '<th>Book Name</th>';
                tableHtml += '<th>Book Accession No.</th>';
                tableHtml += '<th>Barcode</th>';
                tableHtml += '<th>Purchase Date</th>';
                tableHtml += '<th>Price</th>';
                tableHtml += '<th></th>';
                tableHtml += '<th></th>';
                tableHtml += '</tr></thead><tbody>';

                // Loop through each row
                $.each(res.Data, function (i, row) {
                    tableHtml += '<tr>';
                    tableHtml += '<td>' + (i + 1) + '</td>';
                    tableHtml += '<td style="display:none">' + row.BCM_CopyId + '</td>';
                    tableHtml += '<td>' + row.BCM_CopyCode + '</td>';
                    tableHtml += '<td>' + row.BCM_CopyName + '</td>';
                    tableHtml += '<td>' + row.BM_Title + '</td>';
                    tableHtml += '<td>' + row.BCM_AccessionNo + '</td>';
                    tableHtml += '<td>' + row.BCM_Barcode + '</td>';
                    tableHtml += '<td>' + row.BCM_PurchaseDate + '</td>';
                    tableHtml += '<td>' + row.BCM_Price + '</td>';
                    if (res.CanEdit === true) {
                        tableHtml += '<td><a href="/Library/BookCopyMaster?id=' + row.BCM_CopyId + '" class="btn btn-warning">Edit</a></td>';
                    } else {
                        tableHtml += '<td><a href="#" class="btn btn-warning disabled">Edit</a></td>';
                    }

                    if (res.CanDelete === true) {
                        tableHtml += '<td><a class="btn btn-danger SelectDelete" ' + 'data-BCM_CopyId"' + row.BCM_CopyId + '">Delete</a></td>';
                    } else {
                        tableHtml += '<td><a href="#" class="btn btn-danger disabled">Delete</a></td>';
                    }

                    tableHtml += '</tr>';
                });

                tableHtml += '</tbody></table>';

                $('#update-panel').html(tableHtml);

                $('#tblList').DataTable({
                    order: [[1, 'desc']]
                });
            } else {
                $('#update-panel').html('<div>No data found</div>');
            }
        },
        error: function () {
            ErrorToast('Something went wrong');
        }
    });
}
function AddBookCopyDetails() {

        if (!$("#BCM_BookId").val()) {
            WarningToast("Please select Book!");
            return;
        }
        if (!$("#BCM_CopyCode").val()) {
            WarningToast("Please enter Copy Code!");
            return;
        }
        if (!$("#BCM_CopyName").val()) {
            WarningToast("Please enter Copy Name!");
            return;
        }
        //if (!$("#BCM_PurchaseDate").val()) {
        //    WarningToast("Please select Purchase Date!");
        //    return;
        //}

        if ($("#tblList tbody").length === 0) {
            WarningToast("Table not initialized.");
            return;
        }
        var bookId = $("#BCM_BookId").val();
        var bookName = $("#BCM_BookId option:selected").text();
        var copyCode = $("#BCM_CopyCode").val();
        var copyName = $("#BCM_CopyName").val();
        var accessionNo = $("#BCM_AccessionNo").val();
        var barcode = $("#BCM_Barcode").val();
        //var purchaseDate = $("#BCM_PurchaseDate").val();
        var price = $("#BCM_Price").val();
        var status = $("#BCM_Status").val();
        var $row = $('<tr/>');

        $row.append('<td style="display:none">0</td>');                      
        $row.append('<td style="display:none">' + bookId + '</td>');         
        $row.append('<td>' + bookName + '</td>');
        $row.append('<td>' + copyCode + '</td>');       
        $row.append('<td>' + copyName + '</td>');
        $row.append('<td>' + accessionNo + '</td>');                         
        $row.append('<td>' + barcode + '</td>');                             
        //$row.append('<td>' + purchaseDate + '</td>');                        
        $row.append('<td>' + price + '</td>');                               
        $row.append('<td>' + status + '</td>');                              

        $row.append(
            '<td><input type="image" src="/Content/images/close_icon.png" onclick="deleteRow(this)" /></td>'
        );

        $("#tblList>tbody").append($row);

        
   // }
}


function InsertUpdateBookCopyMaster() {
    
        var ArrayItem = [];
        var tblItem = document.getElementById('tblList');
        for (var i = 1; i < tblItem.rows.length; i++) {
            var cells = tblItem.rows[i].cells;
            if (!cells || cells.length < 9) continue; // safety

            ArrayItem.push({
                BCM_CopyId: parseInt(cells[0].innerHTML) || 0,
                BCM_BookId: parseInt(cells[1].innerHTML),
                BCM_CopyCode: cells[3].innerHTML,
                BCM_CopyName: cells[4].innerHTML,
                BCM_AccessionNo: cells[5].innerHTML,
                BCM_Barcode: cells[6].innerHTML,
                BCM_Price: cells[7].innerHTML,
                BCM_Status: cells[8].innerHTML
            });

        }
        //var _data = JSON.stringify({
        //    obj: {
        //        BookCopyMaster_Type: ArrayItem
        //    }
        //});
        var _data = JSON.stringify({
            BCM_CopyId: 0,
            BCM_SchoolId: $("#BCM_SchoolId").val(),
            BCM_CreatedBy: $("#BCM_CreatedBy").val(),
            BookCopyMaster_Type: ArrayItem
        });


        $.ajax({
            url: '/JQuery/InsertUpdateBookCopyMaster',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    window.location.href = '/Library/BookCopyMasterList';
                }
                else {
                    ErrorToast(data.Message);
                }
            },
            error: function () {
                ErrorToast('Something wrong happened.');
            }
        });
}
$(document).ready(function () {
    $('.selectpicker').selectpicker();
});