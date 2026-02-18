$(document).ready(function () {
    retrive();
});

function retrive() {
    $.ajax({
        type: "POST",
        url: rootDir + "JQuery/GetDashBoardData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (res) {

            if (res && res.classes && res.classes.length > 0) {

                // -----------------------------
                // UPDATE DASHBOARD NUMBERS
                // -----------------------------
                $('#Div_TotalStudent').text(res.result.TOTALSTUDENTS).css('text-align', 'right');
                $('#Div_TotalMale').text(res.result.TOTALMALE).css('text-align', 'right');
                $('#Div_TotalFemale').text(res.result.TOTALFEMALE).css('text-align', 'right');
                $('#Div_TotalCollection').text('₹' + res.result.TOTALPAYMENTS).css('text-align', 'right');
                $('#Div_Notice').text(res.result.Notice).css('text-align', 'right');
                $('#Div_Tc').text(res.result.TotalTC).css('text-align', 'right');
                $('#Div_DropOut').text(res.result.TotalDropOut).css('text-align', 'right');

                // ==================================================
                // CALCULATE TOTAL PRESENT STUDENTS (ES5 safe)
                // ==================================================
                var totalPresentCount = 0;
                if (res.presentCounts && res.presentCounts.length > 0) {
                    for (var i = 0; i < res.presentCounts.length; i++) {
                        // ensure numeric (handles strings)
                        var v = Number(res.presentCounts[i]);
                        if (!isNaN(v)) totalPresentCount += v;
                    }
                }

                // Show Total Present in Dashboard
                $("#Div_TotalPresent").text(totalPresentCount);

                // ==================================================
                // Class Wise Student Strength Chart
                // ==================================================
                var ctx1 = document.getElementById("bar_chart").getContext("2d");
                new Chart(ctx1, getChartJs('bar', res.classes, res.studentNos));

                // ==================================================
                // Daily Attendance Chart (Total vs Present)
                // ==================================================
                var ctx2 = document.getElementById("attendance_chart").getContext("2d");
                new Chart(ctx2, {
                    type: 'bar',
                    data: {
                        labels: res.attendanceClasses || [],
                        datasets: [
                            {
                                label: "Total Students",
                                data: res.totalStudentsAttendance || [],
                                backgroundColor: "rgba(54, 162, 235, 0.7)",
                                borderColor: "rgba(54, 162, 235, 1)",
                                borderWidth: 1
                            },
                            {
                                label: "Present Students",
                                data: res.presentCounts || [],
                                backgroundColor: "rgba(255, 99, 132, 0.7)",
                                borderColor: "rgba(255, 99, 132, 1)",
                                borderWidth: 1
                            }
                        ]
                    },
                    options: {
                        responsive: true,
                        scales: {
                            yAxes: [{
                                ticks: { beginAtZero: true }
                            }]
                        }
                    }
                });

            } else {
                $("#Div_TotalPresent").text(0);
            }
        },
        error: function (data) {
            alert("Data not retrieved successfully.");
        }
    });

    return false;
}


// Helper Chart Function (ES5)
function getChartJs(type, classes, studentNos) {
    var config = null;
    if (type === 'bar') {
        config = {
            type: 'bar',
            data: {
                labels: classes || [],
                datasets: [{
                    label: "Student Number",
                    data: studentNos || [],
                    backgroundColor: 'rgba(0, 188, 212, 0.8)',
                    borderColor: 'rgba(0, 188, 212, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                legend: false,
                scales: {
                    yAxes: [{
                        ticks: { beginAtZero: true }
                    }]
                }
            }
        };
    }
    return config;
}
$(function () {
    $('.count-to').countTo({
        formatter: function (value, options) {
            return '₹' + value.toFixed(0);
        }
    });
});

/// Optional: Menu search functionality
$("#menuSearch").on("keyup", function () {
    var value = $(this).val().toLowerCase();
    var $list = $("#menuSuggestions");
    $list.empty();

    if (value.length === 0) {
        $list.hide();
        return;
    }

    var matches = [];
    $("#leftsidebar .menu ul li a").each(function () {
        var originalText = $(this).find("span").text().trim();
        var compareText = originalText.toLowerCase();
        var href = $(this).attr("href");

        if (compareText.indexOf(value) !== -1 && href && href !== "javascript:void(0);") {
            matches.push("<li data-href='" + href + "' class='list-group-item'>" + originalText + "</li>");
        }
    });

    if (matches.length > 0) {
        $list.html(matches.join("")).show();
    } else {
        $list.html("<li class='disabled list-group-item'>No results</li>").show();
    }
});

$(document).on("click", "#menuSuggestions li:not(.disabled)", function () {
    var link = $(this).attr("data-href");
    if (link && link !== "javascript:void(0);") {
        window.location.href = link;
    }
});

