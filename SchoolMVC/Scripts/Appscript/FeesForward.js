//var ctrlURL = rootDir + "Master";

$(document).ready(function () {
    validateNumeric();
    $('#div_AlertSuccess').hide();
    $('#div_danger').hide();
    loadClasses();
    loadFromSession();
    loadToSession();
    //$(document).on("change", '.GridddlClass', function (e) {
    //    var Id = $(this).val();
    //    var ddl = $(this).closest('tr').find('.GridddlSection').attr('id');
    //    LoadGridDropDownSection(Id, '#'+ ddl);
    //});
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
//function loadFromSession() {
//    $.get({
//        type: "GET",
//        url: rootDir + "JQuery/GetSchoolWiseSessions",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        async: false,
//        success: function (data, status) {
//            if (status == "success") {
//                BindSessionDropDownList($('#ddlFromSession'), data, $('#hiddenSessionId').val());
//                $("#ddlFromSession").selectpicker('refresh');
//            } else {
//                showMessage("Unable to process the request...", 0);
//            }
//        }
//    });
//}
//function loadToSession() {
//    $.get({
//        type: "GET",
//        url: rootDir + "JQuery/GetSchoolWiseSessions",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        async: false,
//        success: function (data, status) {
//            if (status == "success") {
//                BindSessionDropDownList($('#ddlToSession'), data);
//                $("#ddlToSession").selectpicker('refresh');
//            } else {
//                showMessage("Unable to process the request...", 0);
//            }
//        }
//    });
//}






//function BindSessionDropDownList(ddl, dataCollection) {
//    $(ddl).get(0).length = 0;
//    $(ddl).get(0).options[0] = new Option("Select", "0", "Selected");
//    if (dataCollection.length != 0) {
//        for (var i = 0; i < dataCollection.length; i++) {
//            $(ddl).get(0).options[i + 1] = new Option(dataCollection[i].SM_SESSIONCODE, dataCollection[i].SM_SESSIONID);
//        }
//    }
//}

//function BindSessionDropDownList(ddl, dataCollection, selectedSessionId) {
//    // Clear existing options
//    $(ddl).get(0).length = 0;
//    alert(selectedSessionId);
//    // Add the default "Select" option
//    $(ddl).get(0).options[0] = new Option("Select", "0", "Selected");

//    // Check if dataCollection has any data
//    if (dataCollection.length != 0) {
//        // Loop through dataCollection to add options to the dropdown
//        for (var i = 0; i < dataCollection.length; i++) {
//            var sessionOption = new Option(dataCollection[i].SM_SESSIONCODE, dataCollection[i].SM_SESSIONID);
//            $(ddl).get(0).options[i + 1] = sessionOption;

//            // Pre-select the option if the sessionId matches
//            if (dataCollection[i].SM_SESSIONID == selectedSessionId) {
//                $(ddl).val(selectedSessionId); // Set the dropdown to selected sessionId

//                // Set the selected sessionId to the hidden field
//                $('#hiddenSessionId').val(selectedSessionId); // Set hidden field value
//            }
//        }
//    }
//}
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

            $('#hiddenSessionId').val(res.CurrentSessionId);
        }
    });
}

