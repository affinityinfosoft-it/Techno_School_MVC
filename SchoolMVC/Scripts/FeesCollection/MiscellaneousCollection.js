
var isValidStudentId = false; 

$(document).ready(function () {

    $('#MTM_FeesHeadId').change(function () {
        var selectedId = $(this).val();

        if (selectedId && selectedId !== "Select Fees Head") {
            $.ajax({
                url: rootDir + 'JQuery/GetFeesHeadAmount',
                type: 'GET',
                data: { feesHeadId: selectedId },
                success: function (response) {
                    if (response.success) {
                        $('#MTM_Amount').val(response.amount);
                    } else {
                        $('#MTM_Amount').val('');
                        alert(response.message || 'Unable to fetch amount.');
                    }
                },
                error: function () {
                    $('#MTM_Amount').val('');
                    alert('Error while fetching Fees Head amount.');
                }
            });
        } else {
            $('#MTM_Amount').val('');
        }
    });

});
$(document).on("click", '#radio_Cash', function (e) {
    paydetailsVerify();
    var rdCash = $("#radio_Cash").is(":checked");
    if (rdCash) {

        $("#ChkId").val("Cash");
    }


});
$(document).on("click", '#radio_Cheque', function (e) {
    paydetailsVerify();
    var rdChq = $("#radio_Cheque").is(":checked");
    if (rdChq) {

        $("#ChkId").val("Cheque");
    }


});
$(document).on("click", '#radio_DD', function (e) {
    paydetailsVerify();
    var rdDD = $("#radio_DD").is(":checked");
    if (rdDD) {

        $("#ChkId").val("DD");
    }


});
$(document).on("click", '#radio_Card', function (e) {
    paydetailsVerify();
    var rdDD = $("#radio_DD").is(":checked");
    if (rdDD) {

        $("#ChkId").val("Card");
    }

});
function paydetailsVerify() {

    var mode = $("input[name='MFD_Paymentmode']:checked").val();
    $("#ChkId").val(mode);

    if (mode === "Cheque" || mode === "DD") {

        $("#Div_PayDetails").show();
        $("#Div_ChqDate").show();
        $("#Div_TrnsRefNo").hide();

        $("#MFD_CheqDDDate").prop("disabled", false);
        $("#MFD_Card_TrnsRefNo").prop("disabled", true);

    }
    else if (mode === "Card" || mode === "Online") {

        $("#Div_PayDetails").show();
        $("#Div_ChqDate").hide();
        $("#Div_TrnsRefNo").show();

        $("#MFD_CheqDDDate").prop("disabled", true);
        $("#MFD_Card_TrnsRefNo").prop("disabled", false);

    }
}

