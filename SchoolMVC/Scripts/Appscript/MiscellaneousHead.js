


function InsertUpdateMiscellaneousHead() {
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
            MISC_Id: $('#MISC_Id').val(),
            MISC_FeeHeadCode: $('#MISC_FeeHeadCode').val(),
            MISC_FeeHeadName: $('#MISC_FeeHeadName').val(),
            MISC_Amount: $('#MISC_Amount').val(),
            //Userid: $('#MISC_CreatedBy').val()

        });

        $.ajax({
            url: '/JQuery/InsertUpdateMiscellaneousHead',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    $('#btnSave').attr('disabled', 'disabled');
                    setTimeout(function () {

                        window.location.href = '/Masters/MiscellaneousHeadList';
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
    var code = $('#MISC_FeeHeadCode').val().trim();
    var name = $('#MISC_FeeHeadName').val().trim();

    if (code === "") {
        ErrorToast("Please enter Miscellaneous Code.");
        $('#MISC_FeeHeadCode').focus();
        return false;
    }
    if (name === "") {
        ErrorToast("Please enter Miscellaneous Head Name.");
        $('#MISC_FeeHeadName').focus();
        return false;
    }

    return true;
}

function MiscellaneousHeadList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        //papai
        $.ajax({
            url: '/JQuery/MiscellaneousHeadList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th style=display:none>Id</th><th>Code</th><th>Fee Name</th><th>Amount</th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td style=display:none>').append(row.MISC_Id));
                        $row.append($('<td>').append(row.MISC_FeeHeadCode));
                        $row.append($('<td>').append(row.MISC_FeeHeadName));
                        $row.append($('<td>').append(row.MISC_Amount));
                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/MiscellaneousHead/" + row.MISC_Id + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.MISC_Id + ");'" + row.MISC_Id + " class='btn btn-danger'>Delete</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-danger disabled'>Delete</a>"));
                        }
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
    }, 1000);

}
$(document).ready(function () {
    MiscellaneousHeadList();
});
function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'MiscellaneousHeadMaster_MISC',
        MainFieldName: 'MISC_Id',
        PId: parseInt(fieldId),
    });
    $.ajax({
        url: '/JQuery/DeleteData',
        type: 'POST',
        data: _data,
        contentType: 'application/json ; utf-8',
        dataType: 'json',
        success: function (data) {
            if (data != null && data != undefined && data.IsSuccess == true) {
                SuccessToast(data.Message);
                setTimeout(function () {
                    //papai
                    window.location.href = '/Masters/MiscellaneousHeadList';
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