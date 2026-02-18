
var tableArr = [];

$(document).ready(function () {

    $("#btnSearch").click(function () {
        BindStudents();

        $('#example tbody tr').each(function (index, e) {

            var classID = $(this).find('input[id^="ddlclassId"]').val();
            var sectionId = $(this).find('.show-tick').attr('id');

            var dbSectionId = tableArr[index].SD_CurrentSectionId;
            var dbRollNo = tableArr[index].SD_CurrentRoll;

            // Load dropdown + set selected value inside AJAX success
            AjaxPostForDropDownSectionFrom(parseInt(classID), sectionId, dbSectionId);

            // Roll retrieve
            $(this).find('input[id^="Roll"]').val(dbRollNo);
        });
    });

    $("#btnSave").click(function () {
        SaveRecord();
    });

});


function bindGrid() {

    $('#update-panel').html('loading data.....');
    $("#tblStudent").show();

    var ClassId = ($('#SR_CLASSID').val() == 0) ? '' : $('#SR_CLASSID option:selected').val();

    $.ajax({
        url: rootDir + "JQuery/GetStudentListRollSecSetting",
        dataType: 'json',
        data: { SD_ClassId: ClassId, SD_StudentId: $('#SR_STUDENTID').val() },
        type: 'POST',
        async: false,
        success: function (d) {

            if (d.length > 0) {

                var $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                var $header = "<thead><tr><th>Student Id</th><th>Student Name</th><th>Class</th><th>Choose Section</th><th>Roll No</th></tr></thead>";
                $data.append($header);

                $.each(d, function (i, row) {

                    var $row = $('<tr/>');

                    $row.append($('<td>').append(row.SD_StudentId));
                    $row.append($('<td>').append(row.SD_StudentName));
                    $row.append($('<td style="display:none">').append(row.SD_CurrentClassId));
                    $row.append($('<td>').append(row.SD_CM_CLASSNAME));
                    $row.append("<td><select class='form-control show-tick' id='ddlSection" + row.SD_StudentId + "'>" +
                        "<option value='0'>Select Section</option></select></td>");
                    $row.append("<td><input type='text'></td>");
                    $row.append("<td><input type='hidden' id='ddlclassId" + row.SD_StudentId + "' value='" + row.SD_CurrentClassId + "'></td>");

                    $data.append($row);
                });

                $("#update-panel").html($data);
            }
            else {
                $("#update-panel").html("<div>No data Found</div>");
            }
        },
        failure: function () {
            ErrorToast('something wrong happen');
        }
    });

}


function SaveRecord() {

    var tbl = document.getElementById('example');
    var example = $('#example tr');
    var tableStudentsArr = [];

    for (var i = 1; i < tbl.rows.length; i++) {

        tableStudentsArr.push({
            SR_STUDENTID: example[i].cells[1].innerHTML.trim(),
            SR_CLASSID: parseInt(example[i].cells[3].innerHTML), 
            SR_SECTIONID: parseInt($(example[i].cells[5]).find('select').val()),
            SR_ROLLNO: $(example[i].cells[6]).find('input').val()
        });
    }

    var _data = JSON.stringify({
        obj: {
            SecRollList: tableStudentsArr
        }
    });

    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/InsertUpdateSecRollSetting",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data != null && data.IsSuccess) {
                alert(data.Message);
                window.location.href = '/Masters/SectionRollSetting';
            }
            else {
                alert(data.Message);
            }
        },
        error: function () {
            alert('Process Fail...');
        }
    });

}


function safeSelector(id) {
    return id.replace(/([ #;&,.+*~':"!^$[\]()=>|/@])/g, '\\$1');
}


// ⭐ FIXED FUNCTION — Now accepts selectedValue & sets after load
function AjaxPostForDropDownSectionFrom(Id, ddl, selectedValue) {

    var _data = JSON.stringify({ Id: Id });

    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/GetSectionByClass",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (data) {

            var CreateddlId = $('#' + safeSelector(ddl));

            CreateddlId.empty().append($("<option></option>").val("0").text("Select"));

            $.each(data, function (key, value) {
                CreateddlId.append($("<option></option>")
                    .attr("value", value.SECM_SECTIONID)
                    .text(value.SECM_SECTIONNAME));
            });

            // ⭐ Select saved value AFTER dropdown is filled
            if (selectedValue != null && selectedValue != 0) {
                CreateddlId.val(selectedValue);
            }
        },

        error: function (xhr, status, error) {
            console.error("AJAX Error:", status, error);
        }
    });
}


function BindDropDownListForSection(ddl, dataCollection) {
    $(ddl).empty();
    $(ddl).append(new Option("Select", "0", true, true));

    for (var i = 0; i < dataCollection.length; i++) {
        $(ddl).append(new Option(dataCollection[i].SECM_SECTIONNAME, dataCollection[i].SECM_SECTIONID));
    }
}


function BindStudents() {
    var ClassId = ($('#SR_CLASSID').val() == 0) ? '' : $('#SR_CLASSID option:selected').val();

    var _data = JSON.stringify({
        SD_ClassId: parseInt(ClassId),
        SD_StudentId: $.trim($('#SR_STUDENTID').val())
    });

    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/GetStudentListRollSecSetting",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,

        success: function (data) {

            $("#example > tbody").empty();
            tableArr = data;

            for (var i = 0; i < tableArr.length; i++) {

                var tr = $('<tr/>');
                tr.append("<td>" + (i + 1) + "</td>");
                tr.append("<td>" + tableArr[i].SD_StudentId + "</td>");
                tr.append("<td>" + tableArr[i].SD_StudentName + "</td>");
                tr.append("<td hidden='hidden'>" + tableArr[i].SD_CurrentClassId + "</td>");
                tr.append("<td>" + tableArr[i].SD_CM_CLASSNAME + "</td>");
                tr.append("<td><select class='form-control show-tick' id='ddlSection" + tableArr[i].SD_StudentId + "'><option value='0'>Select Section</option></select></td>");
                tr.append("<td><input type='text' id='Roll" + tableArr[i].SD_StudentId + "'></td>");
                tr.append("<td><input type='hidden' id='ddlclassId" + tableArr[i].SD_StudentId + "' value='" + tableArr[i].SD_CurrentClassId + "'></td>");

                $('#example').append(tr);
            }
        }
    });
}
