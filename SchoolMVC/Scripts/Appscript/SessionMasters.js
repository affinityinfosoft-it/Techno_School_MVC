function InsertUpdateSession()
{
    if (validation() == true) {

        var fromSession = $('#SM_STARTDATE').val();
        var toSession = $('#SM_ENDDATE').val();
        var fromParts = parseInt(fromSession.split('/')[2]);
        var toParts = parseInt(toSession.split('/')[2]);
        //var sescode = fromParts.substring(2, fromParts.length) + '-' + toParts.substring(2, toParts.length);
        var sesName = fromParts + '-' + toParts;
        var OneyearGapCheck = fromParts + 2;
        if (OneyearGapCheck <= toParts)
        {
            alert('Please maintain one year gap. ')
            return false;
        }
       var _data = JSON.stringify({
           ses: {
               SM_STARTDATE: convertToSqlDate($("#SM_STARTDATE").val()),
               SM_ENDDATE: convertToSqlDate($("#SM_ENDDATE").val()),
               SM_SESSIONCODE: "",
               SM_SESSIONNAME: sesName,
               SM_SESSIONID: $("#SM_SESSIONID").val(),
               Userid: $('#hdnUserid').val()
           }
       });
       $.ajax({
           url: '/JQuery/InsertUpdateSession',
           contentType: "application/json",
           dataType: "json",
           type: "POST",
           data: _data,
           success: function (response) {
               if (response != null && response != undefined && response.IsSuccess == true) {
                   SuccessToast(response.Message);
                   $('#btnSave').attr('disabled','disabled');
                   setTimeout(function () {
                       //papai
                       window.location.href = '/Masters/SessionList';
                   }, 2000);
               } else {
                   ErrorToast(response.Message);
               }
           },
           error: function (jqxhr, settings, thrownError) {
               console.log(jqxhr.status + '\n' + thrownError);
           }
       });
   }
}

function convertToSqlDate(dateStr) {
    if (!dateStr) return null;

    var parts = dateStr.split('/'); // dd-MM-yyyy
    return parts[2] + '-' + parts[1] + '-' + parts[0]; // yyyy-MM-dd
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

function validation() {
    if ($("#SM_STARTDATE").val() == '') {
        WarningToast('Provide Start Date');
        return false;
    }
    if ($("#SM_ENDDATE").val() == '') {
        WarningToast('Provide End Date');
        return false;
    }
    if ($("#SM_ENDDATE").val() == $("#SM_STARTDATE").val()) {
        WarningToast('Session Date can not be same');
        return false;
       
    }
    if (CheckingValidSession() == true) return true;
}
    function CheckingValidSession() {
        var fromSession = $('#SM_STARTDATE').val();
        var toSession = $('#SM_ENDDATE').val();
        var fromParts = parseInt(fromSession.split('/')[2]);
        var toParts = parseInt(toSession.split('/')[2]);
        if (toParts <= fromParts) {
            alert('Please Select To Proper Session....')
            return false;
        }
        return true;
    }
function getYears(from, to) {
    var d1 = new Date(from),
        d2 = new Date(to),
        yr = [];

    for (var i = d1.getFullYear() ; i <= d2.getFullYear() ; i++) {
        yr.push(i);       
    }
   var year= yr.join('-')
   return year;
}
function GetSessionCode(from, to) {
    var d1 = new Date(from)
    var d2 = new Date(to)
  
    yr = [];
    var year1 = d1.getFullYear()
    var year2 = d2.getFullYear()

    yr.push(year1.toString().substring(2))
    yr.push(year2.toString().substring(2))  
    var year = yr.join('-') 
    return year;
}
function SessionList() {
    $("#update-panel").html("Loading data.....");
    setTimeout(function () {
        $.ajax({
            url: '/JQuery/GetSessionList',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>Start from </th><th>End at</th><th>Session</th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td>').text(i + 1));
                        $row.append($('<td style=display:none>').append(row.SM_SESSIONID));

                        if (row.SM_STARTDATE == null) {
                            $row.append($('<td>').append(row.SM_STARTDATE));
                        }
                        else {
                            var d = new Date(parseInt(row.SM_STARTDATE.slice(6, -2)));
                            $row.append($('<td>').append(formatJsonDate(row.SM_STARTDATE)));
                        }

                        if (row.SM_ENDDATE == null) {
                            $row.append($('<td>').append(row.SM_ENDDATE));
                        }
                        else {
                            var d = new Date(parseInt(row.SM_ENDDATE.slice(6, -2)));
                            $row.append($('<td>').append(formatJsonDate(row.SM_ENDDATE)));
                        }
                        $row.append($('<td>').append(row.SM_SESSIONNAME));
                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/Session/" + row.SM_SESSIONID + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));

                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.SM_SESSIONID + ");'" + row.SM_SESSIONID + " class='btn btn-danger'>Delete</a>"));
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
        MainTableName: 'SessionMasters_SM',
        MainFieldName: 'SM_SESSIONID',
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
                alert(data.Message);
                setTimeout(function () {
                    window.location.href = '/Masters/SessionList';
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
