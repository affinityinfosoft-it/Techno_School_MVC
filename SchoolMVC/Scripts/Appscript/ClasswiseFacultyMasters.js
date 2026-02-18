$(document).ready(function () {

    var isEditMode = $("#CWF_Id").val(); // hidden field value

    // If ADD mode -> load list
    // If EDIT mode -> DO NOT load full list
    if (isEditMode == "" || isEditMode == "0" || isEditMode == null) {
        BindList();   // list page data
    }
});

function BindList() {
    $('#update-panel').html('loading data.....');

    $.ajax({
        url: '/JQuery/ClassWiseFacultyList',
        data: { ClassId: parseInt($("#CWF_ClassId").val()), SubId: parseInt($("#CWF_SubjectId").val()), FacId: parseInt($("#CWF_FacId").val()) },
        dataType: 'json',
        type: 'GET',
        success: function (res) {
            if (res.Data.length > 0) {
                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                $header = "<thead><tr><th style=display:none>Id</th><th>Serial No</th><th>Faculty</th><th>Class</th><th>Subject</th><th></th><th></th></tr></thead>";
                $data.append($header);

                $.each(res.Data, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td style=display:none>').append(row.CWF_Id));
                    $row.append($('<td>').append(i + 1));
                    $row.append($('<td>').append(row.CWF_FPM_FP_Name));
                    $row.append($('<td>').append(row.CWF_CM_CLASSNAME));
                    $row.append($('<td>').append(row.CWF_SBM_SubjectName));

                    if (res.CanEdit == true) {
                        $row.append($('<td>').append("<a href=/Masters/ClassWiseFaculty/" + row.CWF_Id + " class='btn btn-warning'>Edit</a>"));
                    }
                    else {
                        $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                    }
                    if (res.CanDelete == true) {
                        $row.append($('<td>').append("<a onclick='Confirm(" + row.CWF_Id + ");'" + row.CWF_Id + " class='btn btn-danger'>Delete</a>"));
                    }
                    else {
                        $row.append($('<td>').append("<a href=# class='btn btn-danger disabled'>Delete</a>"));
                    }
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
}


function AddDetails() {
    if (!ValidateOperation()) return;

    var selectedClasses = $('#CWF_ClassId').val();   // array
    var selectedSubjects = $('#CWF_SubjectId').val(); // array
    var facId = $('#CWF_FacId').val();               // single select
    var facText = $("#CWF_FacId option:selected").text();

    selectedClasses.forEach(function (clsId) {
        var clsText = $("#CWF_ClassId option[value='" + clsId + "']").text();
        selectedSubjects.forEach(function (subId) {
            var subText = $("#CWF_SubjectId option[value='" + subId + "']").text();

            // Check duplicate
            var exists = false;
            $("#tblList>tbody>tr").each(function () {
                if ($(this).find("td:eq(1)").text() == facId &&
                    $(this).find("td:eq(3)").text() == clsId &&
                    $(this).find("td:eq(5)").text() == subId) {
                    exists = true;
                }
            });

            if (!exists) {
                var $row = $('<tr/>');
                $row.append($('<td style="display:none">').html(''));   // CWF_Id
                $row.append($('<td style="display:none">').html(facId)); // CWF_FacId
                $row.append($('<td/>').html(facText));
                $row.append($('<td style="display:none">').html(clsId));
                $row.append($('<td/>').html(clsText));
                $row.append($('<td style="display:none">').html(subId));
                $row.append($('<td/>').html(subText));
                $row.append($('<td>').append("<a onclick='EditDetails(this);' class='btn btn-warning' href='#'>Edit</a>"));
                $row.append($('<td>').append("<input type='image' src='/Content/images/delete.png' onclick='deleteRow(this);'>"));
                $("#tblList>tbody").append($row);
            }
        });
    });

    ClearDetails();
}
function EditDetails(r) {
    var row = parseInt($(r).closest('tr').index());
    var check = $("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(7)").text();
    $("#CWF_FacId").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(1)").text());
    $("#CWF_FacId").selectpicker('refresh');
    $("#CWF_ClassId").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(3)").text());
    $("#CWF_ClassId").selectpicker('refresh');
    $("#CWF_SubjectId").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(7)").text());
    $("#CWF_SubjectId").selectpicker('refresh');
    $("#ddlCWF_SectionId").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(5)").text());
    $("#ddlCWF_SectionId").selectpicker('refresh');
    deleteRowWithOutAlert(r);
}

function ClearDetails() {
    $("#CWF_FacId").val('').selectpicker('refresh');
    $("#CWF_ClassId").val([]).selectpicker('refresh');
    $("#CWF_SubjectId").val([]).selectpicker('refresh');
}
function FinalValidation() {
    if ($("#tblList > tbody > tr").length == 0) {
        ErrorToast("Please add any Details!")
        return false;
    }
    return true;
}
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
                BindDropDownListForSection($('#ddlCWF_SectionId'), data);
                $("#ddlCWF_SectionId").selectpicker('refresh');

            } else {
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
function ValidateOperation() {

    if ($('#CWF_FacId').val() == 0) {
        alert('Please select Faculty.');
        return false;
    }
    if ($('#CWF_ClassId').val() == 0) {
        alert('Please select a Class.');
        return false;
    }
    if ($('#CWF_SubjectId').val() == 0) {
        alert('Please select a Subject.');
        return false;
    }

    var classvalue = $('#CWF_ClassId option:selected').text();
    var subject = $('#CWF_SubjectId option:selected').text();
    var Fac = $('#CWF_FacId option:selected').text();
    var sec = $('#ddlCWF_SectionId option:selected').text();
    var tblItem = document.getElementById('tblList');
    if ($("#tblList>tbody:eq(0) tr:eq(" + 0 + ") td:eq(2)").text() == '') {

        $("#tblList").find('tbody').empty();

    }
    var len = tblItem.rows.length;
    for (var i = 1; i < len; i++) {

        if (Fac == tblItem.rows[i].cells[2].innerHTML && classvalue == tblItem.rows[i].cells[4].innerHTML && sec == tblItem.rows[i].cells[6].innerHTML && subject == tblItem.rows[i].cells[8].innerHTML) {
            alert('This Combination already exist.')
            return false;
        }
        else {
            continue;
        }

    }

    return true;
}
function InsertUpdateClasswiseFaculty() {
    if (!FinalValidation()) return;

    var ArrayItem = [];
    $("#tblList>tbody>tr").each(function () {
        ArrayItem.push({
            CWF_FacId: $(this).find("td:eq(1)").text(),
            CWF_ClassId: $(this).find("td:eq(3)").text(),
            CWF_SubjectId: $(this).find("td:eq(5)").text()
        });
    });

    var _data = JSON.stringify({
        CWF_Id: $('#CWF_Id').val(),
        ClasswiseFacultyList: ArrayItem,
        Userid: $('#hdnUserid').val()
    });

    $.ajax({
        url: '/JQuery/InsertUpdateClasswiseFaculty',
        data: _data,
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data && data.IsSuccess) {
                alert(data.Message);
                window.location.href = '/Masters/ClassWiseFacultyList';
            } else {
                alert(data.Message);
            }
        },
        error: function () {
            alert('Something wrong happened.');
        }
    });
}
function Confirm(id) {
    swal({
        title: "Are you sure?",
        text: "You will not be able to recover this imaginary file!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel plx!",
        closeOnConfirm: false,
        closeOnCancel: false
    },
    function (isConfirm) {
        if (isConfirm) {
            Deletedata(id);
        }
    });
   
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'ClassWiseFacultyMasters_CWF',
        MainFieldName: 'CWF_Id',
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
                alert(data.Message);
                window.location.href = '/Masters/ClassWiseFacultyList';
            }
            else {
                alert(data.Message);
            }
        },
        error: function () {
            alert('Something wrong happend.');
        }
    });
}
//Delete Table Row by Row No Without Alert
function deleteRowWithOutAlert(rowNo) {
    $(rowNo).closest('tr').remove();
}
function deleteRow(rowNo) {
    var agree = confirm("Are you sure you want to delete this record?");
    if (agree) {
        $(rowNo).closest('tr').remove();
        return true;
    }
    else {
        return false;
    }

}