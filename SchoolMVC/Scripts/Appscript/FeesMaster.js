var DataList = [];
//fghfhf
function ValidateOperation() {

    if ($('#FEM_FEESNAME').val() == "") {

        WarningToast('Please provide a Fees Head.');

        return false;
    }

    if ($("#FM_InstypeId option:selected").val() == "") {

        WarningToast('Please Installment Type.');

        return false;
    }
    return true;
}
function FeesMasterList() {
    var isAdmission = '';
    var IsAd = '';
    if ($('#chk_IsAdmission').is(":checked"))
    {
        isAdmission = true
    }
    else
    {
      var  isAdmission=false;
    }

    $('#update-panel').html('loading data.....');
    setTimeout(function () {
        $.ajax({
            url: rootDir + 'JQuery/FeesMasterList',
            dataType: 'json',
            type: 'GET',
            data: { IsAdmission: isAdmission },
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th style='display:none'>FEESID</th><th>Sl.</th><th>FEESNAME</th><th>ISACTIVE</th><th>Mandatory Admission</th><th>One Time Admission</th><th></th><th></th></tr></thead>";
                    $data.append($header);
                    var totalRows = res.Data.length;
                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td style=display:none>').append(row.FEM_FEESID));
                        $row.append($('<td>').append(totalRows));
                        $row.append($('<td>').append(row.FEM_FEESNAME));
                        if (row.FEM_ISACTIVE==true)
                        {
                            $row.append($('<td>').append('Yes'));
                        }
                        else
                        {
                            $row.append($('<td>').append('No'));
                        }
                        if (row.FEM_ISADMINTIMEMAN == true) {
                            $row.append($('<td>').append('Yes'));
                        }
                        else {
                            $row.append($('<td>').append('No'));
                        }
                        if (row.FEM_ISONLYADMISSION == true) {
                            $row.append($('<td>').append('Yes'));
                        }
                        else {
                            $row.append($('<td>').append('No'));
                        }
                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href= " + rootDir + "Masters/FeesMaster/" + parseInt(row.FEM_FEESID) + " class='btn btn-warning'>Edit</a> "));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + parseInt(row.FEM_FEESID) + ");'" + parseInt(row.FEM_FEESID) + " class='btn btn-danger'>Delete</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-danger disabled'>Delete</a>"));

                        }
                        $data.append($row);
                        totalRows = totalRows - 1;
                    });
                    $("#update-panel").html($data);
                    $('#tblList').DataTable({
                        "order": [[0, "asc"]]
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
    }, 2000);
    
}
function loadFeesInstypes() {
    $.get({
        type: "GET",
        url: rootDir + "JQuery/loadFeesInstypes",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data, status) {
            if (status == "success") {

                DataList = data;
            } else {
                ErrorToast("Unable to process the request...", 0);
            }
        }
    });
}
$(document).ready(function () {
    $("#FEM_ISACTIVE").prop("checked", true);
    if ($('#action').val() == "") {
        FeesMasterList();
    }
    $('#chk_IsAdmission').change(function () {
        if ($('#chk_IsAdmission').is(":checked")) {
            FeesMasterList();
        }
        else
        {
            FeesMasterList();
        }
    });
    loadFeesInstypes();

    $("#FEM_ISADMINTIMEMAN").click(function () {
        if ($(this).is(":checked")) {
            $('#FEM_NOOFINSTALLMENT').val('1');
            $("#IdNoOfInstalment").show();

        } else { $("#IdNoOfInstalment").hide(); }
    });
    $("#FM_InstypeId").change(function () {
        $("#FEM_INSTYPE").val('');
        $.each(DataList, function (index, element) {
            if (element.INTYP_INSTYPEID == $("#FM_InstypeId").val()) {
                $("#FEM_INSTYPE").val(element.INTYP_INSTYPEVALUE);
            }
        });
    });  
    if ($('#action').val() == "update") {
        if ($("#FEM_ISADMINTIMEMAN").is(":checked")) {
            $("#IdNoOfInstalment").show();
        }
        if ($("#FM_InstypeId").val() != '') {
            $("#FEM_INSTYPE").val('');
            $.each(DataList, function (index, element) {
                if (element.INTYP_INSTYPEID == $("#FM_InstypeId").val()) {
                    $("#FEM_INSTYPE").val(element.INTYP_INSTYPEVALUE);
                }
            });
        }
        if ($("#REFUNDABLE").val() == "True") {
            $("#FEM_ISREFUNDABLE").prop("checked", true);
        }
        else if ($("#HOSTELFEES").val() == "True") {
            $("#FEM_ISHOSTELFEES").prop("checked", true);
        }
       else if ($("#TRANSPORTFEES").val() == "True") {
            $("#FEM_ISTRANSPORTFEES").prop("checked", true);
        }
        else if ($("#DUPDOCFEES").val() == "True") {
            $("#FEM_ISDUPDOCFEES").prop("checked", true);
        }
        else if ($("#DUPIDFEES").val() == "True") {
            $("#FEM_ISDUPIDFEES").prop("checked", true);
        }

        else if ($("#FEM_ISPROCESSINGFEES_HIDDEN").val() == "True") {
            $("#FEM_ISPROCESSINGFEES").prop("checked", true);
        }
        else if ($("#FEM_NONE_HIDDEN").val() == "True") {
            $("#FEM_NONE").prop("checked", true);
        }
    }
});
function InsertUpdateFeesMaster() {

    if (ValidateOperation() == true) {
        //return false;
        var _data = JSON.stringify({
            obj: {
                FEM_FEESID: parseInt($('#FEM_FEESID').val()),
                FEM_FEESNAME: $('#FEM_FEESNAME').val(),
                FEM_AMOUNT: $('#FEM_AMOUNT').val(),
                FEM_INSTYPEID: parseInt($("#FM_InstypeId option:selected").val()),
                FEM_TOTALINSTALLMENT: parseInt($('#FEM_INSTYPE').val()),
                FEM_NOOFINSTALLMENT: parseInt($('#FEM_NOOFINSTALLMENT').val()),
                FEM_ISREFUNDABLE: $('#FEM_ISREFUNDABLE').is(":checked"),
                FEM_ISADMINTIMEMAN: $('#FEM_ISADMINTIMEMAN').is(":checked"),
                FEM_ISHOSTELFEES: $('#FEM_ISHOSTELFEES').is(":checked"),
                FEM_ISTRANSPORTFEES: $('#FEM_ISTRANSPORTFEES').is(":checked"),
                FEM_ISDUPDOCFEES: $('#FEM_ISDUPDOCFEES').is(":checked"),
                FEM_ISDUPIDFEES: $('#FEM_ISDUPIDFEES').is(":checked"),
                FEM_ISPROCESSINGFEES: $('#FEM_ISPROCESSINGFEES').is(":checked"),
                FEM_ISACTIVE: $('#FEM_ISACTIVE').is(":checked"),
                FEM_ISONLYADMISSION: $('#FEM_ISONLYADMISSION').is(":checked"),
                FEM_NONE: $('#FEM_NONE').is(":checked"),
            }
        });

        $.ajax({
            url: rootDir + 'JQuery/InsertUpdateFees',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    $('#btnSave').attr('disabled', 'disabled');
                    setTimeout(function () {
                        //papai
                        window.location.href = rootDir + 'Masters/FeesMasterList';
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
function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'FeesMaster_FEM',
        MainFieldName: 'FEM_FEESID',
        PId: parseInt(fieldId),
    });
    $.ajax({ 
        url: rootDir + 'JQuery/DeleteData',
        type: 'POST',
        data: _data,
        contentType: 'application/json ; utf-8',
        dataType: 'json',
        success: function (data) {
            if (data != null && data != undefined && data.IsSuccess == true) {
                SuccessToast(data.Message);
                setTimeout(function () {
                    //papai
                    window.location.href = rootDir + 'Masters/FeesMasterList';
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
