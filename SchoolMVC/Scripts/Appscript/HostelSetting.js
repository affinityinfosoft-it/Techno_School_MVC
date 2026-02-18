var DataList = [];
$(document).ready(function () {
   
    $("#SD_ClassId").change(function () {
        AjaxPostForDropDownSection($('#SD_ClassId option:selected').val());
    });
    $("#btnSearch").click(function () {
        bindGrid();
    });
    $("#btnAdd").click(function () {
        AddSubject();
    });
    $("#btnAssign").click(function () {
        if (validateForm() == true)
        {
            AssignHostel();
        }
    });
    $("#btnSearchList").click(function () {
        HostelAvailedStudents();
    });
    $("#btnSave").click(function () {
       
    });
    $("#CheckAll").change(function () {
        $("input:checkbox").prop('checked', $(this).prop("checked"));
    });
});
var tableStudentsArr = [];
var tableAbsntArr = [];
var Id = '';

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
                BindDropDownListForSection($('#ddlSWS_SecId'), data);
                $("#ddlSWS_SecId").selectpicker('refresh');

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
function bindGrid() {
    $('#update-panel').html('loading data.....');
        $("#tblStudent").show();
        if ($('#ddlSWS_SecId').val() == 0) {
            var SecId = '';
        }
        else {
            var SecId = $('#ddlSection').val();
        }
        var ClassId = isNaN(parseInt($('#SD_ClassId option:selected').val())) ? null : parseInt($('#SD_ClassId option:selected').val())
        var _data = JSON.stringify({
            obj: {
                SD_ClassId: ClassId,
                SD_CurrentSectionId: parseInt(SecId),
                SD_StudentId: $('#SD_StudentId').val(),
                SD_StudentName: $('#SD_StudentName').val(),
                SD_CurrentSectionId: $('#ddlSWS_SecId').val(),
            }
        });
        $.ajax({
            url: rootDir + "JQuery/GetStudentListForHostelSettings",
            dataType: 'json',
            contentType: 'application/json',
            data: _data,
            type: 'POST',
            success: function (d) {
                if (d.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th>Student Id</th><th>Student Name</th><th>Class</th><th>Select Student</th></tr></thead>";
                    $data.append($header);
                    $.each(d, function (i, row) {
                        $('#classid').val(row.SD_CurrentClassId);
                        var $row = $('<tr/>');
                        $row.append($('<td>').append(row.SD_StudentId));
                        $row.append($('<td>').append(row.SD_StudentName));
                        $row.append($('<td>').append(row.SD_CM_CLASSNAME));
                        $row.append($('<td/>').html('<input type=checkbox />'));

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
function HostelAvailedStudents() {
    $('#update-panel').html('loading data.....');
    $("#tblStudent").show();
    if ($('#ddlSWS_SecId').val() == 0) {
        var SecId = '';
    }
    else {
        var SecId = $('#ddlSection').val();
    }
    var ClassId = isNaN(parseInt($('#SD_ClassId option:selected').val())) ? null : parseInt($('#SD_ClassId option:selected').val())
    var _data = JSON.stringify({
        obj: {
            SD_ClassId: ClassId,
            SD_CurrentSectionId: parseInt(SecId),
            SD_StudentId: $('#SD_StudentId').val(),
            SD_StudentName: $('#SD_StudentName').val(),
            SD_CurrentSectionId: $('#ddlSWS_SecId').val(),
        }
    });
    $.ajax({
        url: rootDir + "JQuery/HostelAviledStudentList",
        dataType: 'json',
        contentType: 'application/json',
        data: _data,
        type: 'POST',
        success: function (d) {
            if (d.length > 0) {
                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                $header = "<thead><tr><th>SL</th><th>Student Id</th><th>Student Name</th><th>Class</th><th>Hostel Fare</th><th>Action</th></tr></thead>";
                $data.append($header);
                $.each(d, function (i, row) {
                    $('#classid').val(row.SD_CurrentClassId);
                    var $row = $('<tr/>');
                    $row.append($('<td>').text(i + 1));
                    $row.append($('<td>').append(row.SD_StudentId));
                    $row.append($('<td>').append(row.SD_StudentName));
                    $row.append($('<td>').append(row.SD_CM_CLASSNAME));
                    $row.append($('<td>').append(row.SD_HostelFare));
                    $row.append($('<td>').append("<a onclick='Confirm(" + row.SD_StudentId + ");'" + row.SD_StudentId + " class='btn btn-danger'>Delete</a>"));

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
function validateForm() {
    
    if ($("#ddlHostelRoomNo").val() == 0) {
        alert("Please select Room to continue.");
        return false
    }
    if ($("#ddlMonth").val() == 0) {
        alert("Please select Month to continue.");
        return false
    }
    if ($("#txtHostelFare").val() == '') {
        alert("Please put an amount of Hostel Fare.");
        return false
    }
    return true;
}
function GetAllSID() {
    if ($('#tblList>tbody input[type="checkbox"]').is(":checked")) {
        var arrStudentID = [];

        var tblItem = document.getElementById('tblList');
        $('#tblList>tbody input[type="checkbox"]:checked').each(function (i, row) {
            var cellrow = parseInt($(row).closest('tr').index());
            arrStudentID.push(tblList.rows[cellrow + 1].cells[0].innerHTML)
        });
        arrStudentID.join(',');
        $("#myModalStudentData").modal('show');
        return arrStudentID;
    }
    else {
        alert('Please Select Student first.')
    }
}
function AssignHostel() {

    var arrStudentID = GetAllSID();

    var arrList = [];
    var MonthId = $("#ddlMonth").val();
    var Fare = $("#txtHostelFare").val();
    var RoomNo = $("#ddlHostelRoomNo").val();
    for (var i = 0; i < arrStudentID.length; i++) {
        arrList.push({
            Hostel_StudentId: arrStudentID[i],
            Hostel_Month: MonthId,
            Hoset_Fare: Fare,
            Hostel_RoomNo: RoomNo
        });
    }
    var _data = JSON.stringify({
        obj: {
            HostelDataList: arrList,
        }
    });
    $.ajax({
        type: "POST",
        url: '/JQuery/InsertStudentForHostel',
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.IsSuccess == true) {
                alert('Data has been updated.')
                var url = '/Masters/HostelSetting'
                window.location.href = url;

            }
            else {
                ErrorToast(data.Message);
            }
        },
        error: function (data) {
            alert('Process Fail...');
        }
    });

}
function AjaxPostForDropDownLocation(Id) {
    var _data = JSON.stringify({
        id: parseInt(Id),
    });
    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/RoutewiseDropListbyRouteId",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data, status) {
            if (status == "success") {
                DataList = data;
                BindLocationDropDownList($('#ddlPickUpLocation'), data);
                $("#ddlPickUpLocation").selectpicker('refresh');
                BindLocationDropDownList($('#ddlDropLocation'), data);
                $("#ddlDropLocation").selectpicker('refresh');
            } else {
                showMessage("Unable to process the request...", 0);
            }
        }
    });
}
function BindLocationDropDownList(ddl, dataCollection) {
    $(ddl).get(0).length = 0;
    $(ddl).get(0).options[0] = new Option("Select", "0", "Selected");
    console.log(dataCollection);
    if (dataCollection.length != 0) {
        for (var i = 0; i < dataCollection.length; i++) {
            $(ddl).get(0).options[i + 1] = new Option(dataCollection[i].RDM_DROPPOINT, dataCollection[i].RDM_ROUTEMAPID);
        }
    }
}
// Confirm to Delete
function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
// Delete Board From database
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        StudentId: fieldId
    });
    $.ajax({
        url: '/JQuery/DeleteHostelSetting',
        type: 'POST',
        data: _data,
        contentType: 'application/json ; utf-8',
        dataType: 'json',
        success: function (data) {
            if (data != null && data != undefined && data.IsSuccess == true) {
                alert(data.Message);
                window.location.href = '/Masters/HostelAvailedList';
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




