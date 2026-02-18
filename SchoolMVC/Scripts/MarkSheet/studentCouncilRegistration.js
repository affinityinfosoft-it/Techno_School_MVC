var ctrlURL = rootDir + "MarkSheet/MarkSheet";
var printStudentList = [];
var classes = [];
var isHigherSecondary = false;

function Reset() {
    var url = window.location.href;
    window.location.href = url;
    return false;
}
$(document).ready(function () {
    $('#div_AlertSuccess').hide();
    $('#div_danger').hide();
    loadClasses();
    $("#ddlClass").change(function () {
        AjaxPostForDropDownSection(parseInt($('#ddlClass option:selected').val()));
        $.each(classes, function (index, element) {
            if (element.CM_CLASSID == parseInt($('#ddlClass option:selected').val())) {
                isHigherSecondary = element.IsHigherSecondary;
            }
        });
    });
});
//$(document).on("click", '.btnPrint', function () {
//    var StudentId = $.trim($(this).attr("data-StudentId"));
//    var ClassId = $.trim($(this).attr("data-ClassId"));
//    var TermId = $.trim($(this).attr("data-TermId"));
//    var url = '';
//    if (isHigherSecondary == true) url = rootDir + "Reports/MarkSheet/MarkSheetReport.aspx?SId=" + StudentId + "&CId=" + ClassId + "&TermId=" + TermId;
//    else url = rootDir + "Reports/MarkSheet/MarkSheetReportSecondary.aspx?SId=" + StudentId + "&CId=" + ClassId + "&TermId=" + TermId;
//    var WindowDimensions = "toolbars=no,menubar=no,location=no,titlebar=no,scrollbars=yes,resizable=yes,status=yes"
//    var PopUp = window.open(url, "MyMarkSheetReportWindow", WindowDimensions);
//    if (PopUp.outerWidth < screen.availWidth || PopUp.outerHeight < screen.availHeight) {
//        PopUp.moveTo(25, 50);
//        PopUp.resizeTo(screen.availWidth - 10, screen.availHeight - 10);
//    }
//});
$(document).on("click", "#btnSearch", function (e) {
    if (ValidateForm() == true) {
        var _data = JSON.stringify({
            query: {
                ClassId: parseInt($('#ddlClass option:selected').val()),
                SectionId: parseInt($('#ddlSection option:selected').val()),
            }

        });
        $.ajax({
            type: "POST",
            url: ctrlURL + "/studentsForCouncilRegistration",
            data: _data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data, status) {
                if (status == "success") {
                    var tr;
                    for (var i = 0; i < data.length; i++) {
                        tr = $('<tr/>');
                        tr.append("<td>" + data[i].StudentId + "</td>");
                        tr.append("<td>" + data[i].StudentName + "</td>");
                        tr.append("<td>" + data[i].Roll + "</td>");
                        tr.append("<td><input type='text' class='txtRegNo' value='" + data[i].SD_RegistrationNo + "' name='txtRegNo" + i + "' ></td>");
                        tr.append("<td></td>");
                        $('#tblStudent').append(tr);
                    }
                    if (data.length == 0) messageBox("No Records are found!!", 0);
                } else {
                    messageBox("Unable to process the request...", 0);
                }
            }

        });
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
                BindClassDropDownList($('#ddlClass'), data);
                $("#ddlClass").selectpicker('refresh');
                classes = data;
            } else {
                messageBox("Unable to process the request...", 0);
            }
        }
    });
}
function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
           results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}
function AjaxPostForDropDownSection(Id) {
    var _data = JSON.stringify({
        classId: parseInt(Id),
    });
    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/ClassWiseSection",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data, status) {
            if (status == "success") {
                BindSectionDropDownList($('#ddlSection'), data);
                $("#ddlSection").selectpicker('refresh');
            } else {
                messageBox("Unable to process the request...", 0);
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
function BindSectionDropDownList(ddl, dataCollection) {
    $(ddl).get(0).length = 0;
    $(ddl).get(0).options[0] = new Option("Select", "0", "Selected");
    if (dataCollection.length != 0) {
        for (var i = 0; i < dataCollection.length; i++) {
            $(ddl).get(0).options[i + 1] = new Option(dataCollection[i].SECM_SECTIONNAME, dataCollection[i].SECM_SECTIONID);
        }
    }
}
function ValidateForm() {
    var ddlClass = $('#ddlClass').val();
    var ddlSection = $('#ddlSection').val();
    if (ddlClass == "0") {
        alert('select class....');
        return false;
    }
    if (ddlSection == "0") {
        alert('select Section....');
        return false;
    }
    return true;
}
function SaveRecord() {
    var tblStudent = document.getElementById('tblStudent');
    tableStudentsArr = [];
    for (var i = 1; i < tblStudent.rows.length; i++) {
        var RegistrationNo = tblStudent.rows[i].cells[3].getElementsByTagName('input')[0].value;
        if (RegistrationNo != '') {
            tableStudentsArr.push({
                StudentId: tblStudent.rows[i].cells[0].innerHTML,
                Roll: tblStudent.rows[i].cells[2].innerHTML,
                SD_RegistrationNo: RegistrationNo,
            });
        }
    }
    //if (SaveValidateForm() == true) {
        var _data = JSON.stringify({
            clsStudents: tableStudentsArr,
        });
        $.get({
            type: "POST",
            url: ctrlURL + "/InsertUpdateStudentCouncilRegistration",
            data: _data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    if (data.Id != -1) clearFormElements();
                    $('#div_AlertSuccess').show().text(data.Message);
                }
                else {

                    $('#div_danger').show().text(data.Message);
                }
            },
            error: function (data) {
                $('#div_danger').show().text('Process Fail...');
            }
        });
    //}
}
function clearFormElements() {
    DeleteRows();
    $(':input', '#StudentCouncilReg_form').not(':button, :submit, :reset,#div_AlertSuccess,#div_danger').val('').prop('checked', false).prop('selected', false);   
    $('#ddlClass').val("0");
    $("#ddlClass").selectpicker('refresh');
    $('#ddlSection').find('option').remove().end().append('<option value="0">Select</option>');
    $("#ddlSection").selectpicker('refresh');
}
function DeleteRows() {
    var tblStudent = document.getElementById('tblStudent')
    var rowCount = tblStudent.rows.length;
    for (var i = rowCount - 1; i > 0; i--) {
        tblStudent.deleteRow(i);
    }
}


