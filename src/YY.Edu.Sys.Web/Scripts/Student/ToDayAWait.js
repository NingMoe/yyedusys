
jQuery.support.cors = true;
function LoadMyCurriculum() {

    nextPagePlus();

    // RequestType 请求类型，1今日，2未上，3已上

    var CurrentDate = "";

    var myCuDate = new Date();
    CurrentDate = myCuDate.getFullYear() + "-" + (myCuDate.getMonth() + 1) + "-" + myCuDate.getDate();

    var requestPrm = " { PageIndex: " + $("#hdPageIndex").val() + ", PageSize:" + $("#hdPageSize").val() + ", RequestType:1,CurrentDate:'" + CurrentDate + "',SearchCondition:{StudentID:" + $("#hdSID").val() + "}}";
    $.ajax({
        type: "get",
        url: ApiUrl + "/Student/GetStudentCurriculum/",
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

            //0预约成功，1上课成功，2学生请假，3老师请假4场馆停课
            $.each(dataCu, function (i, c) {

                var KSstate = c.KSstate;
                var strState = "预约完成";
                if (KSstate == 0) {
                    var myDate = new Date();
                    var currTime = myDate.getFullYear() + "-" + (myDate.getMonth() + 1) + "-" + myDate.getDate() + " " + myDate.getHours() + ":" + myDate.getMinutes() + ":00";
                    var endTime = dateformat(c.CurriculumDate, "yyyy-MM-dd") + " " + c.CurriculumEndTime + ":00";

                    var dcTime = NewDateTime(currTime);
                    var deTime = NewDateTime(endTime);

                    if (dcTime >= deTime) {
                        strState = "未到";
                    }
                }
                else if (KSstate == 1 || KSstate == 5) {
                    strState = "已学完";
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

                    if (dcTime < dbTime) {
                        strState = "等待上课";
                    }
                }
                else if (KSstate == 2) {
                    strState = "请假";
                }
                else if (KSstate == 3) {
                    strState = "老师请假";
                }
                else if (KSstate == 4) {
                    strState = "场馆停课";
                }
                else if (KSstate == 6) {
                    strState = "申请请假";
                }           

                str += '  <li>';
                str += "<a href='CurriculumDetail/?pkid=" + c.PKID + "&cid=" + $("#hdSID").val() + "&cuid=" + c.CurriculumID + "'> ";
                str += '  <p class="time">' + dateformat(c.CurriculumDate, "MM-dd") + "    " + c.CurriculumBeginTime + "-" + c.CurriculumEndTime + '</p>';
                str += '  <p class="address"><i class="iconfont">&#xe600;</i>' + c.VenueName + "-" + c.CampusName + '</p>';
                str += '  <p class="cx"><i class="iconfont">&#xe615;</i>' + c.Title + '</p>';
                str += '  <p class="cx"><i class="iconfont">&#xe612;</i>教练:' + c.CoachFullName + ' 状态:<span><font  style="color:red">' + strState + '</font></span></p>';
                str += '  </a>';
                str += '  </li>';
                str += ' </div>';

            });

            $("#panel0").append(str);
        }
    });
}
function NoEvaluateCurriculum() {

    nextPageWaitClassPlus();
    var CurrentDate = "";

    var myCuDate = new Date();
    CurrentDate = myCuDate.getFullYear() + "-" + (myCuDate.getMonth() + 1) + "-" + myCuDate.getDate();

    var requestPrm = " { PageIndex: " + $("#hdPageIndex1").val() + ", PageSize:" + $("#hdPageSize1").val() + ", RequestType:2,CurrentDate:'" + CurrentDate + "',SearchCondition:{StudentID:" + $("#hdSID").val() + "}}";
    $.ajax({
        type: "get",
        url: ApiUrl + "/Student/GetNoEvaluateCurriculum/",
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
                str += "<a href='CurriculumDetail/?pkid=" + c.PKID + "&cid=" + $("#hdSID").val() + "&cuid=" + c.CurriculumID + "'> ";
                str += '  <p class="time">' + dateformat(c.CurriculumDate, "MM-dd") + "    " + c.CurriculumBeginTime + "-" + c.CurriculumEndTime + '</p>';
                str += '  <p class="address"><i class="iconfont">&#xe600;</i>' + c.VenueName + "-" + c.CampusName + '</p>';
                str += '  <p class="cx"><i class="iconfont">&#xe615;</i>' + c.Title + '</p>';
                str += '  <p class="cx"><i class="iconfont">&#xe612;</i>教练:' + c.CoachFullName + '</p>';
                str += '  </a>';
                str += "  <button type='button' class='order-btn' onclick='Evaluate(" + c.PKID + "," + c.VenueID + "," + c.CoachID + "," + c.StudentID + "," + c.CurriculumID + ")'>评价</button>";
                str += '  </li>';

            });


            $("#panelWaitClass").append(str);
        }
    });
}

