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

function InsertUpdateClasswiseLiveclass() {
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
            CWLS_ID: $('#CWLS_ID').val(),
            CWLS_SubGr_ID: $('#CWLS_SubGr_ID').val(),
            CWLS_Sub_ID: $('#CWLS_Sub_ID').val(),
            CWLS_School_ID: $('#CWLS_School_ID').val(),
            CWLS_Class_ID: $('#CWLS_Class_ID').val(),
            CWLS_Section_ID: $('#ddlCWLS_Section_ID').val(),
            CWLS_Session_ID: $('#CWLS_Session_ID').val(),
            CWLS_Title: $('#CWLS_Title').val(),
            CWLS_Link: $('#CWLS_Link').val(),
            CM_ID: $('#CM_ID').val(),
            CWLS_ClassDate: convertToSqlDate($('#CWLS_ClassDate').val()),
            CWLS_ClassTime: $('#CWLS_ClassTime').val(),
            Userid: $('#hdnUserid').val()
        });

        $.ajax({
            url: '/JQuery/InsertUpdateClasswiseLiveclass',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data != null && data.IsSuccess) {
                    SuccessToast(data.Message);
                    $('#btnSave').attr('disabled', 'disabled');
                    setTimeout(function () {
                        window.location.href = '/Masters/ClassWiseLiveclassList';
                    }, 2000);
                } else {
                    ErrorToast(data.Message);
                }
            },
            error: function () {
                ErrorToast('Something went wrong.');
            }
        });
    }
}

function ValidateOperation() {

    if ($('#CWLS_Class_ID').val() == "") {
        WarningToast('Select any class');
        return false;
    }
    if ($('#ddlCWLS_Section_ID').val() == "") {
        WarningToast('Select Section');
        return false;
    }
    if ($('#CWLS_Title').val() == "") {
        WarningToast('Enter title of the Class.');
        return false;
    }

    return true;
}

function ClassWiseLiveclassList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        $.ajax({
            url: '/JQuery/ClassWiseLiveclassList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>Class</th><th>Section</th><th>Subject</th><th>Start Date</th><th>Time</th><th>Title</th><th>Description</th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td>').text(i + 1));
                        $row.append($('<td style=display:none>').append(row.CWLS_ID));
                        $row.append($('<td>').append(row.CWLS_CM_CLASSNAME));
                        $row.append($('<td>').append(row.CWLS_SECM_SECTIONNAME));
                        $row.append($('<td>').append(row.SBM_SubjectName));
                        if (row.CWLS_ClassDate == null) {
                            $row.append($('<td>').append(row.CWLS_ClassDate));
                        }
                        else {
                            var d = new Date(parseInt(row.CWLS_ClassDate.slice(6, -2)));
                            $row.append($('<td>').append(formatJsonDate(row.CWLS_ClassDate)));
                        }
                        let timeText = 'Invalid';
                        if (row.CWLS_ClassTime && typeof row.CWLS_ClassTime === 'string') {
                            let timeParts = row.CWLS_ClassTime.split(':');
                            if (timeParts.length >= 2) {
                                let hours = parseInt(timeParts[0]);
                                let minutes = timeParts[1];
                                let ampm = hours >= 12 ? 'PM' : 'AM';
                                hours = hours % 12 || 12;
                                timeText = hours.toString().padStart(2, '0') + ':' + minutes + ' ' + ampm;
                            }
                        }
                        $row.append($('<td>').text(timeText));


                        $row.append($('<td>').append(row.CWLS_Title));
                        $row.append($('<td>').append(row.CWLS_Link));



                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/ClassWiseLiveclass/" + row.CWLS_ID + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.CWLS_ID + ");'" + row.CWLS_ID + " class='btn btn-danger'>Delete</a>"));
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
    ClassWiseLiveclassList();

});

function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}

function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'ClassWiseLiveclass_CWLS',
        MainFieldName: 'CWLS_ID',
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

                    window.location.href = '/Masters/ClassWiseLiveclassList';
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
        Id: parseInt($("#CWLS_Class_ID").val()),
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
                BindDropDownListForSection($('#ddlCWLS_Section_ID'), data);
                $("#ddlCWLS_Section_ID").selectpicker('refresh');

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
