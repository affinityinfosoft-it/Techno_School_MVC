function formatJsonDate(jsonDate) {
    if (!jsonDate) return '';
    var timestamp = parseInt(jsonDate.replace(/\/Date\((\d+)\)\//, '$1'));
    var date = new Date(timestamp);
    var day = ('0' + date.getDate()).slice(-2);
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var year = date.getFullYear();
    return day + '/' + month + '/' + year; // dd/MM/yyyy
}

function convertToSqlDate(dateStr) {
    if (!dateStr) return null;

    var parts = dateStr.split('/'); // dd-MM-yyyy
    return parts[2] + '-' + parts[1] + '-' + parts[0]; // yyyy-MM-dd
}


function InsertUpdateTerm() {
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
            TM_Id: $('#TM_Id').val(),
            TM_TermName: $('#TM_TermName').val(),
            TM_DateFrom: convertToSqlDate($('#TM_DateFrom').val()),
            TM_DateTo: convertToSqlDate($('#TM_DateTo').val()),
            Userid: $('#hdnUserid').val()

        });
       
        $.ajax({
            url: '/JQuery/InsertUpdateTerm',
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
                        window.location.href = '/Masters/TermList';
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
    
    if ($('#TM_TermName').val() == '') {
        WarningToast('Please provide term name.');
        return false;
    }
    if ($('#TM_DateFrom').val() == '') {
        WarningToast('Please provide a start date.');
        return false;
    }
    if ($('#TM_DateFrom').val() == '') {
        WarningToast('Please provide a end date.');
        return false;
    }
    return true;
}
function SessionList() {
    $("#update-panel").html("Loading data.....");
    setTimeout(function () {
        //papai
        $.ajax({
            url: '/JQuery/TermList',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>Start from </th><th>End at</th><th>Term</th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td>').text(i + 1));
                        $row.append($('<td style=display:none>').append(row.TM_Id));

                        if (row.TM_DateFrom == null) {
                            $row.append($('<td>').append(row.TM_DateFrom));
                        }
                        else {
                            var d = new Date(parseInt(row.TM_DateFrom.slice(6, -2)));
                            $row.append($('<td>').append(formatJsonDate(row.TM_DateFrom)));
                        }

                        if (row.TM_DateTo == null) {
                            $row.append($('<td>').append(row.TM_DateTo));
                        }
                        else {
                            var d = new Date(parseInt(row.TM_DateTo.slice(6, -2)));
                            $row.append($('<td>').append(formatJsonDate(row.TM_DateTo)));
                        }
                        $row.append($('<td>').append(row.TM_TermName));

                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/Term/" + row.TM_Id + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));

                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.TM_Id + ");'" + row.TM_Id + " class='btn btn-danger'>Delete</a>"));
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
    SessionList();
});
function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'TermMaster_TM',
        MainFieldName: 'TM_Id',
        PId: parseInt(fieldId),
    });
    $.ajax({
        url: '/JQuery/DeleteData',
        type: 'POST',
        data: _data,
        contentType: 'application/json; utf-8',
        dataType: 'json',
        success: function (data) {
            if (data != null && data != undefined && data.IsSuccess == true) {
                SuccessToast(data.Message);
                setTimeout(function () {
                    //papai
                    window.location.href = '/Masters/TermList';
                }, 2000);
            }
            else {

                ErrorToast(data.Message);
            }
        },
        error: function () {
            ErrorToast('something wrong happened.');
        }
    });
}