function checkTSart(c)
{
    var tsnumber = 0;
    if ($(c).attr("class") == 'am-icon-star-o') {
        $(c).attr("class", "am-icon-star");
        tsnumber = 2;
    }
    else {
        $(c).attr("class", "am-icon-star-o");
    }
    var tez = c;

        while(1 == 1)
        {
            var te = $(tez).prev();
            tez = te;
            var strclass = $(te).attr("class");
            if (strclass != "am-icon-star" && strclass != "am-icon-star-o") {
                break;
            }
            else {
                tsnumber = tsnumber + 2;
                $(te).attr("class", "am-icon-star");
            }
        }

        var tez2 = c;
        while(1 == 1)
        {
            var te = $(tez2).next();
            tez2 = te;
            var strclass = $(te).attr("class");
            if (strclass != "am-icon-star" && strclass != "am-icon-star-o") {
                break;
            }
            else {
                $(te).attr("class", "am-icon-star-o");
            }
        }

        $("#Tpf").text(tsnumber);
  
}



function checkSSart(c) {
    var tsnumber = 0;
    if ($(c).attr("class") == 'am-icon-star-o') {
        $(c).attr("class", "am-icon-star");
        tsnumber = 2;
    }
    else {
        $(c).attr("class", "am-icon-star-o");
    }
    var tez = c;

    while (1 == 1) {
        var te = $(tez).prev();
        tez = te;
        var strclass = $(te).attr("class");
        if (strclass != "am-icon-star" && strclass != "am-icon-star-o") {
            break;
        }
        else {
            tsnumber = tsnumber + 2;
            $(te).attr("class", "am-icon-star");
        }
    }

    var tez2 = c;
    while (1 == 1) {
        var te = $(tez2).next();
        tez2 = te;
        var strclass = $(te).attr("class");
        if (strclass != "am-icon-star" && strclass != "am-icon-star-o") {
            break;
        }
        else {
            $(te).attr("class", "am-icon-star-o");
        }
    }

    $("#Gpf").text(tsnumber);

}


$('#Typj span').bind('click', function () {
    var c = $(this);
    
    checkTSart(c);
});


$('#Ggpj span').bind('click', function () {

    var c = $(this);   
    checkSSart(c);
});

function Evaluate(pkid, venueid, coachid, studentid, curriculumID) {
    $("#txtDP").val('');
    $("#hdVID").val(venueid);
    $("#hdPKID").val(pkid);
    $("#hdCUID").val(curriculumID);

    $("#Gpf").text('0');
    $("#Tpf").text('0');

    $("#Typj span").each(function () {
        $(this).attr("class", "am-icon-star-o");
    });

    $("#Ggpj span").each(function () {
        $(this).attr("class", "am-icon-star-o");
    });
    $('#my-confirm2').modal();
}

$('#btnDP').bind('click', function () {

    if ($("#txtDP").val() == "") {
        alert("请输入评价内容");
        return false;
    }

    var icount1 = 0;
    var icount2 = 0;
    $('#Typj span').each(function () {
        var c = $(this).attr("class");
        if (c == 'am-icon-star') {
            icount1 = icount1 + 1;
        }
    });

    $('#Ggpj span').each(function () {
        var c = $(this).attr("class");
        if (c == 'am-icon-star') {
            icount2 = icount2 + 1;
        }
    });

    if (icount1 == 0 && icount2 == 0)
    {
        alert("给教练打个分吧");
        return false;
    }

    var parm = { CurriculumID: $("#hdCUID").val(), StudentID: $("#hdSID").val(), Info: $("#txtDP").val(), StarLevel: icount1, EffectStarLevel: icount2, PKID: $("#hdPKID").val() };
    $.ajax({
        type: "POST",
        url: ApiUrl + "/Student/AddCurriculumEvaluate",
        contentType: "application/json",
        data: JSON.stringify(parm),
        success: function (data, status) {
            if (status == "success") {
                $("#panelWaitClass").html('');
                $("#hdPageIndex1").val('0');
                NoEvaluateCurriculum();
                alert("评价成功");
                $("#my-confirm2").modal('close');
            }
        },
        error: function (e) {
            console.log('error');
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

