function InsertUpdateState()
{
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
            states: {
                STM_STATEID: $('#STM_STATEID').val(),
                STM_STATENAME: $('#STM_STATENAME').val(),
                STM_NATIONID: $('#STM_NATIONID').val(),
                Userid: $('#hdnUserid').val()
            }

        });
        $.ajax({
            url: "/JQuery/InsertUpdateState",
            contentType: "application/json",
            dataType: "json",
            type: "POST",
            data: _data,
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    $('#btnSave').attr('disabled', 'disabled');
                    setTimeout(function () {
                        //papai
                        window.location.href = '/Masters/StateList';
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
function StateList() {
    $("#update-panel").html("Loading data.....");
    setTimeout(function () {
        //papai
        $.ajax({
            url: '/JQuery/StateList',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>State Name </th><th>Nation</th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td>').text(i + 1));
                        $row.append($('<td style=display:none>').append(row.STM_STATEID));
                        $row.append($('<td>').append(row.STM_STATENAME));
                        $row.append($('<td>').append(row.STRING_DATA));

                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/State/" + row.STM_STATEID + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.STM_STATEID + ");'" + row.STM_STATEID + " class='btn btn-danger'>Delete</a>"));
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
                ErrorToast('Something wrong happen');
            }
        });
    }, 1000);
    

}
$(document).ready(function () {
    StateList();
});
function Confirm(id)
{
    var agree = confirm("Are you sure?")
    if(agree)
    {
        Deletedata(id);
    }
}
function Deletedata(fieldId)
{
    var _data = JSON.stringify({
        MainTableName: 'StateMaster_STM',
        MainFieldName: 'STM_STATEID',
        PId: parseInt(fieldId),
    });
    $.ajax({
        url: '/JQuery/DeleteData',
        type: 'POST',
        data:_data,
        contentType: 'application/json ; utf-8',
        dataType: 'json',
        success: function(data)
        {
            if(data !=null && data != undefined && data.IsSuccess==true)
            {
                SuccessToast(data.Message);
                setTimeout(function () {
                    //papai
                    window.location.href = '/Masters/StateList';
                }, 2000);
                window.location.href = '/Masters/StateList';
            }
            else
            {
                ErrorToast(data.Message);
               
            }
        },
        error:function()
        {
            ErrorToast('Something wrong happend.');
        }
       
    });
}
function ValidateOperation()
{
    if ($('#STM_NATIONID').val() == 0)
    {
        WarningToast('Please select a Nation');
        return false;
    }
    if ($('#STM_STATENAME').val() == '') {
        WarningToast('Please provide State name.');
        return false;
    }
    return true;
}