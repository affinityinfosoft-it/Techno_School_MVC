
var ctrlURL = rootDir + "FeesCollection/FeesCollection";

$(document).ready(function () {
    loadRadioCheck();
    paydetailsVerify();
    validateNumeric();
    if ($('#action').val() == "add") {
        $("#radio_Cheque").prop("checked", true).trigger("click");
        $(".tempLateFeesRow").remove();
        CaptureLateFees();
        
    }
    if ($('#action').val() == "update") {
        FetchLateFees();
        FetchServiceCharge();
    }
    CalculateGridAmount();
    //captureFormSubmit();
});

function loadRadioCheck() {

    if ($("#payCash").val() === "True") {
        $("#radio_Cash").prop("checked", true);
    }
    else if ($("#payCheque").val() === "True") {
        $("#radio_Cheque").prop("checked", true);
    }
    else if ($("#payDD").val() === "True") {
        $("#radio_DD").prop("checked", true);
    }
    else if ($("#payCard").val() === "True") {
        $("#radio_Card").prop("checked", true);
    }
    else {
        $("#radio_Online").prop("checked", true);
    }

    // 🔥 This is the missing line
    paydetailsVerify();
}

$(document).on("click", '#radio_Cash', function (e) {
    paydetailsVerify();
    var rdCash = $("#radio_Cash").is(":checked");
    if (rdCash) {
        $(".tempServiceRow").remove();
        $.get(ctrlURL + "/AddServiceCharge?FeesType=C&amount=" + parseFloat("0"));
        $("#PaymodeType").val("Cash");
    }
    CalculateGridAmount();

});
$(document).on('change', '.searchcriteria', function () {
    var elementId = $(this).attr('id');
    var elementName = $(this).attr('name');
    var value = $(this).val();

    if (elementId=="StudentID") {
        $("#CM_CLASSID").val("");
        $('#CM_CLASSID').selectpicker('refresh');
    }
    else {
        $("#StudentID").val("");
    }
    //console.log("ID:", elementId, "Name:", elementName, "Value:", value);
});
$(document).on("click", '#radio_Cheque', function (e) {
    paydetailsVerify();
    var rdChq = $("#radio_Cheque").is(":checked");
    if (rdChq) {
        $(".tempServiceRow").remove();
        $.get(ctrlURL + "/AddServiceCharge?FeesType=C&amount=" + parseFloat("0"));
        $("#PaymodeType").val("Cheque");
    }
    CalculateGridAmount();

});
$(document).on("click", '#radio_DD', function (e) {
    paydetailsVerify();
    var rdDD = $("#radio_DD").is(":checked");
    if (rdDD) {
        $(".tempServiceRow").remove();
        $.get(ctrlURL + "/AddServiceCharge?FeesType=C&amount=" + parseFloat("0"));
        $("#PaymodeType").val("DD");
    }
    CalculateGridAmount();

});
$(document).on("click", '#radio_Card', function () {
    paydetailsVerify();
    $("#PaymodeType").val("Card");
    CalculateServiceFees();
});
$(document).datepicker().on("change", '#FeesDate', function () {
    $(".tempLateFeesRow").remove();
    $(".tempServiceRow").remove();
    CaptureLateFees();
   
});
$(document).on("click", '#radio_Online', function () {
    paydetailsVerify();
    $("#PaymodeType").val("Online");
    CalculateServiceFees();
});
function paydetailsVerify() {

    var isEdit = ($('#action').val() === "update");

    // Hide everything first
    $("#Div_PayDetails").hide();
    $("#Div_BankName, #Div_BranchName, #Div_ChqDDNo, #Div_ChqDate, #Div_TrnsRefNo").hide();

    // Disable all inputs
    $("#BankName, #BranchName, #CheqDDNo, #CheqDDDate, #Card_TrnsRefNo")
        .prop("disabled", true);

    //  Clear values ONLY in ADD mode
    if (!isEdit) {
        $("#BankName, #BranchName, #CheqDDNo, #CheqDDDate, #Card_TrnsRefNo").val("");
    }

    /* ================= CASH ================= */
    if ($("#radio_Cash").is(":checked")) {
        $("#PaymodeType").val("Cash");
        return;
    }

    /* ================= CHEQUE ================= */
    if ($("#radio_Cheque").is(":checked")) {

        $("#Div_PayDetails").show();
        $("#Div_BankName, #Div_BranchName, #Div_ChqDDNo, #Div_ChqDate").show();

        $("#BankName, #BranchName, #CheqDDNo, #CheqDDDate")
            .prop("disabled", false);

        $("#PaymodeType").val("Cheque");
        return;
    }

    /* ================= DD ================= */
    if ($("#radio_DD").is(":checked")) {

        $("#Div_PayDetails").show();
        $("#Div_BankName, #Div_BranchName, #Div_ChqDDNo, #Div_ChqDate").show();

        $("#BankName, #BranchName, #CheqDDNo, #CheqDDDate")
            .prop("disabled", false);

        $("#PaymodeType").val("DD");
        return;
    }

    /* ================= CARD ================= */
    if ($("#radio_Card").is(":checked")) {

        $("#Div_PayDetails").show();
        $("#Div_BankName, #Div_BranchName, #Div_ChqDDNo, #Div_TrnsRefNo").show();

        // Enable only the input fields
        $("#BankName, #BranchName, #CheqDDNo, #Card_TrnsRefNo").prop("disabled", false);

        $("#PaymodeType").val("Card");
        return;
    }
    /* ================= ONLINE ================= */
    if ($("#radio_Online").is(":checked")) {

        $("#Div_PayDetails").show();
        $("#Div_BankName, #Div_BranchName, #Div_TrnsRefNo").show();

        $("#BankName, #BranchName, #Card_TrnsRefNo")
            .prop("disabled", false);

        $("#PaymodeType").val("Online");
        return;
    }
}

