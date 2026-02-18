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

function InsertUpdateStudentDiary() {
    //Add By Uttaran
    var fileUpload = $("#AttachmentPath").get(0);
    getFiles(fileUpload);
    Documentfile = $('#AttachmentPath').val().substring(12);
    if ($('#AttachmentPath').val() != '' && $('#AttachmentPath').val() != null) {
        Documentfile = $('#AttachmentPath').val().substring(12);
        Documentpath2 = '~/UploadAssignment/' + Documentfile;
    }
    else {
        Documentpath2 = $('#hdnImage').val();
    }

    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
            DiaryId: $('#DiaryId').val(),
            ClassId: $('#ClassId').val(),
            SectionId: $('#ddlSectionId').val(),

            StudentId: $('#StudentId').val(),
            ApDate: convertToSqlDate($('#ApDate').val()),
            DiaryType: $('#DiaryType').val(),
            Topic: $('#Topic').val(),

            TeacherId: $('#TeacherId').val(),
            SesssioId: $('#SesssioId').val(),
            CM_ID: $('#CM_ID').val(),
            SchoolId: $('#SchoolId').val(),
            AttachmentPath: Documentpath2,
            Userid: $('#hdnUserid').val()

        });

        $.ajax({
            url: '/JQuery/InsertUpdateStudentDiary',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    $('#btnSave').attr('disabled', 'disabled');
                    setTimeout(function () {
                        window.location.href = '/Masters/StudentDiaryList';
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

function StudentDiaryList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        $.ajax({
            url: '/JQuery/StudentDiaryList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>Class</th><th>Section</th><th>StudentId</th><th>Date</th><th>Topic</th><th>Description</th><th></th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td>').text(i + 1));
                        $row.append($('<td style=display:none>').append(row.DiaryId));
                        $row.append($('<td>').append(row.CLASSNAME));
                        $row.append($('<td>').append(row.SECTIONNAME));
                        $row.append($('<td>').append(row.StudentId));
                        if (row.ApDate == null) {
                            $row.append($('<td>').append(row.ApDate));
                        }
                        else {
                            var d = new Date(parseInt(row.ApDate.slice(6, -2)));
                            $row.append($('<td>').append(formatJsonDate(row.ApDate)));
                        }
                        $row.append($('<td>').append(row.DiaryType));
                        $row.append($('<td>').append(row.Topic));

                        if (res.CanView == true) {
                            $row.append($('<td>').append("<a href=/Masters/StudentDiary/" + row.DiaryId + " class='btn btn-warning'>View</a>"));
                        } else {
                            if (row.AttachmentPath) { // Check if NM_UploadFile is not null or undefined
                                $row.append($('<td>').append('<a href=' + row.AttachmentPath.replace("~", location.origin) + ' class="btn btn-info" target="_blank">View</a>'));
                            } else {
                                $row.append($('<td>').text('No File Available')); // Handle the case where NM_UploadFile is null or undefined
                            }
                        }

                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/StudentDiary/" + row.DiaryId + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.DiaryId + ");'" + row.DiaryId + " class='btn btn-danger'>Delete</a>"));
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
    StudentDiaryList();

    if ($('#DiaryId').val() && $('#DiaryId').val() > 0) {
        $('#ClassId').val($('#hdnClassId').val());
        AjaxPostForDropDownSection();
        $('#ddlSectionId').val($('#hdnSectionId').val());
        $('#ddlSectionId').selectpicker('refresh');
    }
});

function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
function getFiles(uploadFileName) {
    var files = uploadFileName.files;
    var data = new FormData();
    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
        data.append("fileName", files[i].name);
    }

    $.ajax({
        url: "/api/FileUload/UploadFiles",
        type: "POST",
        data: data,
        contentType: false,
        processData: false,
        async: false,
        success: function (result) {
        },
        error: function (err) {
        }
    });
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'StudentDiary_STD',
        MainFieldName: 'DiaryId',
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
                    window.location.href = '/Masters/StudentDiaryList';
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
        Id: parseInt($("#ClassId").val()),
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
                BindDropDownListForSection($('#ddlSectionId'), data);
                $("#ddlSectionId").selectpicker('refresh');

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

    if ($('#ClassId').val() == "") {
        WarningToast('Select any class');
        return false;
    }
    if ($('#SectionId').val() == "") {
        WarningToast('Select Section');
        return false;
    }
    if ($('#Topic').val() == "") {
        WarningToast('Enter topics.');
        return false;
    }

    return true;
}

