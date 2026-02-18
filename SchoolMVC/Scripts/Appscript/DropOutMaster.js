$(document).ready(function () {
});
function BindList() {
    $('#update-panel').html('loading data.....');
    $.ajax({
        url: '/JQuery/GetAllDStudent',
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
                $header = "<thead><tr><th>SL</th><th>Student Id</th><th>Student Name</th><th>Class</th><th>Mobile No</th><th>Select</th><th>Reason</th><th></th></thead>";
                $data.append($header);

                $.each(res.Data, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td>').text(i + 1));
                    $row.append($('<td>').append(row.SD_StudentId));
                    $row.append($('<td>').append(row.SD_StudentName));
                    $row.append($('<td>').append(row.CM_CLASSNAME));
                    $row.append($('<td>').append(row.SD_ContactNo1));
                    $row.append($('<td>').append('<input type="checkbox"  id=' + row.SD_StudentId + ' class="clickMe filled-in chk-col-green" /><label for=' + row.SD_StudentId + '>DropOut</label>'));
                    $row.append($('<td>').append('<input type="text" class="form-control w-75" id="DOP_Reason' + row.SD_StudentId + '" placeholder="Enter reason" />'));

                    $data.append($row);
                });
                $("#update-panel").html($data);
               
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
function btnSave() {
    var ob = [];
    var Student = {};
    $('.clickMe:checked').each(function () {
        $(this).attr('id')
        var Student = Object.create(null);
        Student.SD_StudentId = $(this).attr('id');
        Student.CM_CLASSID = $("#DOP_ClassId").val();
        var escapedId = CSS.escape(Student.SD_StudentId);
        Student.SECM_SECTIONID = $("#SectionId" + escapedId).val();
        Student.DOP_Reason = $("#DOP_Reason" + escapedId).val();
        ob.push(Student);
    });
    $.ajax({
        type: 'POST',
        url: "/JQuery/CancelDStudents",
        data: JSON.stringify(ob),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            if (data = "Students are DropOut successfully") {
                window.location.href = '/StudentManagement/DropOutList';
            }
        }
    });
}

