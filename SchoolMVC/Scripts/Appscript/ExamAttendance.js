var isHigherSecondary = false;
var classes = [];
$(document).ready(function () {
    var ddlSEA_ClassId = isNaN(parseInt(getParameterByName('ddlSEA_ClassId'))) ? 0 : parseInt(getParameterByName('ddlSEA_ClassId'));
    
   
    bindExamAttendanceList();

    //$("#SEA_Attn_Date").change(function () {
    //    var dateVal = $(this).val();  // dd-mm-yyyy
    //    if (!dateVal) return;

    //    console.log("Picked:", dateVal);

    //    // Split dd-mm-yyyy
    //    var parts = dateVal.split("-");
    //    var day = parseInt(parts[0], 10);
    //    var month = parseInt(parts[1], 10) - 1;
    //    var year = parseInt(parts[2], 10);

    //    var d = new Date(year, month, day);
    //    if (isNaN(d)) {
    //        alert("Invalid Date");
    //        return;
    //    }

    //    var dayOfWeek = d.getDay(); // 0 = Sunday

    //    console.log("DayOfWeek:", dayOfWeek);

    //    if (dayOfWeek === 0) {
    //        alert("It's weekend!!");
    //        $("#SEA_Attn_Date").val('');
    //        return;
    //    }

    //    CheckHoliday(dateVal);
    //});
    $(".Date01").change(function () {
        var dateVal = $(this).val();  // dd/mm/yyyy
        if (!dateVal) return;

        console.log("Picked:", dateVal);

        // Split dd/mm/yyyy
        var parts = dateVal.split("/");
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
            $(this).val('');
            return;
        }

        CheckHoliday(dateVal);
    });



    loadClasses();
    $("#ddlSEA_ClassId").change(function () {
        $.each(classes, function (index, element) {
        
            if (element.CM_CLASSID == parseInt($("#ddlSEA_ClassId").val())) {
                isHigherSecondary = element.IsHigherSecondary;
                if (isHigherSecondary == true) {
                    $('#ddlSEA_SectionId').attr("disabled", "disabled")
                }
            }
        });
        AjaxPostForDropDownSection($('#ddlSEA_ClassId option:selected').val());
        AjaxPostForDropDownSubject($('#ddlSEA_ClassId option:selected').val());
    });
    $("#btnSave").click(function () {
       
    SaveAttendance();
    });
    $("#btnSearch").click(function () {
        if (ValidateOperation() == true) {
            bindGrid(null, null, null, null, null);
        }
    });
    $("#btnSearchList").click(function () {
        bindExamAttendanceList();
    });
    //if ($('#SEA_ClassId').val() !='')
    //{
    //    $("#ddlSEA_ClassId").val(parseInt($('#SEA_ClassId').val()));
    //    $("#ddlSEA_ClassId").selectpicker('refresh');
    //    $("#ddlSEA_SectionId").val(parseInt($('#SEA_SecId').val()));
    //    $("#ddlSEA_SectionId").selectpicker('refresh');
    //    $("#ddlSEA_SubjectId").val(parseInt($('#SEA_SubjectId').val()));
    //    $("#ddlSEA_SubjectId").selectpicker('refresh');
    //    $("#SEA_TermId").val(parseInt($('#hdnSEA_TermId').val()));
    //    $("#SEA_TermId").selectpicker('refresh');
    //}
    if (ddlSEA_ClassId > 0) {
        Edit();
    }
});
function bindGrid(SEA_Attn_Date, SEA_TermId, SEA_SubjectId, SEA_ClassId, SEA_SecId) {
    $('#update-panel').html('loading data.....');
    $("#tblStudent").show();

    var _data = JSON.stringify({
        obj: {
            SEA_Attn_Date: SEA_Attn_Date == null ? convertToSqlDate ($("#SEA_Attn_Date").val()) : SEA_Attn_Date,
            SEA_TermId: SEA_TermId == null ? $("#SEA_TermId").val() : SEA_TermId,
            SEA_SubjectId: SEA_SubjectId == null ? $("#ddlSEA_SubjectId").val() : SEA_SubjectId,
            SEA_ClassId: SEA_ClassId == null ? $("#ddlSEA_ClassId").val() : SEA_ClassId,
            SEA_SecId: SEA_SecId == null ? $("#ddlSEA_SectionId").val() : SEA_SecId
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

            if (data && data.length > 0) {

                //  Roll No DESC sort
                data.sort(function (a, b) {
                    return parseInt(a.SEA_RollNo) - parseInt(b.SEA_RollNo);
                });

                for (var i = 0; i < data.length; i++) {

                    var tr = $('<tr/>');

                  
                    tr.append("<td style='display:none;'>" + data[i].SEA_StudentId + "</td>");
                    tr.append("<td>" + (i + 1) + "</td>"); // SL NO
                    tr.append("<td>" + data[i].SEA_StudentName + "</td>");
                    tr.append("<td>" + data[i].SEA_ClassName + "</td>");
                    tr.append("<td>" + data[i].SEA_SectionName + "</td>");
                    tr.append("<td>" + data[i].SEA_RollNo + "</td>");
                    tr.append("<td><input type='checkbox' id='chkStatus" + i + "' class='chkStatus'></td>");
                    tr.append("<td></td>");

                    $('#tblStudent').append(tr);

                    $('#chkStatus' + i).prop('checked', data[i].SEA_isAbsent);
                }
            }
            else {
                $("#update-panel").html("<div>No data Found</div>");
            }
        },
        error: function () {
            ErrorToast('something wrong happened');
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
    if (ValidateOperation() == true) {
        if (FinalValidation() == true) {
            var arrStudent = [];
            $("#tblStudent input[type=checkbox]:checked").each(function () {
                var row = $(this).closest("tr")[0];
                arrStudent.push({
                    SEAD_StudentId: row.cells[0].innerHTML,
                    SEAD_IsAbsent: true
                });
            });
            // added on 10/12/2018 by Mousumi
            var _data = JSON.stringify({
                ExamAttendanceList: arrStudent,
                SEA_Attn_Date: convertToSqlDate ($("#SEA_Attn_Date").val()),
                SEA_TermId: $("#SEA_TermId").val(),
                SEA_SubjectId: $("#ddlSEA_SubjectId").val(),
                SEA_ClassId: $("#ddlSEA_ClassId").val(),
                SEA_SecId: $("#ddlSEA_SectionId").val(),
                Userid: $('#hdnUserid').val()
            });

            $.ajax({
                url: '/JQuery/InsertUpdateExamAttendance',
                data: _data,
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json ; utf-8',
                success: function (data) {
                    if (data != null && data != undefined && data.IsSuccess == true) {
                        alert(data.Message);
                        window.location.href = '/StudentManagement/ExamAttendanceList';
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
}

function ValidateOperation()
{
    if ($("#ddlSEA_ClassId").val() == 0) {
        alert('Please Select Class.');
        return false;
    }
    if ($("#ddlSEA_SectionId").val() == 0) {
        alert('Please Select Section.');
        return false;
    }
    if ($("#ddlSEA_SubjectId").val() == 0)
    {
        alert('Please Select Subject.');
        return false;
    }
    if ($('#SEA_TermId').val()=='')
    {
        alert('Please Select Term.');
        return false;
    }
    if ($('#SEA_Attn_Date').val() == '') {
        alert('Please provide Date. ');
        return false;
    }
    return true;
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
                BindDropDownListForSection($('#ddlSEA_SectionId'), data);
                $("#ddlSEA_SectionId").selectpicker('refresh');

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
function loadClasses() {
    $.get({
        type: "GET",
        url: rootDir + "JQuery/getClasses",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data, status) {
            if (status == "success") {
                //$("#Div_ClassDDL").empty().append('<select id="CWF_ClassId" name="CWF_ClassId" class="form-control show-tick" required="required"><option value="0">Select Class</option></select>');
                BindClassDropDownList($('#ddlSEA_ClassId'), data);
                $("#ddlSEA_ClassId").selectpicker('refresh');
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
function AjaxPostForDropDownSubject(Id) {
    var _data = JSON.stringify({
        ClassId: parseInt(Id),
    });
    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/ClassWiseSubjectBy_ClassList",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data, status) {
            if (status == "success") {
                BindDropDownListForSubject($('#ddlSEA_SubjectId'), data);
                $("#ddlSEA_SubjectId").selectpicker('refresh');

            } else {
                showMessage("Unable to process the request...", 0);
            }
        }
    });
}
function BindDropDownListForSubject(ddl, dataCollection) {
    $(ddl).get(0).length = 0;
    $(ddl).get(0).options[0] = new Option("Select", "0", "Selected");
    for (var i = 0; i < dataCollection.length; i++) {
        $(ddl).get(0).options[i + 1] = new Option(dataCollection[i].CSGWS_SBM_SubjectName, dataCollection[i].CSGWS_Sub_Id);
    }
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
            if (data != null && data != undefined ) {
                if (data == 1) {
                    alert('This date is invalid for this Session');
                    $("#SEA_Attn_Date").val('');
                }
                if (data != 0 && data !=1) {
                    alert('Sorry! We can not take attendance during '+ data + ' vacation.');
                    $("#SEA_Attn_Date").val('');
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
function FinalValidation() {
    if ($("#tblStudent > tbody > tr").length == 0) {
        ErrorToast("No record to save!")
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
function bindExamAttendanceList() {
    $('#update-panel').html('loading data.....');
    var _data = JSON.stringify({
    });
    $.ajax({
        url: '/JQuery/ExamAttendanceList',
        dataType: 'json',
        type: 'GET',
        data: {
            SEA_ClassId: $("#ddlSEA_ClassId").val(),
            //SEA_Attn_Date_S: $("#SEA_Attn_Date").val(),
            SEA_TermId: $("#SEA_TermId").val(),
            SEA_SubjectId: $("#ddlSEA_SubjectId").val(),
            SEA_SecId: $("#ddlSEA_SectionId").val(),
            FromDate: convertToSqlDate ( $('#FromDate').val()) || null,
            ToDate: convertToSqlDate ($('#ToDate').val()) || null
        },
        success: function (res) {
            if (res.length > 0) {
                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>Class</th><th>Section</th><th>Subject</th><th>Term</th><th>Date of Exam</th><th></th><th></th></tr></thead>";
                $data.append($header);

                $.each(res, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td>').text(i + 1));
                    $row.append($('<td style=display:none>').append(row.SEA_Attn_Id));
                    $row.append($('<td>').append(row.SEA_ClassName));
                    $row.append($('<td>').append(row.SEA_SectionName));
                    $row.append($('<td>').append(row.SEA_SubjectName));
                    $row.append($('<td>').append(row.SEA_TermName));
                    if (row.SEA_Attn_Date) {
                        var d = new Date(parseInt(row.SEA_Attn_Date.slice(6, -2)));
                        var dateStr = d.getDate() + '/' + (d.getMonth() + 1) + '/' + d.getFullYear();
                        $row.append($('<td>').append(formatJsonDate(row.SEA_Attn_Date)));
                    } else {
                        $row.append($('<td>').append(''));
                    }

                    $row.append($('<td>').append("<a href=/StudentManagement/StudentExamAttendance?ddlSEA_ClassId=" + row.SEA_ClassId + "&ddlSEA_SectionId=" + row.SEA_SecId + "&ddlSEA_SubjectId=" + row.SEA_SubjectId + "&SEA_TermId=" + row.SEA_TermId + "&SEA_Attn_Date=" + row.SEA_Attn_Date_S + " class='btn btn-warning'>Edit</a>"));
                   // $row.append($('<td>').append("<a onclick='DeleteRecord(" + row.SEA_Attn_Id + ");'" + row.SEA_Attn_Id + " class='btn btn-danger'>Delete</a>"));
                    $row.append($('<td>').append("<a href='javascript:void(0);' onclick='ConfirmDelete(" + row.SEA_Attn_Id + ");' class='btn btn-danger'>Delete</a>"));

                    $data.append($row);
                });
                $("#update-panel").html($data);
                $('#tblList').DataTable({
                    "order": [[0, "desc"]]
                });
            }
            else
            {
                $noData = "<div>No data Found</td>"
                $("#update-panel").html($noData);
            }

        },
        failure: function () {
            ErrorToast('something wrong happen');
        }

    });
}
function Edit()
{
    var ddlSEA_ClassId=isNaN(parseInt(getParameterByName('ddlSEA_ClassId')))?0:parseInt(getParameterByName('ddlSEA_ClassId'));
    var ddlSEA_SectionId=isNaN(parseInt(getParameterByName('ddlSEA_SectionId')))?0:parseInt(getParameterByName('ddlSEA_SectionId'));
    var ddlSEA_SubjectId=isNaN(parseInt(getParameterByName('ddlSEA_SubjectId')))?0:parseInt(getParameterByName('ddlSEA_SubjectId'));
    var hdnSEA_TermId = isNaN(parseInt(getParameterByName('SEA_TermId'))) ? 0 : parseInt(getParameterByName('SEA_TermId'));
    var SEA_Attn_Date = getParameterByName('SEA_Attn_Date');
    $("#SEA_ClassId").val(ddlSEA_ClassId);
    $("#SEA_SecId").val(ddlSEA_SectionId);
    $("#SEA_SubjectId").val(ddlSEA_SubjectId);
    $("#hdnSEA_TermId").val(hdnSEA_TermId);
    $("#SEA_Attn_Date").val(SEA_Attn_Date);
     AjaxPostForDropDownSection(ddlSEA_ClassId);
     AjaxPostForDropDownSubject(ddlSEA_ClassId);
     $("#ddlSEA_ClassId").val(parseInt($('#SEA_ClassId').val()));
     $("#ddlSEA_ClassId").selectpicker('refresh');
     $("#ddlSEA_SectionId").val(parseInt($('#SEA_SecId').val()));
     $("#ddlSEA_SectionId").selectpicker('refresh');
     $("#ddlSEA_SubjectId").val(parseInt($('#SEA_SubjectId').val()));
     $("#ddlSEA_SubjectId").selectpicker('refresh');
     $("#SEA_TermId").val(parseInt($('#hdnSEA_TermId').val()));
     $("#SEA_TermId").selectpicker('refresh');
     bindGrid(SEA_Attn_Date, hdnSEA_TermId, ddlSEA_SubjectId, ddlSEA_ClassId, ddlSEA_SectionId);
}
function getParameterByName(name) {
    //name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
           results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}
function DeleteRecord(Id) {
    var _data = JSON.stringify({
        id: parseInt(Id),
    });
    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/DeleteExamAttendance",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data, status) {
            if (status == "success") {  
                alert(data.Message);
                window.location.href = '/StudentManagement/ExamAttendanceList';
            }
            else {
                alert(data.Message);
            }
        }
    });
}


function ConfirmDelete(id) {
    if (confirm("Are you sure you want to delete this attendance record?")) {
        DeleteRecord(id);
    }
}