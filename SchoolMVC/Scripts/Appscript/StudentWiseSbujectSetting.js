$(document).ready(function () {
    $("#ddlSWS_SubGroupId").change(function () {
        AjaxPostForDropDownSubject();
    });

    $("#btnAdd").click(function () {
        AddSubject();
    });

    $("#btnSubmit").click(function () {
        InsertStudentwiseSubject();
    });

    if ($("#ddlSWS_SubGroupId").val() != null && $("#ddlSWS_SubGroupId").val() != "0") {
        AjaxPostForDropDownSubject();
    }
});
function AjaxPostForDropDownSubject() {
    var selectedSubjectId = $("#ddlSWS_SubjectId").attr("data-selected-id");
    var _data = JSON.stringify({
        CSGWS_SubGr_Id: parseInt($("#ddlSWS_SubGroupId").val()),
        CSGWS_Class_Id: parseInt($('#SWS_ClassId').val())  // this ID is correct, it's the hidden input
    });

    $.ajax({
        type: "POST",
        data: _data,
        url: rootDir + "JQuery/GetGroupWiseSubject",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data, status) {
            if (status == "success") {
                BindDropDownListForSubject($('#ddlSWS_SubjectId'), data);

                if (selectedSubjectId) {
                    $('#ddlSWS_SubjectId').val(selectedSubjectId);
                }

                $("#ddlSWS_SubjectId").selectpicker('refresh');
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
function bindGridForEdit() {
    $('#update-panel').html('loading data.....');

    if (validateForm()) {
        $("#tblStudentdata").show();

        var ClassId = $('#SWS_ClassId').val() == 0 ? '' : $('#SWS_ClassId option:selected').val();

        $.ajax({
            url: rootDir + "JQuery/StudentWiseSubjectListForEdit",
            type: 'POST',
            dataType: 'json',
            data: { SWS_ClassId: parseInt(ClassId) },
            success: function (d) {
                if (d.length > 0) {
                    var $table = $('<table id="tblStudentdata"></table>').addClass('table table-bordered table-striped');
                    var $header = "<thead><tr><th>SL</th><th>Student Id</th><th>Subjects</th><th>Edit</th><th>Delete</th></tr></thead>";
                    $table.append($header);

                    $.each(d, function (i, row) {
                        $('#classid').val(row.SD_CurrentClassId);

                        var $row = $('<tr/>');
                        $row.append($('<td>').text(i + 1));
                        console.log("SWS_StudentSID:", row.SWS_StudentSID);

                        $row.append($('<td>').text(row.SWS_StudentSID));
                        $row.append($('<td>').text(row.SWS_SubjectCode));

                        $row.append($('<td>').html(
                            "<a href='/Masters/StudentWiseSubjectSetting/" + row.SWS_StudentSID + "' class='btn btn-warning'>Edit</a>"
                        ));
                        $row.append($('<td>').append(
     "<a href='#' onclick=\"DeleteRecord('" + row.SWS_StudentSID + "')\" class='btn btn-danger'>Delete</a>"
 ));
                        $table.append($row);
                    });

                    $("#update-panel").html($table);
                    $("#tblStudentdata").DataTable();
                } else {
                    $("#update-panel").html("<div>No data Found</div>");
                }
            },
            error: function () {
                ErrorToast('Something went wrong while loading data.');
            }
        });
    }
}
//function InsertStudentwiseSubject() {
//    var StudentSubList = [];
//    var TableItem = document.getElementById('tblList');

//    for (var i = 1; i < TableItem.rows.length; i++) {
//        StudentSubList.push({
//            SWS_StudentSID: TableItem.rows[i].cells[0].innerHTML,
//            SWS_SubGroupId: parseInt(TableItem.rows[i].cells[1].innerHTML),
//            SWS_SubjectId: parseInt(TableItem.rows[i].cells[3].innerHTML)
//        });
//    }

//    var _data = JSON.stringify({
//        subject: {
//            StudentWiseSubjectList: StudentSubList,
//            SWS_ClassId: parseInt($('#SWS_ClassId').val())
//        }
//    });

//    $.ajax({
//        type: "POST",
//        url: rootDir + "JQuery/InsertUpdateStudentwiseSubject",
//        data: _data,
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        async: false,
//        success: function (data) {
//            if (data && data.IsSuccess === true) {
//                swal({
//                    title: "Success!",
//                    text: data.Message,
//                    type: "success",
//                    confirmButtonText: "OK"
//                }, function () {
//                    // Redirect after success
//                    window.location.href = '/Masters/StudentWiseSubjectList';
//                });
//            } else {
//                swal("Error", data.Message || "Something went wrong.", "error");
//            }
//        },
//        error: function () {
//            swal("Error", "Process failed due to server error.", "error");
//        }
//    });
//}
function InsertStudentwiseSubject() {

    var StudentSubList = [];
    var TableItem = document.getElementById('tblList');

    for (var i = 1; i < TableItem.rows.length; i++) {

        StudentSubList.push({
            SWS_StudentSID: TableItem.rows[i].cells[0].innerHTML.trim(),
            SWS_SubGroupId: parseInt(TableItem.rows[i].cells[1].innerHTML),
            SWS_SubjectId: parseInt(TableItem.rows[i].cells[3].innerHTML)
        });
    }

    var _data = JSON.stringify({
        subject: {
            StudentWiseSubjectList: StudentSubList,
            SWS_ClassId: parseInt($('#SWS_ClassId').val())
        }
    });

    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/InsertUpdateStudentwiseSubject",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            if (data.IsSuccess === true) {

                swal({
                    title: "Success!",
                    text: data.Message,
                    type: "success"
                }, function () {
                    window.location.href = '/Masters/StudentWiseSubjectList';
                });

            } else {

                swal("Error", data.Message, "error");
            }
        },
        error: function () {
            swal("Error", "Server error occurred.", "error");
        }
    });
}


