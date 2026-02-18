
function getEffectiveFacultyId() {
    try {
        var $select = $('#NM_FacultyId');

        // If the faculty field is a SELECT
        if ($select.length && $select.prop('tagName') === 'SELECT') {
            var selVal = $select.val();
            if (selVal && selVal !== "" && selVal !== "0") {
                return parseInt(selVal, 10) || 0;
            }
        }

        // If it's hidden (readonly case)
        var hiddenVal = $('input[name="NM_FacultyId"]').val() || $('#NM_FacultyId').val();
        return parseInt(hiddenVal, 10) || 0;
    }
    catch (e) {
        console.error("getEffectiveFacultyId ERROR:", e);
        return 0;
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


function InsertUpdateNotice() {
    var fileUpload = $("#NM_UploadFile").get(0);
    if (fileUpload) getFiles(fileUpload);

    var Documentfile = $('#NM_UploadFile').val();
    var Documentpath2 = '';
    if (Documentfile && Documentfile.length > 0) {
        // if browser returns full path (IE), get filename
        Documentfile = Documentfile.split('\\').pop().split('/').pop();
        Documentpath2 = '~/UploadNotice/' + Documentfile;
    } else {
        Documentpath2 = $('#hdnImage').val();
    }

    var entryDate = convertToSqlDate($('#NM_EntryDate').val());
    if (!entryDate) {
        const today = new Date();
        entryDate = today.toISOString().split('T')[0];
        $('#NM_EntryDate').val(entryDate);
    }

    var expDate = convertToSqlDate($('#NM_ExpDate').val());
    if (!expDate) {
        const today = new Date();
        const sessionEnd = new Date(today.getFullYear() + 1, 2, 31);
        expDate = sessionEnd.toISOString().split('T')[0];
        $('#NM_ExpDate').val(expDate);
    }

    if (ValidateOperation() == true) {
        let classSectionPair = "";

        // Loop through selected classes
        $("#NM_ClassId option:selected").each(function () {
            const classId = $(this).val();

            // Get selected sections for this class
            const selectedSections = $("#ddlNM_SectionId option:selected")
                .filter(function () { return $(this).data("class") == classId; });

            if (selectedSections.length > 0) {
                selectedSections.each(function () {
                    const sectionId = $(this).val();
                    if (sectionId && sectionId != "0") {
                        classSectionPair += classId + "|" + sectionId + ",";
                    }
                });
            } else {
                classSectionPair += classId + "|0,";
            }
        });

        classSectionPair = classSectionPair.replace(/,$/, "");

        var facultyIdValue = getEffectiveFacultyId(); // <- robust getter

        var _data = JSON.stringify({
            NM_Id: $('#NM_Id').val(),
            NM_EntryDate: entryDate,
            NM_ExpDate: expDate,

            NM_StudentId: $('#NM_StudentId').val(),
            NM_FacultyId: getEffectiveFacultyId(),
            NM_NtId: $('#NM_NtId').val(),

            NM_Title: $('#NM_Title').val(),
            NM_Notice: noticeEditor ? noticeEditor.getData() : $('#NM_Notice').val(),
            NM_ClassSectionPair: classSectionPair,
            NM_IsPublish: $('#NM_IsPublish').val(),
            NM_UploadFile: Documentpath2,
            Userid: $('#hdnUserid').val(),
            NM_Link: $('#NM_Link').val()
        });

        $.ajax({
            url: '/JQuery/InsertUpdateNotice',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data && data.IsSuccess) {
                    SuccessToast(data.Message);
                    $('#btnSave').attr('disabled', 'disabled');
                    setTimeout(function () {
                        window.location.href = '/Masters/NoticeList';
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

    if ($('#NM_NtId').val() == "") {
        WarningToast('Enter any Notice Type.');
        return false;
    }
    if ($('#NM_Title').val() == "") {
        WarningToast('Enter title of the notice.');
        return false;
    }

    return true;
}
function NoticeList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        //papai
        $.ajax({
            url: '/JQuery/NoticeList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>Class</th><th>Section</th><th>From date</th><th>To date</th><th>Title</th><th>Notice</th><th></th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td>').text(i + 1));
                        $row.append($('<td style=display:none>').append(row.NM_Id));
                        $row.append($('<td>').append(row.NM_CM_CLASSNAME));
                        $row.append($('<td>').append(row.NM_SECM_SECTIONNAME));
                        if (row.NM_EntryDate == null) {
                            $row.append($('<td>').append(row.NM_EntryDate));
                        }
                        else {
                            var d = new Date(parseInt(row.NM_EntryDate.slice(6, -2)));
                            $row.append($('<td>').append(formatJsonDate(row.NM_EntryDate)));
                        }

                        if (row.NM_ExpDate == null) {
                            $row.append($('<td>').append(row.NM_ExpDate));
                        }
                        else {
                            var d = new Date(parseInt(row.NM_ExpDate.slice(6, -2)));
                            $row.append($('<td>').append(formatJsonDate(row.NM_ExpDate)));
                        }

                        $row.append($('<td>').append(row.NM_Title));
                        //$row.append($('<td>').append(row.NM_Notice));

                        //$row.append($('<td>').append(
                        //    '<button type="button" class="btn btn-info btn-sm viewNoticeBtn" data-notice="' +
                        //    encodeURIComponent(row.NM_Notice || '') + '"><i class="fa fa-eye"></i> View</button>'
                        //));
                        $row.append(
                $('<td>').append(
                    $('<button>')
                        .addClass('btn btn-info btn-sm viewNoticeBtn')
                        .attr('type', 'button')
                        .attr('title', 'Preview Notice')
                        .attr('data-notice', encodeURIComponent(row.NM_Notice || ''))
                        .html('<i class="fa fa-eye"></i>') // or 'fas fa-eye' for FA5+
                )
            );

                        if (res.CanView == true) {
                            $row.append($('<td>').append("<a href=/Masters/Notice/" + row.NM_Id + " class='btn btn-warning'>View</a>"));
                        } else {
                            if (row.NM_UploadFile) { // Check if NM_UploadFile is not null or undefined
                                $row.append($('<td>').append('<a href=' + row.NM_UploadFile.replace("~", location.origin) + ' class="btn btn-info" target="_blank">Upload View</a>'));
                            } else {
                                $row.append($('<td>').text('No File Available')); // Handle the case where NM_UploadFile is null or undefined
                            }
                        }

                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/Notice/" + row.NM_Id + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.NM_Id + ");'" + row.NM_Id + " class='btn btn-danger'>Delete</a>"));
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
$(document).on('click', '.viewNoticeBtn', function () {
    
    try {
        const encoded = $(this).data('notice');
        const htmlContent = decodeURIComponent(encoded || '');
        $('#noticePreviewContent').html(htmlContent || "<i>No notice available</i>");
        $('#noticePreviewModal').modal('show');
    } catch (e) {
        console.error("Preview error:", e);
    }
});
$(document).ready(function () {
    NoticeList();

    if ($('#NM_Id').val() && $('#NM_Id').val() > 0) {
        // Convert CSV to array
        var selectedClasses = $('#hdnNM_ClassId').val().split(',');
        $('#NM_ClassId').val(selectedClasses);
        $('#NM_ClassId').selectpicker('refresh'); // ✅ refresh class multi-select

        // Now load sections
        AjaxPostForDropDownSection(function () {
            var selectedSections = $('#hdnNM_SectionId').val().split(',');
            $('#ddlNM_SectionId').val(selectedSections);
            $('#ddlNM_SectionId').selectpicker('refresh');
        });
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
        MainTableName: 'NoticeMasters_NM',
        MainFieldName: 'NM_Id',
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
                    window.location.href = '/Masters/NoticeList';
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
function BindDropDownListForSection(ddl, dataCollection) {
    $(ddl).empty();
    $(ddl).append(new Option("Select", "0", true, true));
    for (var i = 0; i < dataCollection.length; i++) {
        var opt = new Option(dataCollection[i].SECM_SECTIONNAME, dataCollection[i].SECM_SECTIONID);
        $(opt).attr("data-class", dataCollection[i].SECM_CM_CLASSID); // Make sure this is set
        $(ddl).append(opt);
    }
}
function AjaxPostForDropDownSection(callback) {
    var selectedClassIds = $('#NM_ClassId').val() || [];

    var _data = JSON.stringify({
        ClassIds: selectedClassIds.map(Number)
    });

    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/GetSectionByClassA",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            BindDropDownListForSection($('#ddlNM_SectionId'), data);
            $("#ddlNM_SectionId").selectpicker('refresh');
            if (callback) callback();
        }
    });
}
//// new add 31-10-25 dropdown student list

$(document).ready(function () {
    console.log("Autocomplete ready");

    $("#txtStudentSearch").autocomplete({
        appendTo: "body",
        source: function (request, response) {
            $.ajax({
                url: '/JQuery/SearchStudentList',
                dataType: 'json',
                data: {
                    searchTerm: request.term,
                    schoolId: $("#SchoolId").val(),
                    sessionId: $("#SessionId").val()
                },



                success: function (data) {
                    console.log("Data received:", data);
                    response($.map(data, function (item) {
                        return {
                            label: item.SD_StudentName + ' # ' + item.SD_StudentId + ' # ' + item.ClassName + ' # ' + item.SectionName,
                            value: item.SD_StudentName,
                            id: item.SD_StudentId
                        };
                    }));
                },
                error: function (xhr) {
                    console.error("Error:", xhr.responseText);
                }
            });
        },
        minLength: 2,
        delay: 300,
        select: function (event, ui) {
            $("#NM_StudentId").val(ui.item.id);
        }
    });
});


$(document).ready(function () {

    function debugLog(msg, val) {
        if (window.console && console.log) console.log(msg, val);
    }

    function loadClassesForFaculty(facultyId) {
        facultyId = parseInt(facultyId, 10);
        debugLog('calling loadClassesForFaculty with id:', facultyId);
        var $classSelect = $('#NM_ClassId');

        if (isNaN(facultyId) || facultyId <= 0) {
            $classSelect.empty().append('<option value="">No classes found</option>');
            if ($classSelect.selectpicker) $classSelect.selectpicker('refresh');
            return;
        }

        $.ajax({
            url: '/JQuery/GetClassesByFaculty',
            type: 'GET',
            data: { facultyId: facultyId },
            success: function (resp) {
                debugLog('ajax response:', resp);
                $classSelect.empty();

                if (resp && resp.success && resp.data && resp.data.length > 0) {
                    $.each(resp.data, function (i, item) {
                        $classSelect.append($('<option>', { value: item.Id, text: item.Name }));
                    });
                } else {
                    $classSelect.append('<option value="">No classes found</option>');
                }

                if ($classSelect.selectpicker) $classSelect.selectpicker('refresh');
            },
            error: function (xhr, status, err) {
                console.error('GetClassesByFaculty error', status, err);
            }
        });
    }

    // when editable dropdown changes
    $(document).on('change', '#NM_FacultyId', function () {
        var id = $(this).val();
        debugLog('change event fired, val=', id);
        loadClassesForFaculty(id);
    });

    // initial load
    var initialFacultyId = getEffectiveFacultyId();
    debugLog('initialFacultyId computed:', initialFacultyId);
    if (initialFacultyId) loadClassesForFaculty(initialFacultyId);
});