function CheckRadioForEdit(paymode) {
    if (paymode == 'Cash') {
        $("#radio_Cash").prop("checked", true);
    }
    if (paymode == 'Card') {
        $("#radio_Card").prop("checked", true);
        paydetailsVerify();
    }
    if (paymode == 'DD') {
        $("#radio_DD").prop("checked", true);
        paydetailsVerify();
    }
    if (paymode == 'Cheque') {
        $("#radio_Cheque").prop("checked", true);
        paydetailsVerify();
    }

}
function InsertUpdateMiscCollection() {
    if (!isValidStudentId) {
        alert("Invalid Student ID. Please enter a valid one before saving.");
        return; // stop the function
    }
    var model = {
        MTM_Id: $('#MTM_Id').val(),
        MTM_StudentId: $('#MTM_StudentId').val(),
        MTM_FeesHeadId: $('#MTM_FeesHeadId').val(),
        MTM_Amount: $('#MTM_Amount').val(),
        MTM_Narration: $('#MTM_Narration').val(),
        MFD_Paymentmode: $('input[name="MFD_Paymentmode"]:checked').val(),
        MFD_BankId: $('#MFD_BankId').val(),
        MFD_BranchName: $('#MFD_BranchName').val(),
        MFD_CheqDDNo: $('#MFD_CheqDDNo').val(),
        MFD_CheqDDDate: $('#MFD_CheqDDDate').val(),
        MFD_Card_TrnsRefNo: $('#MFD_Card_TrnsRefNo').val()
    };

    $.ajax({
        type: "POST",
        url: rootDir + 'JQuery/InsertUpdateMiscCollection',
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $('#btnSave').prop('disabled', true);
        },
        success: function (response) {
            if (response.IsSuccess) {
                alert(response.Message);

                if (response.TransactionId) {
                    //  Open the receipt page in a new tab
                    var printUrl = "/FeesCollection/FeesCollection/MiscFeesCollectionListView?MTM__TransId=" + encodeURIComponent(response.TransactionId);
                    window.open(printUrl, '_blank'); // <-- opens in new tab

                    // Optionally, redirect main page to list view
                    window.location.href = "/FeesCollection/FeesCollection/MiscCollectionList";
                } else {
                    window.location.href = "/FeesCollection/FeesCollection/MiscCollectionList";
                }
            } else {
                alert(response.Message);
            }
        },


        error: function (xhr) {
            alert("Error while saving: " + xhr.responseText);
        },
        complete: function () {
            $('#btnSave').prop('disabled', false);
        }
    });
}
function bindMiscellaneousFeesCollectionList() {

    $('#update-panel').html('loading data.....');

    var _data = JSON.stringify({
        obj: {
            MTM_StudentId: $.trim($('#MTM_StudentId').val()),
            SD_CurrentClassId: $('#CM_CLASSID').val(),
            FromDate: $('#FromDate').val(),
            ToDate: $('#ToDate').val()
        }
    });

    $.ajax({
        url: rootDir + "JQuery/MiscFeesCollectionList",
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        data: _data,
        success: function (d) {

            if (d && d.length > 0) {

                if ($.fn.DataTable.isDataTable('#tblList')) {
                    $('#tblList').DataTable().clear().destroy();
                }

                $("#update-panel").empty();

                var $data = $('<table id="tblList" class="table table-bordered table-striped"></table>');

                var $header =
                    "<thead>" +
                    "<tr>" +
                    "<th>ID</th>" +
                    "<th>Date</th>" +
                    "<th>TransId</th>" +
                    "<th>StudentId</th>" +
                    "<th>Student Name</th>" +
                    "<th>Class</th>" +
                    "<th>Narration</th>" +
                    "<th>Paid Amount</th>" +
                    "<th>Payment Mode</th>" +
                    "<th>Print</th>" +
                    "<th>Edit</th>" +
                     "<th>Delete</th>" +
                    "</tr>" +
                    "</thead>";

                $data.append($header);

                var $tbody = $('<tbody></tbody>');

                $.each(d, function (i, row) {

                    var $row = $('<tr></tr>');

                    $row.append($('<td></td>').text(row.MTM_Id));

                    if (row.MFD__FeesCollectionDate) {
                        var ticks = row.MFD__FeesCollectionDate.replace(/\/Date\((\d+)\)\//, "$1");
                        var date = new Date(parseInt(ticks));
                        var formattedDate =
                            ("0" + date.getDate()).slice(-2) + "/" +
                            ("0" + (date.getMonth() + 1)).slice(-2) + "/" +
                            date.getFullYear();
                        $row.append($('<td></td>').text(formattedDate));
                    } else {
                        $row.append($('<td></td>').text(''));
                    }

                    $row.append($('<td></td>').text(row.MTM__TransId));
                    $row.append($('<td></td>').text(row.MTM_StudentId));
                    $row.append($('<td></td>').text(row.SD_StudentName));
                    $row.append($('<td></td>').text(row.MTM_ClassName));
                    $row.append($('<td></td>').text(row.MTM_Narration));
                    $row.append($('<td></td>').text(row.MFD_PaidAmount));
                    $row.append($('<td></td>').text(row.MFD_Paymentmode));

                    $row.append($('<td></td>').html(
                        "<a href='/FeesCollection/FeesCollection/MiscFeesCollectionListView?MTM__TransId=" +
                        encodeURIComponent(row.MTM__TransId) +
                        "' target='_blank' class='btn btn-success btn-sm'>Print</a>"
                    ));

                    $row.append($('<td></td>').html(
                        "<a href='/FeesCollection/FeesCollection/MiscCollection?id=" +
                        row.MTM_Id +
                        "' class='btn btn-warning btn-sm'>Edit</a>"
                    ));

                    $row.append(
                        $('<td>').append(
                            "<a href='javascript:void(0);' " +
                            "onclick=\"ConfirmDelete('" + row.MTM__TransId + "')\" " +
                            "class='btn btn-danger'>Delete</a>"
                        )
                    );
                    $tbody.append($row);
                });

                $data.append($tbody);
                $("#update-panel").append($data);

                $('#tblList').DataTable({
                    columnDefs: [{ targets: 0, visible: false }],
                    order: [[0, "desc"]]
                });

            } else {
                $("#update-panel").html("<div>No data Found</div>");
            }
        },
        error: function () {
            ErrorToast('Something went wrong');
        }
    });
}

$(document).ready(function () {
    $('#MTM_StudentId').on('blur', function () {
        var studentId = $(this).val().trim();
        var $display = $('#studentNameDisplay');
        isValidStudentId = false; // reset every time

        if (studentId !== "") {
            $display
                .text("Checking...")
                .removeClass("text-danger")
                .addClass("text-success")
                .show();

            $.ajax({
                url: rootDir + 'JQuery/CheckStudentId',
                type: 'POST',
                data: { studentId: studentId },
                success: function (response) {
                    if (response.success) {
                        isValidStudentId = true; // ✅ Mark valid
                        $display
                            .text("Student: " + response.studentName)
                            .removeClass("text-danger")
                            .addClass("text-success")
                            .fadeIn();
                    } else {
                        isValidStudentId = false; // ❌ Invalid
                        $display
                            .text(response.message)
                            .removeClass("text-success")
                            .addClass("text-danger")
                            .fadeIn();
                    }

                    // Auto-hide after 3 seconds
                    setTimeout(function () {
                        $display.fadeOut('slow');
                    }, 3000);
                },
                error: function () {
                    isValidStudentId = false; // ❌ Mark invalid
                    $display
                        .text("Error checking Student ID.")
                        .removeClass("text-success")
                        .addClass("text-danger")
                        .fadeIn();

                    setTimeout(function () {
                        $display.fadeOut('slow');
                    }, 3000);
                }
            });
        } else {
            $display.hide();
        }
    });
});
$(document).on("change", "input[name='MFD_Paymentmode']", function () {
    paydetailsVerify();
});
$(document).ready(function () {
    var mode = $("input[name='MFD_Paymentmode']:checked").val();

    if (mode) {
        paydetailsVerify(); // EDIT MODE
    } else {
        // ADD MODE ONLY
        $("#radio_Cheque").prop("checked", true);
        paydetailsVerify();
    }
});

function ConfirmDelete(id) {
    if (confirm("Are you sure?")) {
        DeleteMiscCollection(id);
    }
}

function DeleteMiscCollection(id) {
    $.ajax({
        url: '/JQuery/DeleteMiscCollection',
        type: 'POST',
        data: { id: id },
        success: function (res) {
            if (res.IsSuccess) {
                SuccessToast(res.Message);
                setTimeout(function () {
                    location.reload();
                }, 1500);
            } else {
                ErrorToast(res.Message);
            }
        },
        error: function () {
            ErrorToast('Something went wrong.');
        }
    });
}