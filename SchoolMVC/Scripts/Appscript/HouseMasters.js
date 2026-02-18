function InsertUpdateHouse() {
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
            HM_HOUSEID: $('#HM_HOUSEID').val(),
            HM_HOUSECODE: $('#HM_HOUSECODE').val(),
            HM_HOUSENAME: $('#HM_HOUSENAME').val(),
            Userid: $('#hdnUserid').val()
        });
        $.ajax({
            url: '/JQuery/InsertUpdateHouse',
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
                        window.location.href = '/Masters/HouseList';
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
function ValidateOperation() {

    if ($('#HM_HOUSECODE').val() == "") {
        WarningToast('Please provide a house code');
        return false;
    }
    if ($('#HM_HOUSENAME').val() == "") {
        WarningToast('Please provide a house name');
        return false;
    }
    return true;
}
function HouseList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        //papai
        $.ajax({
            url: '/JQuery/HouseList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>House Code</th><th>House Name</th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td>').text(i + 1));
                        $row.append($('<td style=display:none>').append(row.HM_HOUSEID));
                        $row.append($('<td>').append(row.HM_HOUSECODE));
                        $row.append($('<td>').append(row.HM_HOUSENAME));
                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/House/" + row.HM_HOUSEID + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.HM_HOUSEID + ");'" + row.HM_HOUSEID + " class='btn btn-danger'>Delete</a>"));
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
$(document).ready(function () {
    HouseList();
});
function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'HouseMasters_HM',
        MainFieldName: 'HM_HOUSEID',
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
                    window.location.href = '/Masters/HouseList';
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