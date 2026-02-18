$(document).ready(function () {
    BindDropOutStudentList();
});
function BindDropOutStudentList() {
    $('#update-panel').html('loading data.....');
    $.ajax({
        url: '/JQuery/GetAllDropOutStudent',
        data: {
            CM_CLASSID: parseInt($('#DOP_ClassId').val()),
            SD_StudentId: $.trim($('#txtStudentId').val()),
            SD_StudentName: $.trim($('#txtStudentName').val())
        },
        dataType: 'json',
        type: 'GET',
        success: function (res) {
            if (res.Data.length > 0) {
                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                $header = "<thead><tr><th>SL</th><th>Application No.</th><th>Student Id</th><th>Student Name</th><th>Class</th><th>Mobile No</th><th>Date</th><th>Reason</th><th>Action</th></thead>";
                $data.append($header);
                $.each(res.Data, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td>').text(i + 1));
                    $row.append($('<td>').append(row.DOP_ANumber));
                    $row.append($('<td>').append(row.SD_StudentId));
                    $row.append($('<td>').append(row.SD_StudentName));
                    $row.append($('<td>').append(row.CM_CLASSNAME));
                    $row.append($('<td>').append(row.SD_ContactNo1));
                    $row.append($('<td>').append(row.DOP_Date));
                    $row.append($('<td>').append(row.DOP_Reason));

                    $row.append(
      $('<td>').append(
          '<a href="javascript:void(0);" onclick="Confirm(\'' + row.SD_StudentId + '\');" class="btn btn-danger btn-sm">Delete</a>'
      )
  );



                    $data.append($row);
                });
                $("#update-panel").html($data);
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
    if (confirm("Are you sure you want to delete this dropout record?")) {
        DeleteDropOut(SD_StudentId);
    }
}

function DeleteDropOut(SD_StudentId) {

    $.ajax({
        url: '/JQuery/DeleteDropOut',
        type: 'POST',
        data: { SD_StudentId: SD_StudentId },    // ← send as form-data, not JSON
        success: function (data) {
            if (data.IsSuccess) {
                SuccessToast(data.Message);
                BindDropOutStudentList();
            } else {
                ErrorToast(data.Message);
            }
        },
        error: function () {
            ErrorToast("Something went wrong.");
        }
    });
}
