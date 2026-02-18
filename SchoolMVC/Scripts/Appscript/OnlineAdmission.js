$(document).ready(function () {
    console.log("ApprovalList page loaded");
    GetApprovalList();
});

function GetApprovalList() {
    $("#update-panel").html("Loading data.....");

    var _data = JSON.stringify({
        obj: {
            CLASS: $.trim($('#CLASS').val()),
            IsApprove: $.trim($('#IsApprove').val()),
            IsAdmission: $.trim($('#IsAdmission').val()),
        },
    });

    setTimeout(function () {
        $.ajax({
            url: '/JQuery/GetApprovalList',
            data: _data,
            dataType: 'json',
            type: 'POST',
            async: false,
            contentType: "application/json; charset=utf-8",

            success: function (res) {

                if (res.Data.length > 0) {

                    var $data = $('<table id="tblList" class="table table-striped"></table>');

                    // Table Header including NEW Admission Status
                    var $header = $('<thead><tr>' +
                        '<th><label class="custom-checkbox"><input type="checkbox" id="selectAllCheckbox"><span class="checkmark"></span></label></th>' +
                        '<th>Application Date</th>' +
                        '<th>Session</th>' +
                        '<th>Candidate ID</th>' +
                        '<th>Name</th>' +
                        '<th>DOB</th>' +
                        '<th>CLASS</th>' +
                        '<th>Mobile No</th>' +
                        '<th>Email ID</th>' +
                        '<th>Approval Status</th>' +
                        '<th>Admission Status</th>' +  // <-- NEW COLUMN
                        '<th>Actions</th>' +
                        '</tr></thead>');

                    $data.append($header);

                    var $tbody = $('<tbody></tbody>');

                    $.each(res.Data, function (i, row) {

                        var $row = $('<tr/>');

                        // Determine status
                        var statusText = (row.App_Status === 'A') ? 'Approved' : 'Pending';
                        var disabledFlag = (row.App_Status === 'A') ? 'disabled' : '';

                        // Checkbox column
                        $row.append(
                            $('<td>').html(
                                '<label class="custom-checkbox">' +
                                '<input type="checkbox" ' +
                                'class="rowCheckbox" ' +
                                'value="' + row.SA_ID + '" ' +
                                'data-session="' + row.SA_ST_SESSION + '" ' +
                                'data-class="' + row.SA_ST_CLASS + '" ' +
                                'data-candidateid="' + row.SA_ST_CANDIDATEID + '" ' +
                                disabledFlag + '>' +
                                '<span class="checkmark"></span>' +
                                '</label>'
                            )
                        );

                        // Application Date
                        if (row.APPLICATION_DATE) {
                            var formattedAppDate =
                                new Date(parseInt(row.APPLICATION_DATE.replace(/\/Date\((.*?)\)\//, '$1')))
                                    .toLocaleDateString('en-GB');
                            $row.append($('<td>').text(formattedAppDate));
                        } else {
                            $row.append($('<td>').text(''));
                        }

                        // Other columns
                        $row.append($('<td>').text(row.SA_ST_SESSION));
                        $row.append($('<td>').text(row.SA_ST_CANDIDATEID));
                        $row.append($('<td>').text(row.ST_NAME));

                        // DOB
                        if (row.ST_DOB) {
                            var formattedDOB =
                                new Date(parseInt(row.ST_DOB.replace(/\/Date\((.*?)\)\//, '$1')))
                                    .toLocaleDateString('en-GB');
                            $row.append($('<td>').text(formattedDOB));
                        } else {
                            $row.append($('<td>').text(''));
                        }

                        $row.append($('<td>').text(row.SA_ST_CLASS));
                        $row.append($('<td>').text(row.ST_MOBILENO));
                        $row.append($('<td>').text(row.ST_EMAIL));

                        // Approval Status
                        $row.append($('<td>').text(statusText));

                        // NEW: Admission Status column
                        $row.append($('<td>').text(row.Admission_Status));

                        // Action Buttons (View)
                        var $actionBtns = $('<td></td>');
                        $actionBtns.append(
                            $('<button>')
                                .addClass('btn btn-info btn-sm')
                                .attr('title', 'View')
                                .attr('onclick', 'window.location.href="/Registration/ApplyView?saId=' + row.SA_ID + '"')
                                .html('<i class="fas fa-eye"></i> View')
                        );

                        $row.append($actionBtns);
                        $tbody.append($row);
                    });

                    $data.append($tbody);
                    $("#update-panel").html($data);
                }

                else {
                    $("#update-panel").html($('<div>').text('No data found'));
                }
            },

            error: function () {
                ErrorToast('Something went wrong');
            }

        });
    }, 1000);
}

// Select/Deselect all checkboxes
$(document).on("change", "#selectAllCheckbox", function () {
    $(".rowCheckbox").prop("checked", $(this).prop("checked")).trigger('change');
});

// Keep header checkbox in sync with row checkboxes
$(document).on("change", ".rowCheckbox", function () {
    $("#selectAllCheckbox").prop(
        "checked",
        $(".rowCheckbox:checked").length === $(".rowCheckbox").length
    );
});

// Submit Approval for selected SA_IDs

$(document).on("click", "#submitApprovalBtn", function () {

    var selectedIds = [];
    $(".rowCheckbox:checked").each(function () {
        selectedIds.push({
            saId: $(this).val(),
            session: $(this).data("session"),
            class: $(this).data("class"),
            candidateId: $(this).data("candidateid")
        });
    });

    var validityDate = $('#ValidityDate').val();

    if (selectedIds.length > 0) {

        $.ajax({
            url: '/JQuery/ApproveApplications',
            type: 'POST',
            data: JSON.stringify({
                Applications: selectedIds,
                ValidityDate: validityDate
            }),
            contentType: 'application/json',
            success: function (response) {

                // *** FIX HERE ***
                if (response.IsSuccess === true) {
                    alert(response.Message || 'Applications approved successfully!');
                    GetApprovalList();
                } else {
                    alert(response.Message || 'Failed to approve applications!');
                }
            },
            error: function () {
                alert('Error occurred while approving applications.');
            }
        });

    } else {
        alert('Please select at least one application to approve.');
    }

});

