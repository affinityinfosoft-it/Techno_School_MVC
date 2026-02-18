
var ctrlURL = rootDir + "FeesCollection/FeesCollection";
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
$(document).ready(function () {

    loadRadioCheck();
    validateNumeric();
    gridAmtPress();

    if ($('#action').val() !== "update") {
        $("#radio_Cheque")
            .prop("checked", true)
            .trigger("change");   // 🔥 important
    } else {
        FetchServiceCharge();
        $("input[name='Paymode']:checked").trigger("change");
    }
});
function loadRadioCheck() {
    if ($("#payCash").val() == "True") {
        $("#radio_Cash").prop("checked", true);
    }
    else if ($("#payCheque").val() == "True") {
        $("#radio_Cheque").prop("checked", true);
    }
    else if ($("#payCard").val() == "True") {
        $("#radio_Card").prop("checked", true);
    }
    else {
        $("#radio_DD").prop("checked", true);
    }
}

function paydetailsVerify() {

    // RESET ALL FIRST
    $("#Div_PayDetails").hide();

    $("#Div_BankName, #Div_BranchName, #Div_ChqDDNo, #Div_ChqDate, #Div_TrnsRefNo").hide();

    $("#BankName, #BranchName, #CheqDDNo, #CheqDDDate, #Card_TrnsRefNo")
        .prop("disabled", true);

    // CASH
    if ($("#radio_Cash").is(":checked")) {
        return;
    }

    // COMMON FOR NON-CASH
    $("#Div_PayDetails").show();

    // CHEQUE
    if ($("#radio_Cheque").is(":checked")) {

        $("#Div_BankName, #Div_BranchName, #Div_ChqDDNo, #Div_ChqDate").show();

        $("#BankName, #BranchName, #CheqDDNo, #CheqDDDate")
            .prop("disabled", false);

        return;
    }

    // DD
    if ($("#radio_DD").is(":checked")) {

        $("#Div_BankName, #Div_BranchName, #Div_ChqDDNo, #Div_ChqDate").show();

        $("#BankName, #BranchName, #CheqDDNo, #CheqDDDate")
            .prop("disabled", false);

        return;
    }

    // CARD
    if ($("#radio_Card").is(":checked")) {

        $("#Div_BankName, #Div_BranchName, #Div_ChqDDNo, #Div_TrnsRefNo").show();

        $("#BankName, #BranchName, #CheqDDNo, #Card_TrnsRefNo")
            .prop("disabled", false);

        return;
    }

    // ONLINE
    if ($("#radio_Online").is(":checked")) {

        $("#Div_BankName, #Div_BranchName, #Div_TrnsRefNo").show();

        $("#BankName, #BranchName, #Card_TrnsRefNo")
            .prop("disabled", false);

        return;
    }
}

$("#btnSubmit").on("click", function () {

    if (!$("#PaymodeType").val()) {
        alert("Please select payment mode");
        return false;
    }

    console.log("Submitting Paymode:", $("#PaymodeType").val());
});


$(document).on("change", "input[name='Paymode']", function () {

    let mode = $(this).attr("id").replace("radio_", "");

    //  ALWAYS SET hidden field
    $("#PaymodeType").val(mode);

    paydetailsVerify();

    if (mode === "Card" || mode === "Online") {
        CalculateServiceFees();
    } else {
        $(".tempServiceRow").remove();
        $.get(ctrlURL + "/AddServiceCharge?FeesType=A&amount=0");
        CalculateGridAmount();
    }

    console.log("Paymode changed to:", mode);
});


