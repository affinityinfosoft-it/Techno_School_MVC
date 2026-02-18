function InsertUpdateBookMaster() {
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
            BM_BookId: $('#BM_BookId').val(),
            BM_SchoolId: $('#BM_SchoolId').val(),
            BM_Title: $('#BM_Title').val(),
            BM_ISBN: $('#BM_ISBN').val(),
            BM_Author: $('#BM_Author').val(),
            BM_Publisher: $('#BM_Publisher').val(),
            BM_Edition: $('#BM_Edition').val(),
            BM_Language: $('#BM_Language').val(),
            BM_ClassLevel: $('#BM_ClassLevel').val(),
            BM_ShelfNo: $('#BM_ShelfNo').val(),
            BM_IsReferenceOnly: $('#BM_IsReferenceOnly').val(),
            BM_IsActive: $('#BM_IsActive').val(),
            Userid: $('#hdnUserid').val()

        });

        $.ajax({
            url: '/JQuery/InsertUpdateBookMaster',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    $('#btnSave').attr('disabled', 'disabled');
                    setTimeout(function () {
                        //papai
                        window.location.href = '/Library/BookMasterList';
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
function ValidateOperation() {

    if ($('#BM_Title').val() == "") {
        WarningToast('Please provide a Title name.');
        return false;
    }
    if ($('#BM_ISBN').val() == "") {
        WarningToast('Please provide valid ISBN.');
        return false;
    }
    if ($('#BM_Author').val() == "") {
        WarningToast('Please provide a Author name.');
        return false;
    }
    if ($('#BM_Publisher').val() == "") {
        WarningToast('Please provide a Publisher name.');
        return false;
    }
    if ($('#BM_Edition').val() == "") {
        WarningToast('Please provide Edition For this book.');
        return false;
    }
    if ($('#BM_Language').val() == "") {
        WarningToast('Please provide a Language name.');
        return false;
    }
    if ($('#BM_ClassLevel').val() == "") {
        WarningToast('Please provide a Class Level.');
        return false;
    }
    if ($('#BM_ShelfNo').val() == "") {
        WarningToast('Please provide a Shelf number.');
        return false;
    }
    //if ($('#BM_IsReferenceOnly').val() == "") {
    //    WarningToast('Please provide a route name.');
    //    return false;
    //}
    if ($('#BM_IsActive').val() == "") {
        WarningToast('Please check the active field.');
        return false;
    }
    return true;
}
function BindList() {
    $('#update-panel').html('loading data.....');

    setTimeout(function () {
        $.ajax({
            url: '/JQuery/BookMasterList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {

                if (res.Data.length > 0) {

                    $data = $('<table id="tblList"></table>')
                        .addClass('table table-bordered table-striped');

                    $header = "<thead><tr><th style=display:none>Id</th><th>Serial No.</th><th>Book Title</th><th>Book ISBN</th><th>Book Author</th><th>Book Publisher</th><th>Book Edition</th><th>Book Language</th><th>Book Class Level</th><th>Book Shelf No</th><th>Active</th><th>Action</th></tr></thead>";

                    $data.append($header);

                    var $tbody = $('<tbody></tbody>');   // ✅ REQUIRED

                    $.each(res.Data, function (i, row) {

                        var $row = $('<tr/>');

                        $row.append($('<td style=display:none>').append(row.BM_BookId));
                        $row.append($('<td>').append(i + 1));
                        $row.append($('<td>').append(row.BM_Title));
                        $row.append($('<td>').append(row.BM_ISBN));
                        $row.append($('<td>').append(row.BM_Author));
                        $row.append($('<td>').append(row.BM_Publisher));
                        $row.append($('<td>').append(row.BM_Edition));
                        $row.append($('<td>').append(row.BM_Language));
                        $row.append($('<td>').append(row.BM_ClassLevel));
                        $row.append($('<td>').append(row.BM_ShelfNo));
                        $row.append($('<td>').append(row.BM_IsActive));

                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Library/BookMaster/" + row.BM_BookId + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }

                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.BM_BookId + ");' class='btn btn-danger'>Delete</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-danger disabled'>Delete</a>"));
                        }

                        $tbody.append($row);   
                    });

                    $data.append($tbody);  

                    $("#update-panel").html($data);

                    $('#tblList').DataTable({
                        "order": [[0, "desc"]]
                    });

                } else {
                    $("#update-panel").html("<div>No data Found</div>");
                }
            },
            failure: function () {
                ErrorToast('Something went wrong');
            }
        });
    }, 1000);
}


