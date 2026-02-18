

function InsertUpdateSection() {
    if (!FinalValidation()) return;

    var ArrayItem = [];
    $("#tblList > tbody > tr").each(function () {
        ArrayItem.push({
            SECM_CM_CLASSID: $(this).find("td:eq(0)").text().trim(),
            SECM_SECTIONNAME: $(this).find("td:eq(2)").text().trim()
        });
    });

    var _data = JSON.stringify({
        Userid: $('#hdnUserid').val(),
        SectionList: ArrayItem,
        SECM_CM_CLASSID: $('#CM_CLASSID').val()
    });

    $.ajax({
        url: '/JQuery/InsertUpdateSection',
        type: 'POST',
        data: _data,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data && data.IsSuccess) {
                SuccessToast(data.Message);
                $('#btnSave').prop('disabled', true);
                setTimeout(function () {
                    window.location.href = '/Masters/SectionList';
                }, 2000);
            } else {
                ErrorToast(data.Message);
            }
        },
        error: function (xhr) {
            var message = 'Something wrong happened.';
            // Try to read json { Message: "..." } returned by server (if any)
            try {
                if (xhr.responseJSON && xhr.responseJSON.Message) {
                    message = xhr.responseJSON.Message;
                } else if (xhr.responseText) {
                    // fallback: show server text (could be RAISERROR text/html)
                    message = xhr.responseText;
                }
            } catch (e) {
                // ignore parsing errors
            }
            ErrorToast(message);
        }
    });
}

function AddDetails() {
    if (ValidateOperation() == true) {
        var selectedClassId = $("#SECM_CM_CLASSID option:selected").val();
        var selectedSectionName = $("#SECM_SECTIONNAME").val().trim().toLowerCase();

        var isDuplicate = false;
        $("#tblList > tbody > tr").each(function () {
            var classId = $(this).find("td:eq(0)").text().trim();
            var sectionName = $(this).find("td:eq(2)").text().trim().toLowerCase();

            if (classId === selectedClassId && sectionName === selectedSectionName) {
                isDuplicate = true;
                return false;
            }
        });

        if (isDuplicate) {
            WarningToast("This Class and Section combination already exists in the list.");
            return;
        }

        if ($("#tblList > tbody:eq(0) tr:eq(0) td:eq(2)").text() == '') {
            $("#tblList").find('tbody').empty();
        }

        var $row = $('<tr/>');
        $row.append($('<td style=display:none>').html(selectedClassId));
        $row.append($('<td/>').html($("#SECM_CM_CLASSID option:selected").text()));
        $row.append($('<td>').html($("#SECM_SECTIONNAME").val()));
        $row.append($('<td>').append("<input type='image' name='imgede'  src='/Content/images/delete.png' onclick='deleteRow(this);' alt='button'>"));

        $("#tblList>tbody").append($row);
        ClearDetails();
    }
}
function SectionList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        $.ajax({
            url: '/JQuery/SectionList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>Class</th><th>Section</th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td>').text(i + 1));
                        $row.append($('<td style=display:none>').append(row.SECM_SECTIONID));
                        $row.append($('<td>').append(row.SECM_CM_CLASSNAME));
                        $row.append($('<td>').append(row.SECM_SECTIONNAME));
                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/Section/" + row.SECM_CM_CLASSID + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.SECM_SECTIONID + ");'" + row.SECM_SECTIONID + " class='btn btn-danger'>Delete</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-danger disabled'>Delete</a>"));
                        }
                        $data.append($row);
                    });
                    $("#update-panel").html($data);
                    //$("#tblList").DataTable();
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

function ValidateOperation() {
    if ($('#SECM_CM_CLASSID').val() == '') {
        WarningToast('Please select a class.');
        return false;
    }
    if ($('#SECM_SECTIONNAME').val() == "") {
        WarningToast('Please provide a section name.');
        return false;
    }

    return true;
}
$(document).ready(function () {
    SectionList();
});
function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'SectionMaster_SECM',
        MainFieldName: 'SECM_SECTIONID',
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
                    window.location.href = '/Masters/SectionList';
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


function ClearDetails() {

    $("#SECM_CM_CLASSID").val('');
    $("#SECM_CM_CLASSID").selectpicker('refresh');
    $("#SECM_SECTIONNAME").val('');

}
function FinalValidation() {
    if ($("#tblList > tbody > tr").length == 0) {
        ErrorToast("Please add any Details!")
        return false;
    }

    return true;
}
function deleteRow(rowNo) {
    var agree = confirm("Are you sure you want to delete this record?");
    if (agree) {
        $(rowNo).closest('tr').remove();
        return true;
    } else {
        return false;
    }
    //swal({
    //    title: "Are you sure?",
    //    text: "You will not be able to recover this imaginary file!",
    //    type: "warning",
    //    showCancelButton: true,
    //    confirmButtonColor: "#DD6B55",
    //    confirmButtonText: "Yes, delete it!",
    //    cancelButtonText: "No, cancel plx!",
    //    closeOnConfirm: true,
    //    closeOnCancel: true
    //}, function (isConfirm) {
    //    if (isConfirm) {
    //        $(rowNo).closest('tr').remove();

    //    }
    //});

}



$(document).on("change", "#SECM_CM_CLASSID", function () {
    var classId = $(this).val();
    if (classId) {
        $.ajax({
            url: '/Masters/GetSectionsByClassId',
            type: 'GET',
            data: { classId: classId },
            dataType: 'json',
            success: function (res) {
                if (res.IsSuccess) {
                    var tbody = $("#modalSectionTable tbody");
                    tbody.empty();

                    if (res.Data && res.Data.length > 0) {
                        $.each(res.Data, function (i, section) {
                            tbody.append("<tr><td>" + section.SECM_SECTIONNAME + "</td></tr>");
                        });
                    } else {
                        tbody.append("<tr><td>No sections found for this class.</td></tr>");
                    }

                    $("#sectionModal").modal("show");
                } else {
                    ErrorToast(res.Message || "Failed to load sections.");
                }
            },
            error: function () {
                ErrorToast("Something went wrong while loading sections.");
            }
        });
    }
});