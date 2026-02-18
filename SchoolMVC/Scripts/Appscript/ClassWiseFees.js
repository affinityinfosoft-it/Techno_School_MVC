var DataList = [];
var Admission_NOOFINSTALLMENT = 0;

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
    validateNumeric();
    loadNoOfInstallment();
    var Installment;
    $("#CF_FEESID").change(function () {
        $("#CF_FEESAMOUNT").val('');
        $("#CF_INSAMOUNT").val('');
        $.each(DataList, function (index, element) {
            if (element.FEM_FEESID == $("#CF_FEESID").val()) {
                $("#CF_NOOFINS").val(element.FEM_TOTALINSTALLMENT);
                Installment = element.FEM_TOTALINSTALLMENT;
                Admission_NOOFINSTALLMENT = element.FEM_NOOFINSTALLMENT;
            }
        });
    });
    $("#CF_FEESAMOUNT").keyup(function () {
        if ($("#CF_NOOFINS").val() != '') {
            var NoOfInstallments = $('#CF_NOOFINS').val();
            NoOfInstallments = (NoOfInstallments == "" || isNaN($.trim(NoOfInstallments)) == true) ? "0" : parseInt(NoOfInstallments);
            var FeesAmount = $('#CF_FEESAMOUNT').val();
            FeesAmount = (FeesAmount == "" || isNaN($.trim(FeesAmount)) == true) ? "0" : parseFloat(FeesAmount);

            $("#CF_INSAMOUNT").val(parseFloat(FeesAmount / NoOfInstallments).toFixed(2));
        }
        if ($("#CF_NOOFINS").val() == '') {
            $("#CF_INSAMOUNT").val('');
        }
    });
    $("#CF_INSTALLMENTNO").keyup(function () {
        var ints = $("#CF_NOOFINS").val();
        var no = $("#CF_INSTALLMENTNO").val();

        if ($("#CF_INSTALLMENTNO").val() == 0 || $("#CF_INSTALLMENTNO").val() > Installment) {
            alert('Please provide valide Instalment no.');
            $("#CF_INSTALLMENTNO").val('');

        }
    });
    if ($.trim($("#CF_FEESID").val()) != '') {
        if (DataList.length > 0) {
            $.each(DataList, function (index, element) {
                if (element.FEM_FEESID == $("#CF_FEESID").val()) {

                    $("#CF_NOOFINS").val(element.FEM_TOTALINSTALLMENT);
                    Installment = element.FEM_TOTALINSTALLMENT;
                    Admission_NOOFINSTALLMENT = element.FEM_NOOFINSTALLMENT;
                }
            });
        }
    }
});

function ValidateOperation() {

    var classIds = $('#CF_CLASSID').val();
    if (!classIds || classIds.length === 0) {
        WarningToast('Please select at least one class.');
        return false;
    }

    if ($('#CF_CATEGORYID').val() == 0) {
        WarningToast('Please select a Category.');
        return false;
    }

    if ($('#CF_FEESID').val() == 0) {
        WarningToast('Please select Fees Head.');
        return false;
    }

    if ($('#CF_FEESAMOUNT').val() === '') {
        WarningToast('Please provide total Amount.');
        return false;
    }

    if ($('#CF_INSAMOUNT').val() === '') {
        WarningToast('Please provide Installment Amount.');
        return false;
    }

    var tbl = document.getElementById('tblList');
    if (!tbl) return true;

    for (var c = 0; c < classIds.length; c++) {

        var classId = classIds[c];

        for (var i = 1; i < tbl.rows.length; i++) {

            var tblClassId = tbl.rows[i].cells[1].innerHTML; // hidden CF_CLASSID
            var tblCategoryId = tbl.rows[i].cells[3].innerHTML;
            var tblFeesId = tbl.rows[i].cells[5].innerHTML;

            if (
                parseInt(tblClassId) === parseInt(classId) &&
                parseInt(tblCategoryId) === parseInt($('#CF_CATEGORYID').val()) &&
                parseInt(tblFeesId) === parseInt($('#CF_FEESID').val())
            ) {
                messageBox('This combination already exists.');
                return false;
            }
        }
    }

    return true;
}

