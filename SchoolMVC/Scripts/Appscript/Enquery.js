
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
    if ($("#hdnENQ_Type").val() != "") {
        var Type = $("#hdnENQ_Type").val();
        $("#radio_" + Type).prop("checked", true);
    }
    var ff = $("#hdnchk_Form").val();
    if ($("#hdnchk_Form").val() != "") {

        $("#chk_Form").prop("checked", true);
        $('#DivForm').show();
    }

    var ee = $("#hdnchk_OptionalTest").val();
    if ($("#hdnchk_OptionalTest").val() != "") {

        $("#chk_OptionalTest").prop("checked", true);
        $('#DivOptionalTest').show();
    }
    BindList();
});

// Insert update data
//function InsertUpdateEnquery() {
//    var sex = $('input[name=ENQ_SexId]:checked').val();
//    if (ValidateOperation() == true) {
    

//        $('#btnSave')
//          .prop('disabled', true)
//          .html('<i class="fa fa-spinner fa-spin"></i> Saving...');


//        var fileUpload = $("#ENQ_DocFile").get(0);
//        getFiles(fileUpload);
//        Documentfile = $('#ENQ_DocFile').val().substring(12);

//        if ($('#ENQ_DocFile').val() != '' && $('#ENQ_DocFile').val() != null) {
//            Documentfile = $('#ENQ_DocFile').val().substring(12);
//            Documentpath2 = '~/UploadFile/' + Documentfile;
//        }
//        else {
//            Documentpath2 = $('#hdnImage').val();
//        }



//        if ($('#chk_Form').is(':checked')) {
//            if ($('#ENQ_FormNo').val() == '') {
//                WarningToast('Please provide Form no Or Uncheck the Checkbox.');
//                return false;
//            }
//            else {
//                IsCheck = 'true'
//                $("#hdnchk_Form").val(IsCheck);
//            }
//        }
//        else {
//            IsCheck = 'False'
//            $("#hdnchk_Form").val(IsCheck);
//            $('#ENQ_FormAmount').val(0)
//        }
//        ///added on 01/08/25 by uttaran
//        var isTestChecked = $('#chk_OptionalTest').is(':checked');
//        if (isTestChecked) {
//            if ($('#ENQ_DocFile').val() === '') {
//                WarningToast('Please upload the document file for test.');
//                return false;
//            }
//            if ($('#ENQ_ObMarks').val() === '') {
//                WarningToast('Please enter obtained marks for test.');
//                return false;
//            }
//            if ($('#ENQ_Attributes').val() === '') {
//                WarningToast('Please enter attributes for test.');
//                return false;
//            }
//        }
//        var _data = JSON.stringify({
//            ENQ_Id: $('#ENQ_Id').val(),
//            ENQ_StudentName: $('#ENQ_StudentName').val(),
//            ENQ_GuardianName: $('#ENQ_GuardianName').val(),
//            ENQ_MobNo: $('#ENQ_MobNo').val(),
//            ENQ_DOB: convertToSqlDate($('#ENQ_DOB').val()),
//            ENQ_Age: $('#ENQ_Age').val(),
//            ENQ_SexId: sex,
//            ENQ_ClassId: $('#ENQ_ClassId').val(),
//            ENQ_Date: convertToSqlDate($('#ENQ_Date').val()),
//            ENQ_IsTest: isTestChecked ? 'true' : 'false',
//            ENQ_DocFile: Documentpath2,
//            ENQ_ObMarks: isTestChecked ? $('#ENQ_ObMarks').val() : '',
//            ENQ_Attributes: isTestChecked ? $('#ENQ_Attributes').val() : '',
//            ENQ_Type: $('input[name=Sd_TypeId]:checked').val(),
//            ENQ_IsForm: IsCheck,
//            ENQ_FormNo: $('#ENQ_FormNo').val(),
//            ENQ_AlternativeMobNo: $('#ENQ_AlternativeMobNo').val(),
//            ENQ_FormAmount: $('#ENQ_FormAmount').val(),
//            Paymode: $('input[name="Paymode"]:checked').val(),
//            BankName: $('#BankName').val(),
//            BranchName: $('#BranchName').val(),
//            CheqDDNo: $('#CheqDDNo').val(),
//            CheqDDDate: convertToSqlDate($('#CheqDDDate').val()),
//            Card_TrnsRefNo: $('#Card_TrnsRefNo').val(),
//            Userid: $('#hdnUserid').val()
//        });

//        $.ajax({
//            url: '/JQuery/InsertUpdateEnquery',
//            data: _data,
//            type: 'POST',
//            dataType: 'json',
//            contentType: 'application/json ; utf-8',
//            success: function (data) {
//                if (data != null && data != undefined && data.IsSuccess == true) {
//                    alert(data.Message);
//                    window.location.href = '/StudentManagement/EnqueryList';
//                }
//                else {
//                    $('#btnSave')
//                   .prop('disabled', false)
//                   .html('Save');
//                    ErrorToast(data.Message);
//                }
//            },
//            error: function () {

//                $('#btnSave')
//                    .prop('disabled', false)
//                    .html('Save');
//                ErrorToast('Something wrong happened.');
//            }
//        });
//    }
//}
function InsertUpdateEnquery() {
    var sex = $('input[name=ENQ_SexId]:checked').val();

    if (ValidateOperation() === true) {

        // disable button
        $('#btnSave')
            .prop('disabled', true)
            .html('<i class="fa fa-spinner fa-spin"></i> Saving...');

        try {

            // ---------- File handling ----------
            var fileUpload = $("#ENQ_DocFile").get(0);
            getFiles(fileUpload);

            var Documentfile = '';
            var Documentpath2 = '';

            if ($('#ENQ_DocFile').val() != '' && $('#ENQ_DocFile').val() != null) {
                // safer: extract only file name (works for C:\fakepath\ too)
                Documentfile = $('#ENQ_DocFile').val().split('\\').pop();
                Documentpath2 = '~/UploadFile/' + Documentfile;
            } else {
                Documentpath2 = $('#hdnImage').val();
            }

            // ---------- Form checkbox validation ----------
            var IsCheck = 'False';
            if ($('#chk_Form').is(':checked')) {
                if ($('#ENQ_FormNo').val() == '') {
                    WarningToast('Please provide Form no Or Uncheck the Checkbox.');
                    $('#btnSave').prop('disabled', false).html('Save');
                    return false;
                } else {
                    IsCheck = 'true';
                    $("#hdnchk_Form").val(IsCheck);
                }
            } else {
                IsCheck = 'False';
                $("#hdnchk_Form").val(IsCheck);
                $('#ENQ_FormAmount').val(0);
            }

            // ---------- Optional Test validation ----------
            var isTestChecked = $('#chk_OptionalTest').is(':checked');

            if (isTestChecked) {
                if ($('#ENQ_DocFile').val() === '') {
                    WarningToast('Please upload the document file for test.');
                    $('#btnSave').prop('disabled', false).html('Save');
                    return false;
                }
                if ($('#ENQ_ObMarks').val() === '') {
                    WarningToast('Please enter obtained marks for test.');
                    $('#btnSave').prop('disabled', false).html('Save');
                    return false;
                }
                if ($('#ENQ_Attributes').val() === '') {
                    WarningToast('Please enter attributes for test.');
                    $('#btnSave').prop('disabled', false).html('Save');
                    return false;
                }
            }

            // ---------- Payload ----------
            var _data = JSON.stringify({
                ENQ_Id: $('#ENQ_Id').val(),
                ENQ_StudentName: $('#ENQ_StudentName').val(),
                ENQ_GuardianName: $('#ENQ_GuardianName').val(),
                ENQ_MobNo: $('#ENQ_MobNo').val(),
                ENQ_DOB: convertToSqlDate($('#ENQ_DOB').val()),
                ENQ_Age: $('#ENQ_Age').val(),
                ENQ_SexId: sex,
                ENQ_ClassId: $('#ENQ_ClassId').val(),
                ENQ_Date: convertToSqlDate($('#ENQ_Date').val()),

                // Optional Test
                ENQ_IsTest: isTestChecked ? 'true' : 'false',
                ENQ_DocFile: Documentpath2,
                ENQ_ObMarks: isTestChecked ? $('#ENQ_ObMarks').val() : '',
                ENQ_Attributes: isTestChecked ? $('#ENQ_Attributes').val() : '',

                // Others
                ENQ_Type: $('input[name=Sd_TypeId]:checked').val(),
                ENQ_IsForm: IsCheck,
                ENQ_FormNo: $('#ENQ_FormNo').val(),
                ENQ_AlternativeMobNo: $('#ENQ_AlternativeMobNo').val(),

                // Payment
                ENQ_FormAmount: $('#ENQ_FormAmount').val(),
                Paymode: $('input[name="Paymode"]:checked').val(),
                BankName: $('#BankName').val(),
                BranchName: $('#BranchName').val(),
                CheqDDNo: $('#CheqDDNo').val(),
                CheqDDDate: convertToSqlDate($('#CheqDDDate').val()),
                Card_TrnsRefNo: $('#Card_TrnsRefNo').val(),

                // User
                Userid: $('#hdnUserid').val()
            });

            // ---------- Ajax ----------
            $.ajax({
                url: '/JQuery/InsertUpdateEnquery',
                data: _data,
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {

                    // always enable button back
                    $('#btnSave')
                        .prop('disabled', false)
                        .html('Save');

                    if (data && data.IsSuccess === true) {

                        // ✅ success (backend returns enquiry no in message)
                        SuccessToast(data.Message);

                        setTimeout(function () {
                            window.location.href = '/StudentManagement/EnqueryList';
                        }, 1200);

                    } else {

                        // ❌ duplicate / business error
                        if (data && data.Message) {
                            $('#btnSave')
                      .prop('disabled', false)
                      .html('Save');
                            WarningToast(data.Message);
                        } else {
                            ErrorToast('Something wrong happened.');
                        }
                    }
                },
                error: function () {
                    $('#btnSave')
                        .prop('disabled', false)
                        .html('Save');

                    ErrorToast('Something wrong happened.');
                }
            });

        } catch (e) {
            $('#btnSave')
                .prop('disabled', false)
                .html('Save');

            ErrorToast('Something wrong happened.');
        }
    }
}

// data list in list page
function BindList() {

    $('#update-panel').html('loading data.....');

    $.ajax({
        url: '/JQuery/EnqueryList',
        data: {
            ENQ_ClassId: parseInt($('#ENQ_ClassId').val()),
            ENQ_EnqueryNo: $.trim($('#EnquiryNo').val()),
            ENQ_StudentName: $.trim($('#StudentName').val()),
            FromDate: convertToSqlDate($('#FromDate').val()),
            ToDate: convertToSqlDate($('#ToDate').val())
        },
        dataType: 'json',
        type: 'GET',
        success: function (res) {

            if (res.Data && res.Data.length > 0) {

                var $data = $('<table id="tblList"></table>')
                    .addClass('table table-bordered table-striped');

                // TABLE HEADER
                var $header =
                    "<thead><tr>" +
                    "<th>SL No</th>" +
                    "<th style='display:none'>Id</th>" +
                    "<th>Enquiry Date</th>" +
                    "<th>Enquiry No</th>" +
                    "<th>Student Name</th>" +
                    "<th>Guardian Name</th>" +
                    "<th>Mobile No</th>" +
                    "<th>Class</th>" +
                    "<th>Enquiry Type</th>" +
                    "<th></th>" +
                    "<th></th>" +
                    "<th></th>" +
                    "</tr></thead>";

                $data.append($header);
                // TABLE BODY
                $.each(res.Data, function (i, row) {

                    var $row = $('<tr/>');

                    $row.append($('<td>')); // SL No
                    // Date formatting
                    if (row.ENQ_Date) {
                        var d = new Date(parseInt(row.ENQ_Date.slice(6, -2)));
                        var dateStr = d.getDate() + '/' + (d.getMonth() + 1) + '/' + d.getFullYear();
                        $row.append($('<td>').append(formatJsonDate(row.ENQ_Date)));
                    } else {
                        $row.append($('<td>').append(''));
                    }
                    $row.append($('<td style="display:none">').append(row.ENQ_Id));
                    $row.append($('<td>').append(row.ENQ_EnqueryNo));
                    $row.append($('<td>').append(row.ENQ_StudentName));
                    $row.append($('<td>').append(row.ENQ_GuardianName));
                    $row.append($('<td>').append(row.ENQ_MobNo));
                    $row.append($('<td>').append(row.ENQ_CM_CLASSNAME));
                    $row.append($('<td>').append(row.ENQ_Type));

                    // CHECK ADMISSION STATUS
                    var isAdmitted =
                        row.ENQ_IsAdmitted === true ||
                        row.ENQ_IsAdmitted === 1 ||
                        row.ENQ_IsAdmitted === "1" ||
                        row.ENQ_IsAdmitted === "true" ||
                        row.ENQ_IsAdmitted === "True";

                    /* ================= EDIT ================= */
                    if (isAdmitted) {
                        $row.append(
                            $('<td>').append(
                                "<a class='btn btn-warning disabled' title='Already Admitted'>Edit</a>"
                            )
                        );
                    } else if (res.CanEdit === true) {
                        $row.append(
                            $('<td>').append(
                                "<a href='/StudentManagement/Enquery/" + row.ENQ_Id +
                                "' class='btn btn-warning'>Edit</a>"
                            )
                        );
                    } else {
                        $row.append(
                            $('<td>').append(
                                "<a class='btn btn-warning disabled'>Edit</a>"
                            )
                        );
                    }

                    /* ================= DELETE ================= */
                    if (isAdmitted) {
                        $row.append(
                            $('<td>').append(
                                "<a class='btn btn-danger disabled' title='Already Admitted'>Delete</a>"
                            )
                        );
                    } else if (res.CanDelete === true) {
                        $row.append(
                            $('<td>').append(
                                "<a onclick='Confirm(" + row.ENQ_Id +
                                ");' class='btn btn-danger'>Delete</a>"
                            )
                        );
                    } else {
                        $row.append(
                            $('<td>').append(
                                "<a class='btn btn-danger disabled'>Delete</a>"
                            )
                        );
                    }

                    /* ================= ADMISSION ================= */
                    if (isAdmitted) {
                        $row.append(
                            $('<td>').append(
                                "<button class='btn btn-success' disabled>Admitted</button>"
                            )
                        );
                    } else {
                        $row.append(
                            $('<td>').append(
                                "<a href='/StudentManagement/Admission?EnQNo=" +
                                row.ENQ_Id + "' class='btn btn-primary'>Admission</a>"
                            )
                        );
                    }

                    $data.append($row);
                });

                $("#update-panel").html($data);

                // DATATABLE INIT
                var table = $('#tblList').DataTable({
                    "order": [[1, "desc"]], // sort by hidden Id
                    "columnDefs": [
                        { "orderable": false, "targets": [0, 8, 9, 10] } // SL No + action columns
                    ]
                });

                // SL NO AUTO INCREMENT
                table.on('order.dt search.dt draw.dt', function () {
                    table.column(0, { search: 'applied', order: 'applied' })
                        .nodes()
                        .each(function (cell, i) {
                            cell.innerHTML = i + 1;
                        });
                }).draw();

            } else {
                $("#update-panel").html("<div>No data Found</div>");
            }
        },
        failure: function () {
            ErrorToast('Something went wrong');
        }
    });
}

// Validate Form
function ValidateOperation() {
    var payMode = $('input[name="Paymode"]:checked').val();

    if ($('#chk_Form').is(':checked')) {

        if (!payMode) {
            WarningToast('Please select Payment Mode');
            return false;
        }

        if (payMode === "Cheque" || payMode === "DD") {
            if ($('#BankName').val() === '' ||
                $('#BranchName').val() === '' ||
                $('#CheqDDNo').val() === '' ||
                $('#CheqDDDate').val() === '') {
                WarningToast('Please fill all cheque/DD details');
                return false;
            }
        }

        if (payMode === "Card" || payMode === "Online") {
            if ($('#Card_TrnsRefNo').val() === '') {
                WarningToast('Please enter Transaction Reference No');
                return false;
            }
        }
    }


    if ($('#ENQ_StudentName').val() == "") {
        WarningToast('Please provide Student Name');
        return false;
    }
    if ($('#ENQ_GuardianName').val() == "") {
        WarningToast('Please provide Guardian Name');
        return false;
    }
    if ($('#ENQ_MobNo').val() == '') {
        WarningToast('Please provide Mobile no.');
        return false;
    }
    if ($('#ENQ_ClassId').val() == '') {
        WarningToast('Please Select  a Class');
        return false;
    }
    if ($('#ENQ_DOB').val() == '') {
        WarningToast('Please provide DOB.');
        return false;
    }
    if ($('#ENQ_SexId').val() == '') {
        WarningToast('Please provide Gender.');
        return false;
    }
    if ($('#ENQ_Type').val() == '') {
        WarningToast('Please provide type.');
        return false;
    }
    if ($('#ENQ_Date').val() == '') {
        WarningToast('Please provide Date.');
        return false;
    }
    return true;

}

function Confirm(id) {
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(id);
    }
}
function Deletedata(fieldId) {
    var _data = JSON.stringify({
        MainTableName: 'StudentEnquery_ENQ',
        MainFieldName: 'ENQ_Id',
        PId: parseInt(fieldId),
    });
    $.ajax({
        url: '/JQuery/DeleteData',
        type: 'POST',
        data: _data,
        contentType: 'application/json ; utf-8',
        dataType: 'json',
        success: function (data) {
            if (data != null && data != undefined && data.IsSuccess == true) {
                SuccessToast(data.Message);
                window.location.href = '/StudentManagement/EnqueryList';
            }
            else {
                ErrorToast(data.Message);
            }
        },
        error: function () {
            ErrorToast('Something wrong happend.');
        }

    });
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