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
//////////////////////OLD JS "SLG01/25-26/0004" TYPE STUDENTID NOT WORKING THE TCTYPE AND TC NOT WORKING



//$(document).ready(function () {
//});
//function BindList() {
//    $('#update-panel').html('loading data.....');
//    $.ajax({
//        url: '/JQuery/GetAllStudent',
//        data: { CM_CLASSID: parseInt($('#TC_ClassId').val()), SD_StudentId: $.trim($('#txtStudentId').val()), SD_StudentName: $.trim($('#txtStudentName').val()) },
//        dataType: 'json',
//        type: 'GET',
//        success: function (res) {
//            if (res.Data.length > 0) {
//                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
//                $header = "<thead><tr><th>Student Id</th><th>Student Name</th><th>Class</th><th>Mobile No</th><th>Select</th><th>TC No</th><th>TC Fees</th><th>TC Type</th></thead>";
//                $data.append($header);

//                $.each(res.Data, function (i, row) {
//                    var $row = $('<tr/>');
//                    $row.append($('<td>').append(row.SD_StudentId));
//                    $row.append($('<td>').append(row.SD_StudentName));
//                    $row.append($('<td>').append(row.CM_CLASSNAME));
//                    $row.append($('<td>').append(row.SD_ContactNo1));

//                    //$row.append($('<td>').append('<a onclick="btnTC(\'' + row.SD_StudentId + +row.SD_StudentName + '\');" class="btn btn-warning">TC</a>'));
//                    $row.append($('<td>').append('<input type="checkbox"  id=' + row.SD_StudentId + ' class="clickMe filled-in chk-col-green" /><label for=' + row.SD_StudentId + '>TC</label>'));
//                    $row.append($('<td>').append('<input type="text"  id="TCNO' + row.SD_StudentId + '"/>'));
//                    $row.append($('<td>').append('<input type="text"  id="TCFees' + row.SD_StudentId + '"/>'));
//                    $row.append($('<td>').append('<select class="ms form-control" id="TCType' + row.SD_StudentId + '"><option>Select</option></select>'));

//                    var $id = "#TCType" + row.SD_StudentId;
//                    BindTCTypes($id);

//                    $data.append($row);
//                });
//                $("#update-panel").html($data);
//                //$("#tblList").DataTable();
//                //$('#tblList').DataTable({

//                //});
//            }
//            else {
//                $noData = "<div>No data Found</td>"
//                $("#update-panel").html($noData);
//            }

//        },
//        failure: function () {
//            ErrorToast('something wrong happen');
//        }

//    });
//}
//function btnSave() {
//    var ob = [];
//    var Student = {};
//    $('.clickMe:checked').each(function () {
//        $(this).attr('id')
//        //StudentIds.push($(this).attr('id'));
//        var Student = Object.create(null);
//        Student.SD_StudentId = $(this).attr('id');
//        Student.SD_TCNo = $("#TCNO" + Student.SD_StudentId).val();
//        Student.TCTypeId = $("#TCType" + Student.SD_StudentId).val();

//        Student.CM_CLASSID = $("#TC_ClassId").val();
//        Student.SECM_SECTIONID = $("#SectionId" + Student.SD_StudentId).val();

//        Student.TC_Fees = $("#TCFees" + Student.SD_StudentId).val();
//        ob.push(Student);
//    });
//    //alert(TCStudents);
//    $.ajax({
//        type: 'POST',
//        url: "/JQuery/CancelStudents",
//        data: JSON.stringify(ob),
//        contentType: "application/json; charset=utf-8",
//        dataType: 'json',
//        success: function (data) {
//            if (data = "Students are canceled successfully") {
//                window.location.href = '/StudentManagement/TCList';
//            }
//        }
//    });
//}
//function BindTCTypes($id) {


//    $.ajax({
//        url: '/JQuery/BindTCTypes',
//        data: {},
//        dataType: 'json',
//        type: 'GET',
//        success: function (json) {
//            var $el = $("" + $id + "");
//            $el.empty();
//            $el.append($("<option style='color:black'></option>").attr("value", '0').text('Select'));

//            $.each(json, function (key, value) {


//                var html = "<option style='color:black' value='" + value.TCTypeId + "'>" + value.TcTypeName + "</option>";
//                //$el.append($("<option></option>").attr("value", value.FacultyId).text(value.FacultyName));
//                $el.append(html);

//            });
//        },
//        failure: function () {
//            ErrorToast('something wrong happen');
//        }

//    });
//}


////////ADD BY UTTARAN NEW CODE 30/10-2025
$(document).ready(function () {
});
function BindTCTypes(selector) {
    $.ajax({
        url: '/JQuery/BindTCTypes',
        type: 'GET',
        dataType: 'json',
        success: function (json) {
            var $el = $(selector);
            console.log("Populating:", selector, "Found:", $el.length, "Items:", json.length);

            $el.empty().append("<option value='0'>Select</option>");
            $.each(json, function (key, value) {
                $el.append("<option value='" + value.TCTypeId + "'>" + value.TcTypeName + "</option>");
            });
        },
        error: function (xhr, status, error) {
            console.log("Error:", error);
            ErrorToast('Something went wrong while loading TC types');
        }
    });
}
function BindList() {
   
    $('#update-panel').html('loading data.....');

    $.ajax({
        url: '/JQuery/GetAllStudent',
        data: {
            CM_CLASSID: $('#TC_ClassId').val() ? parseInt($('#TC_ClassId').val()) : null,
            SD_StudentId: $('#txtStudentId').val().trim() || null,
            SD_StudentName: $('#txtStudentName').val().trim() || null
        },
        dataType: 'json',
        type: 'GET',
        success: function (res) {
            if (res.Data.length > 0) {
                var $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                var $header = "<thead><tr><th>SL</th><th>Student Id</th><th>Student Name</th><th>Class</th><th>Mobile No</th><th>Select</th></tr></thead>";
                $data.append($header);

                $.each(res.Data, function (i, row) {
                    // Create safe HTML id
                    var safeId = row.SD_StudentId.replace(/[^\w\-]/g, '_');

                    var $row = $('<tr/>');
                    $row.append($('<td>').text(i + 1));
                    $row.append($('<td>').text(row.SD_StudentId));
                    $row.append($('<td>').text(row.SD_StudentName));
                    $row.append($('<td>').text(row.CM_CLASSNAME));
                    $row.append($('<td>').text(row.SD_ContactNo1));

                    // Checkbox with safe id but original in data attribute
                    $row.append($('<td>').append(
                        '<input type="checkbox" id="' + safeId + '" ' +
                        'class="clickMe filled-in chk-col-green" ' +
                        'data-studentid="' + row.SD_StudentId + '"/>' +
                        '<label for="' + safeId + '">TC</label>'
                    ));



                    // Bind TC Types dropdown
                    BindTCTypes("#TCType_" + safeId);

                    $data.append($row);
                });

                $("#update-panel").html($data);
            } else {
                $("#update-panel").html("<div>No data found</div>");
            }
        },
        failure: function () {
            ErrorToast('Something went wrong while loading data');
        }
    });
}
$(document).on('change', '.clickMe', function () {
    var studentId = $(this).data('studentid');

    // Only open modal when checked (not unchecked)
    if ($(this).is(':checked')) {
        var classId = $('#TC_ClassId').val();

        $.ajax({
            url: '/JQuery/GetAllStudent',
            type: 'GET',
            data: {
                CM_CLASSID: classId,
                SD_StudentId: studentId
            },
            dataType: 'json',
            success: function (res) {
                if (res && res.Data.length > 0) {
                    var s = res.Data[0];
                    openTCModal(s); // your modal builder function
                } else {
                    alert('No data found for this student.');
                }
            },
            error: function () {
                alert('Error fetching student data.');
            }
        });
    } else {
        // Optional: close modal or clear if unchecked
        $('#TCModal').modal('hide');
    }
});
$(document).on('click', '.createTcBtn', function () {
    var studentId = $(this).data('studentid');
    var classId = $('#TC_ClassId').val();

    $.ajax({
        url: '/JQuery/GetAllStudent',
        type: 'GET',
        data: {
            CM_CLASSID: classId,
            SD_StudentId: studentId
        },
        dataType: 'json',
        success: function (res) {
            if (res && res.Data.length > 0) {
                var s = res.Data[0]; // only one record expected
                openTCModal(s);
            } else {
                alert('No data found for this student');
            }
        },
        error: function () {
            alert('Error fetching student data.');
        }
    });
});
function openTCModal(student) {
    // ----- STEP 1: BASIC DETAILS -----
    var step1 = ''
      + '<div id="step1">'
      + '  <h6><b>Step 1 – Basic Details</b></h6>'
      + '  <div class="row">'
      + '    <div class="col-md-4"><label>Student ID</label><input type="text" class="form-control" id="SD_StudentId" value="' + (student.SD_StudentId || '') + '" readonly></div>'
      + '    <div class="col-md-4"><label>Student Name</label><input type="text" class="form-control" id="SD_StudentName" value="' + (student.SD_StudentName || '') + '" readonly></div>'
      + '    <div class="col-md-4"><label>Father Name</label><input type="text" class="form-control" id="SD_FatherName" value="' + (student.SD_FatherName || '') + '" readonly></div>'
      + '    <div class="col-md-4"><label>Mother Name</label><input type="text" class="form-control" id="SD_MotherName" value="' + (student.SD_MotherName || '') + '" readonly></div>'
      + '    <div class="col-md-4"><label>Application No</label><input type="text" class="form-control" id="SD_AppliactionNo" value="' + (student.SD_AppliactionNo || '') + '" readonly></div>'
      + '    <div class="col-md-4"><label>Application Date</label><input type="text" class="form-control" id="SD_AppliactionDate" value="' + formatJsonDate(student.SD_AppliactionDate) + '" readonly></div>'
      + '    <div class="col-md-4"><label>DOB</label><input type="text" class="form-control" id="SD_DOB" value="' + formatJsonDate(student.SD_DOB) + '" readonly></div>'
      + '    <div class="col-md-4"><label>Admitted Class</label><input type="text" class="form-control" id="AdmittedClassName" value="' + (student.AdmittedClassName || '') + '" readonly></div>'
      + '    <div class="col-md-4"><label>Present Class</label><input type="text" class="form-control" id="CM_CLASSNAME" value="' + (student.CM_CLASSNAME || '') + '" readonly></div>'
      + '    <div class="col-md-4"><label>Mobile No</label><input type="text" class="form-control" id="SD_ContactNo1" value="' + (student.SD_ContactNo1 || '') + '" readonly></div>'
      + '    <div class="col-md-4"><label>Caste</label><input type="text" class="form-control" id="CSM_CASTENAME" value="' + (student.CSM_CASTENAME || '') + '" readonly></div>'
      + '    <div class="col-md-4"><label>Nationality</label><input type="text" class="form-control" id="NM_NATIONNAME" value="' + (student.NM_NATIONNAME || '') + '" readonly></div>'
      + '    <div class="col-md-4"><label>Subject Group</label><input type="text" class="form-control" id="SGM_SubjectGroupName" value="' + (student.SGM_SubjectGroupName || '') + '" readonly></div>'
      + '    <div class="col-md-4"><label>Subject studied</label><input type="text" class="form-control" id="SubjectNames" value="' + (student.SubjectNames || '') + '" readonly></div>'
      + '  </div>'
      + '  <div class="text-right m-t-3">'
      + '    <button type="button" class="btn btn-primary" id="btnNext">Next &raquo;</button>'
      + '  </div>'
      + '</div>';

    // ----- STEP 2: TC DETAILS -----
    var step2 = ''
      + '<div id="step2" style="display:none;">'
      + '  <h6><b>Step 2 – Transfer Certificate Details</b></h6>'
      + '  <div class="row">'
      + '    <div class="col-md-4"><label>TC No<span style="color:red;">*</span></label><input type="text" class="form-control" id="SD_TCNo" placeholder="Enter TC No"></div>'
      + '    <div class="col-md-4"><label>TC Fees<span style="color:red;">*</span></label><input type="text" class="form-control" id="TC_Fees" placeholder="Enter Fees"></div>'
      + '    <div class="col-md-4"><label>TC Type<span style="color:red;">*</span></label><select class="form-control" id="TCType"></select></div>'
      //added new fields
      + '    <div class="col-md-4"><label>DOB(In words)<span style="color:red;">*</span></label><input type="text" class="form-control" id="DOB_Words"></div>'
      + '    <div class="col-md-4"><label>Proof for DOB submitted of admission<span style="color:red;">*</span></label><input type="text" class="form-control" id="DOB_Proof"></div>'
      + '    <div class="col-md-4"><label>School/Board Last exam taken<span style="color:red;">*</span></label><input type="text" class="form-control" id="Last_Exam"></div>'
      + '    <div class="col-md-4">' + '<label>Failed in the same class<span style="color:red;">*</span></label>'
      + '    <select class="form-control" id="FailedStatus">'+'<option value="">-- Select --</option>'+'<option value="No">No</option>'+'<option value="Yes-Once">Yes-Once</option>'+'<option value="Yes - Twice">Yes-Twice</option>'+'</select>'+'</div>'
      + '    <div class="col-md-4"><label>Qualified for promotion? Specify class<span style="color:red;">*</span></label><input type="text" class="form-control" id="Q_Promotion"></div>'
      + '    <div class="col-md-4"><label> School dues paid up to (month)<span style="color:red;">*</span></label><input type="text" class="form-control" id="Last_DuePaid"></div>'
      + '    <div class="col-md-4">'+'<label>Fee concession availed?<span style="color:red;">*</span></label>'+'<select class="form-control" id="Fee_Consession">'+'<option value="">Select</option>'+'<option value="Yes">Yes</option>'+'<option value="No">No</option>'+'</select>'+'</div>'
      + '    <div class="col-md-4"><label>No. of working days<span style="color:red;">*</span></label><input type="text" class="form-control" id="No_Wdays"></div>'
      + '    <div class="col-md-4"><label>No. of working Present days<span style="color:red;">*</span></label><input type="text" class="form-control" id="No_WPdays"></div>'
      + '    <div class="col-md-4">'+'<label>NCC Cadet/Scout/Guide?<span style="color:red;">*</span></label>'+'<select class="form-control" id="NCC_Cadet_Scout_Guide">'+'<option value="">Select</option>'+'<option value="Yes">Yes</option>'+'<option value="No">No</option>'+'</select>'+'</div>'
      + '    <div class="col-md-4"><label>Games/Activities participated<span style="color:red;">*</span> </label><input type="text" class="form-control" id="Games_Activities" placeholder="Enter reason"></div>'+ '    <div class="col-md-4">'+'<label>School Category <span style="color:red;">*</span></label>'+'<select class="form-control" id="School_Category">'+'<option value="">Select</option>'+'<option value="Government">Government</option>'+'<option value="Minority">Minority</option>'+'<option value="Independent">Independent</option>'+'</select>'+'</div>' 
      + '    <div class="col-md-4"><label>General conduct<span style="color:red;">*</span></label><input type="text" class="form-control" id="Genral_Conduct"></div>'
      + '    <div class="col-md-4"><label>Application Date<span style="color:red;">*</span></label><input type="date" class="form-control" id="AP_Date"></div>'
      + '    <div class="col-md-4"><label>Issue Date<span style="color:red;">*</span></label><input type="date" class="form-control" id="Issue_Date"></div>'
      + '    <div class="col-md-6"><label>Reason for leaving<span style="color:red;">*</span></label><input type="text" class="form-control" id="Reason_Leave"></div>'
      + '    <div class="col-md-6"><label>Any other Remarks</label><input type="text" class="form-control" id="Remarks"></div>'
      + '  </div>'
      + '  <div class="text-right m-t-3">'
      + '    <button type="button" class="btn btn-secondary" id="btnPrev">&laquo; Previous</button>'
      + '    <button type="button" class="btn btn-danger" id="btnSaveTCModal">Submit</button>'
      + '  </div>'
      + '</div>';

    $('#TCModalBody').html(step1 + step2);
    BindTCTypes('#TCType');
    $('#TCModal').modal('show');
}
$(document).on('click', '#btnNext', function () {
    $('#step1').hide();
    $('#step2').fadeIn();
});
$(document).on('click', '#btnPrev', function () {
    $('#step2').hide();
    $('#step1').fadeIn();
});
function formatJsonDate(jsonDate) {
    if (!jsonDate) return '';
    var timestamp = parseInt(jsonDate.replace(/\/Date\((\d+)\)\//, '$1'));
    var date = new Date(timestamp);
    var day = ('0' + date.getDate()).slice(-2);
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var year = date.getFullYear();
    return day + '/' + month + '/' + year; // dd/MM/yyyy
}







function btnSave() {
    var ob = [];

    $('.clickMe:checked').each(function () {
        var studentId = $(this).data('studentid'); 
        var safeId = studentId.replace(/[^\w\-]/g, '_'); 

        var Student = {};
        Student.SD_StudentId = studentId; 
        Student.SD_TCNo = $("#TCNO_" + safeId).val();
        Student.TCTypeId = $("#TCType_" + safeId).val();
        Student.TC_Fees = $("#TCFees_" + safeId).val();
        Student.CM_CLASSID = $("#TC_ClassId").val();

        //  Newly added TC fields
        Student.DOB_Words = $("#DOB_Words_" + safeId).val();
        Student.DOB_Proof = $("#DOB_Proof_" + safeId).val();
        Student.Last_Exam = $("#Last_Exam_" + safeId).val();
        Student.FailedStatus = $("#FailedStatus_" + safeId).val();
        Student.Q_Promotion = $("#Q_Promotion_" + safeId).val();
        Student.Last_DuePaid = $("#Last_DuePaid_" + safeId).val();
        Student.Fee_Consession = $("#Fee_Consession_" + safeId).val();
        Student.No_Wdays = $("#No_Wdays_" + safeId).val();
        Student.No_WPdays = $("#No_WPdays_" + safeId).val();
        Student.NCC_Cadet_Scout_Guide = $("#NCC_Cadet_Scout_Guide_" + safeId).val();
        Student.Games_Activities = $("#Games_Activities_" + safeId).val();
        Student.School_Category = $("#School_Category_" + safeId).val();
        Student.Genral_Conduct = $("#Genral_Conduct_" + safeId).val();
        Student.AP_Date = $("#AP_Date_" + safeId).val();
        Student.Issue_Date = $("#Issue_Date_" + safeId).val();
        Student.Reason_Leave = $("#Reason_Leave_" + safeId).val();
        Student.Remarks = $("#Remarks_" + safeId).val();
        ob.push(Student);
    });

    $.ajax({
        type: 'POST',
        url: "/JQuery/CancelStudents",
        data: JSON.stringify(ob),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            if (data === "Students are canceled successfully") {
                window.location.href = '/StudentManagement/TCList';
            }
        },
        error: function (xhr, status, error) {
            console.error("Save failed:", error);
        }
    });
}


$(document).on('click', '#btnSaveTCModal', function () {
    var Student = {};
    Student.SD_StudentId = $('#SD_StudentId').val();
    Student.SD_TCNo = $('#SD_TCNo').val();
    Student.TC_Fees = $('#TC_Fees').val();
    Student.TCTypeId = $('#TCType').val();
    Student.CM_CLASSID = $('#TC_ClassId').val();

    //  No safeId suffix needed here
    Student.DOB_Words = $("#DOB_Words").val();
    Student.DOB_Proof = $("#DOB_Proof").val();
    Student.Last_Exam = $("#Last_Exam").val();
    Student.FailedStatus = $("#FailedStatus").val();
    Student.Q_Promotion = $("#Q_Promotion").val();
    Student.Last_DuePaid = $("#Last_DuePaid").val();
    Student.Fee_Consession = $("#Fee_Consession").val();
    Student.No_Wdays = $("#No_Wdays").val();
    Student.No_WPdays = $("#No_WPdays").val();
    Student.NCC_Cadet_Scout_Guide = $("#NCC_Cadet_Scout_Guide").val();
    Student.Games_Activities = $("#Games_Activities").val();
    Student.School_Category = $("#School_Category").val();
    Student.Genral_Conduct = $("#Genral_Conduct").val();
    Student.AP_Date = $("#AP_Date").val();
    Student.Issue_Date = $("#Issue_Date").val();
    Student.Reason_Leave = $("#Reason_Leave").val();
    Student.Remarks = $("#Remarks").val();

    var ob = [Student]; // keep same format as bulk save

    $.ajax({
        type: 'POST',
        url: "/JQuery/CancelStudents",
        data: JSON.stringify(ob),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            if (data === "Students are canceled successfully") {
                $('#TCModal').modal('hide');
                alert("TC saved successfully!");
                BindList(); // refresh table
            }
        },
        error: function (xhr, status, error) {
            console.error("Save failed:", error);
        }
    });
});
