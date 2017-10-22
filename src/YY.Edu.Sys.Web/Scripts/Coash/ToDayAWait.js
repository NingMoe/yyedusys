﻿
jQuery.support.cors = true;
function LoadMyCurriculum() {

    nextPagePlus();
    var CurrentDate = "";

    var myCuDate = new Date();
    CurrentDate = myCuDate.getFullYear() + "-" + (myCuDate.getMonth() + 1) + "-" + myCuDate.getDate();

    var requestPrm = " { PageIndex: " + $("#hdPageIndex").val() + ", PageSize:" + $("#hdPageSize").val() + ", RequestType:1,CurrentDate:'" + CurrentDate + "',SearchCondition:{CoachID: " + $("#hdCoachID").val() + "}}";
    $.ajax({
        type: "get",
        url: ApiUrl + "/Coach/GetCoachCurriculum/",
        dataType: "json",
        async: false,
        data: { query: requestPrm },
        success: function (data) {

            var str = "";
            var dataCu = data.data;
            if (dataCu.length >= parseInt($("#hdPageSize").val())) {
                $("#more").show();
            } else {
                $("#more").hide();
            }
            // 0 排课完成 1学生约课完成，2学校停课（需要判断，有没有学生预约）,3请假申请 4请假 5上课中 6上课完成 7发放工资完成

            $.each(dataCu, function (i, c) {

                var KSstate = c.State;

                var strState = "有预约";
                if (KSstate == 1) {                   
                }
                else if (KSstate == 6 || KSstate == 5) {
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

                    //if (dcTime < dbTime) {
                    //    strState = "签到中";
                    //}
                }
                else if (KSstate == 3 || KSstate == 4) {
                    strState = "老师请假";
                }
                else if (KSstate == 2) {
                    strState = "场馆停课";
                }
                else if (KSstate == 7) {
                    strState = "已发工资";
                }
                //0 排课完成 1学生约课完成，2学校停课（需要判断，有没有学生预约）,3请假申请 4请假 5上课中 6上课完成 7发放工资完成
                str += '  <li>';
                str += ' <a href="CurriculumDetail/?cuid=' + c.CurriculumID + '&pkid=' + c.PKID + '">';
                str += '  <p class="time">' + dateformat(c.CurriculumDate, "MM-dd") + "    " + c.CurriculumBeginTime + "-" + c.CurriculumEndTime + '</p>';
                str += '  <p class="address"><i class="iconfont">&#xe600;</i>' + c.VenueName + "-" + c.CampusName + '</p>';
                str += '  <p class="cx"><i class="iconfont">&#xe615;</i>' + c.Title + '</p>';
                str += '  <p class="cx"><i class="iconfont">&#xe612;</i>学生:' + c.Sucount + ' 状态:<span><font  style="color:red">' + strState + '</font></span></p>';
                str += '  </a>';
              
                str += '  </li>';

            });

            $("#panel0").append(str);

        }
    });
}

function NoCommentStudent() {

    nextPageWaitClassPlus();
    var CurrentDate = "";

    var myCuDate = new Date();
    CurrentDate = myCuDate.getFullYear() + "-" + (myCuDate.getMonth() + 1) + "-" + myCuDate.getDate();

    var requestPrm = " { PageIndex: " + $("#hdPageIndex1").val() + ", PageSize:" + $("#hdPageSize1").val() + ", RequestType:2,CurrentDate:'" + CurrentDate + "',SearchCondition:{CoachID:" + $("#hdCoachID").val() + "}}";
    $.ajax({
        type: "get",
        url: ApiUrl + "/Coach/GetNoCommentCurriculumStudent/",
        dataType: "json",
        async: false,
        data: { query: requestPrm },
        success: function (data) {
           
            var str = "";
            var dataCu = data.data;

            if (dataCu.length >= parseInt($("#hdPageSize1").val())) {
                $("#moreWaitClass").show();
            } else {
                $("#moreWaitClass").hide();
            }

            //    1有效，2学校停课（需要判断，有没有学生预约）,3老师请假,4上课

            $.each(dataCu, function (i, c) {

               

                str += '  <li>';
                str += ' <a href="CurriculumDetail/?cuid=' + c.CurriculumID + '&pkid=' + c.PKID + '">';
                str += '  <p class="time">' + dateformat(c.CurriculumDate, "MM-dd") + "    " + c.CurriculumBeginTime + "-" + c.CurriculumEndTime + '</p>';
                str += '  <p class="address"><i class="iconfont">&#xe600;</i>' + c.VenueName + "-" + c.CampusName + '</p>';
                str += '  <p class="cx"><i class="iconfont">&#xe615;</i>' + c.Title + '</p>';
                str += '  <p class="cx"><i class="iconfont">&#xe612;</i>学生:' + c.StudentFullName + '</p>';
                str += '  </a>';
                str += "  <button type='button' class='order-btn' onclick='Comment(" + c.PKID + "," + c.VenueID + "," + c.CoachID + "," + c.StudentID + "," + c.CurriculumID + ")'>点评</button>";
                str += '  </li>';

            });


            $("#panelWaitClass").append(str);
        }
    });
}

function Comment(pkid, venueid, coachid, studentid,curriculumID)
{
    $("#txtDP").val('');
    $("#hdVID").val(venueid);
    $("#hdPKID").val(pkid);
    $("#hdCUID").val(curriculumID);
    $("#hdSID").val(studentid);   
    $('#my-confirm2').modal();

}

$('#btnDP').bind('click', function () {

    if ($("#txtDP").val() == "") {
       alert("请输入点评内容");
        return false;
    }

    var parm = { CurriculumID: $("#hdCUID").val(), StudentID: $("#hdSID").val(), Info: $("#txtDP").val(), CoachID: $("#hdCoachID").val(), PKID: $("#hdPKID").val() };
    $.ajax({
        type: "POST",
        url: ApiUrl + "/Coach/AddCoachComment",
        contentType: "application/json",
        data: JSON.stringify(parm),
        success: function (data, status) {
            if (status == "success") {
                $("#panelWaitClass").html('');
                $("#hdPageIndex1").val('0');
                NoCommentStudent();
                alert("添加点评信息成功");
                $("#my-confirm2").modal('close');
               
            }
        },
        error: function (e) {
            alert("添加点评信息失败，再操作一次吧");
        },
        complete: function () {

        }
    });
});





function nextPagePlus() {
    var iPindex = parseInt($("#hdPageIndex").val()) + 1;
    $("#hdPageIndex").val(iPindex);
}

function nextPageWaitClassPlus() {
    var iPindex = parseInt($("#hdPageIndex1").val()) + 1;
    $("#hdPageIndex1").val(iPindex);
}

function nextPageHistoryPlus() {
    var iPindex = parseInt($("#hdPageIndex2").val()) + 1;
    $("#hdPageIndex2").val(iPindex);
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

$('#btnMore a').bind('click', function () {
    //hdPageIndex
    var iPindex = parseInt($("#hdPageIndex").val()) + 1;
    $("#hdPageIndex").val(iPindex);
    LoadMyCurriculum();
    //hdPageSize
});


function CurriculumStudent(pkid, coachid, cid) {
    location.href = "/Coash/CurriculumStudent/?pkid=" + pkid + "&coachid=" + coachid + "&cid=" + cid;
}