//function BindList() {
//    $('#update-panel').html('loading data.....');
//    setTimeout(function () {
//        $.ajax({
//            url: '/JQuery/BookMasterList',
//            dataType: 'json',
//            type: 'GET',
//            success: function (res) {
//                if (res.Data.length > 0) {
//                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
//                    $header = "<thead><tr><th style=display:none>Id</th><th>Serial No.</th><th>Book Title</th><th>Book ISBN</th><th>Book Author</th><th>Book Author</th><th>Book Publisher</th><th>Book Edition</th><th>Book Language</th><th>Book Class Level</th><th>Book Shelf No</th><th>Active</th></tr></thead>";
//                    $data.append($header);

//                    $.each(res.Data, function (i, row) {
//                        var $row = $('<tr/>');
//                        $row.append($('<td style=display:none>').append(row.BM_BookId));
//                        $row.append($('<td>').append(i + 1));
//                        $row.append($('<td>').append(row.BM_Title));
//                        $row.append($('<td>').append(row.BM_ISBN));
//                        $row.append($('<td>').append(row.BM_Author));
//                        $row.append($('<td>').append(row.BM_Publisher));
//                        $row.append($('<td>').append(row.BM_Edition));
//                        $row.append($('<td>').append(row.BM_Language));
//                        $row.append($('<td>').append(row.BM_ClassLevel));
//                        $row.append($('<td>').append(row.BM_ShelfNo));
//                        $row.append($('<td>').append(row.BM_IsActive));
//                        if (res.CanEdit == true) {
//                           // $row.append($('<td>').append("<a href=/Library/BookMaster/id=" + row.BM_BookId + " class='btn btn-warning'>Edit</a>"));
//                            $row.append($('<td>').append("<a href=/Library/BookMaster/" + row.BM_BookId + " class='btn btn-warning'>Edit</a>"));
//                        }
//                        else {
//                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
//                        }
//                        if (res.CanDelete == true) {
//                            $row.append($('<td>').append("<a onclick='Confirm(" + row.BM_BookId + ");'" + row.BM_BookId + " class='btn btn-danger'>Delete</a>"));
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
$(document).ready(function () {
    BindList();
});
//function Confirm(id) {
//    var agree = confirm("Are you Sure?");
//    if (agree) {
//        Deletedata(id);
//    }
//}
//function Deletedata(fieldId) {
//    var _data = JSON.stringify({
//        MainTableName: 'RouteMastes_RT',
//        MainFieldName: 'RT_ROUTEID',
//        PId: parseInt(fieldId),
//    });
//    $.ajax({
//        url: '/JQuery/DeleteData',
//        type: 'POST',
//        data: _data,
//        contentType: 'application/json ; utf-8',
//        dataType: 'json',
//        success: function (data) {
//            if (data != null && data != undefined && data.IsSuccess == true) {
//                SuccessToast(data.Message);
//                setTimeout(function () {
//                    //papai
//                    window.location.href = '/Masters/BusRouteList';
//                }, 2000);
//                window.location.href = '/Masters/BusRouteList';
//            }
//            else {
//                ErrorToast(data.Message);
//            }
//        },
//        error: function () {
//            ErrorToast('Something wrong happend.');
//        }

//    });
