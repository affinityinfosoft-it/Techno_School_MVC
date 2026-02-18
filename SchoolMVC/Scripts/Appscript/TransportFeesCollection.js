var Id = '';
$(document).ready(function () {
    Id = getParameterByName('id');
    if (Id != '')
    {
        bindGridForEdit(Id);
     
       $("#chk").prop("checked", true);
       
    }
    $("#btnSearch").click(function () {
        bindGrid();
    });
    paydetailsVerify();
    $('#Div_PayDetails').hide();
    $("#btnSave").click(function () {
        AddFees();
    });
});
$(document).on('change', '.chk', function () {
    GetTotalAmount();
});
function bindGrid() {
    $('#update-panel').html('loading data.....');
    $("#tblStudent").show();
    var _data = JSON.stringify({
        obj: {
            TR_StudentId: $('#TR_StudentId').val(),
        }
    });
    $.ajax({
        url: rootDir + "JQuery/GetTransportFeesCollectionList",
        dataType: 'json',
        contentType: 'application/json',
        data: _data,
        type: 'POST',
        success: function (d) {
            if (d.length > 0) {
                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                $header = "<thead><tr><th>Student Id</th><th>Fees Head</th><th style=display:none>Fees Id</th><th>Month</th><th>Payment Amount</th><th>Pay</th></tr></thead>";
                $data.append($header);
                $.each(d, function (i, row) {
                    if (row.TR_Id == 0) {
                        for (var i = row.TR_InstallmentMonth; i <= 12; i++) {
                            var $row = $('<tr/>');
                            $row.append($('<td>').append(row.TR_StudentId));
                            $row.append($('<td>').append(row.TR_FeesName));
                            $row.append($('<td style=display:none>').append(row.TR_FeesHeadId));
                            $row.append($('<td>').append(i));
                            $row.append($('<td>').append(row.TR_MonthlyFare));
                            $row.append($('<td/>').html('<input type="checkbox" class="chk" id="chk' + i + '" />'));

                            $data.append($row);
                        }
                    }
                    if (row.TR_Id != 0) {
                        var $row = $('<tr/>');
                        $row.append($('<td>').append(row.TR_StudentId));
                        $row.append($('<td>').append(row.TR_FeesName));
                        $row.append($('<td style=display:none>').append(row.TR_FeesHeadId));
                        $row.append($('<td>').append(row.TR_InstallmentMonth));
                        $row.append($('<td>').append(row.TR_MonthlyFare));
                        $row.append($('<td/>').html('<input type=checkbox class="chk" id="chk' + i + '" onclick="GetTotalAmount();" />'));

                        $data.append($row);
                    }
                });

                $("#update-panel").html($data);
                //$("#tblList").DataTable();
                $('#tblList').dataTable
                $('#btnPay').show();
                $('#btnSave').show();
                ({
                    "sScrollX": "200%", //This is what made my columns increase in size.
                    "bScrollCollapse": true,
                    "sScrollY": "320px",

                    "bAutoWidth": false,
                    "aoColumns": [
                        { "sWidth": "10%" }, // 1st column width 
                        { "sWidth": "null" }, // 2nd column width 
                        { "sWidth": "null" }, // 3rd column width
                        { "sWidth": "null" }, // 4th column width 
                        { "sWidth": "40%" }, // 5th column width 

                    ],
                    "bPaginate": true,
                    "sDom": '<"H"TCfr>t<"F"ip>',
                    "oTableTools":
                    {
                        "aButtons": ["copy", "csv", "print", "xls", "pdf"],
                        "sSwfPath": "copy_cvs_xls_pdf.swf"
                    },

                    "sPaginationType": "full_numbers",
                    "aaSorting": [[0, "desc"]],
                    "bJQueryUI": true
                });

            }
            else {
                $noData = "<div>This Student has no Transport Fees</td>"
                $("#update-panel").html($noData);
            }
        },
        failure: function () {
            ErrorToast('something wrong happen');
        }
    });
}
function AddFees() {
    var arrMonthID = [];
    var arrFare = [];
    var arrFeesId = [];
    var arrStudentId = [];

    // Collect checked rows
    if ($('#tblList>tbody input[type="checkbox"]').is(":checked")) {
        $('#tblList>tbody input[type="checkbox"]:checked').each(function () {
            var $tr = $(this).closest('tr');
            arrStudentId.push($tr.find('td:eq(0)').text());
            arrFeesId.push($tr.find('td:eq(2)').text());
            arrMonthID.push($tr.find('td:eq(3)').text());
            arrFare.push($tr.find('td:eq(4)').text());
        });
    } else {
        alert('Please select an installment to continue.');
        return;
    }

    if (!ValidateForm()) return;

    // Prepare full table data (all fees)
    var arrList = [];
    $('#tblList>tbody tr').each(function () {
        var $tr = $(this);
        arrList.push({
            TR_FeesHeadId: parseInt($tr.find('td:eq(2)').text()),
            TR_StudentId: $tr.find('td:eq(0)').text(),
            TR_InstallmentMonth: parseFloat($tr.find('td:eq(3)').text()),
            TR_MonthlyFare: parseFloat($tr.find('td:eq(4)').text())
        });
    });

        var paidList = [];
        for (var i = 0; i < arrMonthID.length; i++) {
            paidList.push({
                TR_FeesHeadId: parseInt(arrFeesId[i]),    // ✔ correct value
                TR_StudentId: arrStudentId[i],
                TR_InstallmentMonth: parseInt(arrMonthID[i]),
                TR_MonthlyFare: parseFloat(arrFare[i])
            });
        }

   

    // Ensure date is in yyyy-MM-dd format
    var feesDate = $('#TD_FeesCollectionDate').val();
    if (feesDate.includes('/')) {
        var parts = feesDate.split('/');
        feesDate = parts[2] + '-' + parts[1].padStart(2, '0') + '-' + parts[0].padStart(2, '0');
    }

    var _data = JSON.stringify({
        obj: {
            PaidFeesStructure: paidList,
            FeesStructure: arrList,
            TD_Paymentmode: $("#ChkId").val(),
            TD_PaidAmount: $("#TD_PaidAmount").val(),
            TD_FeesCollectionDate: feesDate,
            TD_BankId: $("#TD_BankId").val(),
            TD_BranchName: $("#TD_BranchName").val(),
            TD_CheqDDNo: $("#TD_CheqDDNo").val(),
            TD_CheqDDDate: $("#TD_CheqDDDate").val(),
            TD_Card_TrnsRefNo: $("#TD_Card_TrnsRefNo").val(),
            TR_TransId: Id
        }
    });

    $.ajax({
        type: "POST",
        url: '/JQuery/InsertTransportFeesCollection',
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.IsSuccess) {
                alert('Data has been updated successfully.');

                // OPEN PRINT PAGE WITH RETURNED TRANS ID

                var receiptUrl = '/StudentManagement/TransportFeesCollectionListView?TR_TransId=' + data.TR_TransId;


                window.open(receiptUrl, '_blank');

                window.location.href = '/StudentManagement/TransportFeesCollectionList';
                window.location.href = listUrl;
            } else {
                ErrorToast(data.Message);
            }
        },
        error: function (data) {

            alert('Process failed: ' + error);
        }
    });
}
function GetTotalAmount() {
    if ($('#tblList>tbody input[type="checkbox"]').is(":checked")) {
        var arrMonthID = [];
        var TotalFare = 0
        var tblItem = document.getElementById('tblList');
        $('#tblList>tbody input[type="checkbox"]:checked').each(function (i, row) {
            var cellrow = parseInt($(row).closest('tr').index());
           
            TotalFare = TotalFare + parseInt(tblList.rows[cellrow + 1].cells[4].innerHTML);
        });
      
        $('#TD_PaidAmount').val(TotalFare);
        return TotalFare;
    }
    else {
        alert('Please Select amount first.')
    }
}
$(document).on("click", '#radio_Cash', function (e) {
    paydetailsVerify();
    var rdCash = $("#radio_Cash").is(":checked");
    if (rdCash) {

        $("#ChkId").val("Cash");
    }
});
$(document).on("click", '#radio_Cheque', function (e) {
    paydetailsVerify();
    var rdChq = $("#radio_Cheque").is(":checked");
    if (rdChq) {

        $("#ChkId").val("Cheque");
    }
});
$(document).on("click", '#radio_DD', function (e) {
    paydetailsVerify();
    var rdDD = $("#radio_DD").is(":checked");
    if (rdDD) {

        $("#ChkId").val("DD");
    }


});
$(document).on("click", '#radio_Card', function () {
    if (this.checked) {
        $("#ChkId").val("Card");
        paydetailsVerify();
    }
});
$(document).on("click", '#radio_Online', function () {
    paydetailsVerify();
    if ($("#radio_Online").is(":checked")) {
        $("#ChkId").val("Online");
    }
});
$(document).ready(function () {
    $("#radio_Cheque").prop("checked", true);
    $("#ChkId").val("Cheque");
    paydetailsVerify();
    // Default – Cheque selected
    $("#radio_Cheque").prop("checked", true);
    });