$("form").on("submit", function () {

    var clsId = $("#StudentInformation_ClassId").val();

    if (!clsId || clsId === "0") {
        $("#StudentInformation_ClassId").val($("#ClassIdBackup").val());
    }
});

if ($('#action').val() === "add") {
    $("#radio_Cheque").prop("checked", true);
    paydetailsVerify();
}
////////////SERVICE CHARGE DASABLED START

//function FetchServiceCharge() {
//    $.get({
//        url: ctrlURL + "/FetchServiceCharge?FeesType=C",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (serviceFeesModel, status) {
//            if (status == "success") {
//                if (serviceFeesModel.PaymentAmount > 0) {
//                    var amount = serviceFeesModel.PaymentAmount;
//                    amount = parseFloat(amount).toFixed(2);
//                    createServiceChargeRow(amount);
//                }
//                CalculateGridAmount();
//            }
//            else {
//                showMessage("Unable to process the request FetchServiceCharge...", 0);
//            }
//        }
//    });
//}
//function CaptureServiceCharge() {
//    var paymentAmt = $("#PaidAmount").val();
//    paymentAmt = (paymentAmt == "" || isNaN(paymentAmt) == true) ? "0" : parseFloat(paymentAmt).toFixed(2);
//    var amount = (parseFloat(paymentAmt) / 100).toFixed(2); // 1% service charge add
//    createServiceChargeRow(amount);
//    CalculateGridAmount();
//    $.get(ctrlURL + "/AddServiceCharge?FeesType=C&amount=" + parseFloat(amount));
//}
//function CalculateServiceFees() {

//    var isCard = $("#radio_Card").is(":checked");
//    var isOnline = $("#radio_Online").is(":checked");

//    if (!isCard && !isOnline) {
//        $(".tempServiceRow").remove();
//        $.get(ctrlURL + "/AddServiceCharge?FeesType=C&amount=0");
//        CalculateGridAmount();
//        return;
//    }

//    var classId = $("#StudentInformation_ClassId").val();

//    $(".tempServiceRow").remove();

//    var paidAmount = parseFloat($("#PaidAmount").val()) || 0;

//    if (paidAmount <= 0) {
//        CalculateGridAmount();
//        return;
//    }

//    // 1% service charge
//    var serviceCharge = (paidAmount * 1) / 100;
//    serviceCharge = parseFloat(serviceCharge.toFixed(2));

//    if (serviceCharge > 0) {
//        createServiceChargeRow(serviceCharge);

