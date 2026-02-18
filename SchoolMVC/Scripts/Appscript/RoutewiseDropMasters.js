$(document).ready(function () {
    BindList();
    $("#RDM_DISTANCE").number(true, 2);

});
// VALIDATION WHILE ENTERING DATA IN DATATABLE
function ValidateOperation() {

    if ($('#RDM_ROUTEID').val() == 0) {
        WarningToast('Please select a route.');
        return false;
    }
    if ($('#RDM_DROPPOINT').val() == '') {
        WarningToast('Please provide a drop point.');
        return false;
    }
    if ($('#RDM_DISTANCE').val() == 0) {
        WarningToast('Please provide a distance.');
        return false;
    }
    if ($('#RDM_RATE').val() == 0) {
        WarningToast('Please provide rate.');
        return false;
    }
    //if ($('#RDM_SERIAL').val() == '') {
    //    WarningToast('Please provide a serial no.');
    //    return false;
    //}
    var Route = $('#RDM_ROUTEID option:selected').text();
    var point = $('#RDM_DROPPOINT').val();
    var Rate = $('#RDM_RATE').val();
    var dis = $('#RDM_DISTANCE').val();
    var ser = $('#RDM_SERIAL').val();
    var tblItem = document.getElementById('tblList');
    if ($("#tblList>tbody:eq(0) tr:eq(" + 0 + ") td:eq(2)").text() == '') {

        $("#tblList").find('tbody').empty();

    }
    var len = tblItem.rows.length;
    for (var i = 1; i < len; i++) {

        if (Route == tblItem.rows[i].cells[2].innerHTML && point == tblItem.rows[i].cells[3].innerHTML && Rate == tblItem.rows[i].cells[4].innerHTML && dis == tblItem.rows[i].cells[5].innerHTML && ser == tblItem.rows[i].cells[6].innerHTML) {
            alert('This Combination already exist.')
            return false;
        }
        else {
            continue;
        }

    }
    return true;
}
// BIND DATA IN LIST PAGE
function BindList() {
    $('#update-panel').html('loading data.....');
    //$('#btnSave').attr('disabled', 'disabled');
    setTimeout(function () {
        //papai
        $.ajax({
            url: '/JQuery/RoutewiseDropList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th style=display:none>Id</th><th>Serial No</th><th>Route</th><th>Drop Point</th><th>Distance</th><th>Rate</th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td style=display:none>').append(row.RDM_ROUTEMAPID));
                        $row.append($('<td>').append(row.RDM_SERIAL));
                        $row.append($('<td>').append(row.RDM_RT_ROUTENAME));
                        $row.append($('<td>').append(row.RDM_DROPPOINT));
                        $row.append($('<td>').append(row.RDM_DISTANCE));
                        $row.append($('<td>').append(row.RDM_RATE));

                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/RouteWiseDropMapping/" + row.RDM_ROUTEID + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }

                        $row.append($('<td>').append("<a onclick='Confirm(" + row.RDM_ROUTEMAPID + ");'" + row.RDM_ROUTEMAPID + " class='btn btn-danger'>Delete</a>"));
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
//ADD DATA IN DATATABLE
function AddDetails() {
    if (ValidateOperation() == true) {
        if ($("#tblList>tbody:eq(0) tr:eq(" + 0 + ") td:eq(2)").text() == '') {

            $("#tblList").find('tbody').empty();
        }
        var $row = $('<tr/>');    
        $row.append($('<td style=display:none>').html($('#RDM_ROUTEMAPID ').val()));
        $row.append($('<td style=display:none>').html($("#RDM_ROUTEID option:selected").val()));
        $row.append($('<td/>').html($("#RDM_ROUTEID option:selected").text()));
        $row.append($('<td>').html($("#RDM_DROPPOINT").val()));
        $row.append($('<td>').html($("#RDM_RATE").val()));
        $row.append($('<td>').html($("#RDM_DISTANCE").val()));
        $row.append($('<td/>').html($("#RDM_SERIAL").val()));       
        $row.append($('<td>').append("<a onclick='EditDetails(this);' class='btn btn-warning' href='#'>Edit</a>"));
        $row.append($('<td>').append("<input type='image' name='imgede'  src='/Content/images/delete.png' onclick = 'deleteRow(this);' >"));
        $("#tblList>tbody").append($row);
        ClearDetails();
    }

}
function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
// DELETE DATA FROM DATABASE
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'RoutewiseDropMaster_RDM',
        MainFieldName: 'RDM_ROUTEMAPID',
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
                    window.location.href = '/Masters/RouteWiseDropMappingList';
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

function FinalValidation() {
    if ($("#tblList > tbody > tr").length == 0) {
        WarningToast("Please add any Details!")
        return false;
    }
    return true;
}
// INSERT AND UPDATE 
function InsertUpdateRoutewiseDrop() {
    if (FinalValidation() == true) {
        var ArrayItem = [];
        var tblItem = document.getElementById('tblList');
        var len = tblItem.rows.length;
        for (var i = 1; i < tblItem.rows.length; i++) {
            ArrayItem.push(
                {
                    RDM_ROUTEID: parseInt(tblItem.rows[i].cells[1].innerHTML),
                    RDM_DROPPOINT: tblItem.rows[i].cells[3].innerHTML,
                    RDM_RATE: parseFloat(tblItem.rows[i].cells[4].innerHTML),
                    RDM_DISTANCE: parseFloat(tblItem.rows[i].cells[5].innerHTML),
                    RDM_SERIAL: parseInt(tblItem.rows[i].cells[6].innerHTML),
                });


        }
        var _data = JSON.stringify({
            Userid: $('#hdnUserid').val(),
            RouteWiseDropList: ArrayItem,
            RDM_ROUTEID:$('#htnId').val(),
        });
        $.ajax({
            url: '/JQuery/InsertUpdateRoutewiseDrop',
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
                        window.location.href = '/Masters/RouteWiseDropMappingList';
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
// EDIT DATA FROM DATATABLE
function EditDetails(r) {
    var row = parseInt($(r).closest('tr').index());
    var check = $("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(1)").text();
    $("#RDM_ROUTEID").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(1)").text());
    $("#RDM_ROUTEID").selectpicker('refresh');
    $("#RDM_DROPPOINT").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(3)").text());
    $("#RDM_DISTANCE").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(5)").text());
    $("#RDM_RATE").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(4)").text());
    $("#RDM_SERIAL").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(6)").text());
    deleteRowWithOutAlert(r);
}
// DELETE DATA FROM DATATABLE WITHOUT ALERT
function deleteRowWithOutAlert(rowNo) {
    $(rowNo).closest('tr').remove();
}
//DELETE ROW WITH ALERT
function deleteRow(rowNo) {
    var agree = confirm("Are you sure you want to delete this record?");
    if (agree) {
        $(rowNo).closest('tr').remove();
        return true;
    } else {
        return false;
    }

}
function ClearDetails()
{
    $("#RDM_ROUTEID").val('');
    $("#RDM_ROUTEID").selectpicker('refresh');
    $("#RDM_DROPPOINT").val('');
    $("#RDM_DISTANCE").val('');
    $("#RDM_RATE").val('');
    $("#RDM_SERIAL").val('');
}
