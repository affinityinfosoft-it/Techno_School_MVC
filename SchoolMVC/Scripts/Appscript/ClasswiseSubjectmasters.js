function InsertUpdateClassWiseSub() {
    if (FinalValidation() == true) {
        var ArrayItem = [];
        var tblItem = document.getElementById('tblList');
        var len = tblItem.rows.length;
        for (var i = 1; i < tblItem.rows.length; i++) {
            ArrayItem.push(
                {
                    CSGWS_Class_Id: tblItem.rows[i].cells[1].innerHTML,
                    CSGWS_SubGr_Id: tblItem.rows[i].cells[3].innerHTML,
                    CSGWS_Sub_Id: tblItem.rows[i].cells[5].innerHTML,                   
                });
        }
            var _data = JSON.stringify({

                CSGWS_Id: $('#CSGWS_Id').val(),
                Userid: $('#hdnUserid').val(),
                ClassWiseSubjectList: ArrayItem,
                CSGWS_Class_Id: $('#Class_Id').val()
            });
            $.ajax({
                url: '/JQuery/InsertUpdateClassWiseSubject',
                data: _data,
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json ; utf-8',
                success: function (data) {
                    if (data != null && data != undefined && data.IsSuccess == true) {
                        alert(data.Message);
                        window.location.href = '/Masters/ClassWiseSubjectList';
                    }
                    else {
                        alert(data.Message);
                    }
                },
                error: function () {
                    alert('Something wrong happened.');
                }
            });
        }
    }

   function ValidateOperation() {

        if ($('#CSGWS_Class_Id').val() == 0) {
            alert('Please select a class.');
            return false;
        }
        //if ($('#CSGWS_SubGr_Id').val() == 0) {
        //    alert('Please select a subject group.');
        //    return false;
        //}
        if ($('#CSGWS_Sub_Id').val() == 0) {
            alert('Please select a subject.');
            return false;
        }
         
        var classvalue = $('#CSGWS_Class_Id option:selected').text();
        var subject = $('#CSGWS_Sub_Id option:selected').text();
        var group = $('#CSGWS_SubGr_Id option:selected').text();
        var tblItem = document.getElementById('tblList');
        if ($("#tblList>tbody:eq(0) tr:eq(" + 0 + ") td:eq(2)").text() == '') {

            $("#tblList").find('tbody').empty();

        }
        var len = tblItem.rows.length;
        for (var i = 1; i < tblItem.rows.length; i++)
        {
            
            if (classvalue == tblItem.rows[i].cells[2].innerHTML && group == tblItem.rows[i].cells[4].innerHTML && subject == tblItem.rows[i].cells[6].innerHTML) 
            {
                alert('This Combination already exist.')
                return false;
            }
            else
            {
                continue;
            }
            
         }
             
        return true;
    }
    function BindList() {
        $('#update-panel').html('loading data.....');
        $.ajax({
            url: '/JQuery/ClassWiseSubjectList',
            dataType: 'json',
            type: 'GET',
            success: function (res) {
                if (res.Data.length > 0) {
                    $data = $('<table id="tblList"></table>').addClass('table table-bordered table-striped');
                    $header = "<thead><tr><th>SL</th><th style=display:none>Id</th><th>Class</th><th>Subject Group</th><th>Subject Name</th><th></th><th></th></tr></thead>";
                    $data.append($header);

                    $.each(res.Data, function (i, row) {
                        var $row = $('<tr/>');
                        $row.append($('<td>').text(i + 1));
                        $row.append($('<td style=display:none>').append(row.CSGWS_Id));
                        $row.append($('<td>').append(row.CSGWS_CM_CLASSNAME));
                        $row.append($('<td>').append(row.CSGWS_SGM_SubjectGroupName));
                        $row.append($('<td>').append(row.CSGWS_SBM_SubjectName));

                        if (res.CanEdit == true) {
                            $row.append($('<td>').append("<a href=/Masters/ClassWiseSubject/" + row.CSGWS_Id + " class='btn btn-warning'>Edit</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-warning disabled'>Edit</a>"));
                        }
                        if (res.CanDelete == true) {
                            $row.append($('<td>').append("<a onclick='Confirm(" + row.CSGWS_Id + ");'" + row.CSGWS_Id + " class='btn btn-danger'>Delete</a>"));
                        }
                        else {
                            $row.append($('<td>').append("<a href=# class='btn btn-danger disabled'>Delete</a>"));
                        }
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
                alert('something wrong happen');
            }

        });
    }
    $(document).ready(function () {
        BindList();
    });

    function Confirm(id) {
        var agree = confirm("Are you Sure?");
        if (agree) {
            Deletedata(id);
        }
    }
    function Deletedata(fieldId) {
        var _data = JSON.stringify({
            MainTableName: 'ClsSubGrWiseSubSetting_CSGWS',
            MainFieldName: 'CSGWS_Id',
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
                    alert(data.Message);
                    window.location.href = '/Masters/ClassWiseSubjectList';
                }
                else {
                    alert(data.Message);
                }
            },
            error: function () {
                alert('Something wrong happend.');
            }

        });
    }
    function AddDetails() {
        if (ValidateOperation() == true) {
            var $row = $('<tr/>');
            // $row.append($('<td style=display:none>').append('<input type=text id="data' + j + '" value=' + Id + '>'));
            $row.append($('<td style=display:none>').html($('#CSGWS_Id ').val()));
            $row.append($('<td style=display:none>').html($("#CSGWS_Class_Id option:selected").val()));
            $row.append($('<td/>').html($("#CSGWS_Class_Id option:selected").text()));
            $row.append($('<td style=display:none>').html($("#CSGWS_SubGr_Id option:selected").val()));
            $row.append($('<td/>').html($("#CSGWS_SubGr_Id option:selected").text()));

            $row.append($('<td style=display:none>').html($("#CSGWS_Sub_Id option:selected").val()));
            $row.append($('<td/>').html($("#CSGWS_Sub_Id option:selected").text()));

            //$row.append($('<td/>').html($("#decRate").val()));
            //    

            $row.append($('<td>').append("<a onclick='EditDetails(this);' class='btn btn-warning' href='#'>Edit</a>"));
            $row.append($('<td>').append("<input type='image' name='imgede'  src='/Content/images/delete.png' onclick = 'deleteRow(this);' >"));
            //j++;
            $("#tblList>tbody").append($row);
            ClearDetails();
        }

    }
    function deleteRow(rowNo) {
        var agree = confirm("Are you sure you want to delete this record?");
        if (agree) {
            $(rowNo).closest('tr').remove();
            return true;
        } else {
            return false;
        }

    }
    //Delete Table Row by Row No Without Alert
    function deleteRowWithOutAlert(rowNo) {
        $(rowNo).closest('tr').remove();
    }
    function EditDetails(r) {
        var row = parseInt($(r).closest('tr').index());
        var check = $("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(1)").text();
        $("#CSGWS_Class_Id").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(1)").text());
        $("#CSGWS_Class_Id").selectpicker('refresh');
        $("#CSGWS_SubGr_Id").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(3)").text());
        $("#CSGWS_SubGr_Id").selectpicker('refresh');
        $("#CSGWS_Sub_Id").val($("#tblList>tbody:eq(0) tr:eq(" + row + ") td:eq(5)").text());
        $("#CSGWS_Sub_Id").selectpicker('refresh');

        deleteRowWithOutAlert(r);
    }
    function ClearDetails() {

        $("#CSGWS_Class_Id").val('');
        $("#CSGWS_Class_Id").selectpicker('refresh');
        $("#CSGWS_SubGr_Id").val('');
        $("#CSGWS_SubGr_Id").selectpicker('refresh');
        $("#CSGWS_Sub_Id").val('');
        $("#CSGWS_Sub_Id").selectpicker('refresh');

    }
    function FinalValidation()
    {
        if ($("#tblList > tbody > tr").length == 0) {
            alert("Please add any Details!")
          return false;
        }
        return true;
    }

    