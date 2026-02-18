//function LoadMenuList() {
//    //if (IsValid()) {

//    //$('#SpnResult').html("");
//    var RoleId = $('#RoleId option:selected').val();
//    var ModuleId = $('#ModuleId option:selected').val();
//    var SP_Name = "SP_GetAccessRights";
//    if (RoleId == "") {
//        alert("Please Select Role");
//        return false;
//    }
//    if (ModuleId == "") {
//        alert("Please Select Correct Module");
//        return false;
//    }
//    var _data = JSON.stringify({
//        RoleId: RoleId,
//        ModuleId: ModuleId,
//        SP_Name: SP_Name
//    });


//    $.ajax({
//        url: '/Admin/AccessRightsList',
//        type: 'POST',
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: _data,
//        success: function (data) {
//            if (data.lstAccessRights.length > 0) {
//                $data = $('<table style="border:none;width:99% !important;" id="Access_RightList"></table>').addClass('table1 table-condensed Table_Text_Align Access_RightList');
//                $header = "<thead><tr><th class=col-md-3>Menu</th><th class=col-md-2>View</th><th class=col-md-2>Add</th><th class=col-md-2>Edit</th><th class=col-md-2>Delete</th><th>Save/Submit</th><th style=display:none></th></tr></thead>";
//                $data.append($header);
//                //$("#update-panel").html($data);
//                $.each(data.lstAccessRights, function (i, row) {
//                    var $row = $('<tr/>');
//                    $row.append($('<td class=col-md-4>').append(row.MenuName));
//                    if (row.CanView == true) {
//                        $row.append($('<td/>').html('<input class=chkCanView type=checkbox checked=checked"  />'));

//                    }
//                    else {
//                        $row.append($('<td/>').html('<input class=chkCanView type=checkbox  />'));
//                    }
//                    if (row.CanAdd == true) {
//                        $row.append($('<td/>').html('<input class=chkCanAdd type=checkbox checked=checked"  />'));

//                    }
//                    else {
//                        $row.append($('<td/>').html('<input class=chkCanAdd type=checkbox  />'));
//                    }

//                    if (row.CanEdit == true) {
//                        $row.append($('<td/>').html('<input class=chkCanEdit type=checkbox checked=checked"  />'));

//                    }
//                    else {
//                        $row.append($('<td/>').html('<input class=chkCanEdit type=checkbox  />'));
//                    }
//                    if (row.CanDelete == true) {
//                        $row.append($('<td/>').html('<input class=chkCanDelete type=checkbox checked=checked"  />'));

//                    }
//                    else {
//                        $row.append($('<td/>').html('<input class=chkCanDelete type=checkbox  />'));
//                    }
//                    if (row.CanSubmit == true) {
//                        $row.append($('<td/>').html('<input class=chkCanSubmit type=checkbox checked=checked"  />'));

//                    }
//                    else {
//                        $row.append($('<td/>').html('<input class=chkCanSubmit type=checkbox  />'));
//                    }

//                    $row.append($('<td style=display:none>').html(row.MenuId));
//                    $data.append($row);
//                });
//                $("#DivMenuList").html($data);
//            }
//            else {
//                $noData = "<div>No data Found</td>"
//                $("#DivMenuList").html($noData);
//            }

//        },
//        failure: function () {
//            alert('something wrong happen');
//        }
//    });

//}

//function InsertUpdateAssignRights() {
//    var AccessRightsItem = [];
//    var tblAccess_RightList = document.getElementById('Access_RightList');
//    for (var i = 0; i < tblAccess_RightList.rows.length; i++) {
//        if (parseFloat($("#Access_RightList>tbody:eq(0) tr:eq(" + i + ") td:eq(6)").text().trim()) > 0) {
//            AccessRightsItem.push(
//                {
//                    CanView: $("#Access_RightList>tbody:eq(0) tr:eq(" + i + ") td:eq(1)").find('input[type="checkbox"]').is(":checked"),
//                    CanAdd: $("#Access_RightList>tbody:eq(0) tr:eq(" + i + ") td:eq(2)").find('input[type="checkbox"]').is(":checked"),
//                    CanEdit: $("#Access_RightList>tbody:eq(0) tr:eq(" + i + ") td:eq(3)").find('input[type="checkbox"]').is(":checked"),
//                    CanDelete: $("#Access_RightList>tbody:eq(0) tr:eq(" + i + ") td:eq(4)").find('input[type="checkbox"]').is(":checked"),
//                    CanSubmit: $("#Access_RightList>tbody:eq(0) tr:eq(" + i + ") td:eq(5)").find('input[type="checkbox"]').is(":checked"),
//                    MenuId: $("#Access_RightList>tbody:eq(0) tr:eq(" + i + ") td:eq(6)").text().trim()
//                });
//        }
//    }


