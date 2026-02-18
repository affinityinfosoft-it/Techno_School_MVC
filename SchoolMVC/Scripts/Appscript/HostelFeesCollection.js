$(document).ready(function () {

    Id = getParameterByName('id');

    if (Id != '') {
        // EDIT MODE
        bindGridForEdit(Id);
    } else {
        // ADD MODE → default Cheque
        $("#radio_Cheque").prop("checked", true);
        $("#ChkId").val("Cheque");

        // IMPORTANT: show fields AFTER checking radio
        paydetailsVerify();
    }

    $("#btnSearch").click(function () {
        bindGrid();
    });

    $("#btnSave").click(function () {
        AddFees();
    });

});

$('input.editor-active').on('change', function () {
    alert('checkbox clicked'); // it is never shown
    cb = $(this).prop('checked');
    console.log(cb)
});
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
    paydetailsVerify();
    if ($("#radio_Card").is(":checked")) {
        $("#ChkId").val("Card");
    }
});
$(document).on('change', 'input.chk', function () {
    console.log('checkbox clicked', this.checked);
});
$(document).on("click", "#radio_Online", function () {
    if ($(this).is(":checked")) {
        $("#ChkId").val("Online");
        paydetailsVerify();
    }
});


function bindGrid() {
    $('#update-panel').html('loading data.....');
    $("#tblStudent").show();
    var _data = JSON.stringify({
        obj: {
            Hostel_StudentId: $('#HTM_StudentId').val(),
        }
    });
    $.ajax({
        url: rootDir + "JQuery/GetHotelFeesCollectionList",
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
                    if (row.Hostel_Id != 0) {
                        for (var i = row.Hostel_Month; i <= 12; i++) {
                            var $row = $('<tr/>');
                            $row.append($('<td>').append(row.Hostel_StudentId));
                            $row.append($('<td>').append(row.Hostel_FeesName));
                            $row.append($('<td style=display:none>').append(row.Hostel_FeesId));
                            $row.append($('<td>').append(i));
                            $row.append($('<td>').append(row.Hoset_Fare));
                            $row.append($('<td/>').html('<input type=checkbox class="chk" id="chk' + i + '" onclick="GetTotalAmount();" />'));

                            $data.append($row);
                        }
                    }
                    if (row.Hostel_Id == 0) {
                        var $row = $('<tr/>');
                        $row.append($('<td>').append(row.Hostel_StudentId));
                        $row.append($('<td>').append(row.Hostel_FeesName));
                        $row.append($('<td style=display:none>').append(row.Hostel_FeesId));
                        $row.append($('<td>').append(row.Hostel_Month));
                        $row.append($('<td>').append(row.Hoset_Fare));
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
                $noData = "<div>This Student has no Hostel Fees</td>"
                $("#update-panel").html($noData);
            }
        },
        failure: function () {
            ErrorToast('something wrong happen');
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
            //arrStudentID.push(tblList.rows[cellrow + 1].cells[0].innerHTML)
            TotalFare = TotalFare + parseInt(tblList.rows[cellrow + 1].cells[4].innerHTML);


        });
        //arrStudentID.join(',');


        $('#HFD_PaidAmount').val(TotalFare);
        return TotalFare;
    }
    else {
        alert('Please Select Student first.')
    }
}
function paydetailsVerify() {

    var payMode = $("input[name='HFD_Paymentmode']:checked").val();

    if (payMode === "Cheque" || payMode === "DD") {
        $("#Div_PayDetails").show();
        $("#Div_ChqDate").show();
        $("#Div_TrnsRefNo").hide();
    }
    else if (payMode === "Card" || payMode === "Online") {
        $("#Div_PayDetails").show();
        $("#Div_ChqDate").hide();
        $("#Div_TrnsRefNo").show();
    }
    else {
        $("#Div_PayDetails").hide();
    }
}

