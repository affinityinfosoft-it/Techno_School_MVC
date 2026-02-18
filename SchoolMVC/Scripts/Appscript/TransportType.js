function InsertUpdateTransportType() {
    if (ValidateOperation()) {
        var transport = {
            TypeId: $('#TypeId').val() === "" ? 0 : parseInt($('#TypeId').val()),
            TransportType: $('#TransportType').val(),
            Userid: parseInt($('#hdnUserid').val())
        };

        $.ajax({
            url: '/JQuery/InsertUpdateTransportType',
            type: 'POST',
            data: JSON.stringify(transport),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                if (data && data.IsSuccess) {
                    SuccessToast(data.Message);
                    $('#btnSave').prop('disabled', true);
                    setTimeout(function () {
                        window.location.href = '/Masters/TransportTypeList';
                    }, 2000);
                } else {
                    ErrorToast(data.Message || 'Unknown error occurred.');
                }
            },
            error: function () {
                ErrorToast('Something went wrong during the request.');
            }
        });
    }
}


function ValidateOperation() {

    if ($('#TransportType').val() == "") {
        WarningToast('Please provide a transport name.');
        return false;
    }

    return true;
}
function BindList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        //papai
        $.ajax({
            url: '/JQuery/TransportTypeList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th style=display:none>Id</th><th>Serial No.</th><th>Transport Type</th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td style=display:none>').append(row.TypeId));
                        $row.append($('<td>').append(i + 1));
                        $row.append($('<td>').append(row.TransportType));
                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/TransportType/" + row.TypeId + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.TypeId + ");'" + row.TypeId + " class='btn btn-danger'>Delete</a>"));
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
    }, 1000);

}
$(document).ready(function () {
    BindList();
});
function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'TransportType_TT',
        MainFieldName: 'TypeId',
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
                    window.location.href = '/Masters/TransportTypeList';
                }, 2000);
                window.location.href = '/Masters/TransportTypeList';
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