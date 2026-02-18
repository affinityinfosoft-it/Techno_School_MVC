$(document).ready(function () {
    LateFeesSlapMasterList();
});

//function LateFeesSlapMasterList() {
//    $('#update-panel').html('loading data.....');
  
//    setTimeout(function () {
//        $.ajax({
//            url: rootDir + 'JQuery/LateFeesSlapMasterList',
//            dataType: 'json',
//            type: 'GET',
//            success: function (res) {
//                if (res.Data.length > 0) {
//                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
//                    $header = "<thead><tr><th style='display:none'>SlapID</th><th>Name</th><th>Amount</th><th>Fine Type</th><th>Status</th></tr></thead>";
//                    $data.append($header);
//                    $.each(res.Data, function (i, row) {
//                        var $row = $('<tr/>');
//                        $row.append($('<td style=display:none>').append(row.Id));
//                        $row.append($('<td>').append(row.Slap_Name));
//                        $row.append($('<td>').append(row.Slap_Amount));
//                        $row.append($('<td>').append(row.FineTypeName));
//                        $row.append($('<td>').append(row.IsActive));

//                        if (res.CanEdit == true) {
//                            $row.append($('<td>').append("<a href= " + rootDir + "Masters/LateFeesMaster/" + parseInt(row.Id) + " class='btn btn-warning'>Edit</a> "));
//                        }
//                        else {
//                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
//                        }
//                        if (res.CanDelete == true) {
//                            $row.append($('<td>').append("<a onclick='Confirm(" + parseInt(row.Id) + ");'" + parseInt(row.Id) + " class='btn btn-danger'>Delete</a>"));
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
//                }
//                else {
//                    $noData = "<div>No data Found</td>"
//                    $("#update-panel").html($noData);
//                }

//            },
//            failure: function () {
//                ErrorToast('something wrong happen');
//            }

//        });
//    }, 1000);
   
//}
function LateFeesSlapMasterList() {
    $('#update-panel').html('loading data.....');

    setTimeout(function () {
        $.ajax({
            url: rootDir + 'JQuery/LateFeesSlapMasterList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    var $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    var $header = "<thead><tr>" +
                                  "<th style='display:none'>SlapID</th>" +
                                  "<th>Name</th>" +
                                  "<th>Amount</th>" +
                                  "<th>Fine Type</th>" +
                                  "<th>Status</th>" +
                                  "<th>Edit</th>" +
                                  "<th>Delete</th>" +
                                  "</tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');

                        $row.append($('<td style="display:none">').text(row.Id));
                        $row.append($('<td>').text(row.Slap_Name));
                        $row.append($('<td>').text(row.Slap_Amount));
                        $row.append($('<td>').text(row.FineTypeName));

                        // Status column
                        $row.append($('<td>').text(row.Status));

                        // Highlight active row
                        if (row.IsActive) {
                            $row.css('background-color', '#d4edda'); // light green
                            $row.css('font-weight', 'bold');
                        }

                        // Edit button
                        if (res.CanEdit) {
                            $row.append($('<td>').append("<a href='" + rootDir + "Masters/LateFeesMaster/" + row.Id + "' class='btn btn-warning'>Edit</a>"));
                        } else {
                            $row.append($('<td>').append("<a href='#' class='btn btn-warning disabled'>Edit</a>"));
                        }

                        // Delete button
                        if (res.CanDelete) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.Id + ");' class='btn btn-danger'>Delete</a>"));
                        } else {
                            $row.append($('<td>').append("<a href='#' class='btn btn-danger disabled'>Delete</a>"));
                        }

                        $data.append($row);
                    });

                    $("#update-panel").html($data);

                    $('#tblList').DataTable({
                        "order": [[0, "desc"]]
                    });
                } else {
                    $("#update-panel").html("<div>No data Found</div>");
                }
            },
            failure: function () {
                ErrorToast('Something wrong happened.');
            }
        });
    }, 1000);
}


function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'MSTR_LateFeesSlap',
        MainFieldName: 'ID',
        PId: parseInt(fieldId),
    });
    $.ajax({
        url: rootDir + 'JQuery/DeleteData',
        type: 'POST',
        data: _data,
        contentType: 'application/json ; utf-8',
        dataType: 'json',
        success: function (data) {
            if (data != null && data != undefined && data.IsSuccess == true) {
                SuccessToast(data.Message);
                setTimeout(function () {
                    //papai
                    window.location.href = rootDir + 'Masters/LateFeesMasterList';
                }, 2000);
            }
            else {
                ErrorToast(data.Message);
            }
        },
        error: function () {
            ErrorToast('Something wrong happend.');
        }

    });
}