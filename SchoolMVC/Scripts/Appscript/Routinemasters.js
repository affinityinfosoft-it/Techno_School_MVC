$(document).ready(function () {
    BindList();
    $('#CWTR_Class').change(function () {
        var ClassIs = isNaN(parseInt($('#CWTR_Class').val())) ? null : parseInt($('#CWTR_Class').val())
        AjaxPostForDropDownSection(ClassIs);
        AjaxPostForDropDownSubject(ClassIs);
        AjaxPostForDropDownPeriod(ClassIs);
    });
    $('#ddlCWTR_Subject').change(function () {
        var ClassIs = isNaN(parseInt($('#CWTR_Class').val())) ? null : parseInt($('#CWTR_Class').val())
        var SubjecIs = isNaN(parseInt($('#ddlCWTR_Subject').val())) ? null : parseInt($('#ddlCWTR_Subject').val())
        AjaxPostForDropDownFaculty(ClassIs, SubjecIs);
    });
    $('#btnAddItems').click(function () {
        if (CheckDuplicity() == true) {
            AddDetails();
        }
    });
});
function InsertUpdateRoutine() {
    //Add By Uttaran
    var fileUpload = $("#CWTR_UploadFile").get(0);
    getFiles(fileUpload);
    Documentfile = $('#CWTR_UploadFile').val().substring(12);
    if ($('#CWTR_UploadFile').val() != '' && $('#CWTR_UploadFile').val() != null) {
        //if ($('#hdnImage').val() == '~/UploadFile/' || $('#hdnImage').val() == null || $('#hdnImage').val() == '') {
        Documentfile = $('#CWTR_UploadFile').val().substring(12);
        Documentpath2 = '~/UploadRoutine/' + Documentfile;
    }
    else {
        Documentpath2 = $('#hdnImage').val();
    }
    if (FinalValidation() == true) {
        var ArrayItem = [];
        var tblItem = document.getElementById('tblList');
        var len = tblItem.rows.length - 1;
        for (var i = 0; i < len; i++) {
            ArrayItem.push({
                CWTR_Class: $("#tblList>tbody tr:eq(" + i + ") td:eq(1)").text(),
                CWTR_Section: $("#tblList>tbody tr:eq(" + i + ") td:eq(3)").text(), // SectionId
                CWTR_Day: $("#tblList>tbody tr:eq(" + i + ") td:eq(5)").text(), // DayId
                CWTR_Subject: $("#tblList>tbody tr:eq(" + i + ") td:eq(7)").text(), // SubjectId
                CWTR_Period: $("#tblList>tbody tr:eq(" + i + ") td:eq(9)").text(), // PeriodId
                CWTR_Teacher: $("#tblList>tbody tr:eq(" + i + ") td:eq(11)").text() // TeacherId
            });

        }
        var _data = JSON.stringify({
            Rutine_IsUpload: $("#modeSwitch").is(':checked'),
            CWTR_Id: $('#CWTR_Id').val(),
            CWTR_Class: $('#class').val(),
            CWTR_Section: $('#CWTR_Section').val(),
            CWTR_Title: $('#CWTR_Title').val(),
            CWTR_Description: $('#CWTR_Description').val(),
            CWTR_UploadFile: Documentpath2,

            RoutineList: ArrayItem,
            Userid: $('#hdnUserid').val()

        });
        $.ajax({
            url: '/JQuery/InsertUpdateRoutine',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    $('#btnSave').attr('disabled', 'disabled');
                    setTimeout(function () {

                        window.location.href = '/Masters/RoutineList';
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
function AddDetails() {
    if (ValidateOperation() == true) {
        if ($("#tblList>tbody:eq(0) tr:eq(0) td:eq(2)").text() == '') {
            $("#tblList").find('tbody').empty();
        }

        var $row = $('<tr/>');
        $row.append($('<td style=display:none>').html($('#CWTR_Id').val()));
        $row.append($('<td style=display:none>').html($("#CWTR_Class option:selected").val()));
        $row.append($('<td/>').html($("#CWTR_Class option:selected").text()));
        $row.append($('<td style="display:none">').html($("#ddlCWTR_Section").val()));  
        $row.append($('<td/>').html($("#ddlCWTR_Section option:selected").text()));     
        $row.append($('<td style=display:none>').html($("#CWTR_Day option:selected").val()));
        $row.append($('<td/>').html($("#CWTR_Day option:selected").text()));
        $row.append($('<td style=display:none>').html($("#ddlCWTR_Subject option:selected").val()));
        $row.append($('<td/>').html($("#ddlCWTR_Subject option:selected").text()));
        $row.append($('<td style=display:none>').html($("#Period").val()));
        $row.append($('<td/>').html($("#Period").val()));
        $row.append($('<td style=display:none>').html($("#ddlCWTR_Teacher option:selected").val()));
        $row.append($('<td/>').html($("#ddlCWTR_Teacher option:selected").text()));
        $row.append($('<td>').append("<input type='image' src='/Content/images/delete.png' onclick='Confirm(this);'>"));

        $("#tblList>tbody").append($row);
        ///Disable for after add not clear the others main fields 
        //ClearDetails();
    }
}
function Confirm(rowNo) {
    var r = rowNo;
    var agree = confirm("Are you sure you want to delete this record from database?");
    if (agree) {

        var _data = JSON.stringify({
            MainTableName: 'ClassWiseTeacherRoutine_CWTR',
            MainFieldName: 'CWTR_Id',
            PId: parseInt(rowNo),
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
                    window.location.href = '/Masters/RoutineList';
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
}
function deleteRowWithOutAlert(rowNo) {
    $(rowNo).closest('tr').remove();
}
function ClearDetails() {
    $("#Period").val('');
    $("#Period").selectpicker('refresh');
    $("#CWTR_Class").val('');
    $("#CWTR_Class").selectpicker('refresh');
    $("#CWTR_Day").val('');
    $("#CWTR_Day").selectpicker('refresh');
    $("#ddlCWTR_Section").val('');
    $("#ddlCWTR_Section").selectpicker('refresh');
    $("#ddlCWTR_Subject").val('');
    $("#ddlCWTR_Subject").selectpicker('refresh');
    $("#ddlCWTR_Teacher").val('');
    $("#ddlCWTR_Teacher").selectpicker('refresh');

}
function FinalValidation() {
    if ($("#modeSwitch").is(':checked') == false) {
        if ($("#tblList>tbody:eq(0) tr:eq(" + 0 + ") td:eq(2)").text() == '') {
            ErrorToast("Please add any Details!")
            return false;
        }
    }

    return true;
}
function BindList() {
    $('#update-panel').html('loading data.....');
    $.ajax({
        url: '/JQuery/RoutineList',
        dataType: 'json',
        type: 'GET',
        success: function (res) {
            if (res.Data.length > 0) {
                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                $header = "<thead><tr><th>SL</th><th style='display:none'>Id</th><th style='display:none'>ClassId</th><th>Class</th><th>Section</th><th>Faculty</th><th>Subject</th><th>Day</th><th>Period</th><th>Title</th><th>Description</th><th></th><th></th><th></th></tr></thead>";
                $data.append($header);

                var $tbody = $("<tbody></tbody>");

                $.each(res.Data, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td>').text(i + 1));
                    $row.append($('<td style=display:none>').append(row.CWTR_Id));
                    $row.append($('<td style=display:none>').append(row.CWTR_ClassPreference));
                    $row.append($('<td>').append(row.CWTR_CM_CLASSNAME));
                    $row.append($('<td>').append(row.CWTR_SECM_SECTIONNAME));
                    $row.append($('<td>').append(row.CWTR_FP_Name));
                    $row.append($('<td>').append(row.CWTR_SBM_SubjectName));
                    $row.append($('<td>').append(row.CWTR_DM_DayName));
                    $row.append($('<td>').append(row.CWTR_PeriodName));
                    ///add uttaran 29/11/24
                    $row.append($('<td>').append(row.CWTR_Title));
                    $row.append($('<td>').append(row.CWTR_Description));
                    //$row.append($('<td>').append(row.CWTR_UploadFile));


                    if (res.CanView == true) {
                        $row.append($('<td>').append("<a href=/Masters/Routine/" + row.CWTR_Id + " class='btn btn-warning'>View</a>"));
                    }
                    else {
                        //$row.append($('<td>').append('<a href=.append(row.NM_UploadFile) >View</a>'));
                        $row.append($('<td>').append('<a href=' + row.CWTR_UploadFile.replace("~", location.origin) + ' class="btn btn-info" target="_blank">View</a>'));;
                    }

                    if (res.CanEdit == true) {
                        $row.append($('<td>').append("<a href=/Masters/Routine/" + row.CWTR_Id + " class='btn btn-warning'>Edit</a>"));
                    }
                    else {
                        $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                    }
                    $row.append($('<td>').append("<a onclick='Confirm(" + row.CWTR_Id + ");'" + row.CWTR_Id + " class='btn btn-danger'>Delete</a>"));
                    $data.append($row);
                });
                $("#update-panel").html($data);
                $("#tblList").DataTable({
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
                BindDropDownListForSection($('#ddlCWTR_Section'), data);
                $("#ddlCWTR_Section").selectpicker('refresh');
            }
            else {
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
function AjaxPostForDropDownSubject(Id) {
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
                BindDropDownListForSection($('#ddlCWTR_Section'), data);
                $("#ddlCWTR_Section").selectpicker('refresh');

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
                BindDropDownListForSection($('#ddlCWTR_Section'), data);
                $("#ddlCWTR_Section").selectpicker('refresh');

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
function AjaxPostForDropDownSubject(Id) {
    var _data = JSON.stringify({

        CSGWS_Class_Id: parseInt(Id)
    });
    $.ajax({
        type: "POST",
        data: _data,
        url: rootDir + "JQuery/GetGroupWiseSubject",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",

        success: function (data, status) {
            if (status == "success") {
                BindDropDownListForSubject($('#ddlCWTR_Subject'), data);
                $("#ddlCWTR_Subject").selectpicker('refresh');

            } else {
                showMessage("Unable to process the request...", 0);
            }

        },
        error: function () {
            alert('Something Wrong Happen');
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
function AjaxPostForDropDownFaculty(ClassIs, SubjecIs) {
    var _data = JSON.stringify({
        obj: {
            FP_ClassId: parseInt(ClassIs),
            FP_SubjectId: parseInt(SubjecIs)
        }

    });
    $.ajax({
        type: "POST",
        data: _data,
        url: rootDir + "JQuery/LoadFacultyByClass",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",

        success: function (data, status) {
            if (status == "success") {
                BindDropDownListForFaculty($('#ddlCWTR_Teacher'), data);
                $("#ddlCWTR_Teacher").selectpicker('refresh');

            } else {
                showMessage("Unable to process the request...", 0);
            }

        },
        error: function () {
            alert('Something Wrong Happen');
        }

    });

}
function BindDropDownListForFaculty(ddl, dataCollection) {
    $(ddl).get(0).length = 0;
    $(ddl).get(0).options[0] = new Option("Select", "0", "Selected");
    for (var i = 0; i < dataCollection.length; i++) {
        $(ddl).get(0).options[i + 1] = new Option(dataCollection[i].FP_Name, dataCollection[i].FP_Id);
    }
}
function CheckDuplicity() {
    return true;
}
function ValidateOperation() {
    if ($('#CWTR_Class').val() == 0) {
        WarningToast('Please select a class.');
        return false;
    }
    if ($('#ddlCWTR_Section').val() == 0) {
        WarningToast('Please select a section.');
        return false;
    }
    if ($('#CWTR_Day').val() == 0) {
        WarningToast('Please select a day.');
        return false;
    }
    if ($('#ddlCWTR_Subject').val() == 0) {
        WarningToast('Please select a Subject.');
        return false;
    }
    if ($('#Period').val() == '') {
        WarningToast('Please provide period no.');
        return false;
    }
    if ($('#ddlCWTR_Teacher').val() == 0) {
        WarningToast('Please select a teacher.');
        return false;
    }

    var tblItem = document.getElementById('tblList');
    var len = tblItem.rows.length;
    var classvalue = $('#CWTR_Class option:selected').text();

    var subject = $('#ddlCWTR_Subject option:selected').text();

    var Fac = $('#ddlCWTR_Teacher option:selected').text();

    var sec = $('#ddlCWTR_Section option:selected').text();

    var day = $('#CWTR_Day option:selected').text();

    var period = $('#Period').val();

    for (var i = 1; i < tblItem.rows.length; i++) {
        var classvalue2 = $("#tblList>tbody:eq(0) tr:eq(" + i + ") td:eq(2)").text();
        var subject2 = $("#tblList>tbody:eq(0) tr:eq(" + i + ") td:eq(8)").text();
        var fac2 = $("#tblList>tbody:eq(0) tr:eq(" + i + ") td:eq(11)").text();
        var sec2 = $("#tblList>tbody:eq(0) tr:eq(" + i + ") td:eq(4)").text();
        var day2 = $("#tblList>tbody:eq(0) tr:eq(" + i + ") td:eq(6)").text();
        var period2 = $("#tblList>tbody:eq(0) tr:eq(" + i + ") td:eq(10)").text();
        if (classvalue == classvalue2  && day == day2 && subject == subject2 && period == period2 && Fac == fac2) {
            ErrorToast('This Combination already exist.')
            return false;
        }
        else {
            continue;
        }
    }
    return true;
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
function CheckDuplicity() {
    var flag = 0;
    var _data = JSON.stringify({
        obj: {
            CWTR_Class: $("#CWTR_Class").val(),
            CWTR_Section: $("#ddlCWTR_Section").val(),
            CWTR_Day: $("#CWTR_Day").val(),
            CWTR_Subject: $("#ddlCWTR_Subject").val(),
            CWTR_Period: $("#Period").val(),
            CWTR_Teacher: $("#ddlCWTR_Teacher").val(),
             }
    });
    $.ajax({
        type: "POST",
        data: _data,
        url: rootDir + "JQuery/CheckDuplicity",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (data, status) {
            if (status == "success") {

                if (data=='A')
                {
                    alert('This combination is already exist in database.')
                    flag = 1;
                    return false;

                }
                if (data == 'F') {
                    alert('This Faculty is already assigned for this period.')
                    flag = 1;
                    return false;
                }
                if (data == 'C') {
                    alert('This Period is already assigend.')
                    flag = 1;
                    return false;
                }
                if (data == 'S') {
                    alert('This Subject is already assigend for this period.')
                    flag = 1;
                    return false;
                }
            }
            else
            {
                showMessage("Unable to process the request...", 0);
            }
        },
        error: function ()
        {
            alert('Something Wrong Happen');
        }
    });
    if (flag == 1) {
        return false;
    }
    if (flag == 0) {
        return true;
    }
}
function AjaxPostForDropDownPeriod(classId) {
    var _data = JSON.stringify({ Id: classId });

    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/GetPeriodByClass",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            BindDropDownListForPeriod($('#Period'), data);
            $("#Period").selectpicker('refresh');
        },
        error: function () {
            alert('Unable to load periods');
        }
    });
}
function BindDropDownListForPeriod(ddl, data) {
    ddl.empty();
    ddl.append(new Option("Select", "0"));

    if (data && data.length > 0) {
        $.each(data, function (i, obj) {
            ddl.append(new Option(obj.PM_Period, obj.PM_Id));
        });
    }
}



// Edit details from Table 
//function EditDetails(r) {
//    debugger;
//    var row = parseInt($(r).closest('tr').index());
//    var check = $("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(1)").text();
//    var classname = $("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(1)").text();
//    var sec = $("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(3)").text();
//    var day = $("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(5)").text();
//    var subject = $("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(7)").text();
//    var fac = $("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(11)").text();
//    $("#CWTR_Class").val(classname);
//    $("#CWTR_Class").selectpicker('refresh');
//    AjaxPostForDropDownSection(classname);    
//    $("#ddlCWTR_Section").val(sec);
//    $("#ddlCWTR_Section").selectpicker('refresh');   
//    $("#CWTR_Day").val(day);
//    $("#CWTR_Day").selectpicker('refresh');
//    AjaxPostForDropDownSubject(classname);
//    $("#ddlCWTR_Subject").val(subject);
//    $("#ddlCWTR_Subject").selectpicker('refresh');
//    $("#Period").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(10)").text());
//    AjaxPostForDropDownFaculty(classname);
//    $("#ddlCWTR_Teacher").val(fac);
//    $("#ddlCWTR_Teacher").selectpicker('refresh');
//    deleteRowWithOutAlert(r);
//}
