var DataList = [];
var Rate = 0;
function formatJsonDate(jsonDate) {
    if (!jsonDate) return '';
    var timestamp = parseInt(jsonDate.replace(/\/Date\((\d+)\)\//, '$1'));
    var date = new Date(timestamp);
    var day = ('0' + date.getDate()).slice(-2);
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var year = date.getFullYear();
    return day + '/' + month + '/' + year; // dd/MM/yyyy
}

function convertToSqlDate(dateStr) {
    if (!dateStr) return null;

    var parts = dateStr.split('/'); // dd-MM-yyyy
    return parts[2] + '-' + parts[1] + '-' + parts[0]; // yyyy-MM-dd
}

function ValidateOperation() {

    if ($('#SD_FormNo').val() == "") {
        WarningToast('Please provide Form No.');
        return false;
    }
    if ($('#SD_StudentName').val() == "") {
        WarningToast('Please provide Student Name.');
        return false;
    }
    if ($('#SD_FatherName').val() == "") {
        WarningToast('Please provide Student Fathers Name.');
        return false;
    }
    if ($('#SD_MotherName').val() == "") {
        WarningToast('Please provide Student Mothers Name.');
        return false;
    }
    
    if ($('#SD_DOB').val() == "") {
        WarningToast('Please provide DOB.');
        return false;
    }
    if ($('#Sd_SexId').val() == "") {
        WarningToast('Please Select Gender.');
        return false;
    }
    if ($('#SD_ClassId').val() == "") {
        WarningToast('Please Select Class.');
        return false;
    }
    if ($('#SD_NationalityId').val() == "") {
        WarningToast('Please Select Nationality.');
        return false;
    }
    if ($('#SD_ReligionId').val() == "") {
        WarningToast('Please Select Religion.');
        return false;
    }
    if ($('#SD_CasteId').val() == "") {
        WarningToast('Please Select Caste.');
        return false;
    }
    if ($('#SD_Address').val() == "") {
        WarningToast('Please provide Address.');
        return false;
    }
    if ($('#SD_StateId').val() == "") {
        WarningToast('Please select State.');
        return false;
    }
    if ($('#ddlDistrictId').val() == "") {
        WarningToast('Please select District.');
        return false;
    }
    if ($('#SD_PinCode').val() == "") {
        WarningToast('Please Enter PIN');
        return false;
    }
    if ($('#SD_PoliceStation').val() == "") {
        WarningToast('Please Enter Police station');
        return false;
    }
    if ($('#SD_ContactNo1').val() == "") {
        WarningToast('Please provide Moibile No.');
        return false;
    }
    if ($('#SD_StudentCategoryId').val() == '') {
        WarningToast('Please select Student Fees Category.');
        return false;
    }

    if ($('#SD_EmailId').val() != '') {
        if (validateEmail($('#SD_EmailId').val())) {

        }
        else {
            WarningToast('You Email Address Invalid ....')
            return false;
        }
    }

    if ($('#SD_AppliactionDate').val() == '') {
        WarningToast('Please provide date.');
        return false;
    }
    if ($('#chk_Transport').is(':checked')) {

        if ($('#SD_RouteId').val() == '') {
            WarningToast('Please select a route or uncheck Transport.');
            return false;
        }
        if ($('#SD_TransportTypeId').val() == '') {
            WarningToast('Please select a transport type or uncheck Transport.');
            return false;
        }
        if ($("#dvpickUp").is(':visible')) {

            if ($('#ddlPickUpLocation').val() == "0") {
                WarningToast('Please select a pick up point or uncheck Transport.');
                return false;
            }
        }
        if ($("#dvdropLocation").is(':visible')) {

            if ($('#ddlDropLocation').val() == "0") {
                WarningToast('Please select a Dropping  point or uncheck Transport.');
                return false;
            }
        }
    }

    return true;
}


function StudentList() {
    $('#update-panel').html('loading data.....');

    $.ajax({
        url: rootDir + 'JQuery/AdmittedStudentsList',
        data: {
            SD_ClassId: parseInt($('#SD_ClassId').val()),
            SD_AppliactionNo: $.trim($('#AdmissionNo').val()),
            SD_StudentId: $.trim($('#SD_StudentId').val()),
            SD_StudentName: $.trim($('#StudentName').val()),
            FromDate: convertToSqlDate($('#FromDate').val()),
            ToDate: convertToSqlDate($('#ToDate').val())
        },
        dataType: 'json',
        type: 'GET',
        success: function (res) {
            if (res.Data.length > 0) {
                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>Admitted Date</th><th>Admission No</th><th>StudentId</th><th>Student's Name</th>><th>Father's Name</th><th>Contact No</th><th>Class</th><th></th><th></th><th></th></tr></thead>";
                $data.append($header);

                $.each(res.Data, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td>').text(i + 1));

                    $row.append($('<td style=display:none>').append(row.SD_Id));
                    if (row.SD_AppliactionDate) {
                        var d = new Date(parseInt(row.SD_AppliactionDate.slice(6, -2)));
                        var dateStr = d.getDate() + '/' + (d.getMonth() + 1) + '/' + d.getFullYear();
                        $row.append($('<td>').append(formatJsonDate(row.SD_AppliactionDate)));
                    } else {
                        $row.append($('<td>').append(''));
                    }
                    $row.append($('<td>').append(row.SD_AppliactionNo));
                    
                    //Added by uttaran 27/11/2024
                    $row.append($('<td>').append(row.SD_StudentId));
                    $row.append($('<td>').append(row.SD_StudentName));
                    $row.append($('<td>').append(row.SD_FatherName));
                    $row.append($('<td>').append(row.SD_ContactNo1));
                    $row.append($('<td>').append(row.SD_CM_CLASSNAME));

                    $row.append(
                    $('<td>').append(
                        "<a target='_blank' href='" + rootDir + "StudentManagement/StudentManual?id=" + row.SD_StudentId + "'>" +
                            "<img src='/Content/images/Print.png' alt='Download' style='width:22px;height:22px;'/>" +
                        "</a>"
                    ));
                   
                    if (res.CanEdit == true) {
                        $row.append($('<td>').append("<a href=/StudentManagement/Admission?id=" + row.SD_Id + " class='btn btn-warning'>Edit</a>"));
                    }
                    else {
                        $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                    }
                    if (res.CanDelete == true) {
                        $row.append($('<td>').append("<a onclick='Confirm(" + row.SD_Id + ");'" + row.SD_Id + " class='btn btn-danger'>Delete</a>"));
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
}
$(document).ready(function () {
    validateNumeric();
    StudentList();
    if ($("#SD_FormNo").val() != "") {

        $("#SD_FormNo").attr("disabled", "disabled");
    }
    $('#hdnRouteRate').val("0");
    if ($("#hdnSex").val() != "") {
        var hdnSex = $("#hdnSex").val();
        $("#radio_" + hdnSex).prop("checked", true);
    }
    else {
        $("#radio_M").prop("checked", true);
    }
    if ($("#hdnchk_Trans").val() != "") {

        $("#chk_Transport").prop("checked", true);
        $('#dvTrans').show();
        $('#dvTransRate').show();
        $("#SD_RouteId").val($('#hdnRTId').val());
        $("#SD_RouteId").selectpicker('refresh');
        $("#SD_TransportTypeId").val($('#hdnTTId').val());
        $("#SD_TransportTypeId").selectpicker('refresh');
    }
    $("#SD_StateId").change(function () {
        AjaxPostForDropDownDistrict($("#SD_StateId").val());
    });
    $("#SD_PermanentStateId").change(function () {
        AjaxPostForDropDownPermanentDistrict($("#SD_PermanentStateId").val());
    });
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
    $('#SD_PinCode').keyup(function () {
        if (!this.value.match(/[0-9]/)) {
            this.value = this.value.replace(/[^0-9]/g, '');
        }
    });
    $('#SD_ContactNo1').keyup(function () {
        if (!this.value.match(/[0-9]/)) {
            this.value = this.value.replace(/[^0-9]/g, '');
        }
    });
    $('#SD_ContactNo2').keyup(function () {
        if (!this.value.match(/[0-9]/)) {
            this.value = this.value.replace(/[^0-9]/g, '');
        }
    });
    $("#chk_adr").click(function () {
        if ($('#chk_adr').is(':checked')) {

            FillSameAddress();
        }
            //FillSameAddress();
        else {
            clearAddress();
        }
    });
    $("#chk_Transport").click(function () {
        if ($('#chk_Transport').is(':checked')) {
            $('#dvTrans').show();
            $('#dvTransRate').show();
        }
        else {
            $('#dvTrans').hide();
            $('#dvTransRate').hide();
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

    var Id = getParameterByName('id');
    if (Id != '') {
        var _data = JSON.stringify({
            Id: parseInt(Id),
        });
        $.ajax({
            type: "POST",
            url: rootDir + "JQuery/GetIndividualStudentData",
            data: _data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data, status) {
                if (status == "success") {
                    AjaxPostForDropDownDistrict(parseInt(data.SD_StateId));
                    $("#ddlDistrictId").val(data.SD_DistrictId);
                    $("#ddlDistrictId").selectpicker('refresh');
                    AjaxPostForDropDownPermanentDistrict(parseInt(data.SD_PermanentStateId));
                    $("#ddlPermanentDistrictId").val(data.SD_PermanentDistrictId);
                    $("#ddlPermanentDistrictId").selectpicker('refresh');
                    if (data.SD_IsTransport == true) {
                        $('#dvTrans').show();
                        $('#dvTransRate').show();
                    }
                    else {
                        $('#dvTrans').hide();
                        $('#dvTransRate').hide();
                    }
                    SetTransportationValue(data);

                } else {
                    showMessage("Unable to process the request...", 0);
                }
            }
        });
    }


});
function SetTransportationValue(data) {
    var RouteId = (data.SD_RouteId == null || data.SD_RouteId == '') ? 0 : parseFloat(data.SD_RouteId);
    if (RouteId > 0) {
        AjaxPostForDropDownLocation(parseFloat(RouteId)); // DataList = [] is filled here
    }
    var transportTypeId = (data.SD_TransportTypeId == null || data.SD_TransportTypeId == '') ? 0 : parseFloat(data.SD_TransportTypeId);
    var pickLocationId = (data.SD_PickLocationId == null || data.SD_PickLocationId == '') ? 0 : parseFloat(data.SD_PickLocationId);
    var dropLocationId = (data.SD_DropLocationId == null || data.SD_DropLocationId == '') ? 0 : parseFloat(data.SD_DropLocationId);
    if (transportTypeId == 1) {
        $('#dvpickUp').show();
        $('#dvdropLocation').show();
        $("#ddlDropLocation").val(dropLocationId);
        $("#ddlDropLocation").selectpicker('refresh');
        $("#ddlPickUpLocation").val(pickLocationId);
        $("#ddlPickUpLocation").selectpicker('refresh');
    }
    else if (transportTypeId == 2) {

        $('#dvpickUp').show();
        $("#ddlDropLocation").val('');
        $("#ddlDropLocation").val(dropLocationId);
        $("#ddlDropLocation").selectpicker('refresh');
        $("#ddlPickUpLocation").val(pickLocationId);
        $("#ddlPickUpLocation").selectpicker('refresh');
    }
    else if (transportTypeId == 3) {

        $('#dvdropLocation').show();
        $("#ddlDropLocation").val(dropLocationId);
        $("#ddlDropLocation").selectpicker('refresh');
        $("#ddlPickUpLocation").val(pickLocationId);
        $("#ddlPickUpLocation").selectpicker('refresh');
    }
    else {
        $('#dvpickUp').hide();
        $('#dvdropLocation').hide();
        $("#ddlDropLocation").val(dropLocationId);
        $("#ddlDropLocation").selectpicker('refresh');
        $("#ddlPickUpLocation").val(pickLocationId);
        $("#ddlPickUpLocation").selectpicker('refresh');
    }
    if (pickLocationId > 0) {
        $.each(DataList, function (index, element) {
            if (element.RDM_ROUTEMAPID == parseFloat(pickLocationId)) {
                $("#ddlPickUpLocation").attr("data-feesid", parseFloat(element.RDM_RATE));
                Rate = ((isNaN(parseFloat($('#ddlDropLocation').attr("data-feesid"))) ? 0 : parseFloat($('#ddlDropLocation').attr("data-feesid"))) + parseFloat(element.RDM_RATE));

            }
        });
        $("#SD_TransportFare").val(Rate);
        Rate = 0;
    }
    if (dropLocationId > 0) {
        $.each(DataList, function (index, element) {
            if (element.RDM_ROUTEMAPID == parseFloat(dropLocationId)) {
                var s = parseFloat(element.RDM_RATE) + parseFloat($('#SD_TransportFare').val());
                $("#ddlDropLocation").attr("data-feesid", parseFloat(element.RDM_RATE));
                Rate = ((isNaN(parseFloat($('#ddlPickUpLocation').attr("data-feesid"))) ? 0 : parseFloat($('#ddlPickUpLocation').attr("data-feesid"))) + parseFloat(element.RDM_RATE));

            }
        });


        $("#SD_TransportFare").val(Rate);
        Rate = 0;
    }

}
function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
           results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}
function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'StudetDetails_SD',
        MainFieldName: 'SD_Id',
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
                window.location.href = '/StudentManagement/AdmittedStudentList';
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
function validateEmail(email) {
    var emailReg = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
    var valid = emailReg.test(email);

    if (!valid) {
        return false;
    } else {
        return true;
    }
}
function getFiles(uploadFileName) {
    var files = uploadFileName.files;
    var data = new FormData();
    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
    }

    $.ajax({
        url: "../fileUpload.ashx",
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
function FillSameAddress() {
    $('#SD_PermanentAddress').val($('#SD_PresentAddress').val());
    $('#SD_PermanentStateId').val($('#SD_StateId').val());
    $('#SD_PermanentStateId').selectpicker("refresh");
    AjaxPostForDropDownPermanentDistrict($('#SD_PermanentStateId').val());
    $('#ddlPermanentDistrictId').val($('#ddlDistrictId option:selected').val());
    $('#ddlPermanentDistrictId').selectpicker("refresh");
    $('#SD_PermanentPin').val($('#SD_PinCode').val());
    $('#SD_PermanentPoliceStation').val($('#SD_PoliceStation').val());
}
function clearAddress() {
    $('#SD_PermanentAddress').val('')
    $('#SD_PermanentStateId').val('');
    $('#SD_PermanentStateId').selectpicker("refresh");
    $('#SD_PermanentDistrictId').val('');
    $('#SD_PermanentDistrictId').selectpicker("refresh");
    $('#SD_PermanentPin').val('');
    $('#SD_PermanentPoliceStation').val('');
}
function AjaxPostForDropDownDistrict(Id) {
    var _data = JSON.stringify({
        StateId: parseInt(Id),
    });
    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/GetDistrictListByState",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data, status) {
            if (status == "success") {
                BindDistrictDropDownList($('#ddlDistrictId'), data);
                $("#ddlDistrictId").selectpicker('refresh');

            } else {
                showMessage("Unable to process the request...", 0);
            }
        }
    });
}
function AjaxPostForDropDownPermanentDistrict(Id) {
    var _data = JSON.stringify({
        StateId: parseInt(Id),
    });
    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/GetDistrictListByState",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data, status) {
            if (status == "success") {
                BindDistrictDropDownList($('#ddlPermanentDistrictId'), data);
                $("#ddlPermanentDistrictId").selectpicker('refresh');

            } else {
                showMessage("Unable to process the request...", 0);
            }
        }
    });
}
function BindDistrictDropDownList(ddl, dataCollection) {
    $(ddl).get(0).length = 0;
    $(ddl).get(0).options[0] = new Option("Select", "0", "Selected");
    console.log(dataCollection);
    if (dataCollection.length != 0) {
        for (var i = 0; i < dataCollection.length; i++) {
            $(ddl).get(0).options[i + 1] = new Option(dataCollection[i].DM_DISTRICTNAME, dataCollection[i].DM_DISTRICTID);
        }
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
function validateNumeric() {
    $("[class*='allow_decimal']").on("input", function (evt) {
        var self = $(this);
        self.val(self.val().replace(/[^0-9\.]/g, ''));
        if ((evt.which != 46 || self.val().indexOf('.') != -1) && (evt.which < 48 || evt.which > 57)) {
            evt.preventDefault();
        }
        if (self.val().indexOf('.') > 0) {
            var txtlen = self.val().length;
            var dotpos = self.val().indexOf(".");
            var count = 0;
            inputValue = self.val();
            for (var i = 0, l = inputValue.length; i < inputValue.length; i += 1) {
                if (inputValue[i] === '.') {
                    count += 1;
                }
            }
            if (count > 1) {
                alert('Only one decimal is allowed !');
                $(this).val($(this).val().slice(0, -1));
                evt.preventDefault();
            }
            if ((txtlen - dotpos) > 3) {
                alert('Decimal Value upto 2 places allowed !');
                $(this).val($(this).val().slice(0, -1));
                evt.preventDefault();
            }
        }
    });
}



function InsertUpdateAdmissionData() {
    var fileUpload = $("#SD_Photo").get(0);
    getFiles(fileUpload);

    var sex = $('input[name=Sd_SexId]:checked').val();
    var Documentfile = $('#SD_Photo').val().substring(12);
    var Documentpath2 = '';
    var IsCheck = 'False';
    var IsTransCheck = 'False';

    if ($('#SD_Photo').val() != '' && $('#SD_Photo').val() != null) {
        Documentfile = $('#SD_Photo').val().substring(12);
        Documentpath2 = '~/UploadFile/' + Documentfile;
    } else {
        Documentpath2 = $('#hdnImage').val();
    }

    if ($('#chk_SingleParent').is(':checked')) {
        IsCheck = 'true';
    }

    if ($('#chk_Transport').is(':checked')) {
        IsTransCheck = 'true';
    }

    if (ValidateOperation() == true) {
        $('#btnSave').attr('disabled', 'disabled');
        $('#loader-overlay').fadeIn(200); // Show loader

        var _data = JSON.stringify({
            SD_Id: $('#SD_Id').val(),
            SD_OldStudentId: $('#SD_OldStudentId').val(),
            SD_FormNo: $('#SD_FormNo').val(),
            SD_AppliactionDate: convertToSqlDate($('#SD_AppliactionDate').val()),
            SD_ClassId: $('#SD_ClassId').val(),
            SD_CurrentClassId: $('#SD_CurrentClassId').val(),
            SD_CurrentRoll: $('#SD_CurrentRoll').val(),
            SD_CurrentSectionId: $('#SD_CurrentSectionId').val(),
            SD_StudentName: $('#SD_StudentName').val(),
            Sd_SexId: sex,
            SD_DOB: $('#SD_DOB').val(),
            SD_CasteId: $('#SD_CasteId').val(),
            SD_FatherName: $('#SD_FatherName').val(),
            SD_MotherName: $('#SD_MotherName').val(),
            SD_PresentAddress: $('#SD_PresentAddress').val(),
            SD_StateId: $('#SD_StateId').val(),
            SD_DistrictId: $('#ddlDistrictId').val(),
            SD_PinCode: $('#SD_PinCode').val(),
            SD_ContactNo1: $('#SD_ContactNo1').val(),
            SD_ContactNo2: $('#SD_ContactNo2').val(),
            SD_EmailId: $('#SD_EmailId').val(),
            SD_LastSchoolName: $('#SD_LastSchoolName').val(),
            SD_TCNo: $('#SD_TCNo').val(),
            SD_TCDate: convertToSqlDate($('#SD_TCDate').val()),
            SD_Photo: Documentpath2,
            SD_StudentCategoryId: $('#SD_StudentCategoryId').val(),
            SD_MotherTongueId: $('#SD_MotherTongueId').val(),
            SD_SessionId: $('#SD_SessionId').val(),
            SD_BloodGroupId: $('#SD_BloodGroupId').val(),
            SD_SecondLanguageId: $('#SD_SecondLanguageId').val(),
            SD_ThirdLanguageId: $('#SD_ThirdLanguageId').val(),
            SD_AadharNo: $('#SD_AadharNo').val(),
            SD_ReligionId: $('#SD_ReligionId').val(),
            SD_TCType: $('#SD_TCType').val(),
            SD_HouseId: $('#SD_HouseId').val(),
            SD_PermanentAddress: $('#SD_PermanentAddress').val(),
            SD_PermanentStateId: $('#SD_PermanentStateId').val(),
            SD_PermanentDistrictId: $('#ddlPermanentDistrictId').val(),
            SD_PermanentPin: $('#SD_PermanentPin').val(),
            ENQ_Id: $('#ENQ_Id').val(),
            SD_IsSingleParent: IsCheck,
            SD_PermanentPoliceStation: $('#SD_PermanentPoliceStation').val(),
            SD_PoliceStation: $('#SD_PoliceStation').val(),
            SD_NationalityId: $('#SD_NationalityId').val(),
            SD_LocalGuardianName: $('#SD_LocalGuardianName').val(),
            SD_LocalGuardianPhoneNo: $('#SD_LocalGuardianPhoneNo').val(),
            SD_IsTransport: IsTransCheck,
            SD_TransportTypeId: $('#SD_TransportTypeId').val(),
            SD_TransportFare: $('#SD_TransportFare').val(),
            SD_RouteId: $('#SD_RouteId').val(),
            SD_PickLocationId: $('#ddlPickUpLocation').val(),
            SD_DropLocationId: $('#ddlDropLocation').val(),
            Userid: $('#hdnUserid').val(),
            SD_TransportMonthId: $('#ddlMonth').val(),
            SD_GQualification: $('#SD_GQualification').val(),
            SD_GOccupation: $('#SD_GOccupation').val(),
            SD_GDesignation: $('#SD_GDesignation').val(),
            SD_GIncome: $('#SD_GIncome').val()
        });

        $.ajax({
            url: '/JQuery/InsertUpdateAdmissionDetails',
            type: 'POST',
            data: _data,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#loader-overlay').fadeOut(200, function () {
                    $('#btnSave').removeAttr('disabled');

                    if (data && data.IsSuccess) {
                        alert(data.Message);
                        if (data.CanRedirect) {
                            var admissionNo = $.trim(data.Param);
                            window.location.href = rootDir +
                                "FeesCollection/FeesCollection/FeesCollectionWhileStudentAdmitted?AdmissionNo=" + admissionNo;
                        } else {
                            window.location.href = rootDir + 'StudentManagement/AdmittedStudentList';
                        }
                    } else {
                        ErrorToast(data.Message || 'Something went wrong.');
                    }
                });
            },
            error: function () {
                $('#loader-overlay').fadeOut(200, function () {
                    $('#btnSave').removeAttr('disabled');
                    ErrorToast('Something went wrong.');
                });
            }
        });
    }
}