function paydetailsVerify() {

    var mode = $("#ChkId").val();

    // Hide all by default
    $("#Div_PayDetails").hide();
    $("#Div_ChqDate").hide();
    $("#Div_TrnsRefNo").hide();

    if (mode === "Cash") {
        return;
    }

    if (mode === "Cheque" || mode === "DD") {
        $("#Div_PayDetails").show();
        $("#Div_ChqDate").show();
    }

    if (mode === "Card" || mode === "Online") {
        $("#Div_PayDetails").show();
        $("#Div_TrnsRefNo").show();
    }
}
function ValidateForm() {
    if ($('#TD_FeesCollectionDate').val() == '') {
        alert('Please provide a collection date');
        return false;
    }
    if (Id == '')
    {
        if ($('#ChkId').val() == '') {
            alert('Please select a payment mode');
            return false;
        }
    }
    
    if ($('#ChkId').val() == 'Cheque' || $('#ChkId').val() == 'DD') {
        if ($('#TD_BankId').val() == '' || $('#TD_BranchName').val() == '' || $('#TD_CheqDDNo').val() == '' || $('#TD_CheqDDDate').val() == '') {
            alert('Please fill all fields.');
            return false;
        }
    }
    if ($('#ChkId').val() == 'Card' || $('#ChkId').val() == 'Online') {
        if ($('#TD_Card_TrnsRefNo').val() == '') {
            alert('Please enter transaction reference number.');
            return false;
        }
    }

    return true;
}
function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
           results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}
