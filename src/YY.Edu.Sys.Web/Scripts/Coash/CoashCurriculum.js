
jQuery.support.cors = true;
function LoadMyCurriculum() {


    var CurrentDate = "";

    var myCuDate = new Date();
    CurrentDate = myCuDate.getFullYear() + "-" + (myCuDate.getMonth() + 1) + "-" + myCuDate.getDate();

    var requestPrm = " { PageIndex: " + $("#hdPageIndex").val() + ", PageSize:" + $("#hdPageSize").val() + ", RequestType: " + $(".current").attr("data-type") + ",CurrentDate:'" + CurrentDate + "',SearchCondition:{CoachID: 1}}";
    $.ajax({
        type: "get",
        url: "http://localhost:53262/api/Coach/GetCoachCurriculum/",
        dataType: "json",
        async: false,
        data: { query: requestPrm },
        //beforeSend: function () {
        //},
        success: function (data) {

            var str = "";
            var dataCu = data.data;
            //    1有效，2学校停课（需要判断，有没有学生预约）,3老师请假,4上课

            $.each(dataCu, function (i, c) {

                var KSstate = c.State;
                var strState = "有预约";
                if (KSstate == 1) {
                    var myDate = new Date();
                    var currTime = myDate.getFullYear() + "-" + (myDate.getMonth() + 1) + "-" + myDate.getDate() + " " + myDate.getHours() + ":" + myDate.getMinutes() + ":00";
                    var endTime = dateformat(c.CurriculumDate, "yyyy-MM-dd") + " " + c.CurriculumEndTime + ":00";

                    var dcTime = NewDateTime(currTime);
                    var deTime = NewDateTime(endTime);

                    if (dcTime >= deTime) {
                        strState = "未签到";
                    }
                }
                else if (KSstate == 4) {
                    strState = "完成";
                    var myDate = new Date();
                    var currTime = myDate.getFullYear() + "-" + (myDate.getMonth() + 1) + "-" + myDate.getDate() + " " + myDate.getHours() + ":" + myDate.getMinutes() + ":00";
                    var beginTime = dateformat(c.CurriculumDate, "yyyy-MM-dd") + " " + c.CurriculumBeginTime + ":00";
                    var endTime = dateformat(c.CurriculumDate, "yyyy-MM-dd") + " " + c.CurriculumEndTime + ":00";

                    var dcTime = NewDateTime(currTime);
                    var dbTime = NewDateTime(beginTime);
                    var deTime = NewDateTime(endTime);
                    if (dcTime >= dbTime && dcTime <= deTime) {
                        strState = "上课中";
                    }

                    if (dcTime < dbTime)
                    {
                        strState = "签到中";
                    }
                }              
                else if (KSstate == 3) {
                    strState = "老师请假";
                }
                else if (KSstate == 2) {
                    strState = "场馆停课";
                }


                str += '<div class="courseT am-g">';
                str += ' <a href="CurriculumDetail/?cuid=' + c.CurriculumID + '&pkid='+c.PKID+'"> <img src="http://s.amazeui.org/media/i/demos/bing-1.jpg" alt="" class="am-u-sm-3 am-u-md-3 am-u-lg-3">';
                str += ' <div class="am-u-sm-9 am-u-md-9 am-u-lg-9">';

                str += '<p class="courseName">' + c.Title + '</p>';
                str += ' <p><span class="am-icon-clock-o"></span><span>' + dateformat(c.CurriculumDate, "yyyy-MM-dd") + " " + c.CurriculumBeginTime + "-" + c.CurriculumEndTime + '</span></p>';
                str += ' <p><span class="am-icon-map-marker"></span><span>' + c.VenueName + "-" + c.CampusName + '</span></p>';
                str += ' <div class="coueseNum am-g"><p class="am-u-sm-6 am-u-md-6 am-u-lg-6">学员人数：<span>（' + c.Sucount +' ） </span></p><p class="am-u-sm-6 am-u-md-6 am-u-lg-6">状态:<span>' + strState + '</span></p></div>';

                str += ' </div> </a>  ';
          
                str += ' <div class="am-checkbox am-u-sm-1 am-u-md-1 am-u-lg-1 ClassX">';
            //    str += " <button type='button' onclick='CurriculumStudent(" + c.PKID + "," + $("#hdCoachID").val() + "," + c.CurriculumID + ")' class='order-btn'>详细</button>";
                str += ' <a href="CurriculumDetail/?cuid=' + c.CurriculumID + '&pkid=' + c.PKID + '">详细</a>';
                str += ' </div>';

                // }
                str += " </div>";

                //str += " <li> ";
                //str += "  <a href='order_detail.html'>";

                //str += "  <p class='time'>" + dateformat(c.CurriculumDate, "yyyy-MM-dd") + "    " + c.CurriculumBeginTime + "-" + c.CurriculumEndTime + "</p>";
                //str += "  <p class='address'><i class='iconfont'>&#xe600;</i>" + c.VenueName + "-" + c.CampusName + "</p>";
                //str += "  <p class='cx'><i class='iconfont'>&#xe612;</i>学员人数:（" + c.Sucount + "）  <font color='red'>" + strState + "</font></p> </a>";

                //if (KSstate == 4 && strState == "完成") {
                //    str += "  <button type='button' data-CurriculumID='" + c.CurriculumID + "' data-PKID='" + c.PKID + "' onclick='CurriculumStudent(" + c.PKID + "," + c.CoachID + "," + c.CurriculumID + ")' class='order-btn'>详细</button> ";
                   
                //}
                //str += " </li>";

            });

            //if (str == "") {
            //    $('#btnMore a').text("没有课程啦");
            //} else { $('#btnMore a').text("更多课程 »"); }
            $("#panel1").append(str);

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

    LoadMyCurriculum();


});



$('#btnMore a').bind('click', function () {
    //hdPageIndex
    var iPindex = parseInt($("#hdPageIndex").val()) + 1;
    $("#hdPageIndex").val(iPindex);
    LoadMyCurriculum();
    //hdPageSize
});

$('.order li a').bind('click', function () {

    $('.order li a').removeClass("current");
    $(this).addClass("current");
    $("#hdPageIndex").val(1);
    $("#list").html('');
    LoadMyCurriculum();
});



function CurriculumStudent(pkid, coachid, cid) {
    location.href = "http://localhost:37396/Coash/CurriculumStudent/?pkid=" + pkid + "&coachid=" + coachid + "&cid=" + cid;
}

function Comment(pkid, coachid, cid) {
    location.href = "http://localhost:37396/Coash/MyComment/?pkid=" + pkid + "&coachid=" + coachid + "&cid=" + cid;
}