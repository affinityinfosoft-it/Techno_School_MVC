var ctrlURL = rootDir + "MarkSheet/MarkSheet";
var tableStudentsArr = [];
var tableAbsntArr = [];
var classes = [];
var isHigherSecondary = false;
var Id = '';
var CId = '';
var SubjectGroupId = '';


//////////////ADDING EXTRA FEATURES MARKS ENTRY

    $(document).ready(function () {

    // ------------------ Load Exam Groups ------------------
    function loadExamGroups() {
        $.ajax({
            url: rootDir + "JQuery/getExamGroups",
            type: "GET",
            dataType: "json",
            success: function (data) {
                var options = '<option value="">Select Exam Group</option>';
                $.each(data, function (i, eg) {
                    options += '<option value="' + eg.EGM_Code + '">' + eg.EGM_Name + '</option>';
                    //options += '<option value="' + eg.EGM_Id + '">' + eg.EGM_Name + '</option>';

                });
                $('#ddlExamGroup').html(options).selectpicker('refresh');
            },
            error: function (xhr, status, error) {
                console.error("Error loading exam groups:", error);
            }
        });
    }

    // Initial load
    loadExamGroups();

    // ------------------ Exam Group -> Terms ------------------
    $('#ddlExamGroup').on('change', function () {
        var selectedExamGroupId = $(this).val();

        $('#ddlTerm').html('<option value="">Select Term</option>').selectpicker('refresh');

        if (!selectedExamGroupId) return;

        $.ajax({
            url: rootDir + "JQuery/getTermsByExamGroup",
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ examGroupId: selectedExamGroupId }),
            success: function (data) {
                var options = '<option value="">Select Term</option>';
                if (data && data.length > 0) {
                    $.each(data, function (i, term) {
                        options += '<option value="' + term.TM_Id + '">' + term.TM_TermName + '</option>';
                    });
                }
                $('#ddlTerm').html(options).selectpicker('refresh');
            },
            error: function (xhr, status, error) {
                console.error("Error loading terms:", error);
            }
        });
    });

    // ------------------ Term -> Classes ------------------
    $('#ddlTerm').on('change', function () {
        var termId = $(this).val();
        var examGroupId = $('#ddlExamGroup').val();

        $("#CWF_ClassId").html('<option value="0">Select Class</option>').selectpicker('refresh');

        if (!termId || !examGroupId) return;

        $.ajax({
            type: "POST",
            url: rootDir + "JQuery/getClassesByTerm",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                examGroupId: examGroupId,   // string, e.g. "EGM00001"
                termId: parseInt(termId)    // number
            }),
            success: function (data) {
                var options = '<option value="0">Select Class</option>';
                if (data && data.length > 0) {
                    $.each(data, function (i, cls) {
                        options += '<option value="' + cls.CM_CLASSID + '">' + cls.CM_CLASSNAME + '</option>';
                    });
                }
                $("#CWF_ClassId").html(options).selectpicker('refresh');
            },
            error: function (xhr, status, error) {
                console.error("Error loading classes:", error);
            }
        });
    });

});
    function loadClasses() {
        $.get({
            type: "GET",
            url: rootDir + "JQuery/getClassess",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data, status) {
                if (status == "success") {
                    //$("#Div_ClassDDL").empty().append('<select id="CWF_ClassId" name="CWF_ClassId" class="form-control show-tick" required="required"><option value="0">Select Class</option></select>');
                    BindClassDropDownList($('#CWF_ClassId'), data);
                    $("#CWF_ClassId").selectpicker('refresh');
                    classes = data;
                } else {
                    showMessage("Unable to process the request...", 0);
                }
            }
        });
    }




    $(document).ready(function () {
        validateNumeric();
        //$('.aa').materialSelect('destroy');
        //$('.bootstrap-select').select('destroy');

        $(".prac").prop("disabled", true);
        $(".txtPracMarks").prop("disabled", true);
        $('#div_AlertSuccess').hide();
        $('#div_danger').hide();
        //loadClasses();      ///// Disable for until select any term not show the class 
        Id = getParameterByName('Id');
        CId = getParameterByName('CId');
        SubjectGroupId = getParameterByName('SubGrpId');

        if (Id != '' && CId != '' && SubjectGroupId != '') {
            AjaxPostForDropDownSection(parseInt(CId));
            AjaxPostForDropDownSubgroup(parseInt(CId));
            AjaxPostForDropDownSubject(parseInt(CId), parseInt(SubjectGroupId));
            $("#ddlSection").selectpicker('refresh');
            $("#ddlSubgroup").selectpicker('refresh');
            $("#ddlSubject").selectpicker('refresh');
            $.each(classes, function (index, element) {
                if (element.CM_CLASSID == parseInt(CId)) {
                    isHigherSecondary = element.IsHigherSecondary;
                }
            });
            retrive(Id);
        }

    });
    $(document).on("change", '#CWF_ClassId', function (e) {
        $('#ddlSubgroup').find('option').remove().end().append('<option value="0">Select</option>');
        $('#ddlSubject').find('option').remove().end().append('<option value="0">Select</option>');
        DeleteRows();
        var CWF_ClassId = $('#CWF_ClassId option:selected').val();
        AjaxPostForDropDownSection(CWF_ClassId);
        AjaxPostForDropDownSubgroup(CWF_ClassId);
        $("#ddlSection").selectpicker('refresh');
        $("#ddlSubgroup").selectpicker('refresh');
        $("#ddlSubject").selectpicker('refresh');

        $.each(classes, function (index, element) {
            if (element.CM_CLASSID == CWF_ClassId) {
                isHigherSecondary = element.IsHigherSecondary;
            }
        });
    });
    $(document).on("change", '#ddlSection', function (e) {
        var CWF_ClassId = parseInt($('#CWF_ClassId option:selected').val());
        var SectionId = parseInt($('#ddlSection option:selected').val());
        $('#ddlSubgroup').find('option').remove().end();
        AjaxPostForDropDownSubgroup(CWF_ClassId);
        $("#ddlSubgroup").selectpicker('refresh');
        $('#ddlSubject').find('option').remove().end();
        $("#ddlSubject").selectpicker('refresh');
        DeleteRows();
        if (isHigherSecondary == false) {
            AjaxPostForStudentsList(CWF_ClassId, SectionId, 0);
        }
    });
    $(document).on("change", '#ddlSubgroup', function (e) {
        if (isHigherSecondary == true) {
            DeleteRows();
        }
        AjaxPostForDropDownSubject($('#CWF_ClassId option:selected').val(), $('#ddlSubgroup option:selected').val());
        $("#ddlSubject").selectpicker('refresh');

    });
    $(document).on("change", '#ddlSubject', function (e) {
        if (isHigherSecondary == true) {
            DeleteRows();
            var CWF_ClassId = parseInt($('#CWF_ClassId option:selected').val());
            var SectionId = parseInt($('#ddlSection option:selected').val());
            var SubjectId = parseInt($('#ddlSubject option:selected').val());
            AjaxPostForStudentsList(CWF_ClassId, SectionId, SubjectId);
        }
    });




    function SaveRecord() {
        var isPrac = $('#chkPracProj').prop('checked') == true ? true : false;
        var tblStudent = document.getElementById('tblStudent');
        tableStudentsArr = [];
        for (var i = 1; i < tblStudent.rows.length; i++) {
            if (parseFloat(tblStudent.rows[i].cells[3].getElementsByTagName('input')[0].value) > 0) {
                tableStudentsArr.push({
                    StudentId: tblStudent.rows[i].cells[0].innerHTML,
                    SMT_MarksObtained: parseFloat(tblStudent.rows[i].cells[3].getElementsByTagName('input')[0].value),
                    SMT_MarksObtainedP: isPrac == true ? parseFloat(tblStudent.rows[i].cells[4].getElementsByTagName('input')[0].value) : 0,
                    Grade: tblStudent.rows[i].cells[5].innerHTML,
                });
            }
        }
        if (ValidateForm() == true) {
            var _data = JSON.stringify({
                marks: {
                    SME_Id: parseInt($("#hdnId").val()),
                    SME_ExamGroupId: $('#ddlExamGroup').val() && $('#ddlExamGroup').val() !== "" ? parseInt($('#ddlExamGroup').val()) : 0,
                    SME_TermId: parseInt($('#ddlTerm option:selected').val()),            
                    SME_ClassId: parseInt($('#CWF_ClassId option:selected').val()),
                    SME_SectionId: parseInt($('#ddlSection option:selected').val()),
                    SME_SubjectGrpID: parseInt($('#ddlSubgroup option:selected').val()),
                    SME_SubjectId: parseInt($('#ddlSubject option:selected').val()),
                    SME_FullMarks: parseFloat($("#txtmarks").val()),
                    SME_PassMarks: parseFloat($("#txtPassMrks").val()),
                    SME_FullMarksP: parseFloat($("#txtPmarks").val()),
                    SME_PassMarksP: parseFloat($("#txtPPassMrks").val()),
                    StudentMarksTransactionList: tableStudentsArr,
                    IsPractical: isPrac,
                }
            });
            $.get({
                type: "POST",
                url: ctrlURL + "/InsertUpdateMarks",
                data: _data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data != null && data != undefined && data.IsSuccess == true) {
                        if (data.Id != -1) resetMarksFields();
                        //if (data.Id != -1) clearFormElements();
                        $('#div_AlertSuccess').show().text(data.Message);
                    }
                    else {
                        $('#div_danger').show().text(data.Message);
                    }
                },
                error: function (data) {
                    $('#div_danger').show().text('Process Fail...');
                }
            });
        }
    }






    function resetMarksFields() {
        // Reset top input fields
        $("#txtmarks").val("");
        $("#txtPassMrks").val("");
        $("#chkPracProj").prop("checked", false);
        $("#txtPmarks").val("");
        $("#txtPPassMrks").val("");

        // Reset student table marks + grade
        var tblStudent = document.getElementById('tblStudent');
        for (var i = 1; i < tblStudent.rows.length; i++) {
            var theoryInput = tblStudent.rows[i].cells[3].getElementsByTagName('input')[0];
            var pracInput = tblStudent.rows[i].cells[4].getElementsByTagName('input')[0];
            tblStudent.rows[i].cells[5].innerHTML = ""; // grade cell

            if (theoryInput) theoryInput.value = "";
            if (pracInput) pracInput.value = "";
        }
    }




























    function getParameterByName(name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
               results = regex.exec(location.search);
        return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }
    function DeleteRows() {
        var tblStudent = document.getElementById('tblStudent')
        var rowCount = tblStudent.rows.length;
        for (var i = rowCount - 1; i > 0; i--) {
            tblStudent.deleteRow(i);
        }
    }
    function Enable() {
        $(".prac").prop("disabled", false);
        $(".txtPracMarks").prop("disabled", false);
        if (document.getElementById('chkPracProj').checked == false) {
            $(".txtPracMarks").prop("disabled", true).val("");
            $(".prac").prop("disabled", true).val("");
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
    $(document).on("change", '.txtMarks', function (e) {
        var gridMarksObtain = $(this).val();
        var theoryMarks = isNaN($.trim($("#txtmarks").val())) == true ? "0" : $.trim($("#txtmarks").val());
        if (theoryMarks == "") theoryMarks = "0";
        if (parseFloat(gridMarksObtain) > parseFloat(theoryMarks)) {
            alert('Incorrect obtain marks !');
            $(this).val("0").focus();
        }
    });
    $(document).on("change", '.txtPracMarks', function (e) {
        var gridMarksObtain = $(this).val();
        var practicalMarks = isNaN($.trim($("#txtPmarks").val())) == true ? "0" : $.trim($("#txtPmarks").val());
        if (practicalMarks == "") practicalMarks = "0";
        if (parseFloat(gridMarksObtain) > parseFloat(practicalMarks)) {
            alert('Incorrect obtain marks !');
            $(this).val("0").focus();
        }
    });
    $(document).on("change", '#txtmarks', function (e) {
        var txtmarks = $(this).val();
        var txtPassMrks = isNaN($.trim($("#txtPassMrks").val())) == true ? "0" : $.trim($("#txtPassMrks").val());
        if (txtPassMrks == "") txtPassMrks = "0";
        if (parseFloat(txtmarks) < parseFloat(txtPassMrks)) {
            alert('Pass marks should not be exceeded the theory marks!!!....');
            $(this).val("0").focus();
        }
    });
    $(document).on("change", '#txtPassMrks', function (e) {
        var txtPassMrks = $(this).val();
        var txtmarks = isNaN($.trim($("#txtmarks").val())) == true ? "0" : $.trim($("#txtmarks").val());
        if (txtmarks == "") txtmarks = "0";
        if (parseFloat(txtmarks) < parseFloat(txtPassMrks)) {
            alert('Pass marks should not be exceeded the theory marks!!!....');
            $(this).val("0").focus();
        }
    });
    $(document).on("change", '#txtPmarks', function (e) {
        var txtPmarks = $(this).val();
        var txtPPassMrks = isNaN($.trim($("#txtPPassMrks").val())) == true ? "0" : $.trim($("#txtPPassMrks").val());
        if (txtPPassMrks == "") txtPPassMrks = "0";
        if (parseFloat(txtPmarks) < parseFloat(txtPPassMrks)) {
            alert('Pass marks should not be exceeded the practical marks!!!....');
            $(this).val("0").focus();
        }
    });
    $(document).on("change", '#txtPPassMrks', function (e) {
        var txtPPassMrks = $(this).val();
        var txtPmarks = isNaN($.trim($("#txtPmarks").val())) == true ? "0" : $.trim($("#txtPmarks").val());
        if (txtPmarks == "") txtPmarks = "0";
        if (parseFloat(txtPmarks) < parseFloat(txtPPassMrks)) {
            alert('Pass marks should not be exceeded the practical marks!!!....');
            $(this).val("0").focus();
        }
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
    function AjaxPostForDropDownSubgroup(ClassId) {
        var _data = JSON.stringify({
            classId: parseInt(ClassId),
        });
        $.ajax({
            type: "POST",
            url: rootDir + "JQuery/ClassWiseSubjectGroup",
            data: _data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data, status) {
                if (status == "success") {
                    BindDropDownList($('#ddlSubgroup'), data);
                } else {
                    showMessage("Unable to process the request...", 0);
                }
            }
        });
    }


    function AjaxPostForStudentsList(CWF_ClassId, SectionId, subjectId) {
        var HS = isHigherSecondary == true ? "1" : "0";
        var _data = JSON.stringify({
            classId: parseInt(CWF_ClassId),
            sectionId: parseInt(SectionId),
            subjectId: parseInt(subjectId),
            HS: parseInt(HS),
        });

        $.get({
            type: "Post",
            url: rootDir + "JQuery/getStudentsList",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: _data,
            async: false,
            success: function (data, status) {
                if (status == "success") {
                    var tr;
                    for (var i = 0; i < data.length; i++) {
                        tr = $('<tr/>');
                        tr.append("<td>" + data[i].StudentId + "</td>");
                        tr.append("<td>" + data[i].Roll + "</td>");
                        tr.append("<td>" + data[i].StudentName + "</td>");
                        tr.append("<td class='chk'><input type='text' class='txtMarks allow_decimal" + i + "' name='marks' onkeyup='GetGrade(this)'></td>");
                        tr.append("<td><input type='text' class='txtPracMarks allow_decimal' name='txtPracMarks" + i + "' onkeyup='GetGrade(this)'></td>");
                        tr.append("<td></td>");
                        $('#tblStudent').append(tr);
                    }
                    $(".txtPracMarks").prop("disabled", true);
                    $(".prac").prop("disabled", true);
                    validateNumeric();
                } else {
                    showMessage("Unable to process the request...", 0);
                }
            }
        });

    }





    function AjaxPostForDropDownSubject(ClassId, SubjectGrId) {
        if (parseInt(ClassId) == 0) {
            alert("please select class......");
            return false;
        }
        var _data = JSON.stringify({
            classId: parseInt(ClassId),
            SubGrId: parseInt(SubjectGrId),
        });

        $.ajax({
            type: "POST",
            url: rootDir + "JQuery/ClassWiseSubject",
            data: _data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data, status) {
                if (status == "success") {
                    BindDropDownList($('#ddlSubject'), data);
                } else {
                    showMessage("Unable to process the request...", 0);
                }
            }
        });
    }
    function GetGrade(element) {
        var isPrac = $('#chkPracProj').prop('checked') == true ? true : false;
        var tblStudent = document.getElementById('tblStudent');
        var marks = 0;
        var row = element.parentNode.parentNode.rowIndex;
        if (tblStudent.rows[row].cells[3].getElementsByTagName("input")[0].value != 0 || tblStudent.rows[row].cells[3].getElementsByTagName("input")[0].value != '') {
            marks += parseFloat(tblStudent.rows[row].cells[3].getElementsByTagName("input")[0].value)
        }
        if (isPrac == true) {
            if (tblStudent.rows[row].cells[4].getElementsByTagName("input")[0].value != 0 || tblStudent.rows[row].cells[4].getElementsByTagName("input")[0].value != '') {
                marks += parseFloat(tblStudent.rows[row].cells[4].getElementsByTagName("input")[0].value)
            }
        }
        var theoryMarks = ($.trim($("#txtmarks").val()) == "" || isNaN($.trim($("#txtmarks").val()))) == true ? "0" : $.trim($("#txtmarks").val());
        var practicalMarks = 0;
        if (isPrac == true) {
            practicalMarks = ($.trim($("#txtPmarks").val()) == "" || isNaN($.trim($("#txtPmarks").val()))) == true ? "0" : $.trim($("#txtPmarks").val());
        }
        var marksPercentage = parseFloat(marks) * 100 / (parseFloat(theoryMarks) + parseFloat(practicalMarks));

        var _data = JSON.stringify({
            Marks: parseFloat(marksPercentage).toFixed(2)
        });

        $.ajax({
            type: "POST",
            url: rootDir + "JQuery/GradeCheck",
            data: _data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, status) {
                if (status == "success") {
                    tblStudent.rows[row].cells[5].innerHTML = data.GM_GradeName;
                }
                else {
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
    function BindDropDownList(ddl, dataCollection) {
        $(ddl).get(0).length = 0;
        $(ddl).get(0).options[0] = new Option("Select", "0", "Selected");
        if (dataCollection.length != 0) {
            for (var i = 0; i < dataCollection.length; i++) {
                $(ddl).get(0).options[i + 1] = new Option(dataCollection[i].Text, dataCollection[i].Value);
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
        var ddlClass = $('#CWF_ClassId').val();
        var ddlSection = $('#ddlSection').val();
        var ddlSubject = $('#ddlSubject').val();

        var ddlTerm = $('#TM_Id').val();
        var txtmarks = $('#txtmarks').val();
        var txtPassMrks = $('#txtPassMrks').val();

        var txtPmarks = $('#txtPmarks').val();
        var txtPPassMrks = $('#txtPPassMrks').val();
        var ddlsubjectGroup = $('#ddlSubgroup').val();

        if (ddlClass == "0") {
            alert('select class....');
            return false;
        }
        if (ddlSection == "0") {
            alert('select section....');
            return false;
        }
        if (ddlsubjectGroup == "0") {
            alert('select Subject Group....');
            return false;
        }
        if (ddlSubject == "0") {
            alert('select subject....');
            return false;
        }
        if (ddlTerm == "") {
            alert('select term....');
            return false;
        }
        if (txtmarks == "" || txtmarks == "0") {
            alert('enter Theory marks....');
            return false;
        }
        if (txtPassMrks == "" || txtPassMrks == "0") {
            alert('enter pass marks....');
            return false;
        }
        if (parseFloat(txtmarks) < parseFloat(txtPassMrks)) {
            alert('Pass marks should not be exceeded the theory marks!!!....');
            return false;
        }
        if ($('#chkPracProj').prop('checked') == true) {

            if (txtPmarks == "") {
                alert('Enter Practical marks....');
                return false;
            }
            if (txtPPassMrks == "") {
                alert('Enter Practical pass marks....');
                return false;
            }
            if (parseFloat(txtPmarks) < parseFloat(txtPPassMrks)) {
                alert('Pass marks should not be exceeded the practical marks!!!....');
                return false;
            }
        }
        for (var i = 1; i < tblStudent.rows.length; i++) {
            if (tblStudent.rows[i].cells[3].getElementsByTagName('input')[0].value == '') {
                alert('select fill up marks....');
                return false;
            }
        }
        return true;
    }
    function clearFormElements() {
        DeleteRows();
        $(':input', '#AddMarksEntry_form').not(':button, :submit, :reset,#div_AlertSuccess,#div_danger').val('').prop('checked', false).prop('selected', false);
        $('#ddlSection').find('option').remove().end().append('<option value="0">Select</option>');
        $('#ddlSubgroup').find('option').remove().end().append('<option value="0">Select</option>');
        $('#ddlSubject').find('option').remove().end().append('<option value="0">Select</option>');
        $('#CWF_ClassId').val("0");
    }
    function retrive(id) {
        var _data = JSON.stringify({
            marks: {
                SME_Id: parseInt(id),
            }
        });
        $.ajax({
            type: "POST",
            url: ctrlURL + "/GetStudentMarks",
            data: _data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#hdnId').val(data.SME_Id);

                $('#ddlExamGroup option[value="' + data.SME_ExamGroupId + '"]').attr("selected", "selected"); // ✅ Added
                $('#ddlTerm option[value="' + data.SME_TermId + '"]').attr("selected", "selected");           // ✅ Changed

                $('#CWF_ClassId option[value="' + data.SME_ClassId + '"]').attr("selected", "selected");
                $('#ddlSubject option[value="' + data.SME_SubjectId + '"]').attr("selected", "selected");
                //$('#TM_Id option[value="' + data.SME_TermId + '"]').attr("selected", "selected");
                $('#ddlSection option[value="' + data.SME_SectionId + '"]').attr("selected", "selected");
                $('#ddlSubgroup option[value="' + data.SME_SubjectGrpID + '"]').attr("selected", "selected");
                $('#txtmarks').val(data.SME_FullMarks);
                $('#txtPassMrks').val(data.SME_PassMarks);
                $('#txtPmarks').val(data.SME_FullMarksP);
                $('#txtPPassMrks').val(data.SME_PassMarksP);
                tableAbsntArr = [];
                tableAbsntArr = data.StudentMarksTransactionList;
                var tr;
                for (var i = 0; i < tableAbsntArr.length; i++) {
                    tr = $('<tr/>');
                    tr.append("<td>" + tableAbsntArr[i].StudentId + "</td>");
                    tr.append("<td>" + tableAbsntArr[i].Roll + "</td>");
                    tr.append("<td>" + tableAbsntArr[i].StudentName + "</td>");
                    tr.append("<td class='chk'><input type='text' class='txtMarks allow_decimal' name='marks'  value=" + tableAbsntArr[i].SMT_MarksObtained + " onkeyup='GetGrade(this)'></td>");
                    tr.append("<td><input type='text' class='txtPracMarks allow_decimal' name='txtPracMarks' value=" + tableAbsntArr[i].SMT_MarksObtainedP + "  onkeyup='GetGrade(this)'></td>");
                    tr.append("<td>" + tableAbsntArr[i].Grade + "</td>");

                    $('#tblStudent').append(tr);
                }

                $('#CWF_ClassId').prop('disabled', true);
                $('#ddlSubject').prop('disabled', true);
                $('#ddlSection').prop('disabled', true);
                $('#ddlSubgroup').prop('disabled', true);
                $("#CWF_ClassId").selectpicker('refresh');
                $("#ddlSection").selectpicker('refresh');
                $("#ddlSubgroup").selectpicker('refresh');
                $("#ddlSubject").selectpicker('refresh');
                $("#TM_Id").selectpicker('refresh');


                if (data.IsPractical != true) {
                    $(".txtPracMarks").prop("disabled", true);
                    $(".prac").prop("disabled", true);
                    $('#chkPracProj').attr('checked', false);
                }
                else {
                    $(".txtPracMarks").prop("disabled", false);
                    $(".prac").prop("disabled", false);
                    $('#chkPracProj').attr('checked', true);
                }
                validateNumeric();
            },
            error: function (data) {
                alert("Data not get succesfully.......");
            }
        });
        return false;
    }
