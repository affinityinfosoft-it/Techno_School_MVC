$(document).ready(function () {
    BoardList();
});
// Load Board List in List Page
function BoardList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        //papai
        $.ajax({
            url: '/JQuery/BoardList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th style=display:none>Id</th><th>Board Code</th><th>Board Name</th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td style=display:none>').append(row.BM_BOARDID));
                        $row.append($('<td>').append(row.BM_BOARDCODE));
                        $row.append($('<td>').append(row.BM_BOARDNAME));
                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/Board/" + row.BM_BOARDID + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.BM_BOARDID + ");'" + row.BM_BOARDID + " class='btn btn-danger'>Delete</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-danger disabled'>Delete</a>"));

                        }
                        $data.append($row);
                    });
                    $("#update-panel").html($data);
                    //$("#tblList").DataTable();
                    $('#tblList').DataTable({
                        "order": [[0, "desc"]]
                    });
                }
                else {
                    $noData = "<div>No data Found</td>"
                    $("#update-panel").html($noData);
                }
            },
            failure: function () {
                ErrorToast('something wrong happen');
            }

        });
    }, 1000);
    
}
// Insert & Update Board 
function InsertUpdateBoard() {
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
            BM_BOARDID: $('#BM_BOARDID').val(),
            BM_BOARDCODE: $('#BM_BOARDCODE').val(),
            BM_BOARDNAME: $('#BM_BOARDNAME').val(),           
            Userid: $('#hdnUserid').val()

        });

        $.ajax({
            url: '/JQuery/InsertUpdateBoard',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    $('#btnSave').attr('disabled', 'disabled');
                    setTimeout(function () {
                        //papai
                        window.location.href = '/Masters/BoardList';
                    }, 2000);
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
}
// Validate Form
function ValidateOperation() {

    //if ($('#BM_BOARDCODE').val() == "") {
    //    alert('Please provide a board code');
    //    return false;
    //}
    if ($('#BM_BOARDNAME').val() == "") {
        WarningToast('Please provide a board name');
        return false;
    }   
    return true;
}
// Confirm to Delete
function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
// Delete Board From database
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'BoardMasters_BM',
        MainFieldName: 'BM_BOARDID',
        PId: parseInt(fieldId),
    });
    $.ajax({
        url: '/JQuery/DeleteData',
        type: 'POST',
        data: _data,
        contentType: 'application/json ; utf-8',
        dataType: 'json',
        success: function (data) {
            if (data != null && data != undefined && data.IsSuccess == true) {
                SuccessToast(data.Message);
                setTimeout(function () {
                    //papai
                    window.location.href = '/Masters/BoardList';
                }, 2000);
            }
            else {
                ErrorToast(data.Message);
            }
        },
        error: function () {
            ErrorToast('Something wrong happend.');
        }

    });
}