
    $(document).ready(function () {
        LibrarySettingMasterList();
   
    })
function ValidateLibrarySettingMaster() {
    if ($.trim($("#LS_MaxIssueDays").val()) == "") {
        alert("Max Issue Days can not be blank")
        return false;
    }
    return true;
}
function SaveLibrarySetting() {
    if (ValidateLibrarySettingMaster() == true) {
        var _data = JSON.stringify({
            SettingId: $('#LS_SettingId').val(),
            SchoolId: $('#LS_SchoolId').val(),
            MaxIssueDays: $('#LS_MaxIssueDays').val(),
            FinePerDay: $('#LS_FinePerDay').val(),
            MaxBooksStudent: $('#LS_MaxBooksStudent').val(),
            MaxBooksTeacher: $('#LS_MaxBooksTeacher').val(),
            MaxRenewLimit: $('#LS_MaxRenewLimit').val(),
            GraceDays: $('#LS_GraceDays').val(),
            Active: $('#LS_Active').val(),
            Userid: $('#hdnUserid').val()
        });

        $.ajax({
            url: '/JQuery/InsertUpdateLibrarySettingMaster',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    $('#btnSave').attr('disabled', 'disabled');
                    setTimeout(function () {
                        window.location.href = '/Library/LibrarySettingMasters';
                    }, 2000);
                }
                else {
                    ErrorToast(data.Message);
                }
            },
            error: function () {
                ErrorToast('Something wrong happened.');
            }

        });
    }
}
function LibrarySettingMasterList() {
    $('#update-panel').html('loading data.....');

    $.ajax({
        url: '/JQuery/GetLibrarySettingMasterList',
        dataType: 'json',
        type: 'GET',
        success: function (res) {

            if (res.Data && res.Data.length > 0) {

                var $table = $('<table id="tblList" class="table table-bordered table-striped"></table>');
                var $thead = $('<thead><tr>' +
                    '<th style="display:none">Id</th>' +
                    '<th>Serial No.</th>' +
                    '<th>Max Issue Days</th>' +
                    '<th>Fine Per Day</th>' +
                    '<th>Max Books Student</th>' +
                    '<th>Max Books Teachers</th>' +
                    '<th>Max Renew Limit</th>' +
                    '<th>Grace Days</th>' +
                    '<th>Action</th>' +
                    '</tr></thead>');

                var $tbody = $('<tbody></tbody>');

                $.each(res.Data, function (i, row) {

                    var $tr = $('<tr></tr>');
                    $tr.append('<td style="display:none">' + row.LS_SettingId + '</td>');
                    $tr.append('<td>' + (i + 1) + '</td>');
                    $tr.append('<td>' + row.LS_MaxIssueDays + '</td>');
                    $tr.append('<td>' + row.LS_FinePerDay + '</td>');
                    $tr.append('<td>' + row.LS_MaxBooksStudent + '</td>');
                    $tr.append('<td>' + row.LS_MaxBooksTeacher + '</td>');
                    $tr.append('<td>' + row.LS_MaxRenewLimit + '</td>');
                    $tr.append('<td>' + row.LS_GraceDays + '</td>');

                    var actionHtml = '';
                    //if (res.CanEdit) {
                        actionHtml += '<a href="/Library/LibrarySettingMasters/' + row.LS_SettingId + '" class="btn btn-warning">Edit</a> ';
                    //} else {
                    //    actionHtml += '<a href="#" class="btn btn-warning disabled">Edit</a> ';
                    //}

                  

                    $tr.append('<td>' + actionHtml + '</td>');
                    $tbody.append($tr);
                });

                $table.append($thead).append($tbody);
                $('#update-panel').html($table);

                $('#tblList').DataTable({
                    order: [[1, 'desc']]
                });
            }
            else {
                $('#update-panel').html('<div>No data Found</div>');
            }
        },
        error: function () {
            ErrorToast('Something went wrong');
        }
    });
}

//function LibrarySettingMasterList() {
//    $('#update-panel').html('loading data.....');
//    setTimeout(function () {
//        $.ajax({
//            url: '/JQuery/GetLibrarySettingMasterList',
//            dataType: 'json',
//            type: 'GET',
//            success: function (res) {
//                if (res.Data.length > 0) {
//                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
//                    $header = "<thead><tr><th style=display:none>Id</th><th>Serial No.</th><th>Max Issue Date</th><th>Fine Per Day</th><th>Max Books Student</th><th>Max Books Teachers</th><th>Max Renew Limit</th><th>Grace Days</th></tr></thead>";
//                    $data.append($header);

//                    $.each(res.Data, function (i, row) {
//                        var $row = $('<tr/>');
//                        $row.append($('<td style=display:none>').append(row.LS_SettingId));
//                        $row.append($('<td>').append(i + 1));
//                        $row.append($('<td>').append(row.LS_MaxIssueDays));
//                        $row.append($('<td>').append(row.LS_FinePerDay));
//                        $row.append($('<td>').append(row.LS_MaxBooksStudent));
//                        $row.append($('<td>').append(row.LS_MaxBooksTeacher));
//                        $row.append($('<td>').append(row.LS_MaxRenewLimit));
//                        $row.append($('<td>').append(row.LS_GraceDays));

//                        if (res.CanEdit == true) {
//                            // $row.append($('<td>').append("<a href=/Library/BookMaster/id=" + row.BM_BookId + " class='btn btn-warning'>Edit</a>"));
//                            $row.append($('<td>').append("<a href=/Library/LibrarySettingMasters/" + row.LS_SettingId + " class='btn btn-warning'>Edit</a>"));
//                        }
//                        else {
//                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
//                        }
//                        if (res.CanDelete == true) {
//                            $row.append($('<td>').append("<a onclick='Confirm(" + row.LS_SettingId + ");'" + row.LS_SettingId + " class='btn btn-danger'>Delete</a>"));
//                        }
//                        else {
//                            $row.append($('<td>').append("<a href=# class='btn btn-danger disabled'>Delete</a>"));

//                        }
//                        $data.append($row);
//                    });

//                    $("#update-panel").html($data);
//                    $('#tblList').DataTable({
//                        "order": [[0, "desc"]]
//                    });

//                } else {
//                    $noData = "<div>No data Found</div>";
//                    $("#update-panel").html($noData);
//                }
//            },
//            failure: function () {
//                ErrorToast('Something went wrong');
//            }
//        });
//    }, 1000);
//}