$(document).on("change", '.disc', function (e) {
    var installmentAmount = parseFloat($(this).closest('tr').find('input[id$="InsAmount"]').val()) || 0;
    var paymentAmount = parseFloat($(this).closest('tr').find('input[id$="PaymentAmount"]').val()) || 0;
    var discAmount = parseFloat($(this).closest('tr').find('input[id$="AdustAmnt"]').val()) || 0;

    // Auto-adjust payment amount when discount changes
    var newPayment = installmentAmount - discAmount;
    if (newPayment < 0) {
        messageBox("Discount amount cannot exceed installment amount!");
        discAmount = 0;
        $(this).closest('tr').find('input[id$="AdustAmnt"]').val(discAmount.toFixed(2));
        newPayment = installmentAmount;
    }
    $(this).closest('tr').find('input[id$="PaymentAmount"]').val(newPayment.toFixed(2));

    // Update DueAmt normally
    var dueamt = installmentAmount - newPayment - discAmount;
    $(this).closest('tr').find('input[id$="DueAmt"]').val(dueamt.toFixed(2));

    // Update flags
    var isPaid = $(this).closest('tr').find('input[id^="selectIsPaid"]').is(":checked");
    var isDisc = $(this).closest('tr').find('input[id^="selectIsDisc"]').is(":checked");
    $(this).closest('tr').find('input[id$="IsPaidChecked"]').val(isPaid);
    $(this).closest('tr').find('input[id$="IsDiscChecked"]').val(isDisc);

    CalculateGridAmount();
    CalculateServiceFees();
});
function gridAmtPress() {
    $('.discamt').keyup(function (e) {
        var installmentAmount = parseFloat($(this).closest('tr').find('input[id$="InsAmount"]').val()) || 0;
        var paymentAmount = parseFloat($(this).closest('tr').find('input[id$="PaymentAmount"]').val()) || 0;
        var discAmount = parseFloat($(this).closest('tr').find('input[id$="AdustAmnt"]').val()) || 0;

        if (discAmount > installmentAmount) {
            messageBox("Discount cannot exceed installment amount!");
            discAmount = 0;
            $(this).closest('tr').find('input[id$="AdustAmnt"]').val(discAmount.toFixed(2));
        }

        // Auto-reduce payment amount
        var newPayment = installmentAmount - discAmount;
        if (newPayment < 0) newPayment = 0;
        $(this).closest('tr').find('input[id$="PaymentAmount"]').val(newPayment.toFixed(2));

        // Update DueAmt
        var dueamt = installmentAmount - newPayment - discAmount;
        if (dueamt < 0) dueamt = 0;
        $(this).closest('tr').find('input[id$="DueAmt"]').val(dueamt.toFixed(2));

        CalculateGridAmount();
        CalculateServiceFees();
    });







    $('.payamt').keyup(function (e) {
        var installmentAmount = $(this).closest('tr').find('input[id$="InsAmount"]').val();
        installmentAmount = installmentAmount == "" ? "0" : installmentAmount;
        var paymentAmount = $(this).closest('tr').find('input[id$="PaymentAmount"]').val();
        paymentAmount = paymentAmount == "" ? "0" : paymentAmount;
        var discAmount = $(this).closest('tr').find('input[id$="AdustAmnt"]').val();
        discAmount = discAmount == "" ? "0" : discAmount;
        var dueAmount = $(this).closest('tr').find('input[id$="DueAmt"]').val();
        dueAmount = dueAmount == "" ? "0" : dueAmount;

        var isPaid = $(this).closest('tr').find('input[id^="selectIsPaid"]').is(":checked");
        var isDisc = $(this).closest('tr').find('input[id^="selectIsDisc"]').is(":checked");

        var dueamt = 0;
        if (parseFloat(paymentAmount) > parseFloat(installmentAmount)) {
            messageBox("Please provide a correct payment Amount !");
            $(this).closest('tr').find('input[id$="PaymentAmount"]').val("0");
            $(this).closest('tr').find('input[id^="selectIsPaid"]').prop("checked", false)
            dueamt = parseFloat(installmentAmount);
            if (isDisc) { dueamt -= parseFloat(discAmount); }
            $(this).closest('tr').find('input[id$="DueAmt"]').val(parseFloat(dueamt).toFixed(2));
            CalculateGridAmount();
            CalculateServiceFees();
            e.preventDefault();
            return false;
        }
        dueamt = parseFloat(installmentAmount);
        if (isPaid) { dueamt -= parseFloat(paymentAmount); }
        if (isDisc) { dueamt -= parseFloat(discAmount); }

        if (parseFloat(dueamt) < 0) {
            messageBox("Please provide a correct payment amount.!");
            $(this).closest('tr').find('input[id$="PaymentAmount"]').val("0");
            $(this).closest('tr').find('input[id^="selectIsPaid"]').prop("checked", false)
            var currentDueAmt = parseFloat(installmentAmount);
            if (isDisc) { currentDueAmt -= parseFloat(discAmount); }
            $(this).closest('tr').find('input[id$="DueAmt"]').val(parseFloat(currentDueAmt).toFixed(2));
            CalculateGridAmount();
            CalculateServiceFees();
            e.preventDefault();
            return false;
        }
        $(this).closest('tr').find('input[id$="DueAmt"]').val(parseFloat(dueamt).toFixed(2));
        $(this).closest('tr').find('input[id$="IsPaidChecked"]').val(isPaid);
        $(this).closest('tr').find('input[id$="IsDiscChecked"]').val(isDisc);
        CalculateGridAmount();
        CalculateServiceFees();
    });
}
function CalculateGridAmount() {

    var totFeesAmt = 0;
    var totalPayment = 0;
    var totalDiscAmt = 0;
    var totalDue = 0;

    $('#StudentFeesGrid > tbody tr').not(".tempServiceRow").each(function () {

        var $row = $(this);

        var installAmt = toNumber($row.find('input[id$="InsAmount"]').val());
        var payAmt = toNumber($row.find('input[id$="PaymentAmount"]').val());
        var discAmt = toNumber($row.find('input[id$="AdustAmnt"]').val());
        var dueAmt = toNumber($row.find('input[id$="DueAmt"]').val());

        var isPaid = $row.find(".paid").is(":checked");
        var isWave = $row.find(".wave").is(":checked");
        var isDisc = $row.find('input[id^="selectIsDisc"]').is(":checked");

        // ✅ ALWAYS add installment to Total Amount
        totFeesAmt += installAmt;

        // ❗ Wave affects payment & due, NOT total amount
        if (!isWave) {

            if (isPaid) totalPayment += payAmt;
            if (isDisc) totalDiscAmt += discAmt;

            totalDue += dueAmt;
        }
    });

    $("#TotalAmount").val(totFeesAmt.toFixed(2));
    $("#PaidAmount").val(totalPayment.toFixed(2));
    $("#Discount").val(totalDiscAmt.toFixed(2));
    $("#TotalDue").val(totalDue.toFixed(2));
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
                   "<td colspan=4></td>" +
                   "<td style=\"text-align:center\"><b><input class='text-box single-line'  id='serviceFees__InsAmount' name='serviceFees__InsAmount' readonly='readonly' style='width:76px' type='text' value=" + parseFloat(amount).toFixed(2) + "></b></td>" +
                   "<td style=\"text-align:center\"><b><input class='text-box single-line'  id='serviceFees__PaymentAmount' name='serviceFees__PaymentAmount' readonly='readonly' style='width:76px' type='text' value=" + parseFloat(amount).toFixed(2) + "></b></td>" +
                   "<td colspan=1><input type='hidden' id='serviceFees__IsPaidChecked' value='true' name='serviceFees__IsPaidChecked'></td>" +
                   "<td style=\"text-align:center\"><b><input class='text-box single-line'  id='serviceFees__AdustAmnt' name='serviceFees__AdustAmnt' readonly='readonly' style='width:65px' type='text' value=" + parseFloat("0").toFixed(2) + "></b></td>" +
                   "<td colspan=1><input type='hidden' id='serviceFees__IsDiscChecked' value='true' name='serviceFees__IsDiscChecked'></td>" +
                   "<td style=\"text-align:center\"><b><input class='text-box single-line'  id='serviceFees__DueAmt' name='serviceFees__DueAmt' readonly='readonly' style='width:76px' type='text' value=" + parseFloat("0").toFixed(2) + "></b></td>" +
                   "</tr>"));
}

