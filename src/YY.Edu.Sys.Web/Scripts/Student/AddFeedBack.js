﻿jQuery.support.cors = true;
$("#save").on("click", function () {
    var parm = { UserId: 0, UserName: 'student', FDMsg: $("#txtContent").val(), StudentID: $("#hdStudentID").val(), VenueID: $("#hdVenueID").val(), FullName: $("#hdFullName").val() };
    $.ajax({
        type: "POST",
        url: ApiUrl + "/FeedBackLog/Create",
        contentType: "application/json",
        data: JSON.stringify(parm),
        success: function (data, status) {
            if (status == "success") {
                model_alert("留言信息提交成功，等待回复吧");
                $("#save").hide();
            }
        },
        error: function (e) {
            console.log('error');
        },
        complete: function () {

        }
    });

});
