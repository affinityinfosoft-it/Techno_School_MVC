$(document).ready(function () {
    BindTCStudentList();
});
function BindTCStudentList() {
    $('#update-panel').html('loading data.....');
    $.ajax({
        url: '/JQuery/GetAllTCStudent',
        data: { CM_CLASSID: parseInt($('#TC_ClassId').val()), SD_StudentId: $.trim($('#txtStudentId').val()), SD_StudentName: $.trim($('#txtStudentName').val()) },
        dataType: 'json',
        type: 'GET',
        success: function (res) {
            if (res.Data.length > 0) {
                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                $header = "<thead><tr><th>SL</th><th>Application No.</th><th>Student Id</th><th>Student Name</th><th>Class</th><th>Mobile No</th><th>TC Date</th><th>TC Fees</th><th>Action</th><th>Print</th></thead>";
                $data.append($header);
                $.each(res.Data, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td>').text(i + 1));
                    $row.append($('<td>').append(row.TC_ANumber));
                    $row.append($('<td>').append(row.SD_StudentId));
                    $row.append($('<td>').append(row.SD_StudentName));
                    $row.append($('<td>').append(row.CM_CLASSNAME));
                    $row.append($('<td>').append(row.SD_ContactNo1));
                    $row.append($('<td>').append(row.SD_TCDate));
                    $row.append($('<td>').append(row.TC_Fees));
                    $row.append(
                    $('<td>').append(
                        '<a href="javascript:void(0);" onclick="Confirm(\'' + row.SD_StudentId + '\');" class="btn btn-danger btn-sm">Delete</a>'
                    )
                );
                                    // Add Print Button
                    let printBtn = $('<button>')
                        .addClass('btn btn-primary btn-sm')
                        .html('<i class="fa fa-print"></i> Print')
                        .attr('title', 'Print TC Certificate')
                        .on('click', function () {
                            PrintTCCertificate(row.SD_StudentId);
                        });

                    $row.append($('<td>').append(printBtn));
                    //
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
}


function Confirm(SD_StudentId) {
    if (confirm("Are you sure you want to delete this TC record?")) {
        DeleteTC(SD_StudentId);
    }
}

function DeleteTC(SD_StudentId) {

    $.ajax({
        url: '/JQuery/DeleteTC',
        type: 'POST',
        data: { SD_StudentId: SD_StudentId },    // ← send as form-data, not JSON
        success: function (data) {
            if (data.IsSuccess) {
                SuccessToast(data.Message);
                BindTCStudentList();
            } else {
                ErrorToast(data.Message);
            }
        },
        error: function () {
            ErrorToast("Something went wrong.");
        }
    });
}


function PrintTCCertificate(studentId) {
    if (!studentId) {
        alert("Student ID is missing!");
        return;
    }

    var url = '/StudentManagement/TCCertificate?id=' + encodeURIComponent(studentId);

    window.open(url, '_blank');
}
