$(document).ready(function () {
    BindMemberList();
});
function InsertUpdateMemberMaster() {
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
            MM_MemberId: $('#MM_MemberId').val(),
            MM_SchoolId: $('#MM_SchoolId').val(),
            //MM_MemberCode: $('#MM_MemberCode').val(),
            MM_MemberTypeId: $('#MM_MemberTypeId').val(),
            MM_FirstName: $('#MM_FirstName').val(),
            MM_LastName: $('#MM_LastName').val(),
            MM_Email: $('#MM_Email').val(),
            MM_Address: $('#MM_Address').val(),
            MM_Mobile: $('#MM_Mobile').val(),
            MM_Active: $('#MM_Active').val(),
            Userid: $('#hdnUserid').val()
        });

        $.ajax({
            url: '/JQuery/InsertUpdateMemberMaster',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    $('#btnSave').attr('disabled', 'disabled');
                    setTimeout(function () {
                        window.location.href = '/Library/MemberMasterList';
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

    if ($('#MM_MemberTypeId').val() == "") {
        WarningToast('Please select Member Type.');
        return false;
    }
    if ($('#MM_FirstName').val() == "") {
        WarningToast('Please provide First Name.');
        return false;
    }
    if ($('#MM_LastName').val() == "") {
        WarningToast('Please provide a Last Name.');
        return false;
    }
    if ($('#MM_Email').val() == "") {
        WarningToast('Please provide a Email.');
        return false;
    }
    if ($('#MM_Address').val() == "") {
        WarningToast('Please provide Address.');
        return false;
    }
    if ($('#MM_Mobile').val() == "") {
        WarningToast('Please provide a Mobile.');
        return false;

        if ($('#MM_Active').val() == "") {
            WarningToast('Please check the active field.');
            return false;
        }
        return true;
    }
}
function BindMemberList() {

        $('#update-panel').html('loading data.....');

        $.ajax({
            url: '/JQuery/MemberMasterList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {

                if (res.Data && res.Data.length > 0) {

                    var $table = $('<table id="tblList" class="table table-bordered table-striped"></table>');

                    var $thead = $('<thead><tr>' +
                        '<th style="display:none">Id</th>' +
                        '<th>Serial No.</th>' +
                        '<th>Member Code</th>' +
                        '<th>Member Name</th>' +
                        '<th>Member Type</th>' +
                        '<th>Email</th>' +
                        '<th>Address</th>' +
                        '<th>Mobile</th>' +
                        '<th>Active</th>' +
                        '<th>Action</th>' +
                        '</tr></thead>');

                    var $tbody = $('<tbody></tbody>');

                    $.each(res.Data, function (i, row) {

                        var $tr = $('<tr></tr>');

                        $tr.append('<td style="display:none">' + row.MM_MemberId + '</td>');
                        $tr.append('<td>' + (i + 1) + '</td>');
                        $tr.append('<td>' + row.MM_MemberCode + '</td>');
                        $tr.append('<td>' + row.MemberName + '</td>');
                        $tr.append('<td>' + row.TypeName + '</td>');
                        $tr.append('<td>' + row.MM_Email + '</td>');
                        $tr.append('<td>' + row.MM_Address + '</td>');
                        $tr.append('<td>' + row.MM_Mobile + '</td>');
                        $tr.append('<td>' + row.MM_Active + '</td>');

                        var actionHtml = '';

                        if (res.CanEdit) {
                            actionHtml += '<a href="/Library/MemberMaster/' + row.MM_MemberId + '" class="btn btn-warning">Edit</a> ';
                        } else {
                            actionHtml += '<a href="#" class="btn btn-warning disabled">Edit</a> ';
                        }

                        if (res.CanDelete) {
                            actionHtml += '<a href="#" onclick="Confirm(' + row.MM_MemberId + ')" class="btn btn-danger">Delete</a>';
                        } else {
                            actionHtml += '<a href="#" class="btn btn-danger disabled">Delete</a>';
                        }

                        $tr.append('<td>' + actionHtml + '</td>');

                        $tbody.append($tr);
                    });

                    $table.append($thead).append($tbody);
                    $('#update-panel').html($table);

                    $('#tblList').DataTable({
                        order: [[1, 'desc']]
                    });
                }
                else {
                    $('#update-panel').html('<div>No data Found</div>');
                }
            },
            error: function () {
                ErrorToast('Something went wrong');
            }
        });
    }