function convertToSqlDate(dateStr) {
    if (!dateStr) return null;

    var parts = dateStr.split('/'); // dd-MM-yyyy
    return parts[2] + '-' + parts[1] + '-' + parts[0]; // yyyy-MM-dd
}
function AddFees() {

    /* =========================
       1. Payment Mode (SAFE)
    ========================== */
    var payMode = $("input[name='HFD_Paymentmode']:checked").val();
    if (!payMode) {
        alert("Please select payment mode");
        return;
    }
    $("#ChkId").val(payMode);


    /* =========================
       2. Selected Installments
    ========================== */
    var arrMonthID = [];
    var arrFare = [];
    var arrFeesId = [];
    var arrStudentId = [];

    if (!$('#tblList>tbody input[type="checkbox"]').is(":checked")) {
        alert('Please select an installment to continue.');
        return;
    }

    var tblList = document.getElementById('tblList');

    $('#tblList>tbody input[type="checkbox"]:checked').each(function () {
        var rowIndex = $(this).closest('tr').index() + 1;

        arrStudentId.push(tblList.rows[rowIndex].cells[0].innerHTML);
        arrFeesId.push(tblList.rows[rowIndex].cells[2].innerHTML);
        arrMonthID.push(tblList.rows[rowIndex].cells[3].innerHTML);
        arrFare.push(tblList.rows[rowIndex].cells[4].innerHTML);
    });


    /* =========================
       3. Validate Form Fields
    ========================== */
    if ($('#HFD_FeesCollectionDate').val() === '') {
        alert('Please provide a collection date');
        return;
    }

    if (payMode === 'Cheque' || payMode === 'DD') {
        if (
            $('#HFD_BankId').val() === '' ||
            $('#HFD_BranchName').val() === '' ||
            $('#HFD_CheqDDNo').val() === '' ||
            $('#HFD_CheqDDDate').val() === ''
        ) {
            alert('Please fill all cheque/DD details');
            return;
        }
    }

    // Card / Online
    if (payMode === 'Card' || payMode === 'Online') {
        if ($('#HFD_Card_TrnsRefNo').val() === '') {
            alert('Please enter transaction reference number');
            return;
        }
    }


    /* =========================
       4. Build Fees Structure
    ========================== */
    var PaidFeesStructure = [];
    var FeesStructure = [];

    // All rows
    for (var i = 1; i < tblList.rows.length; i++) {
        FeesStructure.push({
            HTM_FeesHeadId: parseInt(tblList.rows[i].cells[2].innerHTML),
            HTM_StudentId: tblList.rows[i].cells[0].innerHTML,
            HTM_InstalmentNo: parseInt(tblList.rows[i].cells[3].innerHTML),
            HTM_MonthlyFare: parseFloat(tblList.rows[i].cells[4].innerHTML)
        });
    }

    // Selected rows only
    for (var j = 0; j < arrMonthID.length; j++) {
        PaidFeesStructure.push({
            HTM_FeesHeadId: arrFeesId[j],
            HTM_StudentId: arrStudentId[j],
            HTM_InstalmentNo: arrMonthID[j],
            HTM_MonthlyFare: arrFare[j]
        });
    }


    /* =========================
       5. Final Payload
    ========================== */
    var _data = JSON.stringify({
        obj: {
            PaidFeesStructure: PaidFeesStructure,
            FeesStructure: FeesStructure,

            HFD_Paymentmode: payMode,
            HFD_PaidAmount: $("#HFD_PaidAmount").val(),
            HFD_FeesCollectionDate: convertToSqlDate( $("#HFD_FeesCollectionDate").val()),
            HFD_BankId: $("#HFD_BankId").val(),
            HFD_BranchName: $("#HFD_BranchName").val(),
            HFD_CheqDDNo: $("#HFD_CheqDDNo").val(),
            HFD_CheqDDDate: convertToSqlDate( $("#HFD_CheqDDDate").val()),
            HFD_Card_TrnsRefNo: $("#HFD_Card_TrnsRefNo").val(),

            HTM_TransId: Id
        }
    });


    /* =========================
       6. AJAX Save
    ========================== */
    $.ajax({
        type: "POST",
        url: "/JQuery/InsertHostelFeesCollection",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            if (data.IsSuccess === true) {

                alert('Data has been saved successfully.');

                // Open receipt
                window.open(
                    '/StudentManagement/HostelFeesCollectionListView?HFD_FeesTransId=' + data.HFD_FeesTransId,
                    '_blank'
                );

                // Redirect to list
                window.location.href = '/StudentManagement/HosteltFeesCollectionList';
            }
            else {
                alert(data.Message || 'Failed to save data');
            }
        },
        error: function () {
            alert('Process failed. Please try again.');
        }
    });
}


