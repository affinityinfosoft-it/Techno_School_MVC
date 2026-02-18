$("#btnSearch").on('click', function () {
    if ($('#ClassId').val() == "") {
        alert('Please Select Class');
    } else {
        BindList();
    }
    //BindList();
});
function BindList() {
    $('#update-panel').html('loading data.....');
    $.ajax({
        url: '/JQuery/StudentClassChange',
        data: { SD_CurrentClassId: parseInt($('#ClassId').val()) },
        dataType: 'json',
        type: 'GET',
        success: function (res) {
            if (res.Data.length > 0) {
                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                $header = "<thead><tr><th>Student Id</th><th>Student Name</th><th>Class</th><th>Change Class</th><th>Action</th></thead>";
                $data.append($header);

                $.each(res.Data, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td>').append(row.SD_StudentId));
                    $row.append($('<td>').append(row.SD_StudentName));
                    $row.append($('<td>').append(row.SD_CM_CLASSNAME));
                    $row.append($('<td>').append('<select class="ms form-control" id="class' + row.SD_StudentId + '"><option>Select Class</option></select>'));
                    var $id = "#class" + row.SD_StudentId;
                    BindClass($id);

                    if (res.CanEdit == true) {
                        $row.append($('<td>').append("<a href='#' id='btnUpdate' onclick='btnUpdate(this);' class='btn btn-warning'>Update</a>"));
                    }
                    else {
                        $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Update</a>"));
                    }
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
function BindClass($id) {
    $.ajax({
        url: '/JQuery/ClassList',
        data: {},
        dataType: 'json',
        type: 'GET',
        success: function (json) {
            var $el = $("" + $id + "");
            $el.empty();
            $el.append($("<option style='color:black'></option>").attr("value", '0').text('Select Class'));

            $.each(json.Data, function (key, value) {
                var html = "<option style='color:black' value='" + value.CM_CLASSID + "'>" + value.CM_CLASSNAME + "</option>";
                $el.append(html);

            });
        },
        failure: function () {
            ErrorToast('something wrong happen');
        }

    });
}
function btnUpdate(elem) {
   
    var StudentId = $(elem).closest('tr').find('td:eq(0)').text();
    var classId = $("#class" + StudentId).val();
    if (classId == 0) {
        alert('Please Select Class');
    } else {
        $.ajax({
            url: '/JQuery/StudentClassChangeUpdate',
            data: { SD_CurrentClassId: classId, SD_StudentId: StudentId },
            dataType: 'json',
            type: 'POST',
            success: function (res) {

                if (res == 100) {
                    SuccessToast("Update Successfull");
                    BindList();
                }
            },
            failure: function () {
                ErrorToast('something wrong happen');
            }

        });

    }
       // $('#update-panel').html('loading data.....');
      
}