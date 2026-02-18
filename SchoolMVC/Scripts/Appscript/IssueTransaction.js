var DataList = [];

$(document).ready(function () {

    $('#IT_MemberId').prop('disabled', true);
    $('#MTM_TypeId').change(function () {
        var typeId = $(this).val();
        loadMemberDropdown(typeId);   
    });

    $('#IT_CopyId').prop('disabled', true);
    $('#BCM_BookId').change(function () {
        var bookId = $(this).val();
        loadBookCopyDropdown(bookId);
    });

    $('#IT_IssueDate').change(function () {
        DueDate();
    });
});

function loadMemberDropdown(typeId) {
    var $memberDdl = $('#IT_MemberId');
    $memberDdl.empty();
    $memberDdl.append('<option value="">Select Member Code</option>');
    $memberDdl.prop('disabled', true);
    if (!typeId || typeId === "") {
        $memberDdl.selectpicker('refresh');
        return;
    }
    $.ajax({
        url: '/JQuery/MemberMasterList',
        type: 'GET',
        data: { MTM_TypeId: typeId },
        success: function (res) {

            if (res.Data && res.Data.length > 0) {

                $.each(res.Data, function (i, item) {

                    $memberDdl.append(
                        $('<option></option>')
                            .val(item.MM_MemberId)
                            .text(item.MM_MemberCode)
                    );
                });

                $memberDdl.prop('disabled', false);
            }
            $memberDdl.selectpicker('refresh');
        },
        error: function () {
            alert("Error loading members");
        }
    });
}
function loadBookCopyDropdown(bookId) {

    var $copyDdl = $('#BCM_CopyId');
    $copyDdl.empty();
    $copyDdl.append('<option value="">Select Copy Name</option>');
    $copyDdl.prop('disabled', true);
    if (!bookId || bookId === "") {
        $copyDdl.selectpicker('refresh');
        return;
    }
    $.ajax({
        url: '/JQuery/BookCopyMasterList',
        type: 'GET',
        data: { BCM_BookId: bookId},
        success: function (res) {

            if (res.Data && res.Data.length > 0) {

                $.each(res.Data, function (i, item) {

                    $copyDdl.append(
                        $('<option></option>')
                            .val(item.BCM_CopyId)
                            .text(item.BCM_CopyName)
                    );
                });

                $copyDdl.prop('disabled', false);
            }

            $copyDdl.selectpicker('refresh');
        },
        error: function () {
            alert("Error loading book copies");
        }
    });
}
function DueDate() {

    var issueDateVal = $('#IT_IssueDate').val();

    if (!issueDateVal) {
        return;
    }

    $.ajax({
        url: '/JQuery/DueDaysCount',
        type: 'GET',
        data: {},
        success: function (res) {

            if (res.Data && res.Data.length > 0) {

                var dueDays = parseInt(res.Data[0].DueDaysCount);

                if (!isNaN(dueDays)) {

                    var parts = issueDateVal.split('/');

                    var issueDate = new Date(parts[2], parts[1] - 1, parts[0]);

                    issueDate.setDate(issueDate.getDate() + dueDays);

                    var day = ("0" + issueDate.getDate()).slice(-2);
                    var month = ("0" + (issueDate.getMonth() + 1)).slice(-2);
                    var year = issueDate.getFullYear();

                    var finalDueDate = day + '/' + month + '/' + year;

                    $('#IT_DueDate').val(finalDueDate);
                }
            }
        },
        error: function () {
            alert("Error getting due days");
        }
    });
}
function AddIssueDetails() {

    if (!$("#IT_MemberId").val()) {
            WarningToast("Please select Member");
            return;
        }
    if (!$("#BCM_BookId").val()) {
            WarningToast("Please select Book");
            return;
        }
    if (!$("#IT_CopyId").val()) {
            WarningToast("Please select a Copy");
            return;
        }
    if (!$("#IT_IssueDate").val()) {
        WarningToast("Please select a issue date");
        return;
    }

        if ($("#tblList tbody").length === 0) {
            WarningToast("Table not initialized.");
            return;
        }
        var memberId = $("#IT_MemberId").val();
        var memberCode = $("#MM_MemberCode option:selected").text();
        var bookId = $("#BCM_BookId").val();
        var bookname = $("#BM_Title option:selected").text();
        var copyId = $("#IT_CopyId").val();
        var copyname = $("#BCM_CopyCode option:selected").text();
        var issueDate = $("#IT_IssueDate").val();
        var DueDate = $("#IT_DueDate").val();
        var $row = $('<tr/>');
        $row.append('<td style="display:none">0</td>');                      
        $row.append('<td style="display:none">' + memberId + '</td>');
        $row.append('<td>' + memberCode + '</td>');
        $row.append('<td style="display:none">' + bookId + '</td>');
        $row.append('<td>' + bookname + '</td>');
        $row.append('<td style="display:none">' + copyId + '</td>');
        $row.append('<td>' + copyname + '</td>');
        $row.append('<td>' + issueDate + '</td>');
        $row.append('<td>' + DueDate + '</td>');

        $row.append(
            '<td><input type="image" src="/Content/images/close_icon.png" onclick="deleteRow(this)" /></td>'
        );

        $("#tblList>tbody").append($row);
}
