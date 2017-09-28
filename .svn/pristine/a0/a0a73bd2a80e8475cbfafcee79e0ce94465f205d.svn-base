
jQuery.support.cors = true;
function LoadNotice() {

    var requestPrm = " { PageIndex: " + $("#hdPageIndex").val() + ", PageSize:" + $("#hdPageSize").val() + ",SearchCondition:{StudentID: 1}}";

    $.ajax({
        type: "get",
        url: "http://localhost:53262/api/VenueNotice/GetMyNotice/",
        dataType: "json",
        async: false,
        data: { query: requestPrm },
        beforeSend: function () {
        },
        success: function (data) {
      
            var str = "";
            var dataCu = data.data;
        //	4	NoticeType			int	4	10	0			公告类型 1公告 2活动 3微信消息 4短信消息 5提醒
            //	11	source			int	4	10	0		((1))	来源，1场馆，2运营平台，3场馆教练 
            $.each(dataCu, function (i, c) {
            
                var NoticeType = "";
                switch (c.NoticeType)
                {
                    case 1:
                        NoticeType = "公告";
                        break;
                    case 2: NoticeType = "活动";
                        break;
                    case 3: NoticeType = "微信推广";
                        break;
                    case 4: NoticeType = "短信推广";
                        break;
                    case 5: NoticeType = "提醒";
                        break;
                }
                var source = "";
                switch (c.source) {
                    case 1:
                        source = "场馆";
                        break;
                    case 2: source = "运营平台";
                        break;
                    case 3: source = "教练";
                        break;
                }
             
                str += '<li>';
                str += ' <p class="newTitle"><span class="am-icon-exclamation"></span><span>【' + source + '】' + NoticeType + '</span></p>';
                str+=' <div>';
                str += '   <p class="_newName">' + c.NoticeTitle + '</p>';
                str += '<p class="_newCon">' + dateformat(c.SendTime, "yyyy-MM-dd HH:mm:ss") + '</p>';
                str+=' </div> </li>';
             
                //str += " <li> ";
                //str += "  <a href='order_detail.html'>";

                //str += "  <p class='time'>" + dateformat(c.SendTime, "yyyy-MM-dd HH:mm:ss") + "</p>";
                //str += "  <p class='address'><i class='iconfont'>&#xe600;</i>" + c.NoticeTitle +"</p>";
                //str += "  <p class='cx'><i class='iconfont'>&#xe612;</i>消息来源：【" + source + "】，消息类型：【" + NoticeType + "】</p> </a>";

              
                //str += "  <button type='button' onclick='Detail(" + c.NoticeId + ")' class='order-btn'>详细</button> ";

              
                //str += " </li>";

            });

            //if (str == "") {
            //    $('#btnMore a').text("没有消息啦");
            //} else { $('#btnMore a').text("更多消息 »"); }

   
            $(".newsCon").append(str);

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

    LoadNotice();


});



$('#btnMore a').bind('click', function () {
    //hdPageIndex
    var iPindex = parseInt($("#hdPageIndex").val()) + 1;
    $("#hdPageIndex").val(iPindex);
    LoadNotice();
    //hdPageSize
});


function Detail(NoticeId) {
    location.href = "http://localhost:37396/Notice/Detail/?NoticeId=" + NoticeId;
}