function loadToSession() {
    $.get({
        type: "GET",
        url: rootDir + "JQuery/GetSchoolWiseSessions",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (res, status) {

            if (status === "success") {
                debugger;
                var fromSessionId = $('#hiddenSessionId').val();

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
//$(document).on("change", '#ddlClass', function (e) {
//    var CWF_ClassId = $('#ddlClass option:selected').val();
//    AjaxPostForDropDownSection(CWF_ClassId);
//    $("#ddlSection").selectpicker('refresh');
//});
//function AjaxPostForDropDownSection(Id) {
//    var _data = JSON.stringify({
//        classId: parseInt(Id),
//    });
//    $.ajax({
//        type: "POST",
//        url: rootDir + "JQuery/ClassWiseSection",
//        data: _data,
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        async: false,
//        success: function (data, status) {
//            if (status == "success") {
//                BindSectionDropDownList($('#ddlSection'), data);

//            } else {
//                showMessage("Unable to process the request...", 0);
//            }
//        }
//    });
//}
//function BindSectionDropDownList(ddl, dataCollection) {
//    $(ddl).get(0).length = 0;
//    $(ddl).get(0).options[0] = new Option("Select", "0", "Selected");
//    if (dataCollection.length != 0) {
//        for (var i = 0; i < dataCollection.length; i++) {
//            $(ddl).get(0).options[i + 1] = new Option(dataCollection[i].SECM_SECTIONNAME, dataCollection[i].SECM_SECTIONID);
//        }
//    }
//}
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
    //if ($('#ddlSection').val() == "0") {
    //    $('#ddlSection').val('');
       
    //}
    return true;
}
//function InsertUpdateClassFees() {
//    if (FinalValidation() == true) {

//        var ArrayItem = [];
//        var tblItem = document.getElementById('tblList');

//        // rows: header row exists -> start from 1
//        for (var i = 1; i < tblItem.rows.length; i++) {

//            // make sure row has expected number of cells
//            var cells = tblItem.rows[i].cells;
//            if (!cells || cells.length < 13) continue; // safety

//            ArrayItem.push({
//                CF_CLASSFEESID: parseInt(cells[0].innerHTML) || 0,        // hidden ID
//                CF_CLASSID: parseInt(cells[1].innerHTML),
//                ClassName: cells[2].innerHTML,
//                CF_CATEGORYID: parseInt(cells[3].innerHTML),
//                CategoryName: cells[4].innerHTML,
//                CF_FEESID: parseInt(cells[5].innerHTML),
//                FeesHeadName: cells[6].innerHTML,
//                CF_FEESAMOUNT: parseFloat(cells[7].innerHTML) || 0,
//                CF_INSAMOUNT: parseFloat(cells[8].innerHTML) || 0,
//                CF_NOOFINS: parseInt(cells[9].innerHTML) || 0,
//                CF_INSTALLMENTNO: parseInt(cells[10].innerHTML) || 0,
//                CF_DUEDATE: cells[11].innerHTML,
//                IsAdmissionTime: (cells[12].innerHTML === 'true' || cells[12].innerHTML === '1') ? true : false
//            });

//        }

//        var _data = JSON.stringify({
//            CF_CLASSID: parseInt($('#CF_CLASSID').val()),
//            CF_CATEGORYID: parseInt($('#CF_CATEGORYID').val()),
//            CF_FEESID: parseInt($('#CF_FEESID').val()),
//            obj: {
//                ClassWiseFeesList: ArrayItem,
//            }
//        });

//        $.ajax({
//            url: '/JQuery/InsertUpdateClasswiseFees',
//            data: _data,
//            type: 'POST',
//            dataType: 'json',
//            contentType: 'application/json ; utf-8',
//            success: function (data) {
//                if (data != null && data.IsSuccess == true) {
//                    SuccessToast(data.Message);
//                    window.location.href = '/Masters/ClasswiseFeesList';
//                }
//                else {
//                    ErrorToast(data.Message);
//                }
//            },
//            error: function () {
//                ErrorToast('Something wrong happened.');
//            }
//        });
//    }
//}



$(document).on("click", "#btnSearch", function (e) {

    if (ValidateForm() === true) {

        var _data = JSON.stringify({
            model: {
                ClassId: parseInt($('#ddlClass option:selected').val())
            }
        });

        $.ajax({
            type: "POST",
            url: "/JQuery/ClasswiseFeesForwardList",
            data: _data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {

                // response = { Data, CanEdit, CanDelete }
                var tableArr = response.Data;
                var canEdit = response.CanEdit;
                var canDelete = response.CanDelete;

                $("#example > tbody").empty();

                if (!tableArr || tableArr.length === 0) {
                    $("#example > tbody").append(
                        "<tr><td colspan='8' class='text-center'>No records found</td></tr>"
                    );
                    return;
                }

                var tr;

                for (var i = 0; i < tableArr.length; i++) {

                    tr = $('<tr/>');

                    // Added checkbox column at the start
                    tr.append("<td><input type='checkbox' class='class-fee-checkbox' data-id='" + tableArr[i].CF_CLASSFEESID + "' /></td>");

                    tr.append("<td hidden='hidden'>" + tableArr[i].CF_CLASSFEESID + "</td>");
                    tr.append("<td>" + tableArr[i].ClassName + "</td>");
                    tr.append("<td>" + tableArr[i].FeesHeadName + "</td>");
                    tr.append("<td>" + tableArr[i].CF_FEESAMOUNT + "</td>");
                    tr.append("<td>" + tableArr[i].CF_NOOFINS + "</td>");
                    //tr.append("<td>" + tableArr[i].CF_INSTALLMENTNO + "</td>");
                    tr.append("<td>" + tableArr[i].CF_INSAMOUNT + "</td>");
                    tr.append("<td hidden='hidden'>" + tableArr[i].CF_CATEGORYID + "</td>");
                    tr.append("<td>" + tableArr[i].CAT_CATEGORYNAME + "</td>");
                    tr.append("<td>" + tableArr[i].DUEDATES + "</td>");
                    tr.append("<td hidden='hidden'>" + tableArr[i].CF_FEESID + "</td>");

                    // Action column with buttons
                    //var actionHtml = "";

                    //if (canEdit) {
                    //    actionHtml += "<button class='btn btn-sm btn-primary' onclick='editFees(" +
                    //        tableArr[i].CF_CLASSFEESID + ")'>Edit</button> ";
                    //}

                    //if (canDelete) {
                    //    actionHtml += "<button class='btn btn-sm btn-danger' onclick='deleteFees(" +
                    //        tableArr[i].CF_CLASSFEESID + ")'>Delete</button>";
                    //}

                    //tr.append("<td>" + actionHtml + "</td>");

                    $('#example').append(tr);

                }
            },
            error: function (xhr, status, error) {
                console.error("Error:", error);
            }
        });
    }
});

function InsertUpdateClassFees() {

    if (ValidateForm() !== true) {
        return;
    }

    var ClassWiseFeesList = [];
    var tbl = document.getElementById('example');

    // start from 1 -> skip header row
    for (var i = 1; i < tbl.rows.length; i++) {

        var row = tbl.rows[i];
        var chk = row.cells[0].querySelector('input[type="checkbox"]');

        // ONLY checked rows
        if (chk && chk.checked) {

            ClassWiseFeesList.push({
                CF_CLASSID: $('#ddlClass option:selected').val(),
                CF_SESSIONID: $('#ddlToSession option:selected').val(),
                CF_CLASSFEESID: parseInt(row.cells[1].innerText) || 0,
                CF_FEESID: parseInt(row.cells[10].innerText) || 0,
                ClassName: row.cells[2].innerText,
                FeesHeadName: row.cells[3].innerText,
                CF_FEESAMOUNT: parseFloat(row.cells[4].innerText) || 0,
                CF_NOOFINS: parseInt(row.cells[5].innerText) || 0,
                CF_INSAMOUNT: parseFloat(row.cells[6].innerText) || 0,
                CF_CATEGORYID: row.cells[7].innerText,
                CF_DUEDATE: row.cells[9].innerText,
                IsAdmissionTime: false   // set as per UI logic if needed
            });
        }
    }

    if (ClassWiseFeesList.length === 0) {
        ErrorToast("Please select at least one record to save.");
        return;
    }

    var _data = JSON.stringify({
        CF_CLASSID:$('#ddlClass option:selected').val(),
        CF_CATEGORYID: 0,   // set if you have dropdown / logic
        CF_FEESID: 0,      // set if required
        obj: {
            ClassWiseFeesList: ClassWiseFeesList
        }
    });

    $.ajax({
        type: "POST",
        url: "/JQuery/InsertUpdateClasswiseFeesForward",
        data: _data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data && data.IsSuccess === true) {
                SuccessToast(data.Message);
                // optional reload
                // $("#btnSearch").click();
            } else {
                ErrorToast(data.Message || "Save failed.");
            }
        },
        error: function () {
            ErrorToast("Something wrong happened while saving.");
        }
    });
}


$(document).on('change', '#selectAllCheckbox', function () {

    var isChecked = $(this).is(':checked');

    $('#example tbody .class-fee-checkbox').prop('checked', isChecked);
});
















// Select/Deselect all
//$(document).on("change", "#selectAllCheckbox", function () {
//    $(".rowCheckbox").prop("checked", $(this).prop("checked"));
//});

//// Keep header checkbox in sync
//$(document).on("change", ".rowCheckbox", function () {
//    $("#selectAllCheckbox").prop(
//        "checked",
//        $(".rowCheckbox:checked").length === $(".rowCheckbox").length
//    );
//});




//function SaveRecord() {
//    if (ValidatePromotion() === true) {
//        var tableStudentsArr = [];

//        $("#example tbody tr").each(function () {
//            var $row = $(this);
//            var $chk = $row.find(".rowCheckbox");

//            // Only push if row checkbox is checked
//            if ($chk.length > 0 && $chk.is(":checked")) {
//                tableStudentsArr.push({
//                    SD_RegistrationNo: $chk.val(), // checkbox value = StudentId
//                    Roll: $row.find(".txtRoll").val(),
//                    ClassId: parseInt($row.find(".GridddlClass").val()),
//                    SectionId: parseInt($row.find(".GridddlSection").val())
//                });
//            }
//        });

//        if (tableStudentsArr.length === 0) {
//            alert("⚠️ Please select at least one student to promote.");
//            return false;
//        }

//        var _data = JSON.stringify({
//            obj: {
//                PromotedStudentList: tableStudentsArr,
//                SessionId: parseInt($('#ddlToSession :selected').val())
//            }
//        });

//        $.ajax({
//            type: "POST",
//            url: rootDir + "JQuery/UpdateStudentPromotion",
//            data: _data,
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            async: false,
//            success: function (data) {
//                if (data && data.IsSuccess) {
//                    clearFormElements();
//                    $('#div_AlertSuccess').show().text(data.Message);
//                } else {
//                    $('#div_danger').show().text(data.Message);
//                }
//            },
//            error: function () {
//                alert("Process Fail...");
//            }
//        });
//    }
//}


//let pendingChange = { type: "", value: "", target: null };

//// Class change
//$(document).on("change", ".GridddlClass", function () {
//    pendingChange = {
//        type: "class",
//        value: $(this).val(),
//        target: $(this)
//    };
//    $("#applyAllModal").modal("show");
//});

//// Section change
//$(document).on("change", ".GridddlSection", function () {
//    pendingChange = {
//        type: "section",
//        value: $(this).val(),
//        target: $(this)
//    };
//    $("#applyAllModal").modal("show");
//});

//// Yes → apply to all
//$(document).on("click", "#btnYes", function () {
//    if (pendingChange.type === "class") {
//        // Update all class dropdowns
//        $(".GridddlClass").val(pendingChange.value).trigger("change");
//    }
//    else if (pendingChange.type === "section") {
//        // Update all section dropdowns
//        $(".GridddlSection").each(function () {
//            // Make sure this select has the option
//            if ($(this).find("option[value='" + pendingChange.value + "']").length) {
//                $(this).val(pendingChange.value);
//            }
//        });
//    }

//    $("#applyAllModal").modal("hide");
//});

//// No → apply only to that row
//$(document).on("click", "#btnNo", function () {
//    $("#applyAllModal").modal("hide");
//});


















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
//function LoadGridDropDownSection(ClassId, ddlSection) {
//    var _data = JSON.stringify({
//        classId: parseInt(ClassId),
//    });
//    $.ajax({
//        type: "POST",
//        url: rootDir + "JQuery/ClassWiseSection",
//        data: _data,
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        async: false,
//        success: function (data, status) {
//            if (status == "success") {
//                BindSectionDropDownList($(ddlSection), data);
//                //$(ddlSection).selectpicker('refresh');
//            } else {
//                showMessage("Unable to process the request...", 0);
//            }
//        }
//    });
//}
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
//function ValidatePromotion() {
//    if ($('#ddlFromSession ').val() == 0) {
//        alert('Please Select From Session....')
//        return false;
//    }
//    if ($('#ddlToSession ').val() == 0) {
//        alert('Please Select To Session....')
//        return false;
//    }
//    if (CheckingValidSession() == true) return true;

//    var tbl = document.getElementById('example');
//    if (tbl.rows.length == 0) {
//        alert('There is no Students to promote!!....');
//        return false;
//    }
//}
//function CheckingValidSession() {
//    var fromSession = $('#ddlFromSession option:selected').text();
//    var toSession = $('#ddlToSession option:selected').text();
//    var fromParts = parseInt(fromSession.split('-')[1]);
//    var toParts = parseInt(toSession.split('-')[1]);
//    if (toParts <= fromParts) {
//        alert('Please Select To Proper Session....')
//        return false;
//    }
//    return true;
//}
//function clearFormElements() {
//    DeleteRows();
//    $(':input', '#Promotion_form').not(':button, :submit, :reset,#div_AlertSuccess,#div_danger').val('').prop('checked', false).prop('selected', false);
//    $('#ddlClass').val("0");
//    $('#ddlClass').selectpicker('refresh');

//    $('#ddlSection').find('option').remove().end().append('<option value="0">Select</option>');
//    $('#ddlSection').selectpicker('refresh');

//    $('#ddlFromSession').val("0");
//    $('#ddlFromSession').selectpicker('refresh');

//    $('#ddlToSession').val("0");
//    $('#ddlToSession').selectpicker('refresh');

//}
//function DeleteRows() {
//    var tblStudent = document.getElementById('example')
//    var rowCount = tblStudent.rows.length;
//    for (var i = rowCount - 1; i > 0; i--) {
//        tblStudent.deleteRow(i);
//    }
//}
//function SelectedClassChanged()
//{
//    var rowCount = $('#example >tbody >tr').length;
//    for (var i = 0; i < rowCount; i++) {
//        $('#ddlClass' + i).val($('#Pro_ClassId').val());
//    }
//}
