var ctrlURL = rootDir + "MarkSheet/MarkSheet/Search";
$(document).ready(function () {
    bindGrid();
});
function bindGrid() {
    var data = searchDetails;
    var oTable = $('#example').dataTable({
        "destroy": true,
        "oLanguage": {
            "sSearch": "Search all columns:"
        },
        "aaData": data,
        "aoColumns": [{ "mDataProp": "SME_Id" },
            { "mDataProp": "ClassName" },
            { "mDataProp": "SectionName" },
            { "mDataProp": "SubGrpName" },
            { "mDataProp": "SM_SubjectName" },
            { "mDataProp": "TM_TermName" },
            { "mDataProp": "SME_FullMarks" },
            { "mDataProp": "SME_PassMarks" },
               {
                   "mDataProp": '',
                   "render": function (data, type, row) {                       
                       if (CanEdit == true) {
                           return ' <a href="' + rootDir + 'MarkSheet/MarkSheet/AddMarksEntry?Id=' + row.SME_Id + '&CId=' + row.SME_ClassId + '&SubGrpId=' + row.SME_SubjectGrpID + '" class="btn btn-info btn-xs"><i class="fa fa-pencil"></i> Edit </a>';
                       }
                       else {
                           return "<a href=# class='btn btn-warning disabled'>Edit</a>";

                       }                       
                   }
               },
                 {
                     "mDataProp": '',
                     "defaultContent": '',
                     "render": function (data, type, row) {
                         if (CanDelete == true) {
                             return ' <a href="#" class="btn btn-warning btn-xs" onclick="return Delete(' + row.SME_Id + ');"><i class="fa fa-pencil"></i> Delete </a>';
                         }
                         else {
                             return "<a href=# class='btn btn-warning disabled'>Edit</a>";

                         }
                     }

                 },
        ],
        "aoColumnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],

        'iDisplayLength': 10,
        "sPaginationType": "full_numbers",
    });
}


function Delete(id) {
    var YN = confirm("Are you sure you want to delete this record?");
    var deleteid = parseInt(id);
    var _data = JSON.stringify({
        MainTableName: 'StudentMarksEntry_SME',
        MainFieldName: 'SME_Id',
        PId: deleteid,
        TransTableName: 'StudentMarksTransaction_SMT',
        TransFieldName:'SMT_SME_Id'
    });
    if (YN == true) {
        $.support.cors = true;
        $.ajax({
            cache: false,
            url: '/JQuery/DeleteData',
            contentType: "application/json",
            dataType: "json",
            type: "POST",
            data: _data,
            success: function (response) {
                if (response != null && response != undefined && response.IsSuccess == true) {
                    alert(response.Message);
                    window.location.reload();
                } else {
                    alert(response.Message);
                    console.log(response.ExMessage);
                }
            },
            error: function (jqxhr, settings, thrownError) {
                console.log(jqxhr.status + '\n' + thrownError);
            }
        });
    }
}
