$(document).ready(function () {
        MenuList();

    })
function ValidateMenuMaster() {
    if ($.trim($("#MenuName").val()) == "") {
        alert("Menu Name can not be blank")
        return false;
    }
    return true;
}
function SaveMenu() {
    if (ValidateMenuMaster() == true) {
        var MenuId = 0;
        if ($('#MenuId').val() > 0) {
            MenuId = $('#MenuId').val();
        }

        var _data = JSON.stringify({
            MenuMasterModel: {
                MenuId: parseInt(MenuId),
                MenuName: $("#MenuName").val(),
                ParentMenuId: $("#ParentMenuId").val(),
                ModuleId: $("#ModuleId").val(),
                ActionUrl: $("#ActionUrl").val(),
                DisplayPosition: $("#DisplayPosition").val()
            }

        });

        var url = "/Admin/MenuMaster";
        // var urlapi = "http://localhost:44060/api/asset/craetecomplain"


        $.ajax({
            url: url,
            contentType: "application/json",
            dataType: "json",
            type: "POST",
            data: _data,
            //            success: function (response) {
            //                if (response != null && response != undefined) {
            //                    alert('save successfylly');
            //                    //if (response != null && response != undefined && response.IsSuccess == true) {
            //                    //alert(response.Message);
            //                    window.location.href = '/Admin/MenuMasters';
            //                } else {
            //                    alert('msg');
            //                    //alert(response.Message);
            //                }
            //            },
            //            error: function (jqxhr, settings, thrownError) {
            //                console.log(jqxhr.status + '\n' + thrownError);
            //            }
            //        });
            //    }
            //}
            //Add by Uttaran 27/11/2024
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    $('#SaveMenu').attr('disabled', 'disabled');
                    setTimeout(function () {

                        window.location.href = '/Admin/MenuMasters';
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
function MenuList() {
    $("#update-panel").html("Loading data.....");

    var _data = JSON.stringify({
        global: {
            TransactionType: 'SELECT_All',
            StoreProcedure: 'SP_MenuMaster'


        }
    });


    $.ajax({
        url: '/Admin/GetMenuMasterList',
        type: 'POST',
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: _data,
        success: function (data) {
            if (data.length > 0) {
                $data = $('<table></table>').addClass('table table-bordered table-striped table-hover dataTable js-exportable');
                $header = "<thead><tr><th style=display:none>Id</th><th>Menu Name</th><th></th></tr></thead>";
                $data.append($header);
                //$("#update-panel").html($data);
                $.each(data, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td style=display:none>').append(row.MenuId));
                    $row.append($('<td>').append(row.MenuName));

                    $row.append($('<td>').append("<a href='#' onclick=GetMenu(" + row.MenuId + ") class='btn btn-warning'>Edit</a>"));
                   // $row.append($('<td>').append("<a href='#' onclick=DeleteMenu(" + row.MenuId + ") class='btn btn-danger'>Delete</a>"));


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
            alert('something wrong happen');
        }
    });

}

 

function GetMenu(id) {
    window.location.href = '/Admin/MenuMasters?id=' + id;
}


// add uttaran 27/11/24
function DeleteMenu(fieldId) {
    // Display a confirmation message
    if (confirm("Are you sure you want to delete this record?")) {
        var _data = JSON.stringify({
            MainTableName: 'MenuMasters',
            MainFieldName: 'MenuId',
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
                        window.location.href = '/Admin/MenuMasters';
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

