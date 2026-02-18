var ctrlURL = rootDir + "MarkSheet/MarkSheet";
var printStudentList = [];
var classes = [];
var isHigherSecondary = false;

function Reset() {
    var url = window.location.href;
    window.location.href = url;
    return false;
}
$(document).ready(function () {
    loadClasses();
    $("#ddlClass").change(function () {
        AjaxPostForDropDownSection(parseInt($('#ddlClass option:selected').val()));
        $.each(classes, function (index, element) {
            if (element.CM_CLASSID == parseInt($('#ddlClass option:selected').val())) {
                isHigherSecondary = element.IsHigherSecondary;
            }
        });
    });
});
$(document).on("click", '.btnPrint', function () {
    var StudentId = $.trim($(this).attr("data-StudentId"));
    var ClassId = $.trim($(this).attr("data-ClassId"));
    var TermId = $.trim($(this).attr("data-TermId"));
    var url = '';
    if (isHigherSecondary == true) url = rootDir + "Reports/MarkSheet/MarkSheetReport.aspx?SId=" + StudentId + "&CId=" + ClassId + "&TermId=" + TermId;
    else url = rootDir + "Reports/MarkSheet/MarkSheetReportSecondary.aspx?SId=" + StudentId + "&CId=" + ClassId + "&TermId=" + TermId;
    var WindowDimensions = "toolbars=no,menubar=no,location=no,titlebar=no,scrollbars=yes,resizable=yes,status=yes"
    var PopUp = window.open(url, "MyMarkSheetReportWindow", WindowDimensions);
    if (PopUp.outerWidth < screen.availWidth || PopUp.outerHeight < screen.availHeight) {
        PopUp.moveTo(25, 50);
        PopUp.resizeTo(screen.availWidth - 10, screen.availHeight - 10);
    }
});
$(document).on("click", "#btnSearch", function (e) {
    if (ValidateForm() == true) {
        var _data = JSON.stringify({
            query: {
                ClassId: parseInt($('#ddlClass option:selected').val()),
                SectionId: parseInt($('#ddlSection option:selected').val()),
                TermId: parseInt($('#ddlTerm option:selected').val()),
                StudentId: $.trim($('#txtStudentId').val()),
            }

        });
        $.ajax({
            type: "POST",
            url: ctrlURL + "/studentsForMarkSheet",
            data: _data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data) {
                printStudentList = [];
                if (data.length > 0) {

                    $.each(data, function (i, Data_row) {
                        printStudentList.push(
                            {
                                StudentId: Data_row.StudentId,
                                ClassId: Data_row.ClassId,
                                TermId: Data_row.TermId,
                            });

                    });
                    $('#Div_print').show();
                    var oTable = $('#StudentList').dataTable({
                        "destroy": true,
                        "oLanguage": {
                            "sSearch": "Search all columns:"
                        },
                        "aaData": data,

                        "aoColumns": [
                            //{ "mDataProp": "SME_ID" },
                            //{ "mDataProp": "ClassId" },
                            //{ "mDataProp": "SectionId" },
                            //{ "mDataProp": "TermId" },
                             {
                                 "mDataProp": '',
                                 "defaultContent": '',
                                 "render": function (data, type, row) {
                                     return ' <input type="checkbox" class="filled-in" id="chk_Marks_' + row.StudentId + '" data-StudentId=' + row.StudentId + ' data-ClassId=' + row.ClassId + ' data-TermId=' + row.TermId + ' /> <label for="chk_Marks_' + row.StudentId + '"></label>';
                                 }
                             },
                            { "mDataProp": "ClassName" },
                            { "mDataProp": "SectionName" },
                            { "mDataProp": "StudentId" },
                            { "mDataProp": "StudentName" },
                            { "mDataProp": "Roll" },
                            { "mDataProp": "TermName" },
                            {
                                "mDataProp": '',
                                "defaultContent": '',
                                "render": function (data, type, row) {
                                    if (CanView == true) {
                                        return "<a class='btnPrint'   title='Print.....'  href='#'  data-StudentId='" + row.StudentId + "' data-ClassId='" + row.ClassId + "'data-TermId='" + row.TermId + "'><img src='" + rootDir + "Content/images/Print.png' title='Print...'/></a>";
                                    }
                                    else {
                                        return "<a class='disabled'   title='Print.....'  href='#'></a>";
                                    }

                                }

                            },
                        ],
                        //"aoColumnDefs": [
                        //     {
                        //         "targets": [0],
                        //         "visible": false,
                        //         "searchable": false
                        //     }],
                        //"aoColumnDefs": [
                        //    {
                        //        "targets": [1],
                        //        "visible": false,
                        //        "searchable": false
                        //    }],
                        //"aoColumnDefs": [
                        //     {
                        //         "targets": [2],
                        //         "visible": false,
                        //         "searchable": false
                        //     }],
                        //"aoColumnDefs": [
                        //     {
                        //         "targets": [3],
                        //         "visible": false,
                        //         "searchable": false
                        //     }],

                        'iDisplayLength': 10,
                        "sPaginationType": "full_numbers",

                    });
                }
                else {
                    messageBox("No Data Found..!!");
                    $('#Div_print').hide();
                }
            }

        });
    }
});
$(document).on("click", "#btnPrintAll", function (e) {
    var data = {
        students: printStudentList,
    };
    $.support.cors = true;

    $.ajax({
        type: "POST",
        url: ctrlURL + "/allStudentMarkSheet",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            var url = '';
            if (isHigherSecondary == true) url = rootDir + "Reports/MarkSheet/MarkSheetReport.aspx";
            else url = rootDir + "Reports/MarkSheet/MarkSheetReportSecondary.aspx";
            var WindowDimensions = "toolbars=no,menubar=no,location=no,titlebar=no,scrollbars=yes,resizable=yes,status=yes"
            var PopUp = window.open(url, "MyMarkSheetReportWindow", WindowDimensions);
            if (PopUp.outerWidth < screen.availWidth || PopUp.outerHeight < screen.availHeight) {
                PopUp.moveTo(25, 50);
                PopUp.resizeTo(screen.availWidth - 10, screen.availHeight - 10);
            }
        }

    });


});
$(document).on("click", "#btnPrintRegister", function (e) {
    if (ValidateForm() == true) {
        var ClassId = parseInt($('#ddlClass option:selected').val());
        var TermId = parseInt($('#ddlTerm option:selected').val());
        var url = rootDir + "Reports/MarkSheet/MarkRegisterReport.aspx?CId=" + ClassId + "&TermId=" + TermId;
        var WindowDimensions = "toolbars=no,menubar=no,location=no,titlebar=no,scrollbars=yes,resizable=yes,status=yes"
        var PopUp = window.open(url, "MyMarkSheetReportWindow", WindowDimensions);
        if (PopUp.outerWidth < screen.availWidth || PopUp.outerHeight < screen.availHeight) {
            PopUp.moveTo(25, 50);
            PopUp.resizeTo(screen.availWidth - 10, screen.availHeight - 10);
        }
    }
});
$(document).on("click", "#btnCheckedPrint", function (e) {
    var checkedStudents = [];
    $('#StudentList>tbody input[type="checkbox"]:checked').each(function (i, row) {
        var cellrow = parseInt($(row).closest('tr').index());
        var Sid = $("#StudentList>tbody:eq(0) tr:eq(" + cellrow + ")").find('input[type="checkbox"]').attr('data-StudentId');
        var Cid = $("#StudentList>tbody:eq(0) tr:eq(" + cellrow + ")").find('input[type="checkbox"]').attr('data-ClassId');
        var Tid = $("#StudentList>tbody:eq(0) tr:eq(" + cellrow + ")").find('input[type="checkbox"]').attr('data-TermId');
        checkedStudents.push(
          {
              StudentId: $.trim(Sid),
              ClassId: parseInt(Cid),
              TermId: parseInt(Tid),
          });
    });
    if (checkedStudents.length == 0) {
        alert("There is a no student to check!!...Please cheched student(s)!!");
        return false;
    }
    var data = {
        students: checkedStudents,
    };
    $.support.cors = true;

    $.ajax({
        type: "POST",
        url: ctrlURL + "/allStudentMarkSheet",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            var url = '';
            if (isHigherSecondary == true) url = rootDir + "Reports/MarkSheet/MarkSheetReport.aspx";
            else url = rootDir + "Reports/MarkSheet/MarkSheetReportSecondary.aspx";
            var WindowDimensions = "toolbars=no,menubar=no,location=no,titlebar=no,scrollbars=yes,resizable=yes,status=yes"
            var PopUp = window.open(url, "MyMarkSheetReportWindow", WindowDimensions);
            if (PopUp.outerWidth < screen.availWidth || PopUp.outerHeight < screen.availHeight) {
                PopUp.moveTo(25, 50);
                PopUp.resizeTo(screen.availWidth - 10, screen.availHeight - 10);
            }
        }

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
                classes = data;
            } else {
                showMessage("Unable to process the request...", 0);
            }
        }
    });
}
function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
           results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}
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
                $("#ddlSection").selectpicker('refresh');
            } else {
                showMessage("Unable to process the request...", 0);
            }
        }
    });
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
function BindSectionDropDownList(ddl, dataCollection) {
    $(ddl).get(0).length = 0;
    $(ddl).get(0).options[0] = new Option("Select", "0", "Selected");
    if (dataCollection.length != 0) {
        for (var i = 0; i < dataCollection.length; i++) {
            $(ddl).get(0).options[i + 1] = new Option(dataCollection[i].SECM_SECTIONNAME, dataCollection[i].SECM_SECTIONID);
        }
    }
}
function ValidateForm() {
    var ddlClass = $('#ddlClass').val();
    var ddlTerm = $('#ddlTerm').val();

    if (ddlClass == "0") {
        alert('select class....');
        return false;
    }
    if (ddlTerm == "" || ddlTerm == "0") {
        alert('select term....');
        return false;
    }
    return true;
}
