

$(document).ready(function () {
    loadClassSecFaculty();   
});


function InsertUpdateClassSecFaculty() {
    var list = [];

    $(".faculty-ddl").each(function () {
        var facultyId = $(this).val();
        if (facultyId) {
            list.push({
                CSF_Id: $(this).data("csf-id") || 0,
                CSF_ClassId: parseInt($(this).data("class")),
                CSF_SectionId: parseInt($(this).data("section")),
                CSF_FacultyId: parseInt(facultyId),
                CSF_CreateBy: parseInt($('#hdnUserid').val() || 0)
            });
        }
    });

    if (list.length === 0) {
        ErrorToast("Please select at least one faculty.");
        return;
    }

    $.ajax({
        url: '/JQuery/InsertUpdateClassSecFaculty',
        type: 'POST',
        data: JSON.stringify({ list: list }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            if (data && data.IsSuccess) {
                SuccessToast(data.Message);

                setTimeout(function () {
                    location.reload();
                }, 1200);
            } else {
                ErrorToast(data.Message || "Operation failed.");
            }
        },
        error: function () {
            ErrorToast("Something went wrong.");
        }
    });
}


function loadClassSecFaculty() {
    $.ajax({
        url: '/JQuery/GetClassSecFaculty',
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            console.log("CSF DATA FROM SERVER:", data);

            if (!data || data.length === 0) return;

            $(".faculty-ddl").each(function () {

                var $ddl = $(this);
                var clsId = Number($ddl.data("class"));
                var secId = Number($ddl.data("section"));

                var record = data.find(function (x) {
                    return Number(x.CSF_ClassId) === clsId &&
                           Number(x.CSF_SectionId) === secId;
                });

                if (record && record.CSF_FacultyId) {
                    $ddl
                        .val(String(record.CSF_FacultyId)) // important
                        .data("csf-id", record.CSF_Id)
                        .trigger("change");
                } else {
                    $ddl.val("").data("csf-id", 0);
                }
            });
        },
        error: function () {
            console.log("Failed to load class-section-faculty data.");
        }
    });
}

