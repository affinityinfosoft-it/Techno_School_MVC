var DataList = [];
var Rate = 0;
$(document).ready(function () {
    //validateNumeric();
    AccountMasterList();



});


function AccountMasterList() {
    $('#update-panel').html('loading data.....');

    $.ajax({
        url: rootDir + 'JQuery/SubAccountMasterList',
        data: {  },
        dataType: 'json',
        type: 'GET',
        success: function (res) {
            if (res.Data.length > 0) {
                $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                $header = "<thead><tr><th style=display:none>SAM_SubId</th><th>Sub Account Name</th><th>Sub Account Code</th><th>Ph No.</th><th>Email</th><th>Address</th><th>Edit</th></tr></thead>";
                $data.append($header);

                $.each(res.Data, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td style=display:none>').append(row.SAM_SubId));
                    $row.append($('<td>').append(row.SAM_SubLongDesc));
                    //Added by uttaran 27/11/2024
                    $row.append($('<td>').append(row.SAM_SubCode));
                    $row.append($('<td>').append(row.SAM_OPhone));
                    $row.append($('<td>').append(row.SAM_Email));
                    $row.append($('<td>').append(row.SAM_Address1));
                    //$row.append($('<td>').append(row.SD_CM_CLASSNAME));

                    //$row.append(
                    //$('<td>').append(
                    //    "<a target='_blank' href='" + rootDir + "StudentManagement/StudentManual?id=" + row.SD_StudentId + "'>" +
                    //        "<img src='/Content/images/Print.png' alt='Download' style='width:22px;height:22px;'/>" +
                    //    "</a>"
                    //));
                   
                    //if (res.CanEdit == true) {
                    $row.append($('<td>').append("<a href=/PettyCash/SubAccountMaster?Id=" + row.SAM_SubId + " class='btn btn-warning'>Edit</a>"));
                    
                    $data.append($row);
                });
                $("#update-panel").html($data);
                //$("#tblList").DataTable();
                $('#tblList').DataTable({
                    "order": [[0, "desc"]]
                });
            }
            else {
                $noData = "<div>No data Found</td>"
                $("#update-panel").html($noData);
            }

        },
        failure: function () {
            ErrorToast('something wrong happen');
        }

    });
}