//        // Save service charge in session
//        $.get(ctrlURL + "/AddServiceCharge", {
//            FeesType: "C",
//            amount: serviceCharge
//        });
//    }

//    CalculateGridAmount();

//    if (classId && classId !== "0") {
//        $("#StudentInformation_ClassId").val(classId);
//    }
//}

//////////SERVICE CHARGE DASABLED END
function FetchLateFees() {
    $.get({
        url: ctrlURL + "/FetchLateFeesCharge",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (lateFees, status) {
            if (status == "success") {
                if (lateFees.PaymentAmount > 0) {
                    var amount = lateFees.PaymentAmount;
                    amount = parseFloat(amount).toFixed(2);
                    createLateFeesRow(amount);
                }
                CalculateGridAmount();
            }
            else {
                showMessage("Unable to process the request FetchLateFeesCharge...", 0);
            }
        }
    });
}
function CaptureLateFees() {
    var feesCollectionDate = $("#FeesDate").val();
    if (!feesCollectionDate) return;

    var dateParts = feesCollectionDate.split('/');
    var collectionDate = new Date(dateParts[2], dateParts[1] - 1, dateParts[0]);

    $("#StudentFeesGrid>tbody tr").not('.tempServiceRow').each(function () {
        var $row = $(this);
        var dueDateStr = $row.find('input[id$="DueDate"]').val();
        if (!dueDateStr) return;

        var dueParts = dueDateStr.split('/');
        var dueDate = new Date(dueParts[2], dueParts[1] - 1, dueParts[0]);

        var basePayable = parseFloat($row.find('input[id$="Payable"]').data("base") || $row.find('input[id$="Payable"]').val()) || 0;
        $row.find('input[id$="Payable"]').data("base", basePayable);

        if (collectionDate > dueDate) {
            var fineTypeId = parseInt($row.find('input[id$="FeesTypeId"]').val()) || 1;
            $.ajax({
                type: "POST",
                async: false,
                url: ctrlURL + "/FindLateFeesAmount",
                data: JSON.stringify({
                    dueDate: dueDateStr,
                    collectionDate: $("#FeesDate").val()
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var lateFee = parseFloat(data.Slap_Amount || 0);
                    $row.find('input[id$="LateFees"]').val(lateFee.toFixed(2));

                    // (Optional) store the actual FineTypeId returned by DB
                    $row.find('input[id$="FeesTypeId"]').val(data.FineTypeId);
                }
            });
        } else {
            $row.find('input[id$="LateFees"]').val("0.00");
        }
    });
    $(".tempLateFeesRow").remove();
    CalculateGridAmount();
}




$(document).on('change', '.discamt, .disc,.payamt,.paid', function () {
        var $currentRow = $(this).closest('tr');
        var $allRows = $("#StudentFeesGrid>tbody tr").not('.tempServiceRow');
        var currentIndex = $allRows.index($currentRow);
        var isPaid = $currentRow.find('input[id^="selectIsPaid"]').is(":checked");
        var isDisc = $currentRow.find('input[id^="selectIsDisc"]').is(":checked");



        if (isPaid || isDisc) {
            //  Sequential enforcement
            if (currentIndex > 0) {
                var $prevRow = $allRows.eq(currentIndex - 1);
                var prevPaid = $prevRow.find('input[id^="selectIsPaid"]').is(":checked");
                if (!prevPaid) {
                    messageBox("Please pay the previous installment first (by due date).");
                    $(this).prop("checked", false);
                    return false;
                }
            }

        } else {
            //  If unchecked, clear this + all future rows
            $allRows.each(function (i) {
                if (i >= currentIndex) {
                    var $row = $(this);
                    var insAmt = parseFloat($row.find('input[id$="Payable"]').val()) || 0;
                    
                    $row.find('input[id^="selectIsPaid"]').prop("checked", false);
                    $row.find('input[id$="IsPaidChecked"]').val("false");
                    $row.find('input[id$="PaymentAmount"]').val("0.00");

                    $row.find('input[id^="selectIsDisc"]').prop("checked", false);
                    $row.find('input[id$="IsDiscChecked"]').val("false");
                    $row.find('input[id$="AdustAmnt"]').val("0.00");
                    $row.find('input[id$="DueAmt"]').val(insAmt.toFixed(2));
                }
            });
        }
        //Calculation

        var $row = $(this).closest('tr');
        var PayableAmt = 0, discAmount = 0, paymentAmount = 0
        PayableAmt = parseFloat($row.find('input[id$="Payable"]').val()) || 0;
        discAmount = parseFloat($row.find('input[id$="AdustAmnt"]').val()) || 0;
        paymentAmount = parseFloat($row.find('input[id$="PaymentAmount"]').val()) || 0;
        
        if ($(this).hasClass('discamt')) {
            if (discAmount > 0) {
                $row.find('input[id$="IsDiscChecked"]').val("true")
                $row.find('input[id^="selectIsDisc"]').prop("checked", true)
            }
            else {
                $row.find('input[id$="IsDiscChecked"]').val("false")
                $row.find('input[id^="selectIsDisc"]').prop("checked", false)
            }
        }
        else if ($(this).hasClass('disc')) {

            if ($row.find('input[id^="selectIsDisc"]').prop("checked")) {
                $row.find('input[id$="IsDiscChecked"]').val("true")
                if (discAmount == 0) {
                    $row.find('input[id$="AdustAmnt"]').val(PayableAmt)
                    discAmount = PayableAmt;
                }

            } else {
                $row.find('input[id$="IsDiscChecked"]').val("false")
                $row.find('input[id$="AdustAmnt"]').val(0)
                discAmount = 0;
                $row.find('input[id$="PaymentAmount"]').val(PayableAmt - discAmount)
                paymentAmount = PayableAmt - discAmount;
            }
        }
        else if ($(this).hasClass('payamt')) {

            if (paymentAmount > 0) {
                $row.find('input[id$="IsPaidChecked"]').val("true")
                $row.find('input[id^="selectIsPaid"]').prop("checked", true)
            }
            else {
                $row.find('input[id$="IsPaidChecked"]').val("false")
                $row.find('input[id^="selectIsPaid"]').prop("checked", false)
            }
        }
        else if ($(this).hasClass('paid')) {
            if ($row.find('input[id^="selectIsPaid"]').prop("checked")) {
                $row.find('input[id$="IsPaidChecked"]').val("true")
                if (paymentAmount == 0) {
                    $row.find('input[id$="PaymentAmount"]').val(PayableAmt - discAmount)
                    paymentAmount = PayableAmt - discAmount;
                }

            } else {
                $row.find('input[id$="IsPaidChecked"]').val("false")
                $row.find('input[id$="PaymentAmount"]').val(0)
                paymentAmount = 0;
            }
        }
        
        

        if (discAmount > PayableAmt) {
            discAmount = PayableAmt
            $row.find('input[id$="AdustAmnt"]').val(discAmount)
        }

        if (paymentAmount > (PayableAmt - discAmount)) {
            paymentAmount = (PayableAmt - discAmount)
            $row.find('input[id$="PaymentAmount"]').val(paymentAmount)
        }
    /// Recalculate Due Amount decode by uttaran for checkbox issue 22-01-26

        //var dueAmt = PayableAmt - (paymentAmount + discAmount);
        //if (dueAmt < 0) dueAmt = 0;
       //$row.find('input[id$="DueAmt"]').val(dueAmt.toFixed(2));
   
        if ($row.find('input[id^="selectIsPaid"]').prop("checked")) {

            paymentAmount = PayableAmt - discAmount;

            if (paymentAmount < 0) paymentAmount = 0;

            $row.find('input[id$="PaymentAmount"]').val(paymentAmount.toFixed(2));
            $row.find('input[id$="IsPaidChecked"]').val("true");
        }


        // Trigger recalculation for totals
        CalculateGridAmount();
        
       
});

$(document).on('change', '.latediscamt, .latedisc', function () {
   
    //Calculation

    var $row = $(this).closest('tr');
    var LateFees = 0, LateDiscountAmt = 0
    LateFees = parseFloat($row.find('input[id$="LateFees"]').val()) || 0;
    LateDiscountAmt = parseFloat($row.find('input[id$="LateDiscountAmt"]').val()) || 0;
 
    if ($(this).hasClass('latediscamt')) {
        if (LateDiscountAmt > 0) {
            $row.find('input[id$="IsLateDiscApplied"]').val("true")
            $row.find('input[id^="selectIsLateDisc"]').prop("checked", true)
        }
        else {
            $row.find('input[id$="IsLateDiscApplied"]').val("false")
            $row.find('input[id^="selectIsLateDisc"]').prop("checked", false)
        }
    }
    else if ($(this).hasClass('latedisc')) {

        if ($row.find('input[id^="selectIsLateDisc"]').prop("checked")) {
            $row.find('input[id$="IsLateDiscApplied"]').val("true")
            if (LateDiscountAmt == 0) {
                $row.find('input[id$="LateDiscountAmt"]').val(LateFees)
                LateDiscountAmt = LateFees;
            }

        } else {
            $row.find('input[id$="IsLateDiscApplied"]').val("false")
            $row.find('input[id$="LateDiscountAmt"]').val(0)
            LateDiscountAmt = 0;


        }
    }


        if (LateDiscountAmt > LateFees) {
            LateDiscountAmt = LateFees
            $row.find('input[id$="LateDiscountAmt"]').val(LateDiscountAmt)
    }

   
    CalculateGridAmount();


});





function CalculateGridAmount() {
    var totPayableAmt = 0, totalPayment = 0, totalDiscAmt = 0, totalDue = 0;
    var totalLateFees = 0, totalLateDisc = 0, totalPaidLateFees = 0;

    $('#StudentFeesGrid>tbody tr').each(function () {
       
        var $row = $(this);

        var payableAmt = parseFloat($row.find('input[id$="Payable"]').val()) || 0;
        var payAmt = parseFloat($row.find('input[id$="PaymentAmount"]').val()) || 0;
        var discAmt = parseFloat($row.find('input[id$="AdustAmnt"]').val()) || 0;
        var dueAmt = parseFloat($row.find('input[id$="DueAmt"]').val()) || 0;
        var lateFee = parseFloat($row.find('input[id$="LateFees"]').val()) || 0;
        var lateDisc = parseFloat($row.find('input[id$="LateDiscountAmt"]').val()) || 0;

        // accumulate common totals
        totPayableAmt += payableAmt;
        totalPayment += payAmt;
        totalDiscAmt += discAmt;
        totalDue += dueAmt;

        totalLateFees += lateFee;
        totalLateDisc += lateDisc;

        // Only add Paid Late Fees where checkbox is checked
        var isPaid = $row.find('input[id^="selectIsPaid"]').is(':checked');
        //alert(isPaid)
        if (isPaid) {
            var PaidlateFee = parseFloat($row.find('input[id$="LateFees"]').val()) || 0;
            totalPaidLateFees += (PaidlateFee - lateDisc);
        }
    });
    //totalPaidLateFees = totalLateFees - totalLateDisc;
      // Update footer fields
    $("#TotalAmount").val(totPayableAmt.toFixed(2));
    $("#PaidAmount").val(totalPayment.toFixed(2));
    $("#Discount").val(totalDiscAmt.toFixed(2));
    $("#TotalDue").val(totalDue.toFixed(2));

    // New footer late fees fields
    $("#LateFeesTotal").val(totalLateFees.toFixed(2));
    $("#LateFeesDiscount").val(totalLateDisc.toFixed(2));
    $("#LateFeesPaid").val(totalPaidLateFees.toFixed(2));

    $("#TotalPaidAmount").val((totalPaidLateFees + totalPayment).toFixed(2));

    
}
function captureFormSubmit() {
    $("#btnSubmit").on('click', function (e) {

    })
    $("#btnReset").on('click', function (e) {
    })
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
function createServiceChargeRow(amount) {
    var row = $('#StudentFeesGrid tr:last');
    if ($(row).next().hasClass("tempServiceRow")) { $(".tempServiceRow").remove(); }
    row.after($("<tr class=\"tempServiceRow\"><td><b><input class='text-box single-line' id='serviceFees__FeesName' name='serviceFees__FeesName' readonly='readonly' style='width:104px' type='text' value='ServiceCharge'></b></td>" +
                   "<td colspan=5></td>" +
                   "<td style=\"text-align:center\"><b><input class='text-box single-line'  id='serviceFees__Payable' name='serviceFees__Payable' readonly='readonly' style='width:64px' type='text' value=" + parseFloat(amount).toFixed(2) + "></b></td>" +
                   "<td style=\"text-align:center\"><b><input class='text-box single-line'  id='serviceFees__PaymentAmount' name='serviceFees__PaymentAmount' readonly='readonly' style='width:64px' type='text' value=" + parseFloat(amount).toFixed(2) + "></b></td>" +
                   "<td colspan=1><input type='hidden' id='serviceFees__IsPaidChecked' value='true' name='serviceFees__IsPaidChecked'></td>" +
                   "<td style=\"text-align:center\"><b><input class='text-box single-line'  id='serviceFees__AdustAmnt' name='serviceFees__AdustAmnt' readonly='readonly' style='width:60px' type='text' value=" + parseFloat("0").toFixed(2) + "></b></td>" +
                   "<td colspan=1><input type='hidden' id='serviceFees__IsDiscChecked' value='true' name='serviceFees__IsDiscChecked'></td>" +
                   "<td style=\"text-align:center\"><b><input class='text-box single-line'  id='serviceFees__DueAmt' name='serviceFees__DueAmt' readonly='readonly' style='width:64px' type='text' value=" + parseFloat("0").toFixed(2) + "></b></td>" +
                   "</tr>"));
}
function createLateFeesRow(amount) {
    var row = $('#StudentFeesGrid tr:last');
    if ($(row).next().hasClass("tempLateFeesRow")) { $(".tempLateFeesRow").remove(); }
    row.after($("<tr class=\"tempLateFeesRow\"><td><b><input class='text-box single-line' id='LateFees__FeesName' name='LateFees__FeesName' readonly='readonly' style='width:104px' type='text' value='Late Fees'></b></td>" +
                    "<td colspan=5></td>" +
                    "<td style=\"text-align:center\"><b><input class='text-box single-line'  id='LateFees__Payable' name='LateFees__Payable' readonly='readonly' style='width:64px' type='text' value=" + parseFloat(amount).toFixed(2) + "></b></td>" +
                    "<td style=\"text-align:center\"><b><input class='text-box single-line'  id='LateFees__PaymentAmount' name='LateFees__PaymentAmount' readonly='readonly' style='width:64px' type='text' value=" + parseFloat(amount).toFixed(2) + "></b></td>" +
                    "<td colspan=1><input type='hidden' id='LateFees__IsPaidChecked' value='true' name='LateFees__IsPaidChecked'></td>" +
                    "<td style=\"text-align:center\"><b><input class='text-box single-line'  id='LateFees__AdustAmnt' name='LateFees__AdustAmnt' readonly='readonly' style='width:60px' type='text' value=" + parseFloat("0").toFixed(2) + "></b></td>" +
                    "<td colspan=1><input type='hidden' id='LateFees__IsDiscChecked' value='true' name='LateFees__IsDiscChecked'></td>" +
                    "<td style=\"text-align:center\"><b><input class='text-box single-line'  id='LateFees__DueAmt' name='LateFees__DueAmt' readonly='readonly' style='width:64px' type='text' value=" + parseFloat("0").toFixed(2) + "></b></td>" +
                    "</tr>"));
}

$(document).on("change", "#PaidAmount", function () {
    var totalAmt = parseFloat($("#PaidAmount").val()) || 0;
    //if (totalAmt <= 0) {
    //    messageBox("Please enter a valid amount.");
    //    return;
    //}

    // Select only real fee rows (rows that contain a Payable input) and ignore temp rows
    var $rows = $("#StudentFeesGrid>tbody tr")
        .has('input[id$="Payable"]')
        .not('.tempServiceRow')
        .not('.tempLateFeesRow');

    if ($rows.length === 0) {
        messageBox("No fee rows found to distribute.");
        return;
    }

    // Use integer math (paise) to avoid floating point errors
    var totalAmtPaise = Math.round(totalAmt * 100);
    var gridTotalPaise = 0;

    $rows.each(function () {
        var payable = parseFloat($(this).find('input[id$="Payable"]').val()) || 0;
        var disc = parseFloat($(this).find('input[id$="AdustAmnt"]').val()) || 0;
        var due = payable - disc;
        gridTotalPaise += Math.round(due * 100);
    });

    if (totalAmtPaise > gridTotalPaise) {
        messageBox("Entered amount cannot be greater than the Total Amount: " + (gridTotalPaise / 100).toFixed(2));
        return;
    }

    // If true, existing payments are cleared first. Set to false if you want to add to existing payments.
    var overwriteExistingPayments = true;
    var remainingPaise = totalAmtPaise;

    if (overwriteExistingPayments) {
        $rows.each(function () {
            $(this).find('input[id$="PaymentAmount"]').val("0.00");
            $(this).find('input[id^="selectIsPaid"]').prop('checked', false);
            $(this).find('input[id$="IsPaidChecked"]').val("false");
        });
    } else {
        // subtract existing payments from remaining
        $rows.each(function () {
            var curr = parseFloat($(this).find('input[id$="PaymentAmount"]').val()) || 0;
            remainingPaise -= Math.round(curr * 100);
        });
        if (remainingPaise <= 0) {
            messageBox("Entered amount is not greater than existing payments; nothing to distribute.");
            return;
        }
    }

    // Distribute row-by-row, never exceeding the due for that row
    $rows.each(function () {
        var $row = $(this);
        var payablePaise = Math.round((parseFloat($row.find('input[id$="Payable"]').val()) || 0) * 100);
        var discPaise = Math.round((parseFloat($row.find('input[id$="AdustAmnt"]').val()) || 0) * 100);
        var currPayPaise = Math.round((parseFloat($row.find('input[id$="PaymentAmount"]').val()) || 0) * 100);

        var dueForRowPaise = payablePaise - discPaise - currPayPaise;
        if (dueForRowPaise < 0) dueForRowPaise = 0;

        if (remainingPaise > 0) {
            var toAdd = Math.min(remainingPaise, dueForRowPaise);
            currPayPaise += toAdd;
            remainingPaise -= toAdd;
        }

        $row.find('input[id$="PaymentAmount"]').val((currPayPaise / 100).toFixed(2));
        $row.find('input[id^="selectIsPaid"]').prop('checked', currPayPaise > 0);
        $row.find('input[id$="IsPaidChecked"]').val((currPayPaise > 0).toString());
        $row.find('input[id$="IsDiscChecked"]').val((discPaise > 0).toString());

        var duePaise = payablePaise - discPaise - currPayPaise;
        if (duePaise < 0) duePaise = 0;
        $row.find('input[id$="DueAmt"]').val((duePaise / 100).toFixed(2));
    });

    // Small safety: if a tiny remainder remains (shouldn't happen with paise math), try to fit it into last row's remaining due
    if (remainingPaise > 0) {
        var $last = $rows.last();
        var curr = Math.round((parseFloat($last.find('input[id$="PaymentAmount"]').val()) || 0) * 100);
        var payable = Math.round((parseFloat($last.find('input[id$="Payable"]').val()) || 0) * 100);
        var disc = Math.round((parseFloat($last.find('input[id$="AdustAmnt"]').val()) || 0) * 100);
        var lastDue = payable - disc - curr;
        var add = Math.min(remainingPaise, Math.max(lastDue, 0));
        curr += add;
        remainingPaise -= add;
        $last.find('input[id$="PaymentAmount"]').val((curr / 100).toFixed(2));
        var duePaise = payable - disc - curr;
        if (duePaise < 0) duePaise = 0;
        $last.find('input[id$="DueAmt"]').val((duePaise / 100).toFixed(2));
    }

    CalculateGridAmount();
    CalculateServiceFees();
});
$(document).ready(function () {
    // Create textbox, distribute button, and refresh button HTML dynamically
    //var controlsHtml =
    //    '<div id="autoAdjustControls" style="margin-bottom: 15px;">' +
    //        '<label style="margin-right:10px;"><b>Auto Pay Amount:</b></label>' +
    //        '<input type="number" id="txtAutoPayAmount" class="form-control" ' +
    //            'style="width:200px; display:inline-block; margin-right:10px;" ' +
    //            'placeholder="Enter amount" />' +
    //        '<button type="button" id="btnAutoDistribute" class="btn btn-primary" ' +
    //            'style="margin-right:10px;">Auto Distribute</button>' +
    //        '<button type="button" id="btnRefreshPage" class="btn btn-secondary">Reset</button>' +
    //    '</div>';

    //// Add the controls above the grid
    //$("#StudentFeesGrid").before(controlsHtml);

    // Refresh button click → reload page
    //$(document).on("click", "#btnRefreshPage", function () {
    //    location.reload();

    //});

    //recalculatefees();
    CalculateGridAmount();
});
$(document).on("click", "#btnPrevPaidDetails", function () {
    var studentId = $("#StudentID").val();

    if (!studentId || studentId === "0") {
        alert("Student Id not found!");
        return;
    }

    $("#prevPaidContent").html("<p>Loading...</p>");

    $.ajax({
        type: "GET",
        url: ctrlURL + "/GetPreviousPayments",
        data: { studentId: studentId },
        success: function (data) {
            console.log("AJAX Response:", data);

            if (data.success && data.data.length > 0) {

                var html = "<table class='table table-bordered table-striped'>" +
                    "<thead><tr>" +
                    "<th>Fees Date</th>" +
                    "<th>Instalment No</th>" +
                    "<th>Fees Name</th>" +
                    "<th>Installment Amount</th>" +
                    "<th>Paid Amount</th>" +
                    "<th>Discount</th>" +
                    "<th>Paid By</th>" +
                    "</tr></thead><tbody>";

                var lastDueAmount = 0;

                $.each(data.data, function (i, item) {
                    html += "<tr>" +
                        "<td>" + item.PaymentDate + "</td>" +
                        "<td>" + item.INSTALMENTNO + "</td>" +
                        "<td>" + item.FEM_FEESNAME + "</td>" +
                        "<td class='text-end'>" + item.INSTALMENTAMOUNT.toFixed(2) + "</td>" +
                        "<td class='text-end'>" + item.PYMENTAMOUNT.toFixed(2) + "</td>" +
                        "<td class='text-end'>" + item.Discount.toFixed(2) + "</td>" +
                        "<td>" + item.PAYMODE + "</td>" +
                        "</tr>";

                    lastDueAmount = item.DueAmount; // last record wins
                });

                // Convert number to words
                var dueInWords = numberToWordsIndian(parseInt(lastDueAmount));

                //  FINAL TOTAL DUE ROW
                html += "<tr style='font-weight:bold;background:#f3f3f3'>" +
                    "<td colspan='5'>Total Due Amount : <span style='text-transform:capitalize'>" + dueInWords + "</span></td>" +
                    "<td colspan='2' class='text-end text-danger'>" + lastDueAmount.toFixed(2) + "</td>" +
                    "</tr>";

                html += "</tbody></table>";

                $("#prevPaidContent").html(html);

            } else {
                $("#prevPaidContent").html("<p>No previous payments found.</p>");
            }
        },
        error: function (xhr, status, error) {
            console.error("AJAX Error:", error);
            $("#prevPaidContent").html("<p>Error loading payment history.</p>");
        }
    });

    $("#prevPaidModal").modal("show");
});

function numberToWordsIndian(num) {
    if (num === 0) return "Zero Rupees";

    var a = [
        "", "One", "Two", "Three", "Four", "Five", "Six",
        "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve",
        "Thirteen", "Fourteen", "Fifteen", "Sixteen",
        "Seventeen", "Eighteen", "Nineteen"
    ];

    var b = ["", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"];

    function inWords(n) {
        if (n < 20) return a[n];
        if (n < 100) return b[Math.floor(n / 10)] + (n % 10 ? " " + a[n % 10] : "");
        if (n < 1000) return a[Math.floor(n / 100)] + " Hundred " + (n % 100 ? inWords(n % 100) : "");
        if (n < 100000) return inWords(Math.floor(n / 1000)) + " Thousand " + (n % 1000 ? inWords(n % 1000) : "");
        if (n < 10000000) return inWords(Math.floor(n / 100000)) + " Lakh " + (n % 100000 ? inWords(n % 100000) : "");
        return inWords(Math.floor(n / 10000000)) + " Crore " + (n % 10000000 ? inWords(n % 10000000) : "");
    }

    return inWords(num).trim() + " Rupees";
}

