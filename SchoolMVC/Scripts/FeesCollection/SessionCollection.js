$(document).ready(function () {
    LoadFeesSessionCollectionList();
});
function LoadFeesSessionCollectionList() {
    var _data = JSON.stringify({
        obj: {
            CM_CLASSID: $('#CM_CLASSID').val() ? parseInt($('#CM_CLASSID').val()) : null,
            admissionNo: $.trim($('#AdmissionNo').val()),
            formNo: $.trim($('#FormNo').val()),
            studentId: $.trim($('#StudentId').val()),
            studentName: $.trim($('#StudentName').val()),
            fromDate: $('#FromDate').val(),
            toDate: $('#ToDate').val()
        },
        FeesColType: "C"
    });

    $.ajax({
        url: rootDir + 'JQuery/FeesCollectionList',
        data: _data,
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (res) {
            if (res.Data.length > 0) {
                if ($.fn.DataTable.isDataTable('#tblList')) {
                    $('#tblList').DataTable().destroy();
                    $('#tblList').remove();
                }
                var $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                var $header = "<thead><tr><th style='display:none'>Id</th><th>Fees Date</th><th>Admission No</th><th>Form No</th><th>Class Name</th><th>Student Name</th><th>Student Id</th><th>Total Amount</th><th>Discount Amount</th><th>Paid Amount</th><th>Paid Mode</th><th>Due Amount</th><th></th><th></th><th></th></tr></thead>";
                $data.append($header);
                $.each(res.Data, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td style="display:none">').append(row.feesCollectionId));
                    $row.append($('<td>').append(formatJsonDate(row.feesDate)));
                    $row.append($('<td>').append(row.admissionNo));
                    $row.append($('<td>').append(row.formNo));
                    $row.append($('<td>').append(row.className));
                    $row.append($('<td>').append(row.studentName));
                    $row.append($('<td>').append(row.studentId));
                    $row.append($('<td>').append(row.totalAmount));
                    $row.append($('<td>').append(row.discount));
                    $row.append($('<td>').append(row.paidAmount));
                    $row.append($('<td>').append(row.paymodeType));
                    $row.append($('<td>').append(row.totalDue));
                    

                    $row.append($('<td>').append(
                        res.CanView ?
                            "<a target='_blank' href='" + rootDir + "FeesCollection/FeesCollection/SeesionFeesCollectionListView?FeesType=A&Id=" + parseInt(row.feesCollectionId) + "' class='btn btn-warning'>View</a>" :
                            "<a href='#' class='btn btn-warning disabled'>View</a>"
                    ));

                    $row.append($('<td>').append(
                        res.CanEdit ?
                            "<a href='" + rootDir + "FeesCollection/FeesCollection/update?FeesType=C&Id=" + parseInt(row.feesCollectionId) + "' class='btn btn-warning'>Edit</a>" :
                            "<a href='#' class='btn btn-warning disabled'>Edit</a>"
                    ));

                    $row.append($('<td>').append(
                        res.CanDelete ?
                            "<a href='#' onclick='Confirm(" + row.feesCollectionId + ");' class='btn btn-danger'>Delete</a>" :
                            "<a href='#' class='btn btn-danger disabled'>Delete</a>"
                    ));

                    $data.append($row);
                });

                $("#update-panel").html($data);
                $('#tblList').DataTable({
                    order: [[0, "desc"]],
                    responsive: true,
                    autoWidth: false,
                    pageLength: 10,
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
                window.location.href = '/FeesCollection/FeesCollection/FeesSessionCollectionList';
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

function formatJsonDate(jsonDate) {
    if (!jsonDate) return '';
    var timestamp = parseInt(jsonDate.replace(/\/Date\((\d+)\)\//, '$1'));
    var date = new Date(timestamp);
    var day = ('0' + date.getDate()).slice(-2);
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var year = date.getFullYear();
    return day + '/' + month + '/' + year; // dd/MM/yyyy
}