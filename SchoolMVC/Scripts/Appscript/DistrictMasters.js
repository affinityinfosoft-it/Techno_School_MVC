function InsertUpdateDistrict()
{
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
            DM_NATIONID: $('#DM_NATIONID').val(),
            DM_DISTRICTID: $('#DM_DISTRICTID').val(),
            DM_STATEID: $('#DM_STATEID').val(),
            DM_DISTRICTNAME: $('#DM_DISTRICTNAME').val(),
            Userid: $('#hdnUserid').val()

        });
        var url =BaseUrl+ "/JQuery/InsertUpdateDistrict";
        $.ajax({
            url: '/JQuery/InsertUpdateDistrict',
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
                        window.location.href = '/Masters/DistrictList';
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
    if ($('#DM_NATIONID').val() == 0) {
        WarningToast('Please select a Nation');
        return false;
    }
    if ($('#DM_STATEID').val() == 0) {
        WarningToast('Please select a State');
        return false;
    }
    if ($('#DM_DISTRICTNAME').val() == '') {
        WarningToast('Please provide District name.');
        return false;
    }
    return true;
}
function DistrictList()
{
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        //papai
        $.ajax({
            url: '/JQuery/DistrictList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th style=display:none>Id</th><th>District</th><th>State</th><th>Nation</th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td style=display:none>').append(row.DM_DISTRICTID));
                        $row.append($('<td>').append(row.DM_DISTRICTNAME));
                        $row.append($('<td>').append(row.DM_STATENAME));
                        $row.append($('<td>').append(row.DM_NATIONNAME));
                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/District/" + row.DM_DISTRICTID + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));

                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.DM_DISTRICTID + ");'" + row.DM_DISTRICTID + " class='btn btn-danger'>Delete</a>"));
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
    DistrictList();
});
function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'DistrictMasters_DM',
        MainFieldName: 'DM_DISTRICTID',
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
                    window.location.href = '/Masters/DistrictList';
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