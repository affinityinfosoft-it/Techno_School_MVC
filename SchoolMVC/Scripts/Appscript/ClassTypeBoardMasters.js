$(document).ready(function () {
    BloodGroupList();
});
function InsertUpdateClassTypeBoard() {
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
            CTB_SCHBRDID: $('#CTB_SCHBRDID').val(),
            CTB_TYPEID: $('#CTB_TYPEID').val(),
            CTB_BOARDID: $('#CTB_BOARDID').val(),
            Userid: $('#hdnUserid').val()
        });
        $.ajax({
            url: '/JQuery/InsertUpdateClassTypeboard',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    window.location.href = '/Masters/ClassTypeBoardMappingList';
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
function ValidateOperation() {

    if ($('#CTB_TYPEID').val() == 0) {
        WarningToast('Please select a class type.');
        return false;
    }
    if ($('#CTB_BOARDID').val() == 0) {
        WarningToast('Please select a board.');
        return false;
    }
    return true;
}
function BloodGroupList() {
    $('#update-panel').html('loading data.....');
    $.ajax({
        url: '/JQuery/ClasstypeBoardList',
        dataType: 'json',
        type: 'GET',
        success: function (res) {
            if (res.Data.length > 0) {
                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>Class Type</th><th>Board</th><th></th><th></th></tr></thead>";
                $data.append($header);

                $.each(res.Data, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td>').text(i + 1));
                    $row.append($('<td style=display:none>').append(row.CTB_SCHBRDID));
                    $row.append($('<td>').append(row.CTB_CTM_TYPENAME));
                    $row.append($('<td>').append(row.CTB_BM_BOARDNAME));

                    if (res.CanEdit == true) {
                        $row.append($('<td>').append("<a href=/Masters/ClassTypeBoardMapping/" + row.CTB_SCHBRDID + " class='btn btn-warning'>Edit</a>"));
                    }
                    else {
                        $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                    }
                    if (res.CanDelete == true) {
                        $row.append($('<td>').append("<a onclick='Confirm(" + row.CTB_SCHBRDID + ");'" + row.CTB_SCHBRDID + " class='btn btn-danger'>Delete</a>"));
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
}
function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'ClassTypeBoard_CTB',
        MainFieldName: 'CTB_SCHBRDID',
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
                window.location.href = '/Masters/ClassTypeBoardMappingList';
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