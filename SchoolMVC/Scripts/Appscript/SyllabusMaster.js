$(document).ready(function () {
    ClassWiseSyllabusList();
});
// Insert & Update Syllabus 
function InsertUpdateSyllabus() {
    var fileUpload = $("#SM_UploadFile").get(0);
    getFiles(fileUpload);
    Documentfile = $('#SM_UploadFile').val().substring(12);
    if ($('#SM_UploadFile').val() != '' && $('#SM_UploadFile').val() != null) {
        //if ($('#hdnImage').val() == '~/UploadFile/' || $('#hdnImage').val() == null || $('#hdnImage').val() == '') {
        Documentfile = $('#SM_UploadFile').val().substring(12);
        Documentpath2 = '~/UploadSyllabus/' + Documentfile;
    }
    else {
        Documentpath2 = $('#hdnImage').val();
    }
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({


            SM_Id: $('#SM_Id').val(),
            SM_SyllabusName: $('#SM_SyllabusName').val(),
            SM_UploadFile: Documentpath2,
            SM_ClassId: $('#SM_ClassId').val(),
            Userid: $('#hdnUserid').val()

        });

        $.ajax({
            url: '/JQuery/InsertUpdateSyllabus',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    $('#btnSave').attr('disabled', 'disabled');
                    setTimeout(function () {
                        window.location.href = '/Masters/ClassWiseSyllabusList';
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
function ClassWiseSyllabusList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {

        $.ajax({
            url: '/JQuery/ClassWiseSyllabusList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>Class</th><th>Syllabus</th><th></th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td>').text(i + 1));
                        $row.append($('<td style=display:none>').append(row.NM_Id));

                        $row.append($('<td>').append(row.SM_ClassName));
                        $row.append($('<td>').append(row.SM_SyllabusName));

                        if (res.CanView == true) {
                            $row.append($('<td>').append("<a href=/Masters/ClassWiseSyllabus/" + row.SM_Id + " class='btn btn-warning'>View</a>"));
                        } else {
                            if (row.SM_UploadFile) { // Check if NM_UploadFile is not null or undefined
                                $row.append($('<td>').append('<a href=' + row.SM_UploadFile.replace("~", location.origin) + ' class="btn btn-info" target="_blank">View</a>'));
                            } else {
                                $row.append($('<td>').text('No File Available')); // Handle the case where NM_UploadFile is null or undefined
                            }
                        }


                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/ClassWiseSyllabus/" + row.SM_Id + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.SM_Id + ");'" + row.SM_Id + " class='btn btn-danger'>Delete</a>"));
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
                ErrorToast('something wrong happen');
            }

        });
    }, 1000);

}
function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'SyllabusMaster_SM',
        MainFieldName: 'SM_Id',
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
                    window.location.href = '/Masters/ClassWiseSyllabusList';
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

function ValidateOperation() {

    if ($('#SM_CLASSID').val() == 0) {
        WarningToast('Please select a class.');
        return false;
    }
    if ($('#SM_SyllabusName').val() == "") {
        WarningToast('Please provide a Syllabus name');
        return false;
    }
    return true;
}
function getFiles(uploadFileName) {
    var files = uploadFileName.files;
    var data = new FormData();
    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
        data.append("fileName", files[i].name);
    }

    $.ajax({
        url: "/api/FileUload/UploadFiles",
        type: "POST",
        data: data,
        contentType: false,
        processData: false,
        async: false,
        success: function (result) {
        },
        error: function (err) {
        }
    });
}