//    var _data = JSON.stringify({

//        AccessRightsVM: {
//            RoleId: $("#RoleId").val(),
//            ModuleId: $("#ModuleId").val(),
//            lstAccessRights: AccessRightsItem


//        }
//    });
//    var RId = $("#RoleId").val();
//    var MId = $("#ModuleId").val();

//    var spname = "SP_GetAccessRights";
//    var url = "/Admib/AccessRights";

//    $.ajax({
//        url: "/Admin/AccessRights",
//        contentType: "application/json",
//        dataType: "json",
//        type: "POST",
//        data: _data,
//        success: function (response) {
//            if (response != null && response != undefined && response.IsSuccess == true) {

//                alert(response.Message);
//            } else {
//                alert(response.Message);
//            }
//        },
//        error: function (jqxhr, settings, thrownError) {
//            console.log(jqxhr.status + '\n' + thrownError);
//        }
//    });
//}


function LoadMenuList() {
    var RoleId = $('#RoleId').val();
    var ModuleId = $('#ModuleId').val();

    if (!RoleId) {
        alert("Please select Role");
        return;
    }
    if (!ModuleId) {
        alert("Please select Module");
        return;
    }

    var _data = JSON.stringify({
        RoleId: RoleId,
        ModuleId: ModuleId,
        SP_Name: "SP_GetAccessRights"
    });

    $.ajax({
        url: '/Admin/AccessRightsList',
        type: 'POST',
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: _data,
        success: function (data) {
            console.log("Access Rights Data:", data);

            if (data && data.lstAccessRights && data.lstAccessRights.length > 0) {

                var html = '';
                html += '<table id="Access_RightList" class="table table-bordered table-hover">';
                html += '<thead><tr>';
                html += '<th><input type="checkbox" id="chkSelectAllMenu" title="Select All Menus" /> Menu</th>';
                html += '<th>View <input type="checkbox" id="chkSelectAllView" /></th>';
                html += '<th>Add <input type="checkbox" id="chkSelectAllAdd" /></th>';
                html += '<th>Edit <input type="checkbox" id="chkSelectAllEdit" /></th>';
                html += '<th>Delete <input type="checkbox" id="chkSelectAllDelete" /></th>';
                html += '<th>Submit <input type="checkbox" id="chkSelectAllSubmit" /></th>';
                html += '<th style="display:none">MenuId</th>';
                html += '</tr></thead><tbody>';

                $.each(data.lstAccessRights, function (i, row) {
                    html += '<tr>';
                    html += '<td><input type="checkbox" class="chkMenuSelect" /> ' + (row.MenuName || '') + '</td>';
                    html += '<td><input type="checkbox" class="chkCanView" ' + (row.CanView ? 'checked' : '') + ' /></td>';
                    html += '<td><input type="checkbox" class="chkCanAdd" ' + (row.CanAdd ? 'checked' : '') + ' /></td>';
                    html += '<td><input type="checkbox" class="chkCanEdit" ' + (row.CanEdit ? 'checked' : '') + ' /></td>';
                    html += '<td><input type="checkbox" class="chkCanDelete" ' + (row.CanDelete ? 'checked' : '') + ' /></td>';
                    html += '<td><input type="checkbox" class="chkCanSubmit" ' + (row.CanSubmit ? 'checked' : '') + ' /></td>';
                    html += '<td style="display:none;">' + (row.MenuId || '') + '</td>';
                    html += '</tr>';
                });

                html += '</tbody></table>';

                $("#DivMenuList").html(html);

                // Attach checkbox logic AFTER table is rendered
                bindCheckboxHandlers();
                syncSelectAllStates();
            } else {
                $("#DivMenuList").html('<div class="text-danger">No access rights data found.</div>');
            }
        },
        error: function (xhr, status, error) {
            console.log("Error loading AccessRights:", status, error);
            $("#DivMenuList").html('<div class="text-danger">Error loading access rights.</div>');
        }
    });
}
//  BIND CHECKBOX LOGIC
function bindCheckboxHandlers() {

    // Select all per column
    $('#chkSelectAllView').off('change').on('change', function () {
        $('.chkCanView').prop('checked', $(this).is(':checked'));
        syncSelectAllStates();
    });
    $('#chkSelectAllAdd').off('change').on('change', function () {
        $('.chkCanAdd').prop('checked', $(this).is(':checked'));
        syncSelectAllStates();
    });
    $('#chkSelectAllEdit').off('change').on('change', function () {
        $('.chkCanEdit').prop('checked', $(this).is(':checked'));
        syncSelectAllStates();
    });
    $('#chkSelectAllDelete').off('change').on('change', function () {
        $('.chkCanDelete').prop('checked', $(this).is(':checked'));
        syncSelectAllStates();
    });
    $('#chkSelectAllSubmit').off('change').on('change', function () {
        $('.chkCanSubmit').prop('checked', $(this).is(':checked'));
        syncSelectAllStates();
    });

    // Select all menus
    $('#chkSelectAllMenu').off('change').on('change', function () {
        var checked = $(this).is(':checked'); 
        $('.chkMenuSelect, .chkCanView, .chkCanAdd, .chkCanEdit, .chkCanDelete, .chkCanSubmit').prop('checked', checked);
        syncSelectAllStates();
    });

    // Per-menu checkbox
    $(document).off('change', '.chkMenuSelect').on('change', '.chkMenuSelect', function () {
        var checked = $(this).is(':checked');
        var $row = $(this).closest('tr');
        $row.find('input[type=checkbox]').not('.chkMenuSelect').prop('checked', checked);
        syncSelectAllStates();
    });

    // Individual checkbox sync
    $(document).off('change', '.chkCanView, .chkCanAdd, .chkCanEdit, .chkCanDelete, .chkCanSubmit')
        .on('change', '.chkCanView, .chkCanAdd, .chkCanEdit, .chkCanDelete, .chkCanSubmit', function () {
            syncSelectAllStates();
        });
}