function BindList() {
    // Show loading message
    $('#update-panel').html('Loading data...');

    $.ajax({
        url: '/JQuery/ClasswiseFeesList',
        type: 'GET',
        dataType: 'json',
        success: function (res) {
            if (res.Data && res.Data.length > 0) {
                let tableHtml = '';
                tableHtml += '<table id="tblList" class="table table-bordered table-striped">';
                tableHtml += '<thead><tr>';
                tableHtml += '<th>SL</th>';
                tableHtml += '<th style="display:none">Id</th>';
                tableHtml += '<th>Class</th>';
                tableHtml += '<th>Fees Head</th>';
                tableHtml += '<th>Amount</th>';
                tableHtml += '<th>No.Of Instalment</th>';
                tableHtml += '<th>Instalment Amount</th>';
                tableHtml += '<th>Fees Category</th>';
                tableHtml += '<th></th>';
                tableHtml += '<th></th>';
                tableHtml += '</tr></thead><tbody>';

                // Loop through each row
                $.each(res.Data, function (i, row) {
                    tableHtml += '<tr>';
                    tableHtml += '<td>' + (i + 1) + '</td>';
                    tableHtml += '<td style="display:none">' + row.CF_CLASSFEESID + '</td>';
                    tableHtml += '<td>' + row.ClassName + '</td>';
                    tableHtml += '<td>' + row.FeesHeadName + '</td>';
                    tableHtml += '<td>' + row.CF_FEESAMOUNT + '</td>';
                    tableHtml += '<td>' + row.CF_NOOFINS + '</td>';
                    tableHtml += '<td>' + row.CF_INSAMOUNT + '</td>';
                    tableHtml += '<td>' + row.CAT_CATEGORYNAME + '</td>';

                    // Edit Button
                    if (res.CanEdit === true) {
                        tableHtml += '<td><a href="/Masters/ClassWiseFees?id=' + row.CF_CLASSFEESID +
                            '&ClassId=' + row.CF_CLASSID +
                            '&CF_CATEGORYID=' + row.CF_CATEGORYID +
                            '&CF_FEESID=' + row.CF_FEESID +
                            '&CF_NOOFINS=' + row.CF_NOOFINS +
                            '" class="btn btn-warning">Edit</a></td>';
                    } else {
                        tableHtml += '<td><a href="#" class="btn btn-warning disabled">Edit</a></td>';
                    }

                    // Delete Button
                    if (res.CanDelete === true) {
                        tableHtml += '<td><a class="btn btn-danger SelectDelete" ' +
                            'data-ClassId="' + row.CF_CLASSID + '" ' +
                            'data-CF_CATEGORYID="' + row.CF_CATEGORYID + '" ' +
                            'data-CF_FEESID="' + row.CF_FEESID + '">Delete</a></td>';
                    } else {
                        tableHtml += '<td><a href="#" class="btn btn-danger disabled">Delete</a></td>';
                    }

                    tableHtml += '</tr>';
                });

                tableHtml += '</tbody></table>';

                // Inject table into DOM
                $('#update-panel').html(tableHtml);

                // Initialize DataTable
                $('#tblList').DataTable({
                    order: [[1, 'desc']]
                });
            } else {
                $('#update-panel').html('<div>No data found</div>');
            }
        },
        error: function () {
            ErrorToast('Something went wrong');
        }
    });
}

$(document).on("click", '.SelectDelete', function () {
    var ClassId = $.trim($(this).attr("data-ClassId"));
    var CF_CATEGORYID = $.trim($(this).attr("data-CF_CATEGORYID"));
    var CF_FEESID = $.trim($(this).attr("data-CF_FEESID"));
    var agree = confirm("Are you Sure?");
    if (agree) {
        Deletedata(ClassId, CF_CATEGORYID, CF_FEESID);
    }
});

