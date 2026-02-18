
$(document).ready(function () {
    ExamGroupList();
});

function InsertUpdateExamGroup() {
    if (!ValidateOperation()) return;

    var rowsData = [];

    $('.exam-table tbody tr').each(function () {
        var $row = $(this);
        var termId = $row.data("termid") || null;
        var classIds = $row.find('select').val() || []; 
        var selectExam = $row.find('input[id^=selectExam_]').is(':checked');
        var finalExam = $row.find('input[id^=finalExam_]').is(':checked');

        if (selectExam) {
            if (classIds.length === 0) {
                rowsData.push({
                    TermId: termId,
                    ClassId: null,
                    SelectExam: true,
                    IsFinal: finalExam
                });
            } else {
                for (var j = 0; j < classIds.length; j++) {
                    var clsId = classIds[j];
                    rowsData.push({
                        TermId: termId,
                        ClassId: clsId ? parseInt(clsId) : null,
                        SelectExam: true,
                        IsFinal: finalExam
                    });
                }
            }
        }
    });

    if (rowsData.length === 0) {
        ErrorToast('Please check at least one "Select Exam" checkbox and choose class(es).');
        return;
    }

    var payload = {
        EGM_Id: parseInt($('#EGM_Id').val() || 0),
        EGM_Name: $('#EGM_Name').val(),
        EGM_SchoolId: parseInt($('#EGM_SchoolId').val() || 0),
        EGM_SessionId: parseInt($('#EGM_SessionId').val() || 0),
        Userid: parseInt($('#hdnUserid').val() || 0),
        ExamGroupDetails: rowsData
    };

    $.ajax({
        url: '/JQuery/InsertUpdateExamGroup',
        data: JSON.stringify(payload),
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data && data.IsSuccess) {
                SuccessToast(data.Message || 'Saved successfully.');
                $('#btnSave').attr('disabled', 'disabled');
                setTimeout(function () {
                    window.location.href = '/Masters/ExamGroupList';
                }, 1200);
            } else {
                ErrorToast(data.Message || 'Something went wrong.');
            }
        },
        error: function (xhr) {
            var msg = 'Something went wrong.';
            if (xhr && xhr.responseJSON && xhr.responseJSON.Message) msg = xhr.responseJSON.Message;
            ErrorToast(msg);
        }
    });
}

function ExamGroupList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        $.ajax({
            url: '/JQuery/ExamGroupList',  // fixed case
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    var $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');

                    var $header = "<thead><tr>" +
                                  "<th>SLNo</th>" +
                                  "<th>Exam Group</th>" +
                                  "<th>Class</th>" +
                                  "<th></th>" +
                                  "<th></th>" +
                                  "</tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');

                        $row.append($('<td>').text(i + 1));
                        $row.append($('<td>').text(row.EGM_Name));
                        $row.append($('<td>').text(row.CLASSNAME));

                        // Edit button
                        if (res.CanEdit === true) {
                            $row.append($('<td>').append("<a href='/Masters/ExamGroup/" + row.EGM_Id + "' class='btn btn-warning'>Edit</a>"));
                        } else {
                            $row.append($('<td>').append("<a href='#' class='btn btn-warning disabled'>Edit</a>"));
                        }

                        // Delete button
                        if (res.CanDelete === true) {
                            $row.append(
                                $('<td>').append(
                                    "<a href='javascript:void(0);' onclick='ConfirmExamGroup(" + row.EGM_Id + ");' class='btn btn-danger'>Delete</a>"
                                )
                            );
                        } else {
                            $row.append($('<td>').append("<a href='#' class='btn btn-danger disabled'>Delete</a>"));
                        }

                        $data.append($row);
                    });

                    $("#update-panel").html($data);

                    $('#tblList').DataTable({
                        "order": [[0, "asc"]]
                    });
                } else {
                    $("#update-panel").html("<div>No data Found</div>");
                }
            },
            failure: function () {
                ErrorToast('Something wrong happened');
            }
        });
    }, 1000);
}
function ConfirmExamGroup(id) {
    var agree = confirm("Are you sure you want to delete this Exam Group?");
    if (agree) {
        DeleteExamGroup(id);
    }
}
function DeleteExamGroup(groupId) {
    $.ajax({
        url: '/JQuery/DeleteExamGroup',   // fixed case
        type: 'POST',
        data: JSON.stringify({ id: groupId }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            if (data && data.IsSuccess) {
                SuccessToast(data.Message);
                setTimeout(function () {
                    window.location.href = '/Masters/ExamGroupList';
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
function ValidateOperation() {

    if ($('#EGM_ClassId').val() == "") {
        WarningToast('Select any class');
        return false;
    }
    if ($('#EGM_Name').val() == "") {
        WarningToast('Enter Exam Group Name');
        return false;
    }

    return true;
}

