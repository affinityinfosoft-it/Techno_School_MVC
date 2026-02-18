
    $(document).ready(function () {
        RoleList();
   
    })
function ValidateRoleMaster() {
    if ($.trim($("#RoleName").val()) == "") {
        alert("Role Name can not be blank")
        return false;
    }
    return true;
}
function SaveRole() {
    if (ValidateRoleMaster() == true) {
        var RoleId = 0;
        if ($('#RoleId').val() > 0) {
            RoleId = $('#RoleId').val();
        }

        var _data = JSON.stringify({
            RoleMasterModel: {
                RoleId: parseInt(RoleId),
                RoleName: $("#RoleName").val()
            }

        });

        var url = "/Admin/RoleMaster";

        $.ajax({
            url: url,
            contentType: "application/json",
            dataType: "json",
            type: "POST",
            data: _data,
            //success: function (response) {
            //    if (response != null && response != undefined) {
            //        alert('Save successfylly');
                        
            //        window.location.href = '/Admin/RoleMasters';
            //    } else {
            //        alert('msg');
                        
            //    }
            //},
            //error: function (jqxhr, settings, thrownError) {
            //    console.log(jqxhr.status + '\n' + thrownError);
            //}



            //Add by Uttaran 27/11/2024
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    $('#SaveRole').attr('disabled', 'disabled');
                    setTimeout(function () {

                        window.location.href = '/Admin/RoleMasters';
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

function dt() {

    $('.js-exportable').DataTable({
        dom: 'Bfrtip',
        responsive: true,
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
    });
}

//Complain Master List 
function RoleList() {
    $("#update-panel").html("Loading data.....");

    var _data = JSON.stringify({
        global: {
            TransactionType: 'SELECT_All',
            StoreProcedure: 'SP_RoleMaster'


        }
    });


    $.ajax({
        url: '/Admin/GetRoleMasterList',
        type: 'POST',
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: _data,
        success: function (data) {
            if (data.length > 0) {
                $data = $('<table></table>').addClass('table table-bordered table-striped table-hover dataTable js-exportable');
                $header = "<thead><tr><th style=display:none>Id</th><th>Complain Name</th><th></th></tr></thead>";
                $data.append($header);
                //$("#update-panel").html($data);
                $.each(data, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td style=display:none>').append(row.RoleId));
                    $row.append($('<td>').append(row.RoleName));

                    $row.append($('<td>').append("<a href='#' onclick=GetRole("+row.RoleId+") class='btn btn-warning'>Edit</a>"));
                    //$row.append($('<td>').append("<a href='#' onclick=DeleteRole(" + row.RoleId + ") class='btn btn-danger'>Delete</a>"));
                    $data.append($row);
                });
                $("#update-panel").html($data);
                dt();
            }
            else {
                $noData = "<div>No data Found</td>"
                $("#update-panel").html($noData);
            }

        },
        failure: function () {
            alert('Something wrong happen');
        }
    });

}
// add uttaran 27/11/24
function DeleteRole(fieldId) {
    // Display a confirmation message
    if (confirm("Are you sure you want to delete this record?")) {
        var _data = JSON.stringify({
            MainTableName: 'RoleMasters',
            MainFieldName: 'RoleId',
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
                        window.location.href = '/Admin/RoleMasters';
                    }, 2000);
                } else {
                    ErrorToast(data.Message);
                }
            },
            error: function () {
                ErrorToast('Something wrong happened.');
            }
        });
    } else {
        // Action canceled by the user
        console.log("Delete operation canceled.");
    }
}



function GetRole(id)
{
    window.location.href = '/Admin/RoleMasters?id=' + id;
}
