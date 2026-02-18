var DataList = [];
$(document).ready(function () {
   
    $("#SD_RouteId").change(function () {
        AjaxPostForDropDownLocation($("#SD_RouteId").val());
    });
        $("#ddlPickUpLocation").change(function () {

            $("#ddlDropLocation").val('');
            $("#ddlDropLocation").selectpicker('refresh');
            $("#ddlDropLocation").attr("data-feesid", parseFloat("0"));
            if ($("#ddlPickUpLocation").val() != '') {
                $('#SD_TransportFare').val('');
                $.each(DataList, function (index, element) {
                    if (element.RDM_ROUTEMAPID == parseFloat($("#ddlPickUpLocation").val())) {
                        if ($('#SD_TransportFare').val() == '') {
                            $('#SD_TransportFare').val("0");

                        }
                        $("#ddlPickUpLocation").attr("data-feesid", parseFloat(element.RDM_RATE));
                        Rate = ((isNaN(parseFloat($('#ddlDropLocation').attr("data-feesid"))) ? 0 : parseFloat($('#ddlDropLocation').attr("data-feesid"))) + parseFloat(element.RDM_RATE));

                    }
                });
                $("#SD_TransportFare").val(Rate);

                Rate = 0;
            }
            else {
                $('#SD_TransportFare').val("0");
            }
        });
        $("#ddlDropLocation").change(function () {
            if ($("#ddlDropLocation").val() != '') {
                $('#SD_TransportFare').val('');

                $.each(DataList, function (index, element) {
                    if (element.RDM_ROUTEMAPID == parseFloat($("#ddlDropLocation").val())) {
                        if ($('#SD_TransportFare').val() == '') {
                            $('#SD_TransportFare').val("0");
                        }
                        var s = parseFloat(element.RDM_RATE) + parseFloat($('#SD_TransportFare').val());
                        $("#ddlDropLocation").attr("data-feesid", parseFloat(element.RDM_RATE));
                        Rate = ((isNaN(parseFloat($('#ddlPickUpLocation').attr("data-feesid"))) ? 0 : parseFloat($('#ddlPickUpLocation').attr("data-feesid"))) + parseFloat(element.RDM_RATE));

                    }
                });


                $("#SD_TransportFare").val(Rate);
                Rate = 0;
            }
            else {
                $('#SD_TransportFare').val('0');
            }
        });
       
        $("#SD_TransportTypeId").change(function () {

            $("#SD_TransportFare").val('');
            if ($('#SD_TransportTypeId').val() == 1) {

                $('#dvpickUp').show();
                $('#dvdropLocation').show();
                var Fees = 0;
                $("#ddlDropLocation").val('');
                $("#ddlDropLocation").attr("data-feesid", Fees);
                $("#ddlDropLocation").selectpicker('refresh');
                $("#ddlPickUpLocation").val('');
                $("#ddlPickUpLocation").attr("data-feesid", Fees);
                $("#ddlPickUpLocation").selectpicker('refresh');
            }
            else {
                $('#dvpickUp').hide();
                $('#dvdropLocation').hide();
                var Fees = 0;
                $("#ddlDropLocation").val('');
                $("#ddlDropLocation").attr("data-feesid", Fees);
                $("#ddlDropLocation").selectpicker('refresh');
                $("#ddlPickUpLocation").val('');
                $("#ddlPickUpLocation").attr("data-feesid", Fees);
                $("#ddlPickUpLocation").selectpicker('refresh');
            }
            if ($('#SD_TransportTypeId').val() == 2) {

                $('#dvpickUp').show();
                $("#ddlDropLocation").val('');
                var Fees = 0;
                $("#ddlDropLocation").val('');
                $("#ddlDropLocation").attr("data-feesid", Fees);
                $("#ddlDropLocation").selectpicker('refresh');
                $("#ddlPickUpLocation").val('');
                $("#ddlPickUpLocation").attr("data-feesid", Fees);
                $("#ddlPickUpLocation").selectpicker('refresh');
            }
            if ($('#SD_TransportTypeId').val() == 3) {

                $('#dvdropLocation').show();
                var Fees = 0;
                $("#ddlDropLocation").val('');
                $("#ddlDropLocation").attr("data-feesid", Fees);
                $("#ddlDropLocation").selectpicker('refresh');
                $("#ddlPickUpLocation").val('');
                $("#ddlPickUpLocation").attr("data-feesid", Fees);
                $("#ddlPickUpLocation").selectpicker('refresh');
            }

        });

    $("#SD_ClassId").change(function () {
        AjaxPostForDropDownSection($('#SD_ClassId option:selected').val());
    });
    $("#btnSearch").click(function () {
        bindGrid();
    });    
    $("#btnAssign").click(function () {

        GetAllSID();
    });
    $("#btnSave").click(function () {
        AssignTransport();
    });
    $("#btnSearchList").click(function () {
        AvailedStudentList();
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
            url: rootDir + "JQuery/GetStudentListForTransportSettings",
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
function AvailedStudentList() {

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
            url: rootDir + "JQuery/TransportAvailedStudentList",
            dataType: 'json',
            contentType: 'application/json',
            data: _data,
            type: 'POST',
            success: function (d) {
                if (d.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th>SL</th><th>Student Id</th><th>Student Name</th><th>Class</th><th>Route</th><th>Pickup Point</th><th>Drop Point</th><th>Total Fare</th><th>Action</th></tr></thead>";
                    $data.append($header);

                    $.each(d, function (i, row) {
                        $('#classid').val(row.SD_CurrentClassId);
                        var $row = $('<tr/>');
                        $row.append($('<td>').text(i + 1));
                        $row.append($('<td>').append(row.SD_StudentId));
                        $row.append($('<td>').append(row.SD_StudentName));
                        $row.append($('<td>').append(row.SD_CM_CLASSNAME));
                       
                        $row.append($('<td>').append(row.SD_ROUTENAME));
                        $row.append($('<td>').append(row.SD_PickPoint));
                        $row.append($('<td>').append(row.SD_DropPoint));
                        $row.append($('<td>').append(row.SD_TransportFare));
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

    if ($("#SD_RouteId").val() == '')  
    {
        alert('Please Seelect a route.')
        return false;
    }
    if ($("#SD_TransportTypeId").val() == '') {
        alert("Please Select Transport Type.");
        return false
    }
    if ($("#SD_TransportTypeId").val() == '') {
        alert("Please Select Transport Type.");
        return false
    }
    if ($("#SD_TransportTypeId").val() == 1) {
        if ($("#ddlPickUpLocation").val() == 0) {
            alert("Please Select Pick up point.");
            return false
        }
        if ($("#ddlDropLocation").val() == 0) {
            alert("Please Select drop point.");
            return false
        }
    }
    if ($("#SD_TransportTypeId").val() == 2) {
        if ($("#ddlPickUpLocation").val() == 0) {
            alert("Please Select Pick up point.");
            return false
        }
    }
    if ($("#SD_TransportTypeId").val() == 3) {
        if ($("#ddlDropLocation").val() == 0) {
            alert("Please Select drop point.");
            return false
        }
    }
    if ($("#ddlMonth").val() == '') {
            alert("Please Select a month.");
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
function AssignTransport() {

    var arrStudentID = GetAllSID();
    if (validateForm() == true) {
        var arrList = [];
        var RouteId = $("#SD_RouteId").val();
        var TypeId = $("#SD_TransportTypeId").val();
        var DropId = $("#ddlDropLocation").val();
        var PickId = $("#ddlPickUpLocation").val();
        var Fares = $("#SD_TransportFare").val();
        var Month = $("#ddlMonth").val();


        for (var i = 0; i < arrStudentID.length; i++) {

            arrList.push({
                SD_StudentId: arrStudentID[i],
                RouteId: RouteId,
                PickUpId: PickId,
                DropId: DropId,
                TypeId: TypeId,
                Fare: Fares,
                TransportMonthId: Month
            });
        }
        var _data = JSON.stringify({
            obj: {
                TransportList: arrList,
            }
        });

        $.ajax({
            type: "POST",
            url: '/JQuery/UpdateTransport',
            data: _data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.IsSuccess == true) {
                    alert('Data has been updated.')
                    var url = '/Masters/TransportSetting'
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
            } else
            {
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
        url: '/JQuery/DeleteTransportFromStudents',
        type: 'POST',
        data: _data,
        contentType: 'application/json ; utf-8',
        dataType: 'json',
        success: function (data) {
            if (data != null && data != undefined && data.IsSuccess == true) {
                alert(data.Message);
                window.location.href = '/Masters/TransportAvailedList';
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