//  Sync header states
function syncSelectAllStates() {
    $('#chkSelectAllView').prop('checked', $('.chkCanView').length === $('.chkCanView:checked').length);
    $('#chkSelectAllAdd').prop('checked', $('.chkCanAdd').length === $('.chkCanAdd:checked').length);
    $('#chkSelectAllEdit').prop('checked', $('.chkCanEdit').length === $('.chkCanEdit:checked').length);
    $('#chkSelectAllDelete').prop('checked', $('.chkCanDelete').length === $('.chkCanDelete:checked').length);
    $('#chkSelectAllSubmit').prop('checked', $('.chkCanSubmit').length === $('.chkCanSubmit:checked').length);
    $('#chkSelectAllMenu').prop('checked', $('.chkMenuSelect').length === $('.chkMenuSelect:checked').length);
}


//  SAVE FUNCTION
function InsertUpdateAssignRights() {
    var AccessRightsItem = [];

    $('#Access_RightList tbody tr').each(function () {
        var tds = $(this).find('td');
        var MenuId = tds.eq(6).text().trim();

        AccessRightsItem.push({
            MenuId: MenuId,
            CanView: tds.eq(1).find('input').is(':checked'),
            CanAdd: tds.eq(2).find('input').is(':checked'),
            CanEdit: tds.eq(3).find('input').is(':checked'),
            CanDelete: tds.eq(4).find('input').is(':checked'),
            CanSubmit: tds.eq(5).find('input').is(':checked')
        });
    });

    var payload = JSON.stringify({
        AccessRightsVM: {
            RoleId: $("#RoleId").val(),
            ModuleId: $("#ModuleId").val(),
            lstAccessRights: AccessRightsItem
        }
    });

    $.ajax({
        url: '/Admin/AccessRights',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: payload,
        success: function (res) {
            if (res && res.IsSuccess) {
                alert(res.Message || 'Saved successfully');
                //  Refresh page after user clicks OK
                location.reload();
            } else {
                alert(res.Message || 'Error saving data');
            }
        },
        error: function (xhr, status, error) {
            console.log("Error saving:", error);
            alert('Error saving access rights.');
        }
    });
}