function toNumber(val) {
    var num = parseFloat(val);
    return isNaN(num) ? 0 : num;
}

$(document).on("change", ".paid, .wave", function () {

    var $row = $(this).closest("tr");

    var isWaveClicked = $(this).hasClass("wave");
    var isPaidClicked = $(this).hasClass("paid");

    /* ---------------- AUTO RESOLVE ---------------- */

    if (isWaveClicked && this.checked) {
        $row.find(".paid").prop("checked", false);
    }

    if (isPaidClicked && this.checked) {
        $row.find(".wave").prop("checked", false);
    }

    var isWave = $row.find(".wave").is(":checked");
    var isPaid = $row.find(".paid").is(":checked");

    var insAmt = getOriginalInsAmount($row);
    var discAmt = toNumber($row.find('input[id$="AdustAmnt"]').val());

    /* ---------------- WAVE LOGIC ---------------- */

    if (isWave) {

        $row.find("input, select").not(".wave").prop("readonly", true);

        // Get original installment
        var originalAmt = getOriginalInsAmount($row);
        if (originalAmt <= 0) {
            originalAmt = toNumber($row.find('input[id$="InsAmount"]').val());
        }

        // Show installment amount in Payment column
        $row.find('input[id$="PaymentAmount"]').val(originalAmt.toFixed(2));

        // No discount for wave
        $row.find('input[id$="AdustAmnt"]').val("0.00");

        // Due should be zero
        $row.find('input[id$="DueAmt"]').val("0.00");

        // Keep hidden flags false so totals ignore it
        $row.find('input[id$="IsPaidChecked"]').val("false");
        $row.find('input[id$="IsDiscChecked"]').val("false");

        recalcAll();
        return;
    }


    /* ---------------- PAID LOGIC ---------------- */

    if (isPaid) {

        $row.find("input, select").prop("readonly", false);

        var payAmt = Math.max(insAmt - discAmt, 0);

        $row.find('input[id$="PaymentAmount"]').val(payAmt.toFixed(2));
        $row.find('input[id$="DueAmt"]').val("0.00");

        recalcAll();
        return;
    }

    /* ---------------- UNCHECK LOGIC ---------------- */

    if (!isWave && !isPaid) {

        $row.find("input, select").prop("readonly", false);

        var newDue = Math.max(insAmt - discAmt, 0);

        $row.find('input[id$="PaymentAmount"]').val("0.00");
        $row.find('input[id$="DueAmt"]').val(newDue.toFixed(2));
    }

    recalcAll();
});



