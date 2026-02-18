
var xhrPool = [];
var capchaVerified = true;
$.ajaxSetup({
    cache: false,   // Disable caching of AJAX responses
    beforeSend: function (jqXHR) {
        xhrPool.push(jqXHR);
    },
    complete: function (jqXHR) {
        initTimeout();
        var index = xhrPool.indexOf(jqXHR);
        if (index > -1) {
            xhrPool.splice(index, 1);
        }
    }
});

$(document).ready(function () {
    showLoadMessages();
    $('.Date01, date').datepicker({ dateFormat: "dd/mm/yy", changeMonth: true, changeYear: true, yearRange: "-100:+100" });
    __addCustomValidators();
    addFileUploadValidator();
    captureEnter();
    captureBackSpace();
    captureValidationMsgAsTooltip();
    captureDateValidation();
    captureResetPage();
   
    captureLogOff();
    enableValidations();
    captureFormSubmissions();
    //initTimeout();
    //defeatTimeout();
    //onlineStatus();
});
function defeatTimeout() {
    window.setInterval(function () { $.ajax({ url: rootDir + "Home/HomeDashboard" }); }, 300000); // ping every 5 min
}
function onlineStatus() {
    window.online = true;
    window.addEventListener("online", function () { window.online = true; $("#imgOnline").hide(); }, false);
    window.addEventListener("offline", function () { window.online = false; $("#imgOnline").show(); }, false);
}
function initTimeout() {
    if (typeof sessionTimeout == "undefined") return;
    window.sessionEndTime = new Date().getTime() + (sessionTimeout * 60 * 1000);
    clearInterval(window.sessionTimer);
    window.sessionTimer = setInterval(checkTimeout, 5000);
    function checkTimeout() {
        var timeleft = Math.round((window.sessionEndTime - new Date()) / 1000);
        if (window.confirmDiv != undefined) {
            if (timeleft <= 0) {
                window.confirmDiv.html("<div style=\"color:red;font-weight:bolder;text-align:center\"><br/><br/>Your session has expired.</div>");
                clearInterval(window.sessionTimer);
                $('div .ui-dialog-buttonset .ui-button:contains("No"), .ui-dialog-titlebar-close').hide();
                $('div .ui-dialog-buttonset .ui-button:contains("Yes")').text('Ok').on('click', function () { window.location = rootDir + 'Login/Index'; });
                return;
            }
            var regexp = /(next)(.+?)(?= seconds)/;
            var text = window.confirmDiv.html().replace(regexp, "$1 " + timeleft)
            window.confirmDiv.html(text)
            return;
        }
        if (timeleft <= 60) {
            window.confirmDiv = confirmBox("Your session would expire in the next " + (timeleft) + " seconds. Do you want to continue?", reactivatePage, null, closeCallback);
        }
        function closeCallback() { window.confirmDiv = null; clearInterval(window.sessionTimer); }
        function reactivatePage(status) {
            if (status) $.ajax({ url: rootDir + "Home/HomeDashboard" });
        }
    }
}
function messageBox(str, callback) {
    $("<div><p><span class='ui-icon ui-icon-circle-close' style='float: left; margin: 0 7px 20px 0;'/>" + str + "</p></div>").dialog({
        modal: true,
        show: { effect: "clip" },
        buttons: {
            Ok: function () { $(this).dialog("close"); if (callback != undefined) callback(); }
        }
    });
}
function progressBox(str) {
    if (str == null) str = "Please wait while your action is being processed...";
    $("<div>&nbsp;<p>&nbsp;<img src='" + rootDir + "Content/images/loader.gif' /><span style='float: left; margin: 0 7px 20px 0;'/>&nbsp;" + str + "</p></div>").dialog({
        modal: true,
        height: 100, width: 350,
        closeOnEscape: false,
        open: function (event, ui) { $(".ui-dialog-titlebar").hide(); }
    });
}
function confirmBox(str, callback, srcElement, closeCallback) {
    return $("<div><p><span class='ui-icon ui-icon-help redIcon' style='float: left; margin: 0 7px 20px 0;'/>" + str + "</p></div>").dialog({
        modal: true,
        close: closeCallback,
        show: { effect: "clip" },
        buttons: {
            Yes: function () { $(this).dialog("close"); callback(true, srcElement) },
            No: function () { $(this).dialog("close"); callback(false) }
        }
    });
}
function alertBox(str, callback) {
    $("<div><p><span class='ui-icon ui-icon-alert' style='float: left; margin: 0 7px 20px 0;'/>" + str + "</p></div>").dialog({
        modal: true,
        show: { effect: "clip" },
        buttons: {
            Ok: function () { $(this).dialog("close"); }
        }
    });
}
function showLoadMessages() {
    var path = /Login\/Login$/
    if (!path.test(location.pathname) && $(".message").text().length > 0)
        messageBox($(".message").text());
}

