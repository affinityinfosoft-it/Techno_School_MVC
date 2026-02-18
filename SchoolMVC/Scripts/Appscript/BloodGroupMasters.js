$(document).ready(function () {
    BloodGroupList();
});
// Load Blood group list In List Page
function BloodGroupList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        //papai
        $.ajax({
            url: '/JQuery/BloodList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th style=display:none>Id</th><th>Serial No</th><th>Blood Group</th><th></th><th></th><</tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td style=display:none>').append(row.BGM_BLDGRPID));
                        $row.append($('<td>').append(i + 1));
                        $row.append($('<td>').append(row.BGM_BLDGRPNAME));

                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/BloodGroup/" + row.BGM_BLDGRPID + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td >').append("<a onclick='Confirm(" + row.BGM_BLDGRPID + ");'" + row.BGM_BLDGRPID + " class='btn btn-danger'>Delete</a>"));
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
    }, 1000);
    
}
// Validate Form 
function ValidateOperation() {

    if ($('#BGM_BLDGRPNAME').val() == "") {
        WarningToast('Please provide a blood group.');
        return false;
    }
    return true;
}
// Insert Update Blood Group
function InsertUpdateBloodGroup() {
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
            BGM_BLDGRPID: $('#BGM_BLDGRPID').val(),
            BGM_BLDGRPNAME: $('#BGM_BLDGRPNAME').val(),
            Userid: $('#hdnUserid').val()
        });
        $.ajax({
            url: '/JQuery/InsertUpdateBloodGroup',
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
                        window.location.href = '/Masters/BloodGroupList';
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
// Confirm to Delete blood Group
function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
// Delete Individual Blood Group From Database
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'BloodGroupMasters_BGM',
        MainFieldName: 'BGM_BLDGRPID',
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
                    window.location.href = '/Masters/BloodGroupList';
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