function ValidateForm()
{
    if ($('#HFD_FeesCollectionDate').val()=='')
    {
        alert('Please provide a collection date');
        return false;
    }
    if (Id == '') {
        if ($('#ChkId').val() == '') {
            alert('Please select a payment mode');
            return false;
        }
    }
    if ($('#ChkId').val() == 'Cheque' || $('#ChkId').val() == 'DD') {
        if ($('#HFD_BankName').val() == '' || $('#HFD_BranchName').val() == '' || $('#HFD_CheqDDNo').val() == '' || $('#HFD_CheqDDDate').val() == '')
        {
            alert('Please fill all fields.');
            return false;
        }
    }
    if ($('#ChkId').val() == 'Card') {
        if ($('#HFD_Card_TrnsRefNo').val() == '') {
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
                HTM_TransId: Id,
            }
    });
    $.ajax({
        url: rootDir + "JQuery/GetHostelFeesCollectionListForEdit",
        dataType: 'json',
        contentType: 'application/json',
        data: _data,
        type: 'POST',
        success: function (d) {
            if (d.length > 0) {
                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                $header = "<thead><tr><th>Student Id</th><th>Fees Head</th><th style=display:none>Fees Id</th><th>Month</th><th>Payment Amount</th><th>Pay</th></tr></thead>";
                $data.append($header);
                var BankId = "";
                var Branch = "";
                var chkdate = "";
                var FeesDate = "";
                var PaymentMode = "";
                var CheqDDNo = "";
                $.each(d, function (i, row) {
                    BankId = row.HFD_BankId;
                    Branch = row.HFD_BranchName;
                    var d = new Date(parseInt(row.HFD_FeesCollectionDate.slice(6, -2)));
                    FeesDate = d.getDate() + '/' + (1 + d.getMonth()) + '/' + d.getFullYear().toString()
                    PaymentMode = row.HFD_Paymentmode;
                    CheqDDNo = row.HFD_CheqDDNo;
                    RefNo = row.HFD_Card_TrnsRefNo;
                    //if (PaymentMode == 'DD' || PaymentMode == 'Cheque') {
                    //    var d2 = new Date(parseInt(row.TD_CheqDDDate.slice(6, -2)));
                    //    chkdate = d2.getDate() + '/' + (1 + d2.getMonth()) + '/' + d2.getFullYear().toString()
                    //}
                    var $row = $('<tr/>');
                    $row.append($('<td>').append(row.HTM_StudentId));
                    $row.append($('<td>').append(row.HTM_FeesName));
                    $row.append($('<td style=display:none>').append(row.HTM_FeesHeadId));
                    $row.append($('<td>').append(row.HTM_InstalmentNo));
                    $row.append($('<td>').append(row.HTM_MonthlyFare));
                    $row.append($('<td/>').html('<input type=checkbox class="chk"checked id="chk' + row.TR_TransId + '" onclick="GetTotalAmount();" />'));
                    $data.append($row);
                });
                $("#update-panel").html($data);
                $('#tblList').dataTable
                GetTotalAmount();
                CheckRadioForEdit(PaymentMode);
                $('#HFD_BankId').val(BankId);
                $("#HFD_BankId").selectpicker('refresh');
                convertToSqlDate($('#HFD_FeesCollectionDate').val(FeesDate));
                $('#HFD_BranchName').val(Branch);
                $('#HFD_CheqDDNo').val(CheqDDNo);
                $('#HFD_Card_TrnsRefNo').val(RefNo);
                convertToSqlDate($('#TD_CheqDDDate').val(chkdate));
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
function CheckRadioForEdit(paymode) {

    $("#ChkId").val(paymode);

    if (paymode === 'Cash') $("#radio_Cash").prop("checked", true);
    else if (paymode === 'Cheque') $("#radio_Cheque").prop("checked", true);
    else if (paymode === 'DD') $("#radio_DD").prop("checked", true);
    else if (paymode === 'Card') $("#radio_Card").prop("checked", true);
    else if (paymode === 'Online') $("#radio_Online").prop("checked", true);

    paydetailsVerify();
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

    var studentId = $.trim($('#StudentId').val());
    var fromDate = $('#FromDate').val();
    var toDate = $('#ToDate').val();
    var classId = $('#SD_ClassId').val();

    // KEY FIX
    if (studentId !== '') {
        fromDate = null;
        toDate = null;
    }

    var _data = JSON.stringify({
        obj: {
            HTM_StudentId: studentId,
            fromDate: fromDate,
            toDate: toDate,
            ClassId: classId
        }
    });

    $.ajax({
        url: rootDir + "JQuery/LoadHostelFeesCollectionList",
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        data: _data,

        success: function (d) {

            if (d && d.length > 0) {

                // Destroy existing DataTable
                if ($.fn.DataTable.isDataTable('#tblList')) {
                    $('#tblList').DataTable().destroy();
                }

                var $table = $('<table id="tblList" class="table table-bordered table-striped"></table>');

                var header =
                    '<thead>' +
                        '<tr>' +
                            '<th>SL</th>' +
                            '<th>Fees Date</th>' +
                            '<th>Trans Id</th>' +
                            '<th>Student Id</th>' +
                            '<th>Student Name</th>' +
                            '<th>Class</th>' +
                            '<th>Paid Amount</th>' +
                            '<th>Payment Mode</th>' +
                            '<th>Print</th>' +
                            '<th>Edit</th>' +
                            '<th>Delete</th>' +
                        '</tr>' +
                    '</thead>';

                $table.append(header);

                var $tbody = $('<tbody></tbody>');

                $.each(d, function (i, row) {

                    var tr = '<tr>';
                    tr += '<td>' + (i + 1) + '</td>';
                    tr += '<td>' + formatJsonDate(row.HFD_FeesCollectionDate) + '</td>';
                    tr += '<td>' + row.HTM_TransId + '</td>';
                    tr += '<td>' + row.HTM_StudentId + '</td>';
                    tr += '<td>' + row.HTM_StudentName + '</td>';
                    tr += '<td>' + row.HTM_ClassName + '</td>';
                    tr += '<td>' + row.HFD_PaidAmount + '</td>';
                    tr += '<td>' + row.HFD_Paymentmode + '</td>';

                    // Print
                    tr += '<td>' +
                            '<a href="/StudentManagement/HostelFeesCollectionListView?HTM_TransId=' +
                            encodeURIComponent(row.HTM_TransId) +
                            '" target="_blank" class="btn btn-success btn-sm">Print</a>' +
                          '</td>';

                    // Edit
                    tr += '<td>' +
                            '<a href="/StudentManagement/HostelFeesCollection?id=' +
                            row.HTM_TransId +
                            '" class="btn btn-warning btn-sm">Edit</a>' +
                          '</td>';

                    // Delete
                    tr += '<td>' +
                            '<button type="button" class="btn btn-danger btn-sm" ' +
                            'onclick="Confirm(\'' + row.HTM_TransId + '\')">Delete</button>' +
                          '</td>';

                    tr += '</tr>';

                    $tbody.append(tr);
                });

                $table.append($tbody);
                $('#update-panel').html($table);

                // Initialize DataTable
                $('#tblList').DataTable({
                    order: [[0, 'desc']],
                    pageLength: 10
                });

            } else {
                $('#update-panel').html('<div class="alert alert-warning">No data found</div>');
            }
        },

        error: function (xhr) {
            console.log(xhr.responseText);
            alert('Something went wrong');
        }
    });
}

function Confirm(transId) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(transId);
    }
}
function Deletedata(transId) {
    console.log("Deleting TransId:", transId); // DEBUG
    var _data = JSON.stringify({
        HTM_TransId: transId
    });
    $.ajax({
        url: '/JQuery/DeleteHostelCollection',
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