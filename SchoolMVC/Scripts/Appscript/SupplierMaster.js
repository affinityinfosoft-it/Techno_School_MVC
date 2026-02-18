
    $(document).ready(function () {
        SupplierMasterList();
   
    })
    function ValidateSupplierMaster() {
        if ($.trim($("#SupplierName").val()) == "") {
            alert("Supplier Name can not be blank")
        return false;
        }
        if ($.trim($("#Mobile").val()) === "") {
            alert("Mobile can not be blank");
            return false;
        }
        if ($.trim($("#Email").val()) === "") {
            alert("Email can not be blank");
            return false;
        }
        if ($.trim($("#Address").val()) === "") {
            alert("Address can not be blank");
            return false;
        }
    return true;
}
    function SaveSupplierMaster() {
        if (ValidateSupplierMaster() == true) {
        var _data = JSON.stringify({
            SupplierId: $('#SM_SupplierId').val(),
            SchoolId: $('#SM_SchoolId').val(),
            SupplierName: $('#SM_SupplierName').val(),
            Mobile: $('#SM_Mobile').val(),
            Email: $('#SM_Email').val(),
            Address: $('#SM_Address').val(),
            Active: $('#SM_Active').val(),
            Userid: $('#hdnUserid').val()
        });

        $.ajax({
            url: '/JQuery/InsertUpdateSupplierMaster',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    $('#btnSave').attr('disabled', 'disabled');
                    setTimeout(function () {
                        window.location.href = '/Library/SupplierMasters';
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
    function SupplierMasterList() {
        $('#update-panel').html('loading data.....');

        $.ajax({
            url: '/JQuery/GetSupplierMasterList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {

                if (res.Data && res.Data.length > 0) {

                    var $table = $('<table id="tblList" class="table table-bordered table-striped"></table>');
                    var $thead = $('<thead><tr>' +
                        '<th style="display:none">Id</th>' +
                        '<th>Serial No.</th>' +
                        '<th>Supplier Name</th>' +
                        '<th>Mobile</th>' +
                        '<th>Email</th>' +
                        '<th>Address</th>' +
                        '<th>Action</th>' +
                        '</tr></thead>');

                    var $tbody = $('<tbody></tbody>');

                    $.each(res.Data, function (i, row) {
                        var $tr = $('<tr></tr>');
                        $tr.append('<td style="display:none">' + row.SM_SupplierId + '</td>');
                        $tr.append('<td>' + (i + 1) + '</td>');
                        $tr.append('<td>' + row.SM_SupplierName + '</td>');
                        $tr.append('<td>' + row.SM_Mobile + '</td>');
                        $tr.append('<td>' + row.SM_Email + '</td>');
                        $tr.append('<td>' + row.SM_Address + '</td>');

                        var actionHtml = '';
                        //if (res.CanEdit) {
                        actionHtml += '<a href="/Library/SupplierMasters/' + row.SM_SupplierId + '" class="btn btn-warning">Edit</a> ';
                           // $row.append($('<td>').append("<a href='#' onclick=GetSupplier(" + row.SupplierId + ") class='btn btn-warning'>Edit</a>"));
                        //} else {
                        //    actionHtml += '<a href="#" class="btn btn-warning disabled">Edit</a> ';
                        //}

                        

                        $tr.append('<td>' + actionHtml + '</td>');
                        $tbody.append($tr);
                    });

                    $table.append($thead).append($tbody);
                    $('#update-panel').html($table);

                    // SAFE DataTable init
                    $('#tblList').DataTable({
                        order: [[1, 'desc']]
                    });
                }
                else {
                    $('#update-panel').html('<div>No data found</div>');
                }
            },
            error: function () {
                ErrorToast('Something went wrong');
            }
        });
    }

    function GetSupplier(id) {
        window.location.href = '/Admin/SupplierMasters?id=' + id;
    }

    //function SupplierMasterList() {
    //    $('#update-panel').html('loading data.....');

    //    $.ajax({
    //        url: '/JQuery/GetSupplierMasterList',
    //        dataType: 'json',
    //        type: 'GET',
    //        success: function (res) {
    //            if (res.Data.length > 0) {
    //                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
    //                $header = "<thead><tr><th style=display:none>Id</th><th>Serial No.</th><th>Supplier Name</th><th>Mobile</th><th>Email</th><th>Address</th><th>Action</th></tr></thead>";
    //                $data.append($header);

    //                $.each(res.Data, function (i, row) {
    //                    var $row = $('<tr/>');
    //                    $row.append($('<td style=display:none>').append(row.SupplierId));
    //                    $row.append($('<td>').append(i + 1));
    //                    $row.append($('<td>').append(row.SupplierName));
    //                    $row.append($('<td>').append(row.Mobile));
    //                    $row.append($('<td>').append(row.Email));
    //                    $row.append($('<td>').append(row.Address));
    //                    if (res.CanEdit == true) {
    //                        $row.append($('<td>').append("<a href=/Library/SupplierMasters/" + row.SupplierId + " class='btn btn-warning'>Edit</a>"));
    //                    }
    //                    else {
    //                        $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
    //                    }
    //                    if (res.CanDelete == true) {
    //                        $row.append($('<td>').append("<a onclick='Confirm(" + row.SupplierId + ");'" + row.SupplierId + " class='btn btn-danger'>Delete</a>"));
    //                    }
    //                    else {
    //                        $row.append($('<td>').append("<a href=# class='btn btn-danger disabled'>Delete</a>"));

    //                    }
    //                    $data.append($row);
    //                });

    //                $('#update-panel').html($data);

    //                $('#tblList').DataTable({
    //                    order: [[1, 'desc']]
    //                });
    //            } else {
    //                $('#update-panel').html('<div>No data found</div>');
    //            }
    //        },
    //        error: function () {
    //            ErrorToast('Something went wrong');
    //        }
    //    });
    //}
