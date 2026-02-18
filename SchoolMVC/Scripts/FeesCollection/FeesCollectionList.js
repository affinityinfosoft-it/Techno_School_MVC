$(document).ready(function () {
    LoadFeesCollectionList();
});
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
function LoadFeesCollectionList() {

    $('#update-panel').html('loading data.....');

    var studentId = $.trim($('#StudentId').val());
    var admissionNo = $.trim($('#AdmissionNo').val());
    var formNo = $.trim($('#FormNo').val());
    var fromDate = $('#FromDate').val();
    var toDate = $('#ToDate').val();

  
    if (studentId !== '' || admissionNo !== '' || formNo !== '') {
        fromDate = null;
        toDate = null;
    }

    var _data = JSON.stringify({
        obj: {
            CM_CLASSID: parseInt($('#CM_CLASSID').val()) || null,
            admissionNo: admissionNo,
            formNo: formNo,
            studentId: studentId,
            studentName: $.trim($('#StudentName').val()),
            paymodeType: $.trim($('#paymodeType').val()),
            fromDate: fromDate === '' ? null : convertToSqlDate(fromDate),
            toDate: toDate === '' ? null : convertToSqlDate(toDate)
        },
        FeesColType: "A"
    });

    $.ajax({
        url: rootDir + 'JQuery/FeesCollectionList',
        data: _data,
        dataType: 'json',
        type: 'POST',
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (res) {

            if (res.Data.length > 0) {

                var $data = $('<table id="tblList"></table>')
                    .addClass('table table-bordered table-striped');

                var $header =
                    "<thead><tr>" +
                    "<th style='display:none'>Id</th>" +
                    "<th>Fees Date</th>" +
                    "<th>Admission No</th>" +
                    "<th>Form No</th>" +
                    "<th>Student Id</th>" +
                    "<th>Student Name</th>" +
                    "<th>Class Name</th>" +
                    "<th>Total Amount</th>" +
                    "<th>Discount Amount</th>" +
                    "<th>Paid Amount</th>" +
                    "<th>Paid Mode</th>" +
                    "<th>Due Amount</th>" +
                    "<th></th><th></th><th></th>" +
                    "</tr></thead>";

                $data.append($header);

                $.each(res.Data, function (i, row) {

                    var $row = $('<tr/>');

                    $row.append($('<td style="display:none">').append(row.feesCollectionId));
                    $row.append($('<td>').append(formatJsonDate(row.feesDate)));
                    $row.append($('<td>').append(row.admissionNo));
                    $row.append($('<td>').append(row.formNo));
                    $row.append($('<td>').append(row.studentId));
                    $row.append($('<td>').append(row.studentName));
                    $row.append($('<td>').append(row.className));
                    $row.append($('<td>').append(row.totalAmount));
                    $row.append($('<td>').append(row.discount));
                    $row.append($('<td>').append(row.paidAmount));
                    $row.append($('<td>').append(row.paymodeType));
                    $row.append($('<td>').append(row.totalDue));

                    if (res.CanView) {
                        $row.append($('<td>').append(
                            "<a target='_blank' href='" + rootDir +
                            "FeesCollection/FeesCollection/FeesCollectionListView?FeesType=A&Id=" +
                            row.feesCollectionId +
                            "' class='btn btn-warning'>View</a>"
                        ));
                    } else {
                        $row.append($('<td>').append("<a class='btn btn-warning disabled'>View</a>"));
                    }

                    if (res.CanEdit) {
                        $row.append($('<td>').append(
                            "<a href='" + rootDir +
                            "FeesCollection/FeesCollection/update?FeesType=A&Id=" +
                            row.feesCollectionId +
                            "' class='btn btn-warning'>Edit</a>"
                        ));
                    } else {
                        $row.append($('<td>').append("<a class='btn btn-warning disabled'>Edit</a>"));
                    }

                    if (res.CanDelete) {
                        $row.append($('<td>').append(
                            "<a onclick='Confirm(" + row.feesCollectionId +
                            ");' class='btn btn-danger'>Delete</a>"
                        ));
                    } else {
                        $row.append($('<td>').append("<a class='btn btn-danger disabled'>Delete</a>"));
                    }

                    $data.append($row);
                });

                $("#update-panel").html($data);

                $('#tblList').DataTable({
                    order: [[1, "desc"]],
                    responsive: true,
                    autoWidth: false,
                    pageLength: 10
                });

            } else {
                $("#update-panel").html("<div>No data Found</div>");
            }
        },
        failure: function () {
            alert('something wrong happen');
        }
    });
}


function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        FEESID: fieldId
    });
    $.ajax({
        url: '/JQuery/DeleteFeesCollection',
        type: 'POST',
        data: _data,
        contentType: 'application/json ; utf-8',
        dataType: 'json',
        success: function (data) {
            if (data != null && data != undefined && data.IsSuccess == true) {
                alert(data.Message);
                window.location.href = '/FeesCollection/FeesCollection/FeesCollectionList';
            }
            else {
                alert(data.Message);
            }
        },
        error: function () {
            alert('Something wrong happend.');
        }

    });
}

function formatDotNetDate(dateValue) {
    if (!dateValue) return '';

    var timestamp = parseInt(dateValue.replace(/[^0-9]/g, ''));
    var date = new Date(timestamp);

    var day = ('0' + date.getDate()).slice(-2);
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var year = date.getFullYear();

    return day + '/' + month + '/' + year; // dd/MM/yyyy
}