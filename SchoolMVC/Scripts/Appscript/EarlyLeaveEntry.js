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

function InsertUpdateStudentEarlyLeave() {
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
            ERL_ID: $('#ERL_ID').val(),
            ERL_ClassId: $('#ERL_ClassId').val(),
            ERL_SectionId: $('#ddlERL_SectionId').val(),

            ERL_FacultyId: $('#ERL_FacultyId').val(),
            ERL_StudentId: $('#ERL_StudentId').val(),
            ERL_Reason: $('#ERL_Reason').val(),
            ERL_Note: $('#ERL_Note').val(),
            ERL_PickupDetails: $('#ERL_PickupDetails').val(),


            ERL_Date: convertToSqlDate($('#ERL_Date').val()),
            ERL_SesssioId: $('#ERL_SesssioId').val(),
            CM_ID: $('#CM_ID').val(),
            ERL_SchoolId: $('#ERL_SchoolId').val(),
            Userid: $('#hdnUserid').val()

        });

        $.ajax({
            url: '/JQuery/InsertUpdateStudentEarlyLeave',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    $('#btnSave').attr('disabled', 'disabled');
                    setTimeout(function () {
                        window.location.href = '/Masters/EarlyLeaveEntryList';
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

function EarlyLeaveEntryList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        $.ajax({
            url: '/JQuery/EarlyLeaveEntryList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>Class</th><th>Section</th><th>StudentId</th><th>Date</th><th>Reason</th><th>Note</th><th>Approved By</th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td>').text(i + 1));
                        $row.append($('<td style=display:none>').append(row.ERL_ID));
                        $row.append($('<td>').append(row.CLASSNAME));
                        $row.append($('<td>').append(row.SECTIONNAME));
                        $row.append($('<td>').append(row.ERL_StudentId));
                        if (row.ERL_Date == null) {
                            $row.append($('<td>').append(row.ERL_Date));
                        }
                        else {
                            var d = new Date(parseInt(row.ERL_Date.slice(6, -2)));
                            $row.append($('<td>').append(formatJsonDate(row.ERL_Date)));
                        }
                        $row.append($('<td>').append(row.ERL_Reason));
                        $row.append($('<td>').append(row.ERL_Note));
                        $row.append($('<td>').append(row.FACULTYNAME));
                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/EarlyLeaveEntry/" + row.ERL_ID + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.ERL_ID + ");'" + row.ERL_ID + " class='btn btn-danger'>Delete</a>"));
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
$(document).ready(function () {
    EarlyLeaveEntryList();

    if ($('#ERL_ID').val() && $('#ERL_ID').val() > 0) {
        $('#ERL_ClassId').val($('#hdnERL_ClassId').val());
        AjaxPostForDropDownSection();
        $('#ddlERL_SectionId').val($('#hdnERL_SectionId').val());
        $('#ddlERL_SectionId').selectpicker('refresh');
    }
});

function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'StudentEarlyLeave_ERL',
        MainFieldName: 'ERL_ID',
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
                    window.location.href = '/Masters/EarlyLeaveEntryList';
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
function AjaxPostForDropDownSection() {
    var _data = JSON.stringify({
        Id: parseInt($("#ERL_ClassId").val()),
    });
    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/GetSectionByClass",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data, status) {
            if (status == "success") {
                BindDropDownListForSection($('#ddlERL_SectionId'), data);
                $("#ddlERL_SectionId").selectpicker('refresh');

            } else {
                showMessage("Unable to process the request...", 0);
            }
        }
    });
}
function BindDropDownListForSection(ddl, dataCollection) {
    $(ddl).get(0).length = 0;
    $(ddl).get(0).options[0] = new Option("Select", "0", "Selected");
    for (var i = 0; i < dataCollection.length; i++) {
        $(ddl).get(0).options[i + 1] = new Option(dataCollection[i].SECM_SECTIONNAME, dataCollection[i].SECM_SECTIONID);
    }
}




function ValidateOperation() {

    if ($('#ERL_ClassId').val() == "") {
        WarningToast('Select any class');
        return false;
    }
    if ($('#ERL_SectionId').val() == "") {
        WarningToast('Select Section');
        return false;
    }
    if ($('#ERL_Reason').val() == "") {
        WarningToast('Enter Reason.');
        return false;
    }

    return true;
}