function __addCustomValidators() {
    $.validator.addMethod('required', function (value, element, params) {
        if ($(element).attr("multi") != undefined) return true; // ignore multiselects
        if (value.length > 0) return true;
    });

    $.validator.addMethod('autocompletedropdown', function (value, element, params) {
        var source = $(element).autocomplete("option", "source");
        var hiddenID = $(element).prop("hiddenID");
        for (var i = 0, len = source.length; i < len; i++) {
            if (value == source[i].value) {
                if (hiddenID == null) return true;
                if ($(element).closest("tr").find(hiddenID).val() == source[i].id) return true;
            };
        }
        if ($(element).closest("td").find("#dropspan .multiselectspan").length > 0) return true;
        return false;
    }, 'Please select a proper value from the autocomplete list.');
    $.validator.unobtrusive.adapters.add("autocompletedropdown", [], function (options) {
        options.rules["autocompletedropdown"] = true;
        options.messages["autocompletedropdown"] = options.message;
    });

    $.validator.addMethod('fromdate', function (value, element, params) {
        var todate = $($(element).prop("todate")).datepicker("getDate");
        return (todate != null && todate.length > 0) ? $(element).datepicker("getDate") < todate : true;
    }, 'Please select a date less than To date.');
    $.validator.unobtrusive.adapters.add("fromdate", [], function (options) {
        options.rules["fromdate"] = true;
        options.messages["fromdate"] = options.message;
    });

    $.validator.addMethod('todate', function (value, element, params) {
        var fromdate = $($(element).prop("fromdate")).datepicker("getDate");
        return (fromdate != null && fromdate.length > 0) ? $(element).datepicker("getDate") > fromdate : true;
    }, 'Please select a date greater than From date.');
    $.validator.unobtrusive.adapters.add("todate", [], function (options) {
        options.rules["todate"] = true;
        options.messages["todate"] = options.message;
    });

    $.validator.addMethod('filesizetype', function (value, element, params) {
        var loLimit = $(element).attr('loLimit');
        var hiLimit = $(element).attr('hiLimit');
        var size = $(element)[0].files[0].size / 1024;
        return (size >= loLimit && size <= hiLimit) ? true : false;
    }, '');
    $.validator.unobtrusive.adapters.add("filesizetype", [], function (options) {
        options.rules["filesizetype"] = true;
        options.messages["filesizetype"] = options.message;
    });
}
function captureValidationMsgAsTooltip() {
    $('form').each(function () {
        var validator = $(this).data('validator');
        if (validator != null) validator.settings.showErrors = onValidated;
    });
    function onValidated(errorMap, errorList) {
        if (this.settings.success) {
            $.each(this.successList, function () {
                onSuccess($(this));
            });
        }
        if (errorList.length > 0 && resetPage != undefined) resetPage();
        $.each(errorList, function () {
            if (errorList.length == 1 && $(this.element).hasClass("ignore")) { errorList.splice(0); return; }
            onError($(this.element), this.message);
        });
        this.defaultShowErrors();
    }
    function onError(inputElement, message) { inputElement.prop("title", message); }
    function onSuccess(inputElement) { inputElement.prop("title", ""); }
}
function captureDateValidation() {
    $.validator.methods.date = function (value, element) {
        if (this.optional(element)) return true;
        var format = $(element).datepicker('option', 'dateFormat');
        var ok = true;
        try { $.datepicker.parseDate(format, value); } catch (err) { ok = false; }
        return ok;
    }
}
function captureFileUploads() {
}
function addDateRangeValidator(fromdate, todate) {
    $(fromdate).prop("todate", todate);
    $(fromdate).attr("data-val-fromdate", "Please select a date less than 'To' date.");
    $(fromdate).datepicker("option", "onSelect", function (selected) { setNextDate(this); });

    $(todate).prop("fromdate", fromdate);
    $(todate).attr("data-val-todate", "Please select a date greater than 'From' date.");
    $(todate).datepicker("option", "onSelect", function (selected) { setPrevDate(this) });

    setNextDate($(fromdate));
    setPrevDate($(todate))
    enableValidations();

    function setNextDate(date) {
        var dt = $(date).datepicker("getDate");
        if (dt == null) return;
        dt.setDate(dt.getDate() + 1);
        var todate = $($(date).prop("todate"));
        todate.datepicker("option", "minDate", dt);
    }
    function setPrevDate(date) {
        var dt = $(date).datepicker("getDate");
        if (dt == null) return;
        dt.setDate(dt.getDate() - 1);
        var fromdate = $($(date).prop("fromdate"));
        fromdate.datepicker("option", "maxDate", dt);
    }
}
function addFileUploadValidator() {
    $("input[type=file]")//.attr("data-val", "true").attr("data-val-required", "Please select a file to be uploaded.");
    $.each($("input[type=file]"), function (i, ele) {
        var loLimit = $(ele).attr('loLimit');
        var hiLimit = $(ele).attr('hiLimit');
        if (loLimit != undefined && hiLimit != undefined) $(ele).attr("data-val-filesizetype", "Please select a file with size " + loLimit + "KB <= filesize <= " + hiLimit + "KB.");
        else if (loLimit != undefined && hiLimit == undefined) $(ele).attr("data-val-filesizetype", "Please select a file with filesize >= " + loLimit + "KB.");
        else if (loLimit == undefined && hiLimit != undefined) $(ele).attr("data-val-filesizetype", "Please select a file with filesize <= " + hiLimit + "KB.");
    });
}
function enableValidations() {
    var validator = $('form').data('validator');
    if (validator == null) return;
    validator.settings.ignore = ".ignore,:hidden";
    parseDynamicContent('form');
    if (validator.settings.showErrors != null) validator.settings.showErrors = null;
    captureValidationMsgAsTooltip();

    function parseDynamicContent(selector) {
        $.validator.unobtrusive.parse(selector);//use the normal unobstrusive.parse method
        var form = $(selector).first().closest('form'); //get the relevant form
        var unobtrusiveValidation = form.data('unobtrusiveValidation');//get the collections of unobstrusive validators, and jquery validators and compare the two
        var validator = form.validate();
        $.each(unobtrusiveValidation.options.rules, function (elname, elrules) {
            if (validator.settings.rules[elname] == undefined) {
                var args = {};
                $.extend(args, elrules);
                args.messages = unobtrusiveValidation.options.messages[elname];
                //edit:use quoted strings for the name selector
                $("[name='" + elname + "']").rules("add", args);
            } else {
                $.each(elrules, function (rulename, data) {
                    if (validator.settings.rules[elname][rulename] == undefined) {
                        var args = {};
                        args[rulename] = data;
                        args.messages = unobtrusiveValidation.options.messages[elname][rulename];
                        //edit:use quoted strings for the name selector
                        $("[name='" + elname + "']").rules("add", args);
                    }
                });
            }
        });
    }
}
function removeValidation(element) {
    $(element).addClass("ignore").removeClass("valid input-validation-error").prop("title", "");
    enableValidations();
}
function addValidation(element) {
    $(element).removeClass("ignore").attr("data-val", "true").attr("data-val-required", "Please select a value.").prop("title", "");
    enableValidations();
}

function disableForms() {
    if (["view", "verify", "validate", "approve"].indexOf($('#action').val()) >= 0) {
        $("form .hasDatepicker").datepicker('disable');
        $("form :input:not(:hidden)").prop("disabled", "disabled");
        $('form .ui-autocomplete-input').autocomplete({ disabled: true });
        $(".fileimage").show();
        hideCaptcha();
    }
    if ($('#action').val() == "view") $("form .view input").prop("disabled", "");
    if ($('#action').val() == "review") $("form .review input").prop("disabled", "");
    if ($('#action').val() == "verify") $("form .verify input").prop("disabled", "");
    if ($('.ValidateClass').length > 0) $("form .validate input").prop("disabled", "");
    if ($('.ApproveClass').length > 0) $("form .approve input").prop("disabled", "");
}
$.fn.fillListvalues = function (url, value, hiddenID, hiddenValue, retArray, callback) {
    if (Array.isArray(url)) return fillListvaluesArray(url, this, value, hiddenID, hiddenValue, callback);
    var targetID = this;
    $.ajax({
        url: url, async: true, type: "GET", dataType: "json",
        success: function (data) {
            if (retArray != undefined) { retArray.length = 0; $(data).each(function (i, e) { retArray.push(e) }); };
            fillListvaluesArray(data, targetID, value, hiddenID, hiddenValue, callback);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $("#error_canvas").html(XMLHttpRequest.responseText.split("<title>")[1].split("</title>")[0])
        }
    })

    function fillListvaluesArray(arr, targetID, value, hiddenID, hiddenValue, callback) {
        $.each($(targetID), function (i, element) {
            element.hiddenID = hiddenID;
            element.callback = callback;
        });

        //console.log(callback)
        //$(targetID).prop("hiddenID", hiddenID);
        //console.log($(targetID).prop("hiddenID"))
        //$(targetID).prop("callback", callback);
        //console.log($(targetID).prop("callback"))
        $(targetID).autocomplete({
            minLength: 0,
            source: $.map(arr, function (item) { return { value: (value == null ? item : item[value]), id: (hiddenValue == null ? item : item[hiddenValue]) }; }), //TODO
            select: function (event, ui) {
                if (hiddenID != null) {
                    $("#" + event.target.id).closest("tr").find(hiddenID).val(ui.item.id);
                }
                if ($(this).attr("multi") != undefined) {
                    $(this).closest('td').find("#dropspan").append("<span class='dragspan '><span class='multiselectspan' id='" + ui.item.id + "'>" + ui.item.value + "</span><img id='multidel' src=" + rootDir + "content/images/icon-delete.png /></span>");
                    this.value = '';
                    var data = $(this).autocomplete('option', 'source')
                    var foundIndex = -1;
                    $.each(data, function (i, val) {
                        if (ui.item.id == val.id) {
                            foundIndex = i;
                        }
                    });
                    if (foundIndex > -1) data.splice(foundIndex, 1);
                    this.blur(); // remove focus so that another item could be picked up
                    if (callback != null) callback(ui.item, $(targetID));
                    return false; // do not propogate! otherwise it would set the text again
                }
                //if (callback != null) callback(ui.item, $(targetID));
                if (callback != null) callback(ui.item, $(event.target));
            }
        }).focus(function () { $(this).autocomplete('search', "") });
        $(targetID).attr("data-val-autocompletedropdown", "The field value must match one in the autocomplete list.");
        enableValidations();
    }
}
function captureBackSpace() { // Prevent 'Backspace' on non-input and readonly fields
    $(document).keydown(function (e) {
        var elid = $(document.activeElement).is("input:focus:not([readonly]),textArea:focus:not([readonly])");
        if (e.keyCode == 8 && !elid) {
            return false;
        };
    });
}
function captureEnter() {
    $(document).on("keypress", "input,select", function (e) {
        if (e.keyCode == 13) {
            if (e.target.type == "submit") { return; }
            window._activeElement = this;
            this.blur();
            setTimeout(setFocus, 1);
            return false;
        }
    });
    function setFocus() {
        var element = window._activeElement;
        var all = $(":input:enabled:visible:not([readonly]):not(.ignore)");
        var index = all.index(element);
        var nextItem = all.eq(index + 1);
        if (nextItem[0] != undefined) nextItem.focus();
    }
}
function captureResetPage() {
    $("input[type='reset']").on("click", function (e) {
        e.preventDefault();
        confirmBox("Do you want to reset the page to original condition?", _reset, this);
    });
    function _reset(choice, element) {
        if (choice) {
            $(element).closest('form').get(0).reset();
            $(".fileimage").prop("src", function () { return $(this).prop("oldsrc"); });
            if (reset) reset();
        }
    }
}
function captureRowActions() {
    $(document).on('click', "img#addrow", function (e) {
        var tr = $(this).closest("tr");
        var clone = tr.clone();
        tr.after(clone);
        var originalInputs = tr.find('input');
        clone.find('input').each(function (i, element) {
            $(element).val("").removeClass("valid input-validation-error");
            cloneAutocomplete(element);
            adjustIdName(element);
        });
        hideshowIcons($(this));
        adjustSerialNum();
        adjustDatePicker(tr, clone);
        enableValidations();

        function cloneAutocomplete(e) {
            if ($(e).hasClass('ui-autocomplete-input')) {
                var previousElement = tr.find("#" + e.id);
                var hiddenID = previousElement.prop("hiddenID");
                var callback = previousElement.prop("callback");

                var source = previousElement.autocomplete("option", "source");
                e.hiddenID = hiddenID;
                e.callback = callback;

                var select = function (event, ui) {
                    if (hiddenID != null) {
                        //console.log($(e).closest("td")[0].outerHTML)
                        //console.log($(e).closest("tr").find(hiddenID))
                        $(e).closest("tr").find(hiddenID).val(ui.item.id);
                        if (callback != null) callback(ui.item, $(e));
                    }
                }
                //console.log($(previousElement).prop("hiddenID", hiddenID));
                //console.log($(previousElement).prop("callback", callback));
                $(e).autocomplete({ minLength: 0, source: source, select: select }).focus(function () { $(this).autocomplete('search', "") });
            }
        }
        function hideshowIcons($this) {
            $this.hide();
            tr.find("#removerow").hide();
            clone.find("#removerow").show();
        }
        function adjustSerialNum() {
            var rowNum = tr.find("td:first label").text();
            if (isFinite(rowNum)) clone.find("td:first label").text(parseInt(rowNum) + 1)
        }
        function adjustIdName(element) {
            var regex = /_([0-9]+)_/;
            var id = element.id;
            console.log(id.length)
            if (id.length == 0) return;     // checkboxes load with a hidden input where the id is not present
            var indexOld = parseInt(id.match(regex)[1]);
            element.id = id.replace("_" + indexOld + "_", "_" + (indexOld + 1) + "_")

            regex = /\[([0-9]+)\]/;
            var name = element.name;
            indexOld = parseInt(name.match(regex)[1]);
            element.name = name.replace("[" + indexOld + "]", "[" + (indexOld + 1) + "]")
        }
        function adjustDatePicker(parent0, parent1) {
            parent1.find('.ui-datepicker-trigger').remove();
            var from, to;
            parent0.find('.hasDatepicker').each(function (i, e) {
                var datepicker_options = $(e).datepicker('option', 'all');
                parent1.find('.hasDatepicker').eq(i).removeClass('hasDatepicker').datepicker(datepicker_options);
                if ($(e).prop("fromdate")) to = parent1.find('.hasDatepicker').eq(i);
                if ($(e).prop("todate")) from = parent1.find('.hasDatepicker').eq(i);
            });
            if (from != null) addDateRangeValidator(from, to);
        }
    });
    $(document).on('click', "img#removerow", function (e) {
        var rows = $(this).closest("table tbody").find("tr");
        if (rows.length == 1) return;
        hideshowIcons($(this));

        function hideshowIcons($this) {
            var previousRow = $(rows[rows.length - 2]);
            previousRow.find("#addrow").show();
            if (rows.length > 2) previousRow.find("#removerow").show();
            $this.closest("tr").remove();
        }
    });
}
function captureFormSubmissions() {
    $('form').bind('invalid-form.validate', function (form, validator) {
        if (window.location.href.indexOf("Login/Login") < 0) messageBox("There are validation issues on this page. Please correct them.")
    });
    var validator = $('form').data('validator');
    if (validator == null) return;
    validator.settings.submitHandler = function (form, event) {
        if (capchaVerified != undefined && !capchaVerified) { messageBox("Please verify the captcha presented!"); return; }
        //        if (document.title == "Login" || $(this.submitButton).hasClass('search')) { form.submit(); return; }
        //if (window.location.href.indexOf("Login/Login") > 0) { Mirajul on comment 02102018
        //    //grecaptcha.execute();
        //    form.submit();
        //    return;
        //}
        if ($(this.submitButton).hasClass('search')) { form.submit(); return; }
        confirmBox("Are you sure you wish to submit the page?", __callback, this.submitButton);
        function __callback(state, submitButton) {
            if (state) {
                var formaction = submitButton.getAttribute("formaction");
                if (formaction != null) form.action = formaction;
                $.each($("#dropspan"), function (i, ele) {
                    var val = "";
                    var text = "";
                    var spans = $(ele).closest('td').find('.multiselectspan');
                    $.each(spans, function (j, e) {
                        val += $(e).prop("id") + ",";
                        //text += $(e).text() + ", ";
                    })
                    $(ele).closest('td').find('.ui-autocomplete-input').val(val.replace(/,\s*$/, ""));
                })
                progressBox();
                if (submitPage) submitPage();
                form.submit();
            }
            if (resetPage) resetPage();

        }
    };
}
function hideshowRowIcons() {
    $("img#addrow, img#removerow").hide();
    $("img#addrow").closest("table").find("tbody tr:last").find("img#addrow, img#removerow").show();
    $("img#addrow").closest("table").find("tbody tr:first").find("img#removerow").hide();
}
function captureLogOff() {
    var url;
    $(".logout").on('click', function (e) {
        url = this.href;
        e.preventDefault();
        confirmBox("Do you want to Logout from the application?", logoff);
    });
    function logoff(status) {
        if (status) window.location = url;
    }
}
function enableMultiSelect() {
    $(document).on('click', '#multidel', function (e) {
        var span = $(this).closest('span').find('span');
        var auto = $(this).closest('#dropspan').next();
        $(auto).autocomplete('option', 'source').splice(0, 0, { value: $(span).text(), id: $(span).prop('id') });
        $(this).closest('span').remove();
        if ($("#dropspan .dragspan").length == 0)
            $.each(removedPosts, function (i, e) {
                $(auto).autocomplete('option', 'source').splice(0, 0, e);
            });
    });

    $("#dropspan").sortable();
}
function bindMultiSelects() {
    $.each($("#dropspan"), function (i, ele) {
        var input = $(ele).closest('td').find('input[multi]')
        var val = input.val();
        input.val("");
        var source = $(input).autocomplete("option", "source");
        if (val.length == 0) return;
        val = val.split(",");
        console.log(val)
        $.each(val, function (i, e) {
            var text = "";
            var foundIndex = -1;
            for (var i = 0, len = source.length; i < len; i++) {
                if (e == source[i].id) {
                    text = source[i].value;
                    foundIndex = i;
                }
            }
            if (foundIndex > -1) source.splice(foundIndex, 1);
            console.log(foundIndex)
            console.log(source)
            $(ele).append("<span class='dragspan '><span class='multiselectspan' id='" + e + "'>" + text + "</span><img id='multidel' src='/content/images/icon-delete.png'/></span>");
        });
    })
}

function getCaptcha() {
    capchaVerified = false;

    $.ajax({
        url: rootDir + 'capcha/get',
        cache: false,
        success: function (data) {
            $('#captcha').html(data).s3Capcha();
        },
    });
};
function verifyCaptcha(code) {
    $.ajax({
        url: rootDir + 'capcha/verify?code=' + code,
        cache: false,
        success: function (data) {
            console.log(data == 'True');
            capchaVerified = (data == 'True');
            if (!capchaVerified) getCaptcha();
        },
        error: function () { capchaVerified = false; console.log(data); getCaptcha(); }
    });
}
function hideCaptcha() { $('#captcha').hide(); }

function unique(array) {
    return array.filter(function (el, index, arr) {
        return index == arr.indexOf(el);
    });
}
function resetPage() { }
function submitPage() { }
function resetFormElement(e) {
    e.wrap('<form>').closest('form').get(0).reset();
    e.unwrap();
}