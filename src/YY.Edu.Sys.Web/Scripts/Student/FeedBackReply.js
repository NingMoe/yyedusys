﻿jQuery.support.cors = true;
function loadFeedBack() {
    $.ajax({
        type: "get",
        url: ApiUrl + "/FeedBackLog/Get",
        contentType: "application/json",
        data: { id: $("#hdID").val() },
        success: function (data) {
            alert(JSON.stringify(data));
            var c = data.Info;
            $("._CD_NOTsc").html(c.ReplyMsg);
            $("._rem_stroy").html("回复时间：" + dateformat(c.ReplyTime,'yyyy-MM-dd HH:mm:ss.fff'));
        },
        error: function (e) {
            console.log('error');
        },
        complete: function () {

        }
    });
}


function dateformat(date, strgs) {
    var myDate = new Date(date);
   
    if (strgs == "yyyy-MM-dd") {
        return myDate.getFullYear() + "-" + (myDate.getMonth() + 1) + "-" + myDate.getDate();
    }
    else {
        return myDate.getFullYear() + "-" + (myDate.getMonth() + 1) + "-" + myDate.getDate() + " " + myDate.getHours() + ":" + myDate.getMinutes() +":" + myDate.getSeconds();  //获取系统时，
    }
}

$(document).ready(function () {

    loadFeedBack();

});
