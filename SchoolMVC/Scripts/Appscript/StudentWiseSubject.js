var selectAllPromptShown = false;

$(document).ready(function () {

    $("#SWS_ClassId").change(function () {
        AjaxPostForDropDownSection($('#SWS_ClassId option:selected').val());
    });
    $("#btnSearch").click(function () {
        bindGrid();
    });
    $("#ddlSubGroup").change(function () {
        AjaxPostForDropDownSubject();
    });
    $("#btnAdd").click(function () {
        AddSubject();
    });
    $("#btnSubmit").click(function () {
        InsertStudentwiseSubject();
    });
    $("#btnAssign").click(function () {
        GetAllSID();
    });
    $("#btnSave").click(function () {
        AssignSubject();
    });
    //$("#CheckAll").change(function () {
    //    $("input:checkbox").prop('checked', $(this).prop("checked"));
    //});
    $("#CheckAll").change(function () {
        $('#tblList .student-check').prop('checked', $(this).prop("checked"));
    });

});
var tableStudentsArr = [];
var tableAbsntArr = [];
var Id = '';
function AddSubject() {
    var count = 0;
    var TableId = document.getElementById("tblStudentWiseSub");

    var SubjectId = $("#ddlSubject").val();
    var SubjectGroupId = $("#ddlSubGroup").val();
    var rowcount = (TableId.rows.length - 1);
    if (rowcount != 0) {
        for (i = 1; i <= rowcount; i++) {
            var Groupid = TableId.rows[i].cells[1].innerHTML
            if (SubjectGroupId == Groupid) {
                var SubID = TableId.rows[i].cells[3].innerHTML
                if (SubID == SubjectId) {
                    alert('This Subject is already assigned for this student.');
                    count = 0;
                    break;
                }
                else {
                    count++;

                }
            }
            else {
                count++;
            }


        }
    }
    else {

        var $row = $('<tr/>');

        $row.append($('<td/>').html($("#ddlSubGroup option:selected").text()));
        $row.append($('<td style=display:none/>').html($("#ddlSubGroup").val()));
        $row.append($('<td/>').html($("#ddlSubject option:selected").text()));
        $row.append($('<td style=display:none/>').html($("#ddlSubject").val()));

        $row.append($('<td>').append("<input type='image' name='imgede' src='/Content/images/close_icon.png' onclick = 'deleteRow(this);' />"));
        $("#tblStudentWiseSub>tbody").append($row);
    }
    if (count != 0) {
        var $row = $('<tr/>');

        $row.append($('<td/>').html($("#ddlSubGroup option:selected").text()));
        $row.append($('<td style=display:none/>').html($("#ddlSubGroup").val()));
        $row.append($('<td/>').html($("#ddlSubject option:selected").text()));
        $row.append($('<td style=display:none/>').html($("#ddlSubject").val()));

        $row.append($('<td>').append("<input type='image' name='imgede' src='/Content/images/close_icon.png' onclick = 'deleteRow(this);' />"));
        $("#tblStudentWiseSub>tbody").append($row);
    }
}
function EditStudentWiseSub(r) {
    var row = parseInt($(r).closest('tr').index());

    var strStudentID = $("#tblStudentWiseSub>tbody:eq(0) tr:eq(" + row + ") td:eq(0)").text();
    var strSubGroup = $("#tblStudentWiseSub>tbody:eq(0) tr:eq(" + row + ") td:eq(1)").text();
    var strSubject = $("#tblStudentWiseSub>tbody:eq(0) tr:eq(" + row + ") td:eq(3)").text();
    $("#ddlSubGroup option:selected").text(strSubGroup);
    $("#ddlSubject option:selected").text(strSubject);
    deleteRowWithoutAlert(r);
}
function deleteRowWithoutAlert(rowNo) {
    $(rowNo).closest('tr').remove();

}
function deleteRow(rowNo) {
    var agree = confirm("Are you sure you want to delete this record?");
    if (agree) {
        $(rowNo).closest('tr').remove();

        return true;
    } else {
        return false;
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
                BindDropDownListForSection($('#ddlSWS_SecId'), data);
                $("#ddlSWS_SecId").selectpicker('refresh');

            } else {
                showMessage("Unable to process the request...", 0);
            }
        }
    });
}
function AjaxPostForDropDownSubject() {
    var _data = JSON.stringify({
        CSGWS_SubGr_Id: parseInt($("#ddlSubGroup").val()),
        //CSGWS_Class_Id: parseInt($('#classid').val())

        CSGWS_Class_Id: parseInt($('#SWS_ClassId').val())
    });
    $.ajax({
        type: "POST",
        data: _data,
        url: rootDir + "JQuery/GetGroupWiseSubject",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",

        success: function (data, status) {
            if (status == "success") {
                BindDropDownListForSubject($('#ddlSubject'), data);
                $("#ddlSubject").selectpicker('refresh');

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
function BindDropDownListForSection(ddl, dataCollection) {
    $(ddl).get(0).length = 0;
    $(ddl).get(0).options[0] = new Option("Select", "0", "Selected");
    for (var i = 0; i < dataCollection.length; i++) {
        $(ddl).get(0).options[i + 1] = new Option(dataCollection[i].SECM_SECTIONNAME, dataCollection[i].SECM_SECTIONID);
    }
}

$(document).on('change', '.student-check', function () {

    if ($(this).is(':checked') && !selectAllPromptShown) {

        selectAllPromptShown = true;

        if (confirm("Do you want to select all students?")) {
            $('.student-check').prop('checked', true);
            $('#CheckAll').prop('checked', true); 
        }
    }

});

function bindGrid() {
    selectAllPromptShown = false;
    $('#update-panel').html('loading data.....');
    if (validateForm() == true) {


        $("#tblStudent").show();
        if ($('#ddlSWS_SecId').val() == 0) {
            var SecId = '';
        }
        else {
            var SecId = $('#ddlSection').val();
        }

        var ClassId = isNaN(parseInt($('#SWS_ClassId option:selected').val())) ? null : parseInt($('#SWS_ClassId option:selected').val())
        var _data = JSON.stringify({
            obj: {
                SD_ClassId: ClassId,
                SD_CurrentSectionId: parseInt(SecId),
                SD_StudentId: $('#SD_StudentId').val(),
                SD_StudentName: $('#SD_StudentName').val(),
                SD_CurrentSectionId: $('#ddlSWS_SecId').val(),
            }
        });
        $.ajax({
            url: rootDir + "JQuery/GetStudentListForSubjectSettings",
            dataType: 'json',
            contentType: 'application/json',
            data: _data,
            type: 'POST',
            success: function (d) {
                if (d.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th>Student Id</th><th>Student Name</th><th>Class</th><th>Select Student</th></tr></thead>";
                    $data.append($header);

                    $.each(d, function (i, row) {
                        $('#classid').val(row.SD_CurrentClassId);
                        var $row = $('<tr/>');
                        //$row.append('<td>' + (i + 1) + '</td>');
                        $row.append($('<td>').append(row.SD_StudentId));
                        $row.append($('<td>').append(row.SD_StudentName));
                        $row.append($('<td>').append(row.SD_CM_CLASSNAME));
                        //$row.append($('<td/>').html('<input type=checkbox />'));
                        $row.append($('<td/>').html('<input type="checkbox" class="student-check" />'));

                        $data.append($row);
                    });
                    $("#update-panel").html($data);
                    $("#tblList").DataTable();
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
}
function SelectStudent(r) {
    var aa = $(r).attr("data-stud-id");
    var Classname = $(r).attr("data-Class-Name");
    var Classid = $(r).attr("data-Class-id");
    $("#hdnId").val(aa);
    $("#myModalStudentData").modal('hide');
    $("#SubDetails").show();
    AjaxPostForDropDownSubjectGroup();
    $("#ddlClass option:selected").val(Classid);
    GetStudentWiseSubjectById();

}



function validateForm() {

    if ($("#ddlClass").val() == 0 && ($("#txtStudentId").val() == '') && ($("#ddlSection").val() == null) && ($("#txtName").val() == '')) {
        alert("Please Select atleast one item to find data");
        return false
    }
    return true;
}
function InsertStudentwiseSubject() {
    var StudentSubList = [];
    var TableItem = document.getElementById('tblStudentWiseSub');
    for (var i = 1 ; i < TableItem.rows.length ; i++) {
        StudentSubList.push({
            strStudentSID: TableItem.rows[i].cells[0].innerHTML,
            intSubGroupId: parseInt(TableItem.rows[i].cells[2].innerHTML),
            intSubjectId: parseInt(TableItem.rows[i].cells[3].innerHTML),
        });
    }


    var _data = JSON.stringify({
        subject: {

            StudentWiseSubjectList: StudentSubList,
        }
    });

    $.ajax({
        type: "POST",

        url: rootDir + "JQuery/InsertUpdateStudentwiseSubject",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d != null && data.d != undefined && data.d.IsSuccess == true) {
                alert(data.d.Message);
                var url = '/Masters/StudentWiseSubjectList'
                window.location.href = url;

            }
            else {
                alert(data.d.Message);
            }
        },
        error: function (data) {
            alert('Process Fail...');
        }
    });
}
function DeleteOldData(r) {
    var agree = confirm("Are you sure you want to delete this record?");
    if (agree) {
        var subjectid = $(r).attr("Subject-data-id");
        var studentid = $(r).attr("Student-data-id");
        var _data = JSON.stringify({


            StudentId: studentid,
            SuBjectId: subjectid

        });

        $.ajax({
            type: "POST",
            url: "MasterService.asmx/DeletetudentwiseSubject",
            data: _data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d != null && data.d != undefined && data.d.IsSuccess == true) {

                    alert(data.d.Message);
                    //var url = '/Masters/StudentWiseSubjectSetting.aspx'
                    //window.location.href = url;

                }
                else {
                    alert(data.d.Message);
                }
            },
            error: function (data) {
                alert('Process Fail...');
            }
        });
        return true;
    } else {
        return false;
    }
}
function GetAllSID() {
    if ($('#tblList>tbody input[type="checkbox"]').is(":checked")) {
        var arrStudentID = [];

        var tblItem = document.getElementById('tblList');
        $('#tblList>tbody input[type="checkbox"]:checked').each(function (i, row) {
            var cellrow = parseInt($(row).closest('tr').index());
            arrStudentID.push(tblList.rows[cellrow + 1].cells[0].innerHTML)

        });
        arrStudentID.join(',');
        $("#myModalStudentData").modal('show');


        return arrStudentID;
    }
    else {
        alert('Please Select Student first.')
    }


}
function AssignSubject() {

    var arrStudentID = GetAllSID();
    var arrSubject = [];
    var arrSubGroup = [];
    var arrList = [];
    var TableItem = document.getElementById('tblStudentWiseSub');
    for (var i = 1 ; i < TableItem.rows.length; i++) {
        arrSubject.push(parseInt(TableItem.rows[i].cells[3].innerHTML)),
        arrSubGroup.push(parseInt(TableItem.rows[i].cells[1].innerHTML))
    }
    arrSubject.join(',');
    arrSubGroup.join(',');
    var le = arrStudentID.length;
    var len = arrSubject.length;
    for (var i = 0; i < arrStudentID.length; i++) {
        for (var j = 0; j < arrSubject.length; j++) {
            arrList.push({
                SWS_StudentSID: arrStudentID[i],
                SWS_SubGroupId: arrSubGroup[j],
                SWS_SubjectId: arrSubject[j]
            });
        }
    }
    var _data = JSON.stringify({
        Subject: {
            StudentWiseSubjectList: arrList,
            SWS_ClassId: parseInt($('#classid').val())
        }
    });

    $.ajax({
        type: "POST",
        url: '/JQuery/InsertUpdateStudentwiseSubject',
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.IsSuccess == true) {
                SuccessToast('Data has been updated.')
                var url = '/Masters/StudentWiseSubject'
                window.location.href = url;

            }
            else {
                ErrorToast(data.Message);
            }
        },
        error: function (data) {
            alert('Process Fail...');
        }
    });


}
function AddStudentWiseSub() {
    var SubjectGroupId = $("#ddlSubGroup").val();
    var groupPreferenceId = $("#ddlpreference").val();
    var SubjectId = $("#ddlSubject").val();

    if (SubjectId == '') {
        alert("please select subject!!");
        return false;
    }
    if (SubjectGroupId == "0") {
        alert("please select subject group!!");
        return false;
    }
    if (groupPreferenceId == "0") {
        alert("please select subject group preference!!");
        return false;
    }

    var count = 0;
    var StudentId = getParameterByName('Id');
    var TableId = document.getElementById("tblStudentWiseSubById");
    var rowcount = (TableId.rows.length - 1);
    if (rowcount != 0) {
        for (i = 1; i <= rowcount; i++) {
            var SID = TableId.rows[i].cells[1].innerHTML
            if (SID == StudentId) {
                var SubID = TableId.rows[i].cells[7].innerHTML
                if (SubID == SubjectId) {
                    alert('This Subject is already assigned for this student.....');
                    count = 0;
                    break;
                }
                else {
                    count++;
                }

            }
            else {

                $row.append($('<td  style=display:none>').append(row.ClassId));
                $row.append($('<td>').append(row.strStudentSID));
                $row.append($('<td>').append(row.SubjectGrPreferName));
                $row.append($('<td  style=display:none>').append(row.intSubjectGrPreferId));
                $row.append($('<td>').append(row.SubjectGroupName));
                $row.append($('<td  style=display:none>').append(row.intSubGroupId));
                $row.append($('<td>').append(row.SubjectName));
                $row.append($('<td  style=display:none>').append(row.intSubjectId));



                var $row = $('<tr/>');
                $row.append($('<td style=display:none/>').html(ClassID));
                $row.append($('<td/>').html(StudentId));

                $row.append($('<td/>').html($("#ddlpreference option:selected").text()));
                $row.append($('<td style=display:none/>').html($("#ddlpreference").val()));

                $row.append($('<td/>').html($("#ddlSubGroup option:selected").text()));
                $row.append($('<td style=display:none/>').html($("#ddlSubGroup").val()));

                $row.append($('<td/>').html($("#ddlSubject option:selected").text()));
                $row.append($('<td style=display:none/>').html($("#ddlSubject").val()));

                $row.append($('<td>').append("<a  class='btn btn-warning btn-xs' href='#' onclick='EditStudentWiseSub(this);'><i class=far fa-edit></i> Edit</a>"));
                $row.append($('<td>').append("<input type='image' name='imgede' src='../images/delete1.png' onclick = 'deleteRow(this);' />"));
                $("#tblStudentWiseSubById>tbody").append($row);
            }

        }
    }
    else {
        var $row = $('<tr/>');
        $row.append($('<td style=display:none/>').html(ClassID));
        $row.append($('<td/>').html(StudentId));

        $row.append($('<td/>').html($("#ddlpreference option:selected").text()));
        $row.append($('<td style=display:none/>').html($("#ddlpreference").val()));

        $row.append($('<td/>').html($("#ddlSubGroup option:selected").text()));
        $row.append($('<td style=display:none/>').html($("#ddlSubGroup").val()));

        $row.append($('<td/>').html($("#ddlSubject option:selected").text()));
        $row.append($('<td style=display:none/>').html($("#ddlSubject").val()));

        $row.append($('<td>').append("<a  class='btn btn-warning btn-xs' href='#' onclick='EditStudentWiseSub(this);'><i class=far fa-edit></i> Edit</a>"));
        $row.append($('<td>').append("<input type='image' name='imgede' src='../images/delete1.png' onclick = 'deleteRow(this);' />"));
        $("#tblStudentWiseSubById>tbody").append($row);
    }
    if (count != 0) {
        var $row = $('<tr/>');
        $row.append($('<td style=display:none/>').html(ClassID));
        $row.append($('<td/>').html(StudentId));

        $row.append($('<td/>').html($("#ddlpreference option:selected").text()));
        $row.append($('<td style=display:none/>').html($("#ddlpreference").val()));

        $row.append($('<td/>').html($("#ddlSubGroup option:selected").text()));
        $row.append($('<td style=display:none/>').html($("#ddlSubGroup").val()));

        $row.append($('<td/>').html($("#ddlSubject option:selected").text()));
        $row.append($('<td style=display:none/>').html($("#ddlSubject").val()));

        $row.append($('<td>').append("<a  class='btn btn-warning btn-xs' href='#' onclick='EditStudentWiseSub(this);'><i class=far fa-edit></i> Edit</a>"));
        $row.append($('<td>').append("<input type='image' name='imgede' src='../images/delete1.png' onclick = 'deleteRow(this);' />"));
        $("#tblStudentWiseSubById>tbody").append($row);
    }
}
$(document).ready(function () {
    $('#btnList').click(function () {
        window.location.href = '/Masters/StudentWiseSubjectList';
    });
});



