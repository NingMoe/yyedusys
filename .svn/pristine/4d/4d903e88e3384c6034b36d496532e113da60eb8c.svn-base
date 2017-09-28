
$(function () {
    var venueID = $("#VenueID").val();

    $.ajax({
        type: "get",
        url: ApiUrl + "api/ControlPanel/Get?venueID=" + venueID,
        data: {},
        success: function (data, status) {
            if (status == "success") {
                if (data.Info == null) {
                    alert('教练未绑定');
                } else {
                    $("#todayWaitForClassStudentCount").html(data.Info.TodayWaitForClassStudentCount);
                    $("#todayBookClassStudentCount").html(data.Info.TodayBookClassStudentCount);
                    $("#todayIncomeMoney").html(data.Info.TodayIncomeMoney);
                    $("#venueSumStudentCount").html(data.Info.VenueSumStudentCount);
                }
            }
        },
        error: function (e) {
        },
        complete: function () {

        }
    });


})