function UpdateSchool() {

    var fileUpload = $("#SD_Photo").get(0);
    getFiles(fileUpload);

    Documentfile = $('#SD_Photo').val().substring(12);

    if ($('#hdnImage').val() == '~/UploadFile/' || $('#hdnImage').val() == null || $('#hdnImage').val() == '') {
        Documentfile = $('#SD_Photo').val().substring(12);
        Documentpath2 = '~/UploadFile/' + Documentfile;
    }
    else {
        Documentpath2 = $('#hdnImage').val();
    }


    if (ValidateOperation() == true) {
        var _data = JSON.stringify({

            SCM_SCHOOLCODE: $('#SCM_SCHOOLCODE').val(),
            SCM_SCHOOLNAME: $('#SCM_SCHOOLNAME').val(),
            SCM_SCHOOLADDRESS1: $('#SCM_SCHOOLADDRESS1').val(),
            SCM_SCHOOLADDRESS2: $('#SCM_SCHOOLADDRESS2').val(),
            SCM_IMAGENAME: Documentpath2,
            SCM_SECRETARYNAME: $('#SCM_SECRETARYNAME').val(),
            SCM_STATEID: $('#SCM_STATEID').val(),
            SCM_DISTRICTID: $('#SCM_DISTRICTID').val(),
            SCM_NATIONID: $('#SCM_NATIONID').val(),
            SCM_PINCODE: $('#SCM_PINCODE').val(),
            SCM_PHONENO1: $('#SCM_PHONENO1').val(),
            SCM_PHONENO2: $('#SCM_PHONENO2').val(),
            SCM_CONTACTPERSON: $('#SCM_CONTACTPERSON').val(),
            SCM_EMAILID: $('#SCM_EMAILID').val(),
            SCM_WEBSITE: $('#SCM_WEBSITE').val(),
            Userid: $('#hdnUserid').val()
        });
        $.ajax({
            url: '/JQuery/UpdateSchool',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data != undefined && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    window.location.href = '/Masters/School';
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
function ValidateOperation() {
    if ($('#SCM_SCHOOLNAME').val() == '') {
        WarningToast('Please provide School Name.');
        return false;
    }
    if ($('#SCM_EMAILID').val() != '') {
        if (validateEmail($('#SCM_EMAILID').val())) {

        }
        else {
            WarningToast('You Email Address Invalid ....')
            return false;
        }
    }

    return true;
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