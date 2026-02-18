function InsertUpdateSubjectGroupWiseSub() {
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
            SGS_Id: $('#SGS_Id').val(),
            SGS_GM_Id: $('#SGS_GM_Id').val(),
            SGS_SM_Id: $('#SGS_SM_Id').val(),
            Userid: $('#hdnUserid').val()
        });
        $.ajax({
            url: '/JQuery/InsertUpdateSubjectgroupWiseSubject',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    window.location.href = '/Masters/SubjectGroupWiseSubjectList';
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

    if ($('#SGS_GM_Id').val() == 0) {
        WarningToast('Please select a subject group.');
        return false;
    }
    if ($('#SGS_SM_Id').val() == 0) {
        WarningToast('Please select a subject.');
        return false;
    }
   
    return true;
}
function BindList() {
    $('#update-panel').html('loading data.....');
    $.ajax({
        url: '/JQuery/SubjectGroupWiseSubjectList',
        dataType: 'json',
        type: 'GET',
        success: function (d) {
            if (d.length > 0) {
                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>Subject Group</th><th>Subject Name</th><th></th><th></th></tr></thead>";
                $data.append($header);

                $.each(d, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td>').text(i + 1));
                    $row.append($('<td style=display:none>').append(row.SGS_Id));

                    $row.append($('<td>').append(row.SGS_SGM_SubjectGroupName));
                    $row.append($('<td>').append(row.SGS_SBM_SubjectName));
                    $row.append($('<td>').append("<a href=/Masters/SubjectGroupWiseSubject/" + row.SGS_Id + " class='btn btn-warning'>Edit</a>"));
                    $row.append($('<td>').append("<a onclick='Confirm(" + row.SGS_Id + ");'" + row.SGS_Id + " class='btn btn-danger'>Delete</a>"));
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
        MainTableName: 'SubjectGroupWiseSubjectSetting_SGS',
        MainFieldName: 'SGS_Id',
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
                window.location.href = '/Masters/SubjectGroupWiseSubjectList';
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