function DeleteRecord(studentSid) {
    console.log("DeleteRecord called with Id:", studentSid);

    if (!studentSid) {
        alert("Invalid Student SID");
        return;
    }

    var _data = JSON.stringify({ id: studentSid }); // treat as string

    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/DeleteStudentWiseSubject",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.IsSuccess) {
                alert(data.Message);
                window.location.href = '/Masters/StudentWiseSubjectList';
            } else {
                alert("Error: " + data.Message);
            }
        },
        error: function () {
            alert("AJAX call failed.");
        }
    });
}

function SuccessToast(msg) {
    alert(msg); // Replace with your custom toast logic if needed
}
function ErrorToast(msg) {
    alert(msg);
}
function validateForm() {
    if ($('#SWS_ClassId').val() == 0) {
        ErrorToast('Please Select Class.');
        return false;
    }

    return true;
}
function AddSubject() {
    if ($("#ddlSWS_SubjectId").val() != 0 && $("#ddlSWS_SubGroupId").val() != 0) {
        var count = 0;
        var TableId = document.getElementById("tblList");

        var SubjectId = $("#ddlSWS_SubjectId").val();
        var SubjectGroupId = $("#ddlSWS_SubGroupId").val();
        var rowcount = (TableId.rows.length - 1);

        if (rowcount != 0) {
            for (var i = 1; i <= rowcount; i++) {
                var Groupid = TableId.rows[i].cells[1].innerHTML;
                if (SubjectGroupId == Groupid) {
                    var SubID = TableId.rows[i].cells[3].innerHTML;
                    if (SubID == SubjectId) {
                        alert('This Subject is already assigned for this student.');
                        count = 0;
                        break;
                    } else {
                        count++;
                    }
                } else {
                    count++;
                }
            }
        } else {
            var $row = $('<tr/>');
            $row.append($('<td/>').html($("#SWS_StudentSID").val()));
            $row.append($('<td style="display:none"/>').html($("#ddlSWS_SubGroupId option:selected").val()));
            $row.append($('<td/>').html($("#ddlSWS_SubGroupId option:selected").text()));
            $row.append($('<td style="display:none"/>').html($("#ddlSWS_SubjectId option:selected").val()));
            $row.append($('<td/>').html($("#ddlSWS_SubjectId option:selected").text()));
            $row.append($('<td/>').append("<input type='image' name='imgede' src='/Content/images/close_icon.png' onclick='deleteRow(this);' />"));
            $("#tblList > tbody").append($row);
        }

        if (count != 0) {
            var $row = $('<tr/>');
            $row.append($('<td/>').html($("#SWS_StudentSID").val()));
            $row.append($('<td style="display:none"/>').html($("#ddlSWS_SubGroupId option:selected").val()));
            $row.append($('<td/>').html($("#ddlSWS_SubGroupId option:selected").text()));
            $row.append($('<td style="display:none"/>').html($("#ddlSWS_SubjectId option:selected").val()));
            $row.append($('<td/>').html($("#ddlSWS_SubjectId option:selected").text()));
            $row.append($('<td/>').append("<input type='image' name='imgede' src='/Content/images/close_icon.png' onclick='deleteRow(this);' />"));
            $("#tblList > tbody").append($row);
        }
    }
}
function deleteRowWithoutAlert(rowNo) {
    $(rowNo).closest('tr').remove();

}
function deleteRow(rowNo) {
    swal({
        title: "Are you sure?",
        text: "This row will be removed from the table.",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel!",
        closeOnConfirm: false,
        closeOnCancel: true
    }, function (isConfirm) {
        if (isConfirm) {
            $(rowNo).closest('tr').remove();

            swal("Deleted!", "Row has been removed.", "success");
        }
    });
}