///TOTAL WAVE AMOUNT
function CalculateWaveAmount() {

    var totalWave = 0;

    $("#StudentFeesGrid > tbody tr").not(".tempServiceRow").each(function () {

        var $row = $(this);

        if (!$row.find(".wave").is(":checked")) return;

        var original = toNumber($row.find('input[id$="InsAmount"]').attr("data-original"));
        var current = toNumber($row.find('input[id$="InsAmount"]').val());

        var amt = original > 0 ? original : current;

        totalWave += amt;
    });

    $("#WaveAmount").val(totalWave.toFixed(2));
}




/// CENTRALIZED RECALC

function recalcAll() {

    CalculateGridAmount();
    CalculateWaveAmount();

    if (typeof CalculateServiceFees === "function") {
        CalculateServiceFees();
    }
}

function getOriginalInsAmount($row) {
    return toNumber($row.find('input[id$="InsAmount"]').attr("data-original"));
}

$(document).ready(function () {

    $("#StudentFeesGrid > tbody tr").each(function () {

        var $ins = $(this).find('input[id$="InsAmount"]');
        var val = toNumber($ins.val());

        if (val > 0) {
            $ins.attr("data-original", val);
        }
    });

    recalcAll();
});



///Add by Uttaran on 12-18-2025 ---Auto Adjust Amount---///
$(document).on("click", "#btnAutoDistribute", function () {
    var totalAmt = parseFloat($("#txtAutoPayAmount").val()) || 0;
    if (totalAmt <= 0) {
        messageBox("Please enter a valid amount.");
        return;
    }

    var gridTotal = 0;
    var $rows = $("#StudentFeesGrid>tbody tr")
        .has('input[id$="InsAmount"]')
        .not('.tempServiceRow')
        .filter(function () {
            return !$(this).find(".wave").is(":checked");
        });

    $rows.each(function () {

        var $row = $(this);

        if ($row.find(".wave").is(":checked")) return;

        var insAmt = parseFloat($row.find('input[id$="InsAmount"]').val()) || 0;
        var discAmt = parseFloat($row.find('input[id$="AdustAmnt"]').val()) || 0;

        gridTotal += (insAmt - discAmt);
    });


    if (totalAmt > gridTotal) {
        messageBox("Entered amount cannot be greater than the Total Amount: " + gridTotal.toFixed(2));
        return;
    }

    if ($rows.length === 0) {
        messageBox("No fee rows found to distribute.");
        return;
    }

    var overwriteExistingPayments = true;
    var remaining = totalAmt;

    if (!overwriteExistingPayments) {
        $rows.each(function () {
            var currPay = parseFloat($(this).find('input[id$="PaymentAmount"]').val()) || 0;
            remaining -= currPay;
        });
        if (remaining <= 0) {
            messageBox("Entered amount is not greater than existing payments; nothing to distribute.");
            return;
        }
    } else {
        $rows.each(function () {
            $(this).find('input[id$="PaymentAmount"]').val("0.00");
            $(this).find('input[id^="selectIsPaid"]').prop('checked', false);
            $(this).find('input[id$="IsPaidChecked"]').val("false");
        });
    }

    $rows.each(function (index) {
        var $row = $(this);
        var insAmt = parseFloat($row.find('input[id$="InsAmount"]').val()) || 0;
        var discAmt = parseFloat($row.find('input[id$="AdustAmnt"]').val()) || 0;
        var currPay = parseFloat($row.find('input[id$="PaymentAmount"]').val()) || 0;

        var dueForRow = insAmt - discAmt - currPay;
        var isLast = (index === $rows.length - 1);
        var toAdd = 0;

        if (remaining > 0) {
            if (!isLast) {
                toAdd = Math.min(remaining, Math.max(dueForRow, 0));
            } else {
                toAdd = remaining;
            }
        }

        var newPay = currPay + toAdd;
        var newDue = insAmt - discAmt - newPay;

        $row.find('input[id$="PaymentAmount"]').val(newPay.toFixed(2));
        $row.find('input[id^="selectIsPaid"]').prop('checked', newPay > 0);
        $row.find('input[id$="IsPaidChecked"]').val((newPay > 0).toString());
        $row.find('input[id$="IsDiscChecked"]').val((discAmt > 0).toString());
        $row.find('input[id$="DueAmt"]').val(newDue.toFixed(2));

        remaining -= toAdd;
    });

    if (remaining > 0) {
        var $last = $rows.last();
        var curr = parseFloat($last.find('input[id$="PaymentAmount"]').val()) || 0;
        var insAmt = parseFloat($last.find('input[id$="InsAmount"]').val()) || 0;
        var discAmt = parseFloat($last.find('input[id$="AdustAmnt"]').val()) || 0;
        $last.find('input[id$="PaymentAmount"]').val((curr + remaining).toFixed(2));
        $last.find('input[id$="DueAmt"]').val((insAmt - discAmt - (curr + remaining)).toFixed(2));
        remaining = 0;
    }

    CalculateGridAmount();
    CalculateServiceFees();
});
$(document).ready(function () {
    // Create textbox, adjust button, and refresh button HTML dynamically
    var controlsHtml =
        '<div id="autoAdjustControls" style="margin-bottom: 15px;">' +
            '<label style="margin-right:10px;"><b>Auto Pay Amount:</b></label>' +
            '<input type="number" id="txtAutoPayAmount" class="form-control" ' +
                   'style="width:200px; display:inline-block; margin-right:10px;" ' +
                   'placeholder="Enter amount">' +
            '<button type="button" id="btnAutoDistribute" class="btn btn-primary" style="margin-right:10px;">' +
                'Auto Distribute' +
            '</button>' +
            '<button type="button" id="btnRefreshPage" class="btn btn-secondary">' +
                'Reset' +
            '</button>' +
        '</div>';

    // Append above your grid
    $("#StudentFeesGrid").before(controlsHtml);

    // Refresh button click event
    $(document).on("click", "#btnRefreshPage", function () {
        location.reload(); // Refreshes the current page
    });
});



//////////////SERVICE CHARGE DASABLED START
//function FetchServiceCharge() {
//    $.get({
//        url: ctrlURL + "/FetchServiceCharge?FeesType=A",
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
//function CalculateServiceFees() {
//    var rdCard = $("#radio_Card").is(":checked");
//    if (rdCard) {
//        $(".tempServiceRow").remove();
//        CalculateGridAmount();
//        CaptureServiceCharge();
//        $("#PaymodeType").val("Card");
//    }
//}
//function CaptureServiceCharge() {
//    var paymentAmt = $("#PaidAmount").val();
//    paymentAmt = (paymentAmt == "" || isNaN(paymentAmt) == true) ? "0" : parseFloat(paymentAmt).toFixed(2);
//    var amount = (parseFloat(paymentAmt) / 100).toFixed(2); // 1% service charge add
//    createServiceChargeRow(amount);
//    CalculateGridAmount();
//    $.get(ctrlURL + "/AddServiceCharge?FeesType=A&amount=" + parseFloat(amount));
//}
////////////SERVICE CHARGE DASABLED END

