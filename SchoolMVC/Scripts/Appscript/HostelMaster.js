$(document).ready(function () {
    HostelRoomList();
});
function HostelRoomList() {
    $('#update-panel').html('loading data.....');
    $.ajax({
        url: '/JQuery/HostelRoomList',
        dataType: 'json',
        type: 'GET',
        success: function (res) {
            if (res.Data.length > 0) {
                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                $header = "<thead><tr><th style=display:none>Id</th><th>Serial No</th><th>Room No</th><th></th><th></th><</tr></thead>";
                $data.append($header);

                $.each(res.Data, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td style=display:none>').append(row.HR_HostelRoomId));
                    $row.append($('<td>').append(i + 1));
                    $row.append($('<td>').append(row.HR_HostelRoomNo));

                    if (res.CanEdit == true) {
                        $row.append($('<td>').append("<a href=/Masters/HostelRoom/" + row.HR_HostelRoomId + " class='btn btn-warning'>Edit</a>"));
                    }
                    else {
                        $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                    }
                    if (res.CanDelete == true) {
                        $row.append($('<td >').append("<a onclick='Confirm(" + row.HR_HostelRoomId + ");'" + row.HR_HostelRoomId + " class='btn btn-danger'>Delete</a>"));
                    }
                    else {
                        $row.append($('<td>').append("<a href=# class='btn btn-danger disabled'>Delete</a>"));

                    }
                    $data.append($row);
                });
                $("#update-panel").html($data);
                $("#tblList").DataTable();
                //$('#tblList').DataTable({
                //    "order": [[0, "desc"]]
                //});
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

    if ($('#HR_HostelRoomNo').val() == "") {
        WarningToast('Please provide a bank name.');
        return false;
    }
    return true;
}
// Insert Update Hostel Room No
function InsertUpdateHostelRoom() {
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
            HR_HostelRoomId: $('#HR_HostelRoomId').val(),
            HR_HostelRoomNo: $('#HR_HostelRoomNo').val(),
            Userid: $('#hdnUserid').val()
        });
        $.ajax({
            url: '/JQuery/InsertUpdateHostelRoom',
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
                        window.location.href = '/Masters/HostelRoomList';
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
        MainTableName: 'HostelRoomMaster_HR',
        MainFieldName: 'HR_HostelRoomId',
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
                    window.location.href = '/Masters/HostelRoomList';
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
