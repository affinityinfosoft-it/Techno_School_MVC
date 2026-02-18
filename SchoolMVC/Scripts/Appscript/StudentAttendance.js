$(document).ready(function () {
    var ClassId = isNaN(parseInt(getParameterByName('ClassId'))) ? 0 : parseInt(getParameterByName('ClassId'));

    loadClasses();
    bindAttendanceList();

    // Corrected Search Click
    $("#btnSearch").click(function () {
        if (ValidateOperation()) {
            bindGrid(null, null, null);
        }
    });

    // Class change event
    $("#ClassId").change(function () {
        $.each(classes, function (index, element) {
            if (element.CM_CLASSID == parseInt($("#ClassId").val())) {
                isHigherSecondary = element.IsHigherSecondary;
                if (isHigherSecondary === true) {
                    $('#ddlSectionId').attr("disabled", "disabled");
                } else {
                    $('#ddlSectionId').removeAttr("disabled");
                }
            }
        });

        AjaxPostForDropDownSection($('#ClassId option:selected').val());
    });

    $("#btnSave").click(function () {
        SaveAttendance();
    });

    //// Weekend check
    $("#SAM_Date").change(function () {
        var dateVal = $(this).val();  // dd-mm-yyyy
        if (!dateVal) return;

        console.log("Picked:", dateVal);

        // Split dd-mm-yyyy
        var parts = dateVal.split("-");
        var day = parseInt(parts[0], 10);
        var month = parseInt(parts[1], 10) - 1;
        var year = parseInt(parts[2], 10);

        var d = new Date(year, month, day);
        if (isNaN(d)) {
            alert("Invalid Date");
            return;
        }

        var dayOfWeek = d.getDay(); // 0 = Sunday

        console.log("DayOfWeek:", dayOfWeek);

        if (dayOfWeek === 0) {
            alert("It's weekend!!");
            $("#SAM_Date").val('');
            return;
        }

        CheckHoliday(dateVal);
    });


    $("#btnSearchList").click(function () {
        bindAttendanceList();
    });

    if (ClassId > 0) {
        Edit();
    }
});
function loadClasses() {
    $.get({
        type: "GET",
        url: rootDir + "JQuery/getClasses",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data, status) {
            if (status == "success") {
                BindClassDropDownList($('#ClassId'), data);
                $("#ClassId").selectpicker('refresh');
                classes = data;

            } else {
                showMessage("Unable to process the request...", 0);
            }
        }
    });
}
function BindClassDropDownList(ddl, dataCollection) {
    $(ddl).get(0).length = 0;
    $(ddl).get(0).options[0] = new Option("Select", "0", "Selected");
    if (dataCollection.length != 0) {
        for (var i = 0; i < dataCollection.length; i++) {
            $(ddl).get(0).options[i + 1] = new Option(dataCollection[i].CM_CLASSNAME, dataCollection[i].CM_CLASSID);
        }
    }
}
function AjaxPostForDropDownSection(Id) {
    var _data = JSON.stringify({
        Id: parseInt(Id),
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

function bindGridWithclass() {
    $('#update-panel').html('loading data.....');
    $("#tblStudent").show();
    var _data = JSON.stringify({
        obj: {
            SEA_ClassId: $('#ClassId').val(),

        }
    });
    $.ajax({
        url: '/JQuery/GetStudentListForExamAttendance',
        dataType: 'json',
        contentType: 'application/json',
        data: _data,
        type: 'POST',
        async: false,
        success: function (data) {
            DeleteRows();
            if (data.length > 0) {

                var tr;

                for (var i = 0; i < data.length; i++) {
                    tr = $('<tr/>');
                    tr.append("<td style='display:none;'>" + data[i].SEA_StudentId + "</td>");
                    tr.append("<td>" + data[i].SEA_StudentName + "</td>");
                    tr.append("<td>" + data[i].SEA_ClassName + "</td>");
                    tr.append("<td>" + data[i].SEA_SectionName + "</td>");
                    tr.append("<td>" + data[i].SEA_RollNo + "</td>");
                    tr.append("<td><input type='checkbox' id='chkStatus" + i + "' class='chkStatus' name='chkStatus" + i + "' ></td>");
                    tr.append("<td></td>");
                    $('#tblStudent').append(tr);
                    var chkStatus = data[i].SEA_isAbsent;
                    $('#chkStatus' + i + '').attr('checked', chkStatus);
                }

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
    //}
}
function ValidateOperation() {
    if ($("#ClassId").val() === '' || $("#ClassId").val() === '0') {
        alert('Please select Class.');
        $("#ClassId").focus();
        return false;
    }

    if ($("#ddlSectionId").val() === '' || $("#ddlSectionId").val() === '0') {
        alert('Please select Section.');
        $("#ddlSectionId").focus();
        return false;
    }

    if ($("#SAM_Date").val() === '') {
        alert('Please provide Date.');
        $("#SAM_Date").focus();
        return false;
    }

    return true;
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

function bindAttendanceList() {
    $('#update-panel').html('loading data.....');
    var _data = JSON.stringify({

    });
    $.ajax({
        url: '/JQuery/AttendanceList',
        dataType: 'json',
        type: 'GET',
        data: {
            SAM_ClassId: $("#ClassId").val(),
            //SAM_Date_S: $("#SAM_Date").val(),
            SAM_SectionId: $("#ddlSectionId").val(),
            FromDateS: convertToSqlDate( $('#FromDateS').val()) || null,
            ToDateS: convertToSqlDate ($('#ToDateS').val()) || null
        },
        success: function (res) {
            if (res.length > 0) {
                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>Class</th><th>Section</th><th>Date of Attendance</th><th>TotalStudent</th><th>PresentStudent</th><th>AbsentStudent</th><th>HalfDay</th><th>LateComming</th><th></th><th></th></tr></thead>";
                $data.append($header);

                $.each(res, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td>').text(i + 1));
                    $row.append($('<td style=display:none>').append(row.SAM_Id));
                    $row.append($('<td>').append(row.ClassName));
                    $row.append($('<td>').append(row.SectionName));

                    if (row.SAM_Date) {
                        var d = new Date(parseInt(row.SAM_Date.slice(6, -2)));
                        var dateStr = d.getDate() + '/' + (d.getMonth() + 1) + '/' + d.getFullYear();
                        $row.append($('<td>').append(formatJsonDate(row.SAM_Date)));
                    } else {
                        $row.append($('<td>').append(''));
                    }

                    $row.append($('<td>').append(row.TotalStudent));
                    $row.append($('<td>').append(row.PresentStudent));
                    $row.append($('<td>').append(row.AbsentStudent));
                    $row.append($('<td>').append(row.HalfDayStudent));
                    $row.append($('<td>').append(row.LateComingStudent));
                    $row.append($('<td>').append("<a href=/StudentManagement/StudentAttendance?ClassId=" + row.SAM_ClassId + "&SectionId=" + row.SAM_SectionId + "&Date=" + row.SAM_Date_S + "&TotalStudent=" + row.TotalStudent + "&PresentStudent=" + row.PresentStudent + "&AbsentStudent=" + row.AbsentStudent + " class='btn btn-warning'>Edit</a>"));
                    $row.append($('<td>').append("<a href='javascript:void(0);' onclick='ConfirmDelete(" + row.SAM_Id + ");' class='btn btn-danger'>Delete</a>"));

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
}
function CheckHoliday(fieldId) {
    var _data = JSON.stringify({
        date: fieldId
    });
    $.ajax({
        url: '/JQuery/CheckHoliday',
        type: 'POST',
        data: _data,
        contentType: 'application/json ; utf-8',
        dataType: 'json',
        success: function (data) {
            if (data != null && data != undefined) {
                if (data == 1) {
                    alert('This date is invalid for this Session');
                    $("#SAM_Date").val('');
                }
                if (data != 0 && data != 1) {
                    alert('Sorry! We can not take attendance during ' + data + ' vacation.');
                    $("#SAM_Date").val('');
                }


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
function getParameterByName(name) {
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
           results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}
function Edit() {
    var ClassIds = isNaN(parseInt(getParameterByName('ClassId'))) ? 0 : parseInt(getParameterByName('ClassId'));
    var SectionId = isNaN(parseInt(getParameterByName('SectionId'))) ? 0 : parseInt(getParameterByName('SectionId'));
    var Date = getParameterByName('Date');
    $("#SAM_ClassId").val(ClassIds);
    $("#SAM_SectionId").val(SectionId);
    $("#SAM_Date").val(Date);
    $("#ClassId").val(parseInt($('#SAM_ClassId').val()));
    $("#ClassId").selectpicker('refresh');
    AjaxPostForDropDownSection(ClassIds);
    $("#ddlSectionId").val(parseInt($('#SAM_SectionId').val()));
    $("#ddlSectionId").selectpicker('refresh');
    bindGrid(Date, ClassIds, SectionId);
}
function DeleteRecord(Id) {
    var _data = JSON.stringify({
        id: parseInt(Id),
    });
    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/DeleteAttendance",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data, status) {
            if (status == "success") {
                alert(data.Message);
                window.location.href = '/StudentManagement/AttendanceList';
            }
            else {
                alert(data.Message);
            }
        }
    });
}
function DeleteRows() {
    var tblStudent = document.getElementById('tblStudent')
    var rowCount = tblStudent.rows.length;
    for (var i = rowCount - 1; i > 0; i--) {
        tblStudent.deleteRow(i);
    }
}

function SaveAttendance() {

    if (!ValidateOperation()) return;

    var arrStudent = [];
    let i = 0;

    $("#tblStudent tbody tr").each(function () {

        arrStudent.push({
            StudentId: $(this).children("td").first().html(),
            IsAbsent: $("#chkAbsent" + i).is(":checked"),
            IsHalfDay: $("#chkHalfDay" + i).is(":checked"),
            IsLateComing: $("#chkLate" + i).is(":checked")
        });

        i++;
    });

    var _data = JSON.stringify({
        attendnceList: arrStudent,
        SAM_Date: convertToSqlDate($("#SAM_Date").val()),
        SAM_ClassId: $("#ClassId").val(),
        SAM_SectionId: $("#ddlSectionId").val(),
        Userid: $('#hdnUserid').val()
    });

    $.ajax({
        url: '/JQuery/InsertUpdateAttendance',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        data: _data,
        success: function (data) {
            if (data.IsSuccess) {
                alert(data.Message);
                window.location.href = '/StudentManagement/AttendanceList';
            } else {
                ErrorToast(data.Message);
            }
        }
    });
}
function bindGrid(Date, ClassId, SectionId) {

    $("#tblStudent").show();

    var _data = JSON.stringify({
        obj: {
            SAM_Date: Date == null ?  convertToSqlDate($("#SAM_Date").val()) : Date,
            SAM_ClassId: ClassId == null ? $("#ClassId").val() : ClassId,
            SAM_SectionId: SectionId == null ? $("#ddlSectionId").val() : SectionId
        }
    });

    $.ajax({
        url: '/JQuery/GetStudentListForAttendance',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: _data,
        async: false,
        success: function (data) {

            DeleteRows();
            data.sort(function (a, b) {
                return parseInt(a.Roll) - parseInt(b.Roll);
            });
            for (var i = 0; i < data.length; i++) {

                var tr = $('<tr/>');
                
                tr.append("<td style='display:none;'>" + data[i].StudentId + "</td>");
                tr.append("<td>" + (i + 1) + "</td>");
                tr.append("<td>" + data[i].StudentName + "</td>");
                tr.append("<td>" + data[i].ClassName + "</td>");
                tr.append("<td>" + data[i].SectionName + "</td>");
                tr.append("<td>" + data[i].Roll + "</td>");

                tr.append("<td><input type='checkbox' id='chkAbsent" + i + "'></td>");
                tr.append("<td><input type='checkbox' id='chkHalfDay" + i + "'></td>");
                tr.append("<td><input type='checkbox' id='chkLate" + i + "'></td>");

                $('#tblStudent').append(tr);

                $('#chkAbsent' + i).prop('checked', data[i].IsAbsent);
                $('#chkHalfDay' + i).prop('checked', data[i].IsHalfDay);
                $('#chkLate' + i).prop('checked', data[i].IsLateComing);

                /* Mutual Exclusion */
                $('#chkAbsent' + i).change(function () {
                    if ($(this).is(":checked")) {
                        $(this).closest('tr').find("input[id^='chkHalfDay'],input[id^='chkLate']").prop("checked", false);
                    }
                });

                $('#chkHalfDay' + i + ',#chkLate' + i).change(function () {
                    if ($(this).is(":checked")) {
                        $(this).closest('tr').find("input[id^='chkAbsent']").prop("checked", false);
                    }
                });
            }
        }
    });
}

function ConfirmDelete(id) {
    if (confirm("Are you sure you want to delete this attendance record?")) {
        DeleteRecord(id);
    }
}

