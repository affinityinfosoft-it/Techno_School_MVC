$('#CWTR_Class').change(function () {
    var ClassIs = isNaN(parseInt($('#CWTR_Class').val())) ? null : parseInt($('#CWTR_Class').val())
    AjaxPostForDropDownSection(ClassIs);
});
function AjaxPostForDropDownSection(Id) {
    var _data = JSON.stringify({
        Id: parseInt(Id),
    });
    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/GetSectionByClass",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data, status) {
            if (status == "success") {
                BindDropDownListForSection($('#ddlCWTR_Section'), data);
                $("#ddlCWTR_Section").selectpicker('refresh');
            }
            else {
                showMessage("Unable to process the request...", 0);
            }
        }
    });
}
function BindDropDownListForSection(ddl, dataCollection) {
    $(ddl).get(0).length = 0;
    $(ddl).get(0).options[0] = new Option("Select", "0", "Selected");
    for (var i = 0; i < dataCollection.length; i++) {
        $(ddl).get(0).options[i + 1] = new Option(dataCollection[i].SECM_SECTIONNAME, dataCollection[i].SECM_SECTIONID);
    }
}
function InsertUpdateGrade() {
    if (ValidateOperation() == true) {
        var _data = JSON.stringify({
            GM_Grade_Id: $('#GM_Grade_Id').val(),
            GM_GradeName: $('#GM_GradeName').val(),
            GM_FromGrade: $('#GM_FromGrade').val(),
            GM_ToGrade: $('#GM_ToGrade').val(),
            Userid: $('#hdnUserid').val()

        });

        $.ajax({
            url: '/JQuery/InsertUpdateGrade',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    $('#btnSave').attr('disabled', 'disabled');
                    setTimeout(function () {
                      
                        window.location.href = '/Masters/GradeList';
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

    if ($('#GM_GradeName').val() == "") {
        WarningToast('Please provide a Grade');
        return false;
    }
    if ($('#GM_FromGrade').val() == "") {
        WarningToast('Please provide a Grade limit.');
        return false;
    }
    if ($('#GM_ToGrade').val() == "") {
        WarningToast('Please provide a Grade limit.');
        return false;
    }
    if ($('#GM_ToGrade').val() == $('#GM_FromGrade').val()) {
        WarningToast('From number and To number cannot be same..');
        return false;
    }
    return true;
}
function GradeList() {
    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        //papai
        $.ajax({
            url: '/JQuery/GradeList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>Grade</th><th>From</th><th>To</th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td>').text(i + 1));
                        $row.append($('<td style=display:none>').append(row.GM_Grade_Id));
                        $row.append($('<td>').append(row.GM_GradeName));
                        $row.append($('<td>').append(row.GM_FromGrade));
                        $row.append($('<td>').append(row.GM_ToGrade));
                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/Grade/" + row.GM_Grade_Id + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.GM_Grade_Id + ");'" + row.GM_Grade_Id + " class='btn btn-danger'>Delete</a>"));
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
$(document).ready(function () {
    GradeList();
});
function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'GradeMaster_GM',
        MainFieldName: 'GM_Grade_Id',
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
                    window.location.href = '/Masters/GradeList';
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