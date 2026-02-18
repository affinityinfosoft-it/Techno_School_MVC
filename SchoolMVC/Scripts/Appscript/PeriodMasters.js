function InsertUpdatePeriod() {
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
           
            PM_Id: $('#PM_Id').val(),
            PM_FromTime: $('#PM_FromTime').val(),
            PM_ToTime: $('#PM_ToTime').val(),
            PM_ClassName : $('#PM_ClassName').val(),
            PM_Period: $('#PM_Period').val(),
            PM_ClassId: $('#PM_ClassId').val(),
            Userid: $('#hdnUserid').val()

        });

      $.ajax({
          url: '/JQuery/InsertUpdatePeriod',
          data: _data,
          type: 'POST',
          dataType: 'json',
          contentType: 'application/json ; utf-8',
          success: function (data) {
              if (data != null && data != undefined && data.IsSuccess == true) {
                  SuccessToast(data.Message);
                  $('#btnSave').attr('disabled', 'disabled');
                  setTimeout(function () {
                      window.location.href = '/Masters/PeriodList';
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
    if ($('#PM_ClassId').val() == "") {
        WarningToast('Please provide Class');
        return false;
    }

    if ($('#PM_FromTime').val() == "") {
        WarningToast('Please provide From Time.');
        return false;
    }
    if ($('#PM_ToTime').val() == "") {
        WarningToast('Please provide To Time.');
        return false;
    }
    if ($('#PM_Period').val() == "") {
        WarningToast('Please provide Period');
        return false;
    }
   
    return true;
}
function PeriodList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        //papai
        $.ajax({
            url: '/JQuery/PeriodList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>Class</th><th>Start At</th><th>End At</th><th>Period No</th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td>').text(i + 1));
                        $row.append($('<td style=display:none>').append(row.PM_Id));
                        $row.append($('<td>').append(row.PM_ClassName));
                        $row.append($('<td>').append(row.PM_FromTime));
                        $row.append($('<td>').append(row.PM_ToTime));
                        $row.append($('<td>').append(row.PM_Period));

                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/Period/" + row.PM_Id + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.PM_Id + ");'" + row.PM_Id + " class='btn btn-danger'>Delete</a>"));
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
                ErrorToast('Something wrong happen');
            }

        });
    }, 1000);
    
}
$(document).ready(function () {
    PeriodList();
});
function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'PeriodMaster_PM',
        MainFieldName: 'PM_Id',
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
                alert(data.Message);
                setTimeout(function () {
                    //papai
                    window.location.href = '/Masters/PeriodList';
                }, 2000);
            }
            else {
                alert(data.Message);
            }
        },
        error: function () {
            alert('Something wrong happend.');
        }

    });
}
