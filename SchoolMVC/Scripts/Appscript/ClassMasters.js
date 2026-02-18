$(document).ready(function () {
    ClassList();
    if ($("#hdnchk_Higer").val() != "") {
        $("#chk_Higer").prop("checked", true);
    }
});
function ClassList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        //papai
        $.ajax({
            url: '/JQuery/ClassList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>Class Type</th><th>Class Name</th><th>Class Code</th><th></th><th></th></tr></thead>";
                    $data.append($header);


                        $.each(res.Data, function (i, row) {
                            var $row = $('<tr/>');
                        $row.append($('<td>').text(i + 1));
                        $row.append($('<td style=display:none>').append(row.CM_CLASSID));
                        $row.append($('<td>').append(row.CM_TYPENAME));
                        $row.append($('<td>').append(row.CM_CLASSNAME));
                        $row.append($('<td>').append(row.CM_CLASSCODE));

                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/Class/" + row.CM_CLASSID + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.CM_CLASSID + ");'" + row.CM_CLASSID + " class='btn btn-danger'>Delete</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-danger disabled'>Delete</a>"));

                        }
                        $data.append($row);
                    });
                    $("#update-panel").html($data);
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
// Validate form...
function ValidateOperation() {
    if ($('#CM_CTM_TYPEID').val() == 0) {
        WarningToast('Please select a class type');
        return false;
    }

    if ($('#CM_CLASSNAME').val() == "") {
        WarningToast('Please provide a class name');
        return false;
    }

    if ($('#CM_ClassPreference').val() == '') {
        WarningToast('Please provide class preference.');
        return false;
    }
    if ($('#CM_FROMAGE').val() > $('#CM_TOAGE').val()) {
        WarningToast('Please provide valid age gap.');
        return false;
    }
    if ($('#CM_FROMAGE').val() == $('#CM_TOAGE').val()) {
        WarningToast('Please provide Age gap.');
        return false;
    }
    return true;
}
// Insert & Update Class in Database
function InsertUpdateClass() {
    var IsCheck;
    if (ValidateOperation() == true) {
        if ($('#chk_Higer').is(':checked')) {
            IsCheck='true'
        }
        else
        {
            IsCheck = 'False'
        }
        var _data = JSON.stringify({
            CM_CLASSID: $('#CM_CLASSID').val(),
            CM_CLASSCODE: $('#CM_CLASSCODE').val(),
            CM_CTM_TYPEID: $('#CM_CTM_TYPEID').val(),
            CM_CLASSNAME: $('#CM_CLASSNAME').val(),          
            CM_FROMAGE: $('#CM_FROMAGE').val(),
            CM_TOAGE: $('#CM_TOAGE').val(),
            CM_Preference: $('#CM_Preference').val(),            
            IsHigherSecondary: IsCheck,
            Userid: $('#hdnUserid').val() 
        });

        $.ajax({
            url: '/JQuery/InsertUpdateClass',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    $('#btnSave').attr('disabled', 'disabled');
                    setTimeout(function () {
                       
                        window.location.href = '/Masters/ClassList';
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
// Confirm to Delete
function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
// Delete data from database
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'ClassMaster_CM',
        MainFieldName: 'CM_CLASSID',
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
                    window.location.href = '/Masters/ClassList';
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