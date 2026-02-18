function InsertUpdateCategory() {
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
                CAT_CATEGORYID:$('#CAT_CATEGORYID').val(),
                CAT_STUDENTCATEGORY: $('#CAT_STUDENTCATEGORY').val(),
                Userid: $('#hdnUserid').val()
        });
        $.ajax({
            url: '/JQuery/InsertUpdateFeesCategory',
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
                        window.location.href = '/Masters/FeesCategoryList';
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

    if ($('#CAT_STUDENTCATEGORY').val() == "") {
        WarningToast('Please provide a Fees Category');
        return false;
    }

    return true;
}
function BindList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        //papai
        $.ajax({
            url: '/JQuery/FeesCategoryList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th style=display:none>Id</th><th>Serial No</th><th>Category</th><th></th><th></th></tr></thead>";
                    $data.append($header);
                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td style=display:none>').append(row.CAT_CATEGORYID));
                        $row.append($('<td>').append(i + 1));
                        $row.append($('<td>').append(row.CAT_STUDENTCATEGORY));
                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/FeesCategory/" + row.CAT_CATEGORYID + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.CAT_CATEGORYID + ");'" + row.CAT_CATEGORYID + " class='btn btn-danger'>Delete</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-danger disabled'>Delete</a>"));

                        }
                        $data.append($row);
                    });
                    $("#update-panel").html($data);
                    $("#tblList").DataTable();
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
    BindList();
});
function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'STUDENTCATEGORY_CAT',
        MainFieldName: 'CAT_CATEGORYID',
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
                    window.location.href = '/Masters/FeesCategoryList';
                }, 2000);
            }
            else
            {
                ErrorToast(data.Message);
            }
        },
        error: function () {
            ErrorToast('Something wrong happend.');
        }

    });
}