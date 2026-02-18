var ctrlURL = rootDir + "StudentManagement";

$(document).ready(function () {
    validateNumeric();
    $('#div_AlertSuccess').hide();
    $('#div_danger').hide();
    loadClasses();
    loadFromSession();
    loadToSession();
    $(document).on("change", '.GridddlClass', function (e) {
        var Id = $(this).val();
        var ddl = $(this).closest('tr').find('.GridddlSection').attr('id');
        LoadGridDropDownSection(Id, '#'+ ddl);
    });
});
function loadClasses() {
    $.get({
        type: "GET",
        url: rootDir + "JQuery/getClasses",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data, status) {
            if (status == "success") {
                BindClassDropDownList($('#ddlClass'), data);
                $("#ddlClass").selectpicker('refresh');
            } else {
                showMessage("Unable to process the request...", 0);
            }
        }
    });
}

function loadFromSession() {
    $.get({
        url: rootDir + "JQuery/GetSchoolWiseSessions",
        type: "GET",
        success: function (res) {

            var ddl = $('#ddlFromSession');
            ddl.empty();
            ddl.append(new Option("Select", "0"));

            $.each(res.SessionList, function (i, item) {
                var opt = new Option(item.SM_SESSIONCODE, item.SM_SESSIONID);
                if (item.SM_SESSIONID === res.CurrentSessionId) {
                    opt.selected = true;
                }
                ddl.append(opt);
            });

            ddl.prop('disabled', true).selectpicker('refresh');

            $('#hdnFromSession').val(res.CurrentSessionId);
        }
    });
}

$(document).ready(function () {
    loadFromSession();   // sets hidden field
    loadToSession();     // depends on from session
});
function loadToSession() {
    $.get({
        type: "GET",
        url: rootDir + "JQuery/GetSchoolWiseSessions",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (res, status) {

            if (status === "success") {

                var fromSessionId = $('#hdnFromSession').val();

                var futureSessions = res.SessionList.filter(function (s) {
                    return s.SM_SESSIONID > fromSessionId;
                });

                BindSessionDropDownList($('#ddlToSession'), futureSessions);
                $('#ddlToSession').selectpicker('refresh');
            }
            else {
                showMessage("Unable to process the request...", 0);
            }
        }
    });
}

function BindSessionDropDownList(ddl, dataCollection) {
    $(ddl).get(0).length = 0;
    $(ddl).get(0).options[0] = new Option("Select", "0", "Selected");
    if (dataCollection.length != 0) {
        for (var i = 0; i < dataCollection.length; i++) {
            $(ddl).get(0).options[i + 1] = new Option(dataCollection[i].SM_SESSIONCODE, dataCollection[i].SM_SESSIONID);
        }
    }
}
function BindClassDropDownList(ddl, dataCollection) {
    $(ddl).get(0).length = 0;
    $(ddl).get(0).options[0] = new Option("Select", "0", "Selected");
    if (dataCollection.length != 0) {
        for (var i = 0; i < dataCollection.length; i++) {
            $(ddl).get(0).options[i + 1] = new Option(dataCollection[i].CM_CLASSNAME, dataCollection[i].CM_CLASSID);
        }
    }
}
$(document).on("change", '#ddlClass', function (e) {
    var CWF_ClassId = $('#ddlClass option:selected').val();
    AjaxPostForDropDownSection(CWF_ClassId);
    $("#ddlSection").selectpicker('refresh');
});
function AjaxPostForDropDownSection(Id) {
    var _data = JSON.stringify({
        classId: parseInt(Id),
    });
    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/ClassWiseSection",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data, status) {
            if (status == "success") {
                BindSectionDropDownList($('#ddlSection'), data);

            } else {
                showMessage("Unable to process the request...", 0);
            }
        }
    });
}
function BindSectionDropDownList(ddl, dataCollection) {
    $(ddl).get(0).length = 0;
    $(ddl).get(0).options[0] = new Option("Select", "0", "Selected");
    if (dataCollection.length != 0) {
        for (var i = 0; i < dataCollection.length; i++) {
            $(ddl).get(0).options[i + 1] = new Option(dataCollection[i].SECM_SECTIONNAME, dataCollection[i].SECM_SECTIONID);
        }
    }
}
function Reset() {
    var url = window.location.href;
    window.location.href = url;
    return false;
}
function ValidateForm() {
    var ddlClass = $('#ddlClass').val();
    if (ddlClass == "0") {
        alert('Please Select Class....');
        return false;
    }

    return true;
}



