$(document).ready(function () {
    validateNumeric();
});
function ValidateOperation() {

    if ($('#Slap_Name').val() == "") {

        WarningToast('Please Provide Slab Name.');

        return false;
    }

    if ($("#Slap_Amount").val() == "") {

        WarningToast('Please provide Slab Amount.');

        return false;
    }
   
    return true;
}
function InsertUpdateLateFeesSlap() {
    if (ValidateOperation() == true) {


        var isActiveValue = $("#IsActive").is(":checked") ? 1 : 0;
        console.log("IsActive before sending:", isActiveValue); // debug
        var _data = JSON.stringify({
            obj: {
                Id: parseInt($("#Id").val()),
                Slap_Name: $.trim($('#Slap_Name').val()),
                Slap_Amount: parseFloat($('#Slap_Amount').val()),
                //Slap_FineTypeID: parseInt($('#Slap_FineTypeID').val()),
                Slap_FineTypeID: $("#Slap_FineTypeID").val(),
                IsActive: isActiveValue
            }
        });

        $.ajax({
            url: rootDir + 'JQuery/InsertUpdateLateFeesSlap',
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
                        window.location.href = rootDir + 'Masters/LateFeesMasterList';
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