function bindGridForEdit(Id) {
    $('#update-panel').html('loading data.....');
    $("#tblStudent").show();
    var _data = JSON.stringify({
        obj:
            {
              TR_TransId: Id,
            }
    });
    $.ajax({
        url: rootDir + "JQuery/GetTransportFeesCollectionListForEdit",
        dataType: 'json',
        contentType: 'application/json',
        data: _data,
        type: 'POST',
        success: function (d) {
            if (d.length > 0)
              {
                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                $header = "<thead><tr><th>Student Id</th><th>Fees Head</th><th style=display:none>Fees Id</th><th>Month</th><th>Payment Amount</th><th>Pay</th></tr></thead>";
                $data.append($header);
                var BankId = "";
                var Branch = "";
                var chkdate = "";
                var FeesDate = "";
                var PaymentMode = "";
                var CheqDDNo = "";
                $.each(d, function (i, row)
                {
                    BankId = row.TD_BankId;
                    Branch = row.TD_BranchName;
                    var d = new Date(parseInt(row.TD_FeesCollectionDate.slice(6, -2)));
                    FeesDate=  d.getDate() + '/' + (1 + d.getMonth()) + '/' + d.getFullYear().toString()
                    PaymentMode = row.TD_Paymentmode;
                    CheqDDNo = row.TD_CheqDDNo;
                    //var d2 = new Date(parseInt(row.TD_CheqDDDate.slice(6, -2)));
                    //chkdate = d2.getDate() + '/' + (1 + d2.getMonth()) + '/' + d2.getFullYear().toString()
                        var $row = $('<tr/>');
                        $row.append($('<td>').append(row.TR_StudentId));
                        $row.append($('<td>').append(row.TR_FeesName));
                        $row.append($('<td style=display:none>').append(row.TR_FeesHeadId));
                        $row.append($('<td>').append(row.TR_InstallmentMonth));
                        $row.append($('<td>').append(row.TR_MonthlyFare));
                        $row.append($('<td/>').html('<input type=checkbox class="chk"checked id="chk' + row.TR_TransId + '" onclick="GetTotalAmount();" />'));
                       $data.append($row);
                });
                $("#update-panel").html($data);
                $('#tblList').dataTable
                GetTotalAmount();
                CheckRadioForEdit(PaymentMode);
                $('#TD_BankId').val(BankId);
                $("#TD_BankId").selectpicker('refresh');
                $('#TD_FeesCollectionDate').val(FeesDate);
                $('#TD_BranchName').val(Branch);
                $('#TD_CheqDDNo').val(CheqDDNo);
                //$('#TD_CheqDDDate').val(chkdate);
                $('#btnPay').show();
                $('#btnSave').show();
                ({
                    "sScrollX": "200%", //This is what made my columns increase in size.
                    "bScrollCollapse": true,
                    "sScrollY": "320px",
                    "bAutoWidth": false,
                    "aoColumns": [
                        { "sWidth": "10%" }, // 1st column width 
                        { "sWidth": "null" }, // 2nd column width 
                        { "sWidth": "null" }, // 3rd column width
                        { "sWidth": "null" }, // 4th column width 
                        { "sWidth": "40%" }, // 5th column width 
                                 ],
                    "bPaginate": true,
                    "sDom": '<"H"TCfr>t<"F"ip>',
                    "oTableTools":
                    {
                        "aButtons": ["copy", "csv", "print", "xls", "pdf"],
                        "sSwfPath": "copy_cvs_xls_pdf.swf"
                    },
                    "sPaginationType": "full_numbers",
                    "aaSorting": [[0, "desc"]],
                    "bJQueryUI": true
                });
            }
            else
            {
                $noData = "<div>This Student has no Transport Fees</td>"
                $("#update-panel").html($noData);
            }  
        },
        failure: function ()
        {
            ErrorToast('something wrong happen');
        }
    });
}
function CheckRadioForEdit(paymode)
{
    if (paymode=='Cash')
    {
        $("#radio_Cash").prop("checked", true);
    }
    if (paymode =='Card')
    {
        $("#radio_Card").prop("checked", true);
        paydetailsVerify();
    }
    if (paymode == 'DD') {
        $("#radio_DD").prop("checked", true);
        paydetailsVerify();
    }
    if (paymode == 'Cheque') {
        $("#radio_Cheque").prop("checked", true);
        paydetailsVerify();
    }
  
}
function formatJsonDate(jsonDate) {
    if (!jsonDate) return '';
    var timestamp = parseInt(jsonDate.replace(/\/Date\((\d+)\)\//, '$1'));
    var date = new Date(timestamp);
    var day = ('0' + date.getDate()).slice(-2);
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var year = date.getFullYear();
    return day + '/' + month + '/' + year; // dd/MM/yyyy
}




function bindFeesCollectionList() {
    $('#update-panel').html('loading data.....');
    var _data = JSON.stringify({
        obj: {
            TR_StudentId: $('#StudentId').val().trim() || null,
            TR_ClassId: parseInt($('#SD_ClassId').val()) || null,
            fromDate: $('#FromDate').val() || null,
            toDate: $('#ToDate').val() || null
        }
    });


    $.ajax({
        url: rootDir + "JQuery/LoadTransportFeesCollectionList",
        dataType: 'json',
        contentType: 'application/json',
        data: _data,
        type: 'POST',
        success: function (d) {
            if (d.length > 0) {
                if ($.fn.DataTable.isDataTable('#tblList')) {
                    $('#tblList').DataTable().destroy();
                }

                var $data = $('<table id="tblList" class="table table-bordered table-striped"></table>');
                var $header = "<thead><tr><th>SL</th><th>Fees Date</th><th>TransId</th><th>Student Id</th><th>Student Name</th><th>Class</th><th>Paid Amount</th><th>Payment Mode</th><th>Print</th><th>Edit</th><th>Delete</th></tr></thead>";
                $data.append($header);

                var $tbody = $('<tbody></tbody>');
                $.each(d, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td>').text(i + 1));
                    $row.append($('<td>').append(formatJsonDate(row.TD_FeesCollectionDate)));
                    $row.append($('<td>').text(row.TD_TransId));
                    $row.append($('<td>').text(row.TR_StudentId));
                    $row.append($('<td>').text(row.TR_StudentName));
                    $row.append($('<td>').text(row.TR_ClassName));
                    $row.append($('<td>').text(row.TR_PaidAmount));
                    $row.append($('<td>').text(row.TD_Paymentmode));
                    $row.append($('<td>').html(
                        "<a href='/StudentManagement/TransportFeesCollectionListView?TD_TransId=" + encodeURIComponent(row.TD_TransId)
                        + "' target='_blank' class='btn btn-success btn-sm'>Print</a>"
                    ));
                    $row.append($('<td>').html(
                        "<a href='/StudentManagement/TransportFeesCollection?id=" + row.TD_TransId + "' class='btn btn-warning btn-sm'>Edit</a>"

                    ));

                    $row.append($('<td>').html(
    "<button type='button' class='btn btn-danger btn-sm' onclick=\"Confirm('" + row.TD_TransId + "')\">Delete</button>"
));

                    $tbody.append($row);
                });
                $data.append($tbody);
                $("#update-panel").html($data);

                $('#tblList').DataTable({
                    order: [[0, "desc"]],
                    pageLength: 10,
                    lengthChange: false,
                    searching: true
                });

            } else {
                $("#update-panel").html("<div>No data Found</div>");
            }
        },
        error: function () {
            ErrorToast('Something went wrong');
        }
    });
}

function Confirm(transId) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(transId);
    }
}
function Deletedata(TD_TransId) {
    console.log("Deleting TD_TransId:", TD_TransId); // DEBUG
    var _data = JSON.stringify({
        TD_TransId: TD_TransId
    });
    $.ajax({
        url: '/JQuery/DeleteTransportCollection',
        type: 'POST',
        data: _data,
        contentType: 'application/json ; utf-8',
        dataType: 'json',
        success: function (data) {
            if (data != null && data != undefined && data.IsSuccess == true) {
                alert(data.Message);
                bindFeesCollectionList();
                //window.location.href = '/FeesCollection/FeesCollection/FeesSessionCollectionList';
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