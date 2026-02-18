$(document).ready(function () {
    LeaveTypeList();
});
function LeaveTypeList() {
    $('#update-panel').html('loading data.....');
    $.ajax({
        url: '/JQuery/LeaveTypeList',
        dataType: 'json',
        type: 'GET',
        success: function (res) {
            if (res.Data.length > 0) {
                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                $header = "<thead><tr><th style=display:none>Id</th><th>Serial No</th><th>LeaveType Code</th><th>LeaveType Name</th><th>Leave Category</th><th></th><th></th><</tr></thead>";
                $data.append($header);

                $.each(res.Data, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td style=display:none>').append(row.LeaveTypeId));
                    $row.append($('<td>').append(i + 1));
                    $row.append($('<td>').append(row.LeaveTypeCode));
                    $row.append($('<td>').append(row.LeaveTypeName));
                    $row.append($('<td>').append(row.LeaveCategory));

                    if (res.CanEdit == true) {
                        $row.append($('<td>').append("<a href=/Masters/LeaveType/" + row.LeaveTypeId + " class='btn btn-warning'>Edit</a>"));
                    }
                    else {
                        $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                    }
                    if (res.CanDelete == true) {
                        $row.append($('<td >').append("<a onclick='Confirm(" + row.LeaveTypeId + ");'" + row.LeaveTypeId + " class='btn btn-danger'>Delete</a>"));
                    }
                    else {
                        $row.append($('<td>').append("<a href=# class='btn btn-danger disabled'>Delete</a>"));

                    }
                    $data.append($row);
                });
                $("#update-panel").html($data);
                $("#tblList").DataTable();
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
function ValidateOperation() {

    if ($('#LeaveTypeCode').val() == "") {
        WarningToast('Please provide a Leave Type Code.');
        return false;
    }
    if ($('#LeaveTypeName').val() == "") {
        WarningToast('Please provide a Leave Type name.');
        return false;
    }
    if ($('#LeaveCategory').val() == "") {
        WarningToast('Please select category.');
        return false;
    }
    return true;
}
// Insert Update Bank
function InsertUpdateLeaveType() {
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
            LeaveTypeId: $('#LeaveTypeId ').val(),
            LeaveTypeCode: $('#LeaveTypeCode ').val(),
            LeaveTypeName: $('#LeaveTypeName  ').val(),
            LeaveCategory: $('#LeaveCategory  ').val(),
            Userid: $('#hdnUserid').val()
        });
        $.ajax({
            url: '/JQuery/InsertUpdateLeaveType',
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
                        window.location.href = '/Masters/LeaveTypeList';
                    }, 2000);
                }
            },
            error: function () {
                ErrorToast('Something wrong happened.');
            }
        });
    }
}
function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'LeaveType_LT',
        MainFieldName: 'LeaveTypeId',
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
                    window.location.href = '/Masters/LeaveTypeList';
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