//////////////Added By Uttaran

$(document).on("click", "#btnSearch", function (e) {
    if (ValidateForm() === true) {
        var _data = JSON.stringify({
            query: {
                ClassId: parseInt($('#ddlClass').val()),
                SectionId: parseInt($('#ddlSection').val())
            }
        });

        $.ajax({
            type: "POST",
            url: rootDir + "JQuery/studentsForPromotion",
            data: _data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data) {
                $("#example > tbody").empty();

                for (var i = 0; i < data.length; i++) {
                    var slNo = i + 1;
                    var tr = "<tr>";
                    tr += "<td style='text-align:center;'>" + slNo + "</td>";
                    // Checkbox
                    tr += "<td style='text-align:center;'>" +
                          "<input type='checkbox' class='rowCheckbox' value='" + data[i].SD_RegistrationNo + "' />" +
                          "</td>";

                    tr += "<td>" + data[i].StudentId + "</td>";
                    tr += "<td>" + data[i].StudentName + "</td>";
                    tr += "<td>" + data[i].Roll + "</td>";
                    tr += "<td>" + data[i].SectionName + "</td>";

                    // CLASS DROPDOWN (store old class here)
                    tr += "<td>" +
                          "<select id='ddlClass" + i + "' class='form-control GridddlClass' " +
                          "data-oldclass='" + data[i].ClassId + "'></select>" +
                          "</td>";

                    // SECTION DROPDOWN
                    tr += "<td><select id='ddlSection" + i + "' class='form-control GridddlSection'></select></td>";

                    // ROLL
                    tr += "<td><input type='text' id='txtRoll" + i + "' class='form-control txtRoll' " +
                          "value='" + data[i].Roll + "' onkeypress='return isNumberKey(event);' /></td>";

                    //  HIDDEN PASS/FAIL FLAG (DEFAULT = FAIL)
                    tr += "<td style='display:none;'>" +
                          "<input type='hidden' class='hdnPromoteStatus' value='F' />" +
                          "</td>";

                    tr += "</tr>";


                    $("#example > tbody").append(tr);

                    // Load dropdowns
                    LoadGridDropdownClass("#ddlClass" + i);
                    $("#ddlClass" + i).val(data[i].ClassId);

                    LoadGridDropDownSection(data[i].ClassId, "#ddlSection" + i);
                    $("#ddlSection" + i).val(data[i].SectionId);
                }
            }
        });
    }
});

// Select/Deselect all
$(document).on("change", "#selectAllCheckbox", function () {
    $(".rowCheckbox").prop("checked", $(this).prop("checked"));
});

// Keep header checkbox in sync
$(document).on("change", ".rowCheckbox", function () {
    $("#selectAllCheckbox").prop(
        "checked",
        $(".rowCheckbox:checked").length === $(".rowCheckbox").length
    );
});




function SaveRecord() {
   
    if (ValidatePromotion() === true) {
        var tableStudentsArr = [];

        $("#example tbody tr").each(function () {
            var $row = $(this);
            var $chk = $row.find(".rowCheckbox");

            // Only push if row checkbox is checked
            if ($chk.length > 0 && $chk.is(":checked")) {
                tableStudentsArr.push({
                    SD_RegistrationNo: $chk.val(),
                    Roll: $row.find(".txtRoll").val(),
                    ClassId: parseInt($row.find(".GridddlClass").val()),
                    SectionId: parseInt($row.find(".GridddlSection").val()),
                    StudentPromoteStatus: $row.find(".hdnPromoteStatus").val()
                });

            }
        });

        if (tableStudentsArr.length === 0) {
            alert("⚠️ Please select at least one student to promote.");
            return false;
        }

        var _data = JSON.stringify({
            obj: {
                PromotedStudentList: tableStudentsArr,
                SessionId: parseInt($('#ddlToSession :selected').val())
            }
        });

        $.ajax({
            type: "POST",
            url: rootDir + "JQuery/UpdateStudentPromotion",
            data: _data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data) {
                if (data && data.IsSuccess) {
                    clearFormElements();
                    $('#div_AlertSuccess').show().text(data.Message);
                } else {
                    $('#div_danger').show().text(data.Message);
                }
            },
            error: function () {
                alert("Process Fail...");
            }
        });
    }
}


let pendingChange = { type: "", value: "", target: null };

