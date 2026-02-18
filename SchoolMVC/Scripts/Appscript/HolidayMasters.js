function InsertUpdateHoliday() {
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
            HM_Id: $('#HM_Id').val(),
            HM_HolidayName: $('#HM_HolidayName').val(),
            HM_FromDate: convertToSqlDate($('#HM_FromDate').val()),
            HM_ToDate: convertToSqlDate($('#HM_ToDate').val()),
            Userid: $('#hdnUserid').val()
        });
        $.ajax({
            url: '/JQuery/InsertUpdateHoliday',
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
                        window.location.href = '/Masters/HolidayList';
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

function ValidateOperation() {

    if ($('#HM_HolidayName').val() == "") {
        WarningToast('Please provide a holiday name.');
        return false;
    }
    if ($('#HM_FromDate').val() == "") {
        WarningToast('Please provide a holiday limit.');
        return false;
    }
    if ($('#HM_ToDate').val() == "") {
        WarningToast('Please provide a holiday limit.');
        return false;
    }
    return true;
}
function GradeList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        //papai
        $.ajax({
            url: '/JQuery/HolidayList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>Holidays</th><th>From</th><th>To</th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td>').text(i + 1));
                        $row.append($('<td style=display:none>').append(row.HM_Id));
                        $row.append($('<td>').append(row.HM_HolidayName));
                        if (row.HM_FromDate == null) {
                            $row.append($('<td>').append(row.HM_FromDate));
                        }
                        else {
                            var d = new Date(parseInt(row.HM_FromDate.slice(6, -2)));
                            $row.append($('<td>').append(formatJsonDate(row.HM_FromDate)));
                        }

                        if (row.HM_ToDate == null) {
                            $row.append($('<td>').append(row.HM_ToDate));
                        }
                        else {
                            var d = new Date(parseInt(row.HM_ToDate.slice(6, -2)));
                            $row.append($('<td>').append(formatJsonDate(row.HM_ToDate)));
                        }
                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/Holiday/" + row.HM_Id + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.HM_Id + ");'" + row.HM_Id + " class='btn btn-danger'>Delete</a>"));
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
$(document).ready(function (){
    GradeList();
});
function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'HolidayMaster_HM',
        MainFieldName: 'HM_Id',
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
                    window.location.href = '/Masters/HolidayList';
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