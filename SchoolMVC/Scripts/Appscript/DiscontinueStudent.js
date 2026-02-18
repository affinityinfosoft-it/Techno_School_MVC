function BindList() {
    $('#update-panel').html('loading data.....');
    $.ajax({
        url: '/JQuery/GetDisStudentList',
        data: { SD_ClassId: parseInt($('#ClassId').val()), SD_StudentId: $.trim($('#txtStudentId').val()), SD_StudentName: $.trim($('#txtStudentName').val()) },
        dataType: 'json',
        type: 'GET',
        success: function (res) {
            if (res.Data.length > 0) {
                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                $header = "<thead><tr><th>Student Id</th><th>Student Name</th><th>Class</th><th>Contact No 1</th><th>Contact No 2</th><th>Select</th></thead>";
                $data.append($header);

                $.each(res.Data, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td>').append(row.SD_StudentId));
                    $row.append($('<td>').append(row.SD_StudentName));
                    $row.append($('<td>').append(row.SD_CM_CLASSNAME));
                    $row.append($('<td>').append(row.SD_ContactNo1));
                    $row.append($('<td>').append(row.SD_ContactNo2));
                    $row.append($('<td>').append('<input type="checkbox"  id=' + row.SD_StudentId + ' class="clickMe filled-in chk-col-green" /><label for=' + row.SD_StudentId + '>Discontinue</label>'));
                    var $id = "#TCType" + row.SD_StudentId;
                    $data.append($row);
                });
                $("#update-panel").html($data);
                //$("#tblList").DataTable();
                //$('#tblList').DataTable({

                //});
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
    var StudentDetailsList = [];
    var Student = {};
    $('.clickMe:checked').each(function () {
        $(this).attr('id')
        //StudentIds.push($(this).attr('id'));
        var Student = Object.create(null);
        Student.SD_StudentId = $(this).attr('id');
        StudentDetailsList.push(Student);
    });
    //alert(TCStudents);
    var _data = JSON.stringify({

        StudetDetails_SD: {
            StudentDetailsList: StudentDetailsList
        }
    });
    $.ajax({
        type: 'POST',
        url: "/JQuery/DisContudentsINS",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            if (data = "Students are canceled successfully") {
                window.location.href = '/StudentManagement/DiscontinueStudentList';
            }
        }
    });
}
function BindList() {
    $('#update-panel').html('loading data.....');
    $.ajax({
        url: '/JQuery/GetDisStudentList',
        data: { SD_ClassId: parseInt($('#ClassId').val()), SD_StudentId: $.trim($('#txtStudentId').val()), SD_StudentName: $.trim($('#txtStudentName').val()), TransType: 'SELECTALL', },
        dataType: 'json',
        type: 'GET',
        success: function (res) {
            if (res.Data.length > 0) {
                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                $header = "<thead><tr><th>Student Id</th><th>Student Name</th><th>Class</th><th>Contact No 1</th><th>Contact No 2</th><th>Select</th></thead>";
                $data.append($header);

                $.each(res.Data, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td>').append(row.SD_StudentId));
                    $row.append($('<td>').append(row.SD_StudentName));
                    $row.append($('<td>').append(row.SD_CM_CLASSNAME));
                    $row.append($('<td>').append(row.SD_ContactNo1));
                    $row.append($('<td>').append(row.SD_ContactNo2));
                    $row.append($('<td>').append('<input type="checkbox"  id=' + row.SD_StudentId + ' class="clickMe filled-in chk-col-green" /><label for=' + row.SD_StudentId + '>Discontinue</label>'));
                    var $id = "#TCType" + row.SD_StudentId;
                    $data.append($row);
                });
                $("#update-panel").html($data);
                //$("#tblList").DataTable();
                //$('#tblList').DataTable({

                //});
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
