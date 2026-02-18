function InsertUpdateFaculty() {
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
            FP_Id: $('#FP_Id').val(),
            FP_Name: $('#FP_Name').val(),
            FP_DesignationId: $('#FP_DesignationId').val(),
            FP_DateOfJoining: convertToSqlDate($('#FP_DateOfJoining').val()),
            FP_DateOfBirth:convertToSqlDate($('#FP_DateOfBirth').val()),
            FP_Address: $('#FP_Address').val(),
            FP_Degree: $('#FP_Degree').val(),
            FP_Phone: $('#FP_Phone').val(),
            FP_Email: $('#FP_Email').val(),
            FP_Pin: $('#FP_Pin').val(),
            FP_FacultyCode: $('#FP_FacultyCode').val(),
            FP_ShortName: $('#FP_ShortName').val(),
            Userid: $('#hdnUserid').val(),
            FP_Max_DEGM_Id: $('#FP_Max_DEGM_Id').val(),
        });

        $.ajax({
            url: '/JQuery/InsertUpdateFaculty',
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
                        window.location.href = '/Masters/FacultyList';
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

    if ($('#FP_Name').val() == "") {
        WarningToast('Please provide a Name.');
        return false;
    }
    if ($('#FP_ShortName').val() == "") {
        WarningToast('Please provide a Short Name.');
        return false;
    }
    if ($('#FP_DesignationId').val() == 0) {
        WarningToast('Please provide a Designation.');
        return false;
    }
    if ($('#FP_DateOfJoining').val() == '') {
        WarningToast('Please provide a date of joining.');
        return false;
    }
    if ($('#FP_DateOfBirth').val() == '') {
        WarningToast('Please provide a date of birth.');
        return false;
    }
    if ($('#FP_Address').val() == '') {
        WarningToast('Please provide a Address.');
        return false;
    }
    if ($('#FP_Max_DEGM_Id').val() == '') {
        WarningToast('Please provide a Highest Qualification.');
        return false;
    }
    if ($('#FP_Degree').val() == '') {
        WarningToast('Please provide a Degree.');
        return false;
    }
    if ($('#FP_Phone').val() == '') {
        WarningToast('Please provide a Mobile no.');
        return false;
    }
    if ($('#FP_Email').val() == '') {
        WarningToast('Please provide a Email.');
        return false;
    }
    if ($('#FP_Email').val() != '') {
        if (validateEmail($('#FP_Email').val())) {

        }
        else {
            WarningToast('You Email Address Invalid ....')
            return false;
        }
    }
    if ($('#FP_Pin').val() == '') {
        WarningToast('Please provide a Pin code.');
        return false;
    }
    if ($('#FP_FacultyCode').val() == '') {
        WarningToast('Please provide a Faculty code.');
        return false;
    }
    return true;
}
function BindList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        $.ajax({
            url: '/JQuery/FacultyList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>Faculty Name</th><th>Designation</th><th>DOJ</th><th>Phone</th><th>Email</th><th>Degree</th><th>Address</th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td>').text(i + 1));
                        $row.append($('<td style=display:none>').append(row.FP_Id));
                        $row.append($('<td>').append(row.FP_Name));
                        $row.append($('<td>').append(row.DM_Name));
                        $row.append($('<td>').text(formatJsonDate(row.FP_DateOfJoining)));
                        $row.append($('<td>').append(row.FP_Phone));
                        $row.append($('<td>').append(row.FP_Email));
                        $row.append($('<td>').append(row.FP_Degree));
                        $row.append($('<td>').append(row.FP_Address));

                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/Faculty/" + row.FP_Id + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.FP_Id + ");'" + row.FP_Id + " class='btn btn-danger'>Delete</a>"));
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
        MainTableName: 'FacluttyProfileMasters_FPM',
        MainFieldName: 'FP_Id',
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
                    window.location.href = '/Masters/FacultyList';
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
function validateEmail(email) {
    var emailReg = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
    var valid = emailReg.test(email);

    if (!valid) {
        return false;
    } else {
        return true;
    }
}


function formatJsonDate(jsonDate) {
    if (!jsonDate) return '';
    var timestamp = parseInt(jsonDate.replace(/\/Date\((\d+)\)\//, '$1'));
    var date = new Date(timestamp);
    var day = ('0' + date.getDate()).slice(-2);
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var year = date.getFullYear();
    return day + '/' + month + '/' + year; // dd/MM/yyyy
}


function convertToSqlDate(dateStr) {
    if (!dateStr) return null;

    var parts = dateStr.split('/'); // dd-MM-yyyy
    return parts[2] + '-' + parts[1] + '-' + parts[0]; // yyyy-MM-dd
}