function Deletedata(ClassId, CF_CATEGORYID, CF_FEESID) {
    var _data = JSON.stringify({
        ClassId: parseInt(ClassId),
        CF_CATEGORYID: parseInt(CF_CATEGORYID),
        CF_FEESID: parseInt(CF_FEESID),
    });
    $.ajax({
        url: rootDir + 'JQuery/DeleteClassWisefees',
        type: 'POST',
        data: _data,
        contentType: 'application/json ; utf-8',
        dataType: 'json',
        success: function (data) {
            if (data != null && data != undefined && data.IsSuccess == true) {
                SuccessToast(data.Message);
                window.location.href = rootDir + 'Masters/ClasswiseFeesList';
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


function deleteRow(rowNo) {
    var agree = confirm("Are you sure you want to delete this record?");
    if (agree) {
        var Currentrow = parseInt($(rowNo).closest('tr').index());
        var tbl_classValue = $("#tblList>tbody:eq(0) tr:eq(" + Currentrow + ") td:eq(1)").text();
        var tbl_feesCatValue = $("#tblList>tbody:eq(0) tr:eq(" + Currentrow + ") td:eq(3)").text();
        var tbl_feesValue = $("#tblList>tbody:eq(0) tr:eq(" + Currentrow + ") td:eq(5)").text();

        $('#tblList>tbody tr').each(function (e) {
            var TableRow = parseInt($(this).closest('tr').index());
            var classValue = $("#tblList>tbody:eq(0) tr:eq(" + TableRow + ") td:eq(1)").text();
            var feesCatValue = $("#tblList>tbody:eq(0) tr:eq(" + TableRow + ") td:eq(3)").text();
            var feesValue = $("#tblList>tbody:eq(0) tr:eq(" + TableRow + ") td:eq(5)").text();

            if (parseInt(classValue) == parseInt(tbl_classValue) && parseInt(feesCatValue) == parseInt(tbl_feesCatValue) && parseInt(feesValue) == parseInt(tbl_feesValue)) {
                $(this).closest('tr').remove();
            }
        });
    }
}
function EditDetails(r) {
    var row = parseInt($(r).closest('tr').index());
    var check = $("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(1)").val();
    //$("#CF_CLASSID ").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(1)").text());
    $("#CF_CLASSID").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(1)").text());
    $("#CF_CLASSID").selectpicker('refresh');
    $("#CF_CATEGORYID").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(3)").text());
    //$("#CF_CATEGORYID").selectpicker('refresh');
    $("#CF_CATEGORYID").val($row.data("categoryid")).selectpicker('refresh');
    $("#CF_FEESID").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(5)").text());
    $("#CF_FEESID").selectpicker('refresh');
    $("#CF_NOOFINS").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(9)").text());
    $("#CF_FEESAMOUNT").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(7)").text());
    $("#CF_INSAMOUNT").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(8)").text());
    $("#CF_INSTALLMENTNO").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(10)").text());

    $("#CF_DUEDATE").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(11)").text());

    deleteRowWithOutAlert(r);
}
function deleteRowWithOutAlert(rowNo) {
    $(rowNo).closest('tr').remove();
}
function ClearDetails() {
    $("#CF_FEESAMOUNT").val('');
    $("#CF_INSTALLMENTNO").val('');
    $("#CF_INSAMOUNT").val('');
    $("#CF_DUEDATE").val('');
}
function FinalValidation() {
    if ($("#tblList > tbody > tr").length == 0) {
        WarningToast("Please add any Details!")
        return false;
    }
    return true;
}
function loadNoOfInstallment() {
    $.get({
        type: "GET",
        url: rootDir + "JQuery/loadNoOfInstallment",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data, status) {
            if (status == "success") {

                DataList = data;
            } else {
                ErrorToast("Unable to process the request...", 0);
            }
        }
    });
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


function AddDetails() {

    if (ValidateOperation() == true) {

        var NoOfInstallments = parseInt($("#CF_NOOFINS").val());
        var firstDateStr = $("#FirstDueDate").val();

        if (!NoOfInstallments || NoOfInstallments <= 0) {
            WarningToast("Please enter No of Installments!");
            return;
        }

        if (!firstDateStr) {
            WarningToast("Please select First Due Date!");
            return;
        }
        if ($("#tblList tbody").length === 0) {
            WarningToast("Table not initialized.");
            return;
        }
        var p = firstDateStr.split('/');
        var firstDate = new Date(p[2], p[1] - 1, p[0]);

        var monthGap = 1;
        if (NoOfInstallments === 12) monthGap = 1;
        else if (NoOfInstallments === 4) monthGap = 3;
        else if (NoOfInstallments === 2) monthGap = 6;
        else if (NoOfInstallments === 1) monthGap = 12;

        var classIds = $('#CF_CLASSID').val();
        var classTexts = $('#CF_CLASSID option:selected');

        for (var c = 0; c < classIds.length; c++) {

            var classId = classIds[c];
            var className = $(classTexts[c]).text();

            for (var i = 1; i <= NoOfInstallments; i++) {

                var due = new Date(firstDate);
                due.setMonth(firstDate.getMonth() + (monthGap * (i - 1)));

                var dd = ("0" + due.getDate()).slice(-2);
                var mm = ("0" + (due.getMonth() + 1)).slice(-2);
                var yyyy = due.getFullYear();

                var dueDateFormatted = dd + "/" + mm + "/" + yyyy;

                var $row = $('<tr/>');

                $row.append('<td style="display:none">0</td>');
                $row.append('<td style="display:none">' + classId + '</td>');
                $row.append('<td>' + className + '</td>');
                $row.append('<td style="display:none">' + $("#CF_CATEGORYID").val() + '</td>');
                $row.append('<td>' + $("#CF_CATEGORYID option:selected").text() + '</td>');
                $row.append('<td style="display:none">' + $("#CF_FEESID").val() + '</td>');
                $row.append('<td>' + $("#CF_FEESID option:selected").text() + '</td>');
                $row.append('<td>' + $("#CF_FEESAMOUNT").val() + '</td>');
                $row.append('<td>' + $("#CF_INSAMOUNT").val() + '</td>');
                $row.append('<td>' + NoOfInstallments + '</td>');
                $row.append('<td>' + i + '</td>');
                $row.append('<td>' + dueDateFormatted + '</td>');
                $row.append('<td style="display:none">' + (Admission_NOOFINSTALLMENT >= i) + '</td>');

                $row.append('<td><input type="image" src="/Content/images/close_icon.png" onclick="deleteRow(this)" /></td>');

                $("#tblList>tbody").append($row);
            }
        }

        ClearDetails();
    }
}


function InsertUpdateClassFees() {
    if (FinalValidation() == true) {

        var ArrayItem = [];
        var tblItem = document.getElementById('tblList');

        // rows: header row exists -> start from 1
        for (var i = 1; i < tblItem.rows.length; i++) {

            // make sure row has expected number of cells
            var cells = tblItem.rows[i].cells;
            if (!cells || cells.length < 13) continue; // safety

            ArrayItem.push({
                CF_CLASSFEESID: parseInt(cells[0].innerHTML) || 0,        // hidden ID
                CF_CLASSID: parseInt(cells[1].innerHTML),
                ClassName: cells[2].innerHTML,
                CF_CATEGORYID: parseInt(cells[3].innerHTML),
                CategoryName: cells[4].innerHTML,
                CF_FEESID: parseInt(cells[5].innerHTML),
                FeesHeadName: cells[6].innerHTML,
                CF_FEESAMOUNT: parseFloat(cells[7].innerHTML) || 0,
                CF_INSAMOUNT: parseFloat(cells[8].innerHTML) || 0,
                CF_NOOFINS: parseInt(cells[9].innerHTML) || 0,
                CF_INSTALLMENTNO: parseInt(cells[10].innerHTML) || 0,
                CF_DUEDATE: convertToSqlDate(cells[11].innerHTML),
                IsAdmissionTime: (cells[12].innerHTML === 'true' || cells[12].innerHTML === '1') ? true : false
            });

        }

        var _data = JSON.stringify({
            obj: {
                ClassWiseFeesList: ArrayItem
            }
        });
        $.ajax({
            url: '/JQuery/InsertUpdateClasswiseFees',
            data: _data,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json ; utf-8',
            success: function (data) {
                if (data != null && data.IsSuccess == true) {
                    SuccessToast(data.Message);
                    window.location.href = '/Masters/ClasswiseFeesList';
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
$(document).ready(function () {
    $('.selectpicker').selectpicker();
});