
function convertToSqlDate(dateStr) {
    if (!dateStr) return null;

    var parts = dateStr.split('/'); // dd-MM-yyyy
    return parts[2] + '-' + parts[1] + '-' + parts[0]; // yyyy-MM-dd
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
function InsertUpdateAssignment() {
    //Add By Uttaran
    var fileUpload = $("#ASM_UploadDoc").get(0);
    getFiles(fileUpload);
    Documentfile = $('#ASM_UploadDoc').val().substring(12);
    if ($('#ASM_UploadDoc').val() != '' && $('#ASM_UploadDoc').val() != null) {
        Documentfile = $('#ASM_UploadDoc').val().substring(12);
        Documentpath2 = '~/UploadAssignment/' + Documentfile;
    }
    else {
        Documentpath2 = $('#hdnImage').val();
    }

    if (ValidateOperation() == true) {
        var facultyId = getEffectiveFacultyId();

        if (facultyId <= 0) {
            WarningToast("Faculty not selected.");
            return false;
        }
        var _data = JSON.stringify({
            ASM_ID: $('#ASM_ID').val(),
            ASM_FP_Id: facultyId,
            ASM_SubGr_ID: $('#ASM_SubGr_ID').val(),
            ASM_Sub_ID: $('#ASM_Sub_ID').val(),
            ASM_School_ID: $('#ASM_School_ID').val(),
            ASM_Class_ID: $('#ASM_Class_ID').val(),
            ASM_Section_ID: $('#ddlASM_Section_ID').val(),
            ASM_Session_ID: $('#ASM_Session_ID').val(),
            ASM_Title: $('#ASM_Title').val(),
            ASM_Desc: $('#ASM_Desc').val(),
            CM_ID: $('#CM_ID').val(),
            ASM_StartDate: convertToSqlDate($('#ASM_StartDate').val()),
            ASM_ExpDate:convertToSqlDate($('#ASM_ExpDate').val()),
            ASM_UploadDoc: Documentpath2,
            Userid: $('#hdnUserid').val()

        });

        $.ajax({
            url: '/JQuery/InsertUpdateAssignmentMaster',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    $('#btnSave').attr('disabled', 'disabled');
                    setTimeout(function () {
                        window.location.href = '/Masters/ClassWiseAssignmentList';
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

    if ($('#ASM_Class_ID').val() == "") {
        WarningToast('Select any class');
        return false;
    }
    if ($('#ddlASM_Section_ID').val() == "0") {
        WarningToast('Select Section');
        return false;
    }
    if ($('#ASM_Title').val() == "") {
        WarningToast('Enter title of the Assignment.');
        return false;
    }

    return true;
}
function ClassWiseAssignmentList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        $.ajax({
            url: '/JQuery/ClassWiseAssignmentList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>Class</th><th>Section</th><th>Subject</th><th>Start Date</th><th>Exp Date</th><th>Title</th><th>Description</th><th></th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td>').text(i + 1));
                        $row.append($('<td style=display:none>').append(row.ASM_ID));
                        $row.append($('<td>').append(row.ASM_CM_CLASSNAME));
                        $row.append($('<td>').append(row.ASM_SECM_SECTIONNAME));
                        $row.append($('<td>').append(row.SBM_SubjectName));
                        if (row.ASM_StartDate == null) {
                            $row.append($('<td>').append(row.ASM_StartDate));
                        }
                        else {
                            var d = new Date(parseInt(row.ASM_StartDate.slice(6, -2)));
                            $row.append($('<td>').append(formatJsonDate(row.ASM_StartDate)));
                        }
                        if (row.ASM_ExpDate == null) {
                            $row.append($('<td>').append(row.ASM_ExpDate));
                        }
                        else {
                            var d = new Date(parseInt(row.ASM_ExpDate.slice(6, -2)));
                            $row.append($('<td>').append(formatJsonDate(row.ASM_ExpDate)));
                        }

                        $row.append($('<td>').append(row.ASM_Title));
                        $row.append($('<td>').append(row.ASM_Desc));

                        if (res.CanView == true) {
                            $row.append($('<td>').append("<a href=/Masters/ClassWiseAssignment/" + row.ASM_ID + " class='btn btn-warning'>View</a>"));
                        } else {
                            if (row.ASM_UploadDoc) { // Check if NM_UploadFile is not null or undefined
                                $row.append($('<td>').append('<a href=' + row.ASM_UploadDoc.replace("~", location.origin) + ' class="btn btn-info" target="_blank">View</a>'));
                            } else {
                                $row.append($('<td>').text('No File Available')); // Handle the case where NM_UploadFile is null or undefined
                            }
                        }

                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/ClassWiseAssignment/" + row.ASM_ID + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.ASM_ID + ");'" + row.ASM_ID + " class='btn btn-danger'>Delete</a>"));
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
    ClassWiseAssignmentList();

    var facultyId = getEffectiveFacultyId();
    if (facultyId > 0) {
        loadClassesForFaculty(facultyId);
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
        MainTableName: 'AssignmentMaster_ASM',
        MainFieldName: 'ASM_ID',
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
                    window.location.href = '/Masters/ClassWiseAssignmentList';
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
        Id: parseInt($("#ASM_Class_ID").val()),
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
                BindDropDownListForSection($('#ddlASM_Section_ID'), data);
                $("#ddlASM_Section_ID").selectpicker('refresh');

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


function getEffectiveFacultyId() {
    var $select = $('#ASM_FP_Id');

    if ($select.length && $select.prop('tagName') === 'SELECT') {
        var v = $select.val();
        if (v && v !== "0") {
            return parseInt(v, 10) || 0;
        }
    }

    var hiddenVal =
        $('input[name="ASM_FP_Id"]').val() ||
        $('#ASM_FP_Id').val();

    return parseInt(hiddenVal, 10) || 0;
}

function loadClassesForFaculty(facultyId) {
    facultyId = parseInt(facultyId, 10);

    var $classSelect = $('#ASM_Class_ID');

    if (isNaN(facultyId) || facultyId <= 0) {
        $classSelect.empty()
            .append('<option value="">No classes found</option>')
            .selectpicker('refresh');
        return;
    }

    $.ajax({
        url: '/JQuery/GetClassesByFaculty',
        type: 'GET',
        data: { facultyId: facultyId },
        success: function (resp) {

            $classSelect.empty();

            if (resp && resp.success && resp.data.length > 0) {
                $.each(resp.data, function (i, item) {
                    $classSelect.append(
                        $('<option>', {
                            value: item.Id,
                            text: item.Name
                        })
                    );
                });

                // ✅ Select first class automatically
                $classSelect.val(resp.data[0].Id);
            } else {
                $classSelect.append('<option value="">No classes found</option>');
            }

            $classSelect.selectpicker('refresh');

            // ✅ IMPORTANT: Load section for default selected class
            AjaxPostForDropDownSection();
        },
        error: function () {
            console.error('GetClassesByFaculty ERROR');
        }
    });
}
$(document).on('change', '#ASM_FP_Id', function () {
    var facultyId = getEffectiveFacultyId();
    loadClassesForFaculty(facultyId);
});