function GetLogin() {
    var _data = JSON.stringify({
        user: {
            UM_LOGINID: $.trim($("#UM_LOGINID").val()),
            UM_PASSWORD: $.trim($("#UM_PASSWORD").val()),
        }
    });
    $.ajax({
        url: "/JQuery/GetLogin",
        contentType: "application/json",
        dataType: "json",
        type: "POST",
        data: _data,
        success: function (data) {
            if (data != null && data.Schoollist && data.Schoollist.length > 0) {


                $("#showschool").slideDown();
                BindSchool($('#ddlSchool'), data.Schoollist);
                //BindSession($('#ddlSession'), data.Sessionlist);
            }

            else {
                alert("Invalid Credential...");
                return false;
            }
        },
        error: function (jqxhr, settings, thrownError) {
            console.log(jqxhr.status + '\n' + thrownError);
        }
    });
}
function BindSchool(ddl, dataCollection) {
    $(ddl).get(0).length = 0;
    $(ddl).get(0).options[0] = new Option("Select School", "0", "Selected");
    for (var i = 0; i < dataCollection.length; i++) {
        $(ddl).get(0).options[i + 1] = new Option(dataCollection[i].UM_SCHOOLNAME, dataCollection[i].UM_SCM_SCHOOLID);
        //$('#btnLogin').disabled;
        $("#UM_LOGINID").prop("disabled", true);
        $("#UM_PASSWORD").prop("disabled", true);
        $("#btnLogin").prop("disabled", true);
        $("#btnGo").show();
    }
}
function BindSession(ddl, dataCollection) {
    $(ddl).get(0).length = 0;
    $(ddl).get(0).options[0] = new Option("Select Session", "0", "Selected");
    for (var i = 0; i < dataCollection.length; i++) {
        $(ddl).get(0).options[i + 1] = new Option(dataCollection[i].SM_SESSIONNAME, dataCollection[i].SM_SESSIONID);
    }
}
function GoToDashboard() {
    var schoolid = $('#ddlSchool').val();
    var sesid = $('#ddlSession').val();
    var returnUrl = $.trim($('#ReturnUrl').val());
    if ($('#ddlSchool').val() == 0) {
        WarningToast('Please Select a School!')
        return false;
    }
    if ($('#ddlSession').val() == 0) {
        WarningToast('Please Select a Session!')
        return false;
    }
    var _data = JSON.stringify({
        user: {
            UM_SCM_SCHOOLID: parseInt($.trim($("#ddlSchool").val())),
            UM_SCM_SESSIONID: parseInt($.trim($("#ddlSession").val())),
        }
    });
    $.ajax({
        url: "/JQuery/SelectSchoolAndSession",
        contentType: "application/json",
        dataType: "json",
        type: "POST",
        data: _data,
        success: function (response) {
            if (response != null && response != undefined && response.IsSuccess == true) {
                window.location.href = "/Home/Dashboard?schoolid=" + parseInt(schoolid) + "&sesid=" + parseInt(sesid) + "&returnUrl=" + returnUrl;
            } else {
                ErrorToast(response.Message);
            }
        },
        error: function (jqxhr, settings, thrownError) {
            console.log(jqxhr.status + '\n' + thrownError);
        }
    });


    return true;
}
$(document).ready(function () {
    $("#ddlSchool").prop("disabled", false).css('background-color', 'azure');
    $("#ddlSession").prop("disabled", false).css('background-color', 'azure');
    $("#ddlSchool,#ddlSession").keypress(function (e) {
        SubmitSchoolSession(e);
    });
    $('#ddlSchool').change(function () {
        loadSession($('#ddlSchool option:selected').val());
    });
});
function SubmitSchoolSession(e) {
    var key;
    if (window.event) key = window.event.keyCode;
    else key = e.which;
    if (key == 13) { GoToDashboard(); }
    else return key;
}
function SubmitFormOnEnterKey(e) {
    var key;
    if (window.event) key = window.event.keyCode;
    else key = e.which;
    if (key == 13) { GetLogin(); }
    else return key;
}
$(document).keypress(function (event) {
    SubmitFormOnEnterKey(event)
});
function loadSession(ScId) {
   
    $.get({
        type: "GET",
        url: "/JQuery/GetSessionForSchool",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        data: { SchoolId: ScId },
        success: function (data, status) {
            if (status == "success") {
                //alert(data);
                BindSession($('#ddlSession'), data)
               
            } else {
                ErrorToast("Unable to process the request...", 0);
            }
        }
    });
}
function togglePassword() {
    var passwordInput = $("#UM_PASSWORD");
    var eyeIcon = $("#eyeIcon");

    if (passwordInput.attr("type") === "password") {
        passwordInput.attr("type", "text");
        passwordInput.addClass("password-visible");
        eyeIcon.text("visibility_off");
    } else {
        passwordInput.attr("type", "password");
        passwordInput.removeClass("password-visible");
        eyeIcon.text("visibility");
    }
}