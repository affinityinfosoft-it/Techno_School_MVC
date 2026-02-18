$(document).ready(function () {
    BindList();
    //SessionList();
    //fixIsActiveText();
});
function InsertUpdateUserMaster() {
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
            UM_USERID: $('#UM_USERID').val(),
            UM_LOGINID: $('#UM_LOGINID').val(),
            UM_PASSWORD: $('#UM_PASSWORD').val(),
            UM_USERNAME: $('#UM_USERNAME').val(),
            UM_USEREMAIL: $('#UM_USEREMAIL').val(),
            UM_USERMOBILE: $('#UM_USERMOBILE').val(),
            UM_ISACTIVE: $('#UM_ISACTIVE').is(":checked"),
            //UM_ISACTIVE: $('input[name="UM_ISACTIVE"]:checked').val(),
            UM_SCM_SCHOOLID: $('#UM_SCM_SCHOOLID').val(),
            Userid: $('#hdnUserid').val(),
            UM_ROLEID: $('#UM_ROLEID').val(),
        });

        $.ajax({
            url: '/JQuery/InsertUpdateUserMaster',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    $('#btnSave').attr('disabled', 'disabled');
                    setTimeout(function () {
                        window.location.href = '/Masters/UserMasterList';
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

    if ($('#UM_LOGINID').val() == "") {
        WarningToast('Please provide a Login Id.');
        return false;
    }
    if ($('#UM_PASSWORD').val() == "") {
        WarningToast('Please provide a Password.');
        return false;
    }
    if ($('#UM_USERNAME').val() == 0) {
        WarningToast('Please provide a Name.');
        return false;
    }
    if ($('#UM_USEREMAIL').val() == '') {
        WarningToast('Please provide a Email.');
        return false;
    }
    if ($('#UM_USEREMAIL').val() != '') {
        if (validateEmail($('#UM_USEREMAIL').val())) {

        }
        else {
            WarningToast('You Email Address Invalid ....')
            return false;
        }
    }
    
    if ($('#UM_USERMOBILE').val() == '') {
        WarningToast('Please provide a Valid Mobile Number.');
        return false;
    }
   
    if ($('#UM_SCM_SCHOOLID').val() == '') {
        WarningToast('Please select School.');
        return false;
    }
    if ($('#UM_ROLEID').val() == '') {
        WarningToast('Please select a Role.');
        return false;
    }
    return true;
}
//function BindList() {
//    $('#update-panel').html('loading data.....');
//    setTimeout(function () {
//        $.ajax({
//            url: '/JQuery/UserMasterList',
//            dataType: 'json',
//            type: 'GET',
//            success: function (res) {
//                if (res.Data.length > 0) {
//                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
//                    $header = "<thead><tr><th style=display:none>Id</th><th>User Name</th><th>User Login Id</th><th>User Mail</th><th>User Mobile No.</th></tr></thead>";
//                    $data.append($header);

//                    $.each(res.Data, function (i, row) {
//                        var $row = $('<tr/>');
//                        $row.append($('<td style=display:none>').append(row.UM_USERID));
//                        $row.append($('<td>').append(row.UM_USERNAME));
//                        $row.append($('<td>').append(row.UM_LOGINID));
//                        $row.append($('<td>').text(row.UM_USEREMAIL));
//                        $row.append($('<td>').append(row.UM_USERMOBILE));

//                        if (res.CanEdit == true) {
//                            $row.append($('<td>').append("<a href=/Masters/User/" + row.UM_USERID + " class='btn btn-warning'>Edit</a>"));
//                        }
//                        else {
//                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
//                        }
//                        //if (res.CanDelete == true) {
//                        //    $row.append($('<td>').append("<a onclick='Confirm(" + row.UM_USERID + ");'" + row.UM_USERID + " class='btn btn-danger'>Delete</a>"));
//                        //}
//                        //else {
//                        //    $row.append($('<td>').append("<a href=# class='btn btn-danger disabled'>Delete</a>"));

//                        //}
//                        $data.append($row);
//                    });
//                    $("#update-panel").html($data);
//                    //$('#tblList').DataTable({
//                    //    "order": [[0, "desc"]]
//                    //});
//                    //if ($.fn.DataTable.isDataTable('#tblList')) {
//                    //    $('#tblList').DataTable().destroy();
//                    //}

//                    //$('#tblList').DataTable({
//                    //    "order": [[0, "desc"]],
//                    //    "columnDefs": [
//                    //        { "targets": [0], "visible": false }, // hide Id column
//                    //        { "orderable": false, "targets": [5, 6] } // disable sort on Edit/Delete
//                    //    ]
//                    //});

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

function BindList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        $.ajax({
            url: '/JQuery/UserMasterList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th style='display:none'>Id</th><th>User Name</th><th>User Login Id</th><th>User Mail</th><th>User Mobile No.</th><th>Active Status</th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        //$row.append($('<td>').text(i + 1)); // SLNO Column
                        $row.append($('<td style="display:none">').append(row.UM_USERID));
                        $row.append($('<td>').append(row.UM_USERNAME));
                        $row.append($('<td>').append(row.UM_LOGINID));
                        $row.append($('<td>').text(row.UM_USEREMAIL));
                        $row.append($('<td>').append(row.UM_USERMOBILE));
                        //$row.append($('<td>').append(row.UM_ISACTIVE));
                        var isActive = (row.UM_ISACTIVE === true) ? 'Yes' : 'No';
                        $row.append($('<td>').text(isActive));
                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href='/Masters/User/" + row.UM_USERID + "' class='btn btn-warning'>Edit</a>"));
                        } else {
                            $row.append($('<td>').append("<a href='#' class='btn btn-warning disabled'>Edit</a>"));
                        }

                        // You can uncomment this section if you want to add delete functionality
                        /*
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.UM_USERID + ");' class='btn btn-danger'>Delete</a>"));
                        } else {
                            $row.append($('<td>').append("<a href='#' class='btn btn-danger disabled'>Delete</a>"));
                        }
                        */

                        $data.append($row);
                    });

                    $("#update-panel").html($data);
                    $('#tblList').DataTable({
                        "order": [[0, "desc"]]
                    });

                } else {
                    $noData = "<div>No data Found</div>";
                    $("#update-panel").html($noData);
                }
            },
            failure: function () {
                ErrorToast('Something went wrong');
            }
        });
    }, 1000);
}




function Confirm(id) {
    alert('Delete is not allowed for users. Please mark the user as inactive instead.');
    $('#infoModal').modal('show');
}

function validateEmail(email) {
    var emailReg = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
    var valid = emailReg.test(email);

    if (!valid) {
        return false;
    } else {
        return true;
    }
}

function fixIsActiveText() {
    $('#UM_ISACTIVE option').each(function () {
        const val = $(this).val()?.toLowerCase();
        if (val === 'true') {
            $(this).text('Yes');
        } else if (val === 'false') {
            $(this).text('No');
        }
    });
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