// Class change
$(document).on("change", ".GridddlClass", function () {

    var oldClass = $(this).data("oldclass"); // original class
    var newClass = $(this).val();            // selected class

    var $row = $(this).closest("tr");
    var $status = $row.find(".hdnPromoteStatus");

    if (parseInt(oldClass) !== parseInt(newClass)) {
        $status.val("P"); // PASS
    } else {
        $status.val("F"); // FAIL
    }

    // keep your modal logic
    pendingChange = {
        type: "class",
        value: newClass,
        target: $(this)
    };

    $("#applyAllModal").modal("show");
});


// Section change
$(document).on("change", ".GridddlSection", function () {
    pendingChange = {
        type: "section",
        value: $(this).val(),
        target: $(this)
    };
    $("#applyAllModal").modal("show");
});

// Yes → apply to all
$(document).on("click", "#btnYes", function () {

    if (pendingChange.type === "class") {

        $(".GridddlClass").each(function () {

            var oldClass = $(this).data("oldclass");
            $(this).val(pendingChange.value);

            var $row = $(this).closest("tr");
            var $status = $row.find(".hdnPromoteStatus");

            if (parseInt(oldClass) !== parseInt(pendingChange.value)) {
                $status.val("P");
            } else {
                $status.val("F");
            }
        });
    }

    $("#applyAllModal").modal("hide");
});

// No → apply only to that row
$(document).on("click", "#btnNo", function () {
    $("#applyAllModal").modal("hide");
});

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
function isNumberKey(event) {
    var key = window.event ? event.keyCode : event.which;
    if (event.keyCode === 8 || event.keyCode === 46) {
        return true;
    } else if (key < 48 || key > 57) {
        return false;
    } else {
        return true;
    }
};
function LoadGridDropDownSection(ClassId, ddlSection) {
    var _data = JSON.stringify({
        classId: parseInt(ClassId),
    });
    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/ClassWiseSection",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data, status) {
            if (status == "success") {
                BindSectionDropDownList($(ddlSection), data);
                //$(ddlSection).selectpicker('refresh');
            } else {
                showMessage("Unable to process the request...", 0);
            }
        }
    });
}
function LoadGridDropdownClass(ddlClass) {
    $.get({
        type: "GET",
        url: rootDir + "JQuery/getClasses",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data, status) {
            if (status == "success") {
                BindClassDropDownList($(ddlClass), data);
                //$(ddlClass).selectpicker('refresh');
            } else {
                showMessage("Unable to process the request...", 0);
            }
        }
    });
}
function ValidatePromotion() {
    if ($('#ddlFromSession ').val() == 0) {
        alert('Please Select From Session....')
        return false;
    }
    if ($('#ddlToSession ').val() == 0) {
        alert('Please Select To Session....')
        return false;
    }
    if (CheckingValidSession() == true) return true;

    var tbl = document.getElementById('example');
    if (tbl.rows.length == 0) {
        alert('There is no Students to promote!!....');
        return false;
    }
}
function CheckingValidSession() {
    var fromSession = $('#ddlFromSession option:selected').text();
    var toSession = $('#ddlToSession option:selected').text();
    var fromParts = parseInt(fromSession.split('-')[1]);
    var toParts = parseInt(toSession.split('-')[1]);
    if (toParts <= fromParts) {
        alert('Please Select To Proper Session....')
        return false;
    }
    return true;
}
function clearFormElements() {
    DeleteRows();
    $(':input', '#Promotion_form').not(':button, :submit, :reset,#div_AlertSuccess,#div_danger').val('').prop('checked', false).prop('selected', false);
    $('#ddlClass').val("0");
    $('#ddlClass').selectpicker('refresh');

    $('#ddlSection').find('option').remove().end().append('<option value="0">Select</option>');
    $('#ddlSection').selectpicker('refresh');

    $('#ddlFromSession').val("0");
    $('#ddlFromSession').selectpicker('refresh');

    $('#ddlToSession').val("0");
    $('#ddlToSession').selectpicker('refresh');

}
function DeleteRows() {
    var tblStudent = document.getElementById('example')
    var rowCount = tblStudent.rows.length;
    for (var i = rowCount - 1; i > 0; i--) {
        tblStudent.deleteRow(i);
    }
}
function SelectedClassChanged()
{
    var rowCount = $('#example >tbody >tr').length;
    for (var i = 0; i < rowCount; i++) {
        $('#ddlClass' + i).val($('#Pro_ClassId').val());
    }
}
