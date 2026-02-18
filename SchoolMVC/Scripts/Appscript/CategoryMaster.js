
    $(document).ready(function () {
        CategoryMasterList();
   
    })
    function ValidateCategoryMaster() {

        if ($.trim($("#CM_CategoryCode").val()) === "") {
            alert("Category Code can not be blank");
            $("#CM_CategoryCode").focus();
            return false;
        }

        if ($.trim($("#CM_CategoryName").val()) === "") {
            alert("Category Name can not be blank");
            $("#CM_CategoryName").focus();
            return false;
        }

        return true;
    }

function SaveCategoryMaster() {
    if (ValidateCategoryMaster() == true) {
        var _data = JSON.stringify({
            SettingId: $('#CM_CategoryId').val(),
            SchoolId: $('#CM_SchoolId').val(),
            MaxIssueDays: $('#CM_CategoryCode').val(),
            FinePerDay: $('#CM_CategoryName').val(),
            MaxBooksStudent: $('#CM_ParentCategoryId').val(),
            Active: $('#CM_IsActive').val(),
            Userid: $('#hdnUserid').val()
        });

        $.ajax({
            url: '/JQuery/InsertUpdateCategoryMaster',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    $('#btnSave').attr('disabled', 'disabled');
                    setTimeout(function () {
                        window.location.href = '/Library/CategoryMasters';
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

function CategoryMasterList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        $.ajax({
            url: '/JQuery/GetCategoryMasterList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th style=display:none>Id</th><th>Serial No.</th><th>Category Code</th><th>Category Name</th><th>Parent category Id</th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td style=display:none>').append(row.CM_CategoryId));
                        $row.append($('<td>').append(i + 1));
                        $row.append($('<td>').append(row.CM_CategoryCode));
                        $row.append($('<td>').append(row.CM_CategoryName));
                        $row.append($('<td>').append(row.CM_ParentCategoryId));
                 
                        if (res.CanEdit == true) {
                            // $row.append($('<td>').append("<a href=/Library/BookMaster/id=" + row.BM_BookId + " class='btn btn-warning'>Edit</a>"));
                            $row.append($('<td>').append("<a href=/Library/CategoryMasters/" + row.CM_CategoryId + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.CM_CategoryId + ");'" + row.CM_CategoryId + " class='btn btn-danger'>Delete</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-danger disabled'>Delete</a>"));

                        }
                        $data.append($row);
                    });

                    $("#update-panel").html($data);
                    $('#tblList').DataTable({
                        "order": [[1, "desc"]]
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
    });

   
}
