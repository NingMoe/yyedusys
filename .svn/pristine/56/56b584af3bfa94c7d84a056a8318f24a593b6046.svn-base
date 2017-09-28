
$(function () {
    var venueID = $("#VenueID").val();

    $.ajax({
        type: "get",
        url: ApiUrl + "api/ControlPanel/Get?venueID=" + venueID,
        data: {},
        success: function (data, status) {
            if (status == "success") {
                console.log(data);
                if (data.Info == null) {
                    alert('教练未绑定');
                } else {
                    $("#todayWaitForClassStudentCount").html(data.Info.TodayWaitForClassStudentCount);
                }
            }
        },
        error: function (e) {
        },
        complete: function () {

        }
    });


})