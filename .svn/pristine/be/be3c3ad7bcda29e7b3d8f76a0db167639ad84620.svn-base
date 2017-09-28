
jQuery.support.cors = true;

function LoadNoticeDetail() {


    $.ajax({
        type: "get",
        url: "http://localhost:53262/api/VenueNotice/Get/",
        dataType: "json",
        async: false,
        data: { id: $("#hdNoticeID").val() },
        beforeSend: function () {
        },
        success: function (data) {
            var dataCu = data.Info;
            
            $(".am-header-title").text(dataCu.NoticeTitle);
            $(".mess-con").html(dataCu.NoticeMsg);

        }
    });
}
function dateformat(date) {
    var myDate = new Date(date);
    return myDate.getFullYear() + "-" + (myDate.getMonth() + 1) + "-" + myDate.getDate();
}


function NewDateTime(str) {
    str = str.replace("-", "/").replace("-", "/");
    var arr = str.split(' ');

    var strY = arr[0].split('/');
    var date = new Date();
    date.setFullYear(Number(strY[0]));
    date.setMonth(Number(strY[1]) - 1);
    date.setDate(Number(strY[2]));
    if (arr.length > 1) {
        var strH = arr[1].split(':');
        date.setHours(Number(strH[0]));
        date.setMinutes(Number(strH[1]));
        date.setSeconds(Number(strH[2]));
        date.setMilliseconds(0);
    } else {
        date.setHours(0, 0, 0, 0);
    }
    return date;
}

$(document).ready(function () {

    LoadNoticeDetail();


});

