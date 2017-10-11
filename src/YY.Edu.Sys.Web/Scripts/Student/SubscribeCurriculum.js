﻿
jQuery.support.cors = true;
function LoadMyCurriculum() {

    var CurrentDate = "";

    var myCuDate = new Date();
    CurrentDate = myCuDate.getFullYear() + "-" + (myCuDate.getMonth() + 1) + "-" + myCuDate.getDate();

    var requestPrm = " { PageIndex: " + $("#hdPageIndex").val() + ", PageSize:" + $("#hdPageSize").val() + ", RequestType: 1,CurrentDate:'" + CurrentDate + "',PKType:1,SearchCondition:{StudentID: " + $("#hdSID").val() + "}}";
    $.ajax({
        type: "get",
        url: ApiUrl + "/Student/GetTeachingScheduleByStudent/",
        dataType: "json",
        async: false,
        data: { query: requestPrm },
        success: function (data) {
            var str = "";
            var dataCu = data.data;
            if (dataCu.length > 0) $("#more").show();
            //0 排课完成 1学生约课完成，2学校停课（需要判断，有没有学生预约）,3请假申请 4请假 5上课中 6上课完成 
            $.each(dataCu, function (i, c) {

                var KSstate = c.State;
                var PKType = c.PKType;
                var iyk = 0;
                if (KSstate == 0 || (KSstate == 1 && PKType == 2)) {
                    iyk = 1;
                }
                var strState = "可预约";
                if (KSstate > 0) {
                    if (KSstate == 1 && PKType == 1) {
                        strState = "已约完";
                    }
                    else {
                        if (KSstate == 2) {
                            strState = "停课";
                        }
                        else if (KSstate == 3) {
                            strState = "教练请假";
                        }
                        else if (KSstate == 4) {
                            strState = "教练请假";
                        }
                        else {
                            strState = "已过预约时间";
                        }
                    }
                }

                var ksInfo = "V" + c.VenueID + "_C" + c.CoachID + "_" + c.ClassNumber;//,VenueID_CoachID_ClassNumber
                //if ($("#hdKS").val() == "") {
                //    $("#hdKS").val(ksInfo); //VenueID_CoachID_ClassNumber,VenueID_CoachID_ClassNumber
                //}
                //else {
                //    var aryInfo = $("#hdKS").val().split(',');
                //    var isExisit = 0;
                //    var temp = "V" + c.VenueID + "_C" + c.CoachID;
                //    for (var i = 0; i < aryInfo.length; i++) {
                //        if (aryInfo[i].indexOf(temp) >= 0) {
                //            isExisit = 1;
                //        }
                //    }
                //    if (isExisit == 0) //不存在加入
                //    {
                //        var v = $("#hdKS").val();
                //        v = v + "," + ksInfo;
                //        $("#hdKS").val(v);
                //    }
                //}
                //str += " <li> ";
                //str += "  <a href='order_detail.html'>";;//格式 VenueID_CoachID_ClassNumber,VenueID_CoachID_ClassNumber

                //str += " <li> ";
                //str += "  <a href='order_detail.html'>";

                //str += "  <p class='time'>" + dateformat(c.CurriculumDate, "yyyy-MM-dd") + "    " + c.CurriculumBeginTime + "-" + c.CurriculumEndTime + "</p>";
                //str += "  <p class='address'><i class='iconfont'>&#xe600;</i>" + c.VenueName + "-" + c.CampusName + "</p>";
                //str += "  <p class='cx'><i class='iconfont'>&#xe612;</i>教练:" + c.CoachFullName + "  <font color='red'>" + strState + "</font></p> </a>";

                //if (iyk == 1) {
                //    str += "  <button type='button'  onclick='Subscribe(" + c.PKID + ",1," + c.VenueID + ","+c.CoachID+",this)' class='order-btn'>约课</button> ";
                //}
                //str += " </li>";

                str += '<li class="am-g">';
                str += ' <img src="http://s.amazeui.org/media/i/demos/bing-1.jpg" alt="" class="am-u-sm-3 am-u-md-3 am-u-lg-3">';
                str += ' <div class="am-u-sm-7 am-u-md-7 am-u-lg-7 ClassD">';
                str += '<p>场馆：<span>' + c.VenueName + "-" + c.CampusName + '</span></p>';
                str += '<p>科目：<span>' + c.Title + '</span></p>';
                str += '<p>教练：<span>' + c.CoachFullName + '</span></p>';
                str += '<p>约课状态：<span>' + strState + '</span></p>';
                str += '<p>上课时间：' + dateformat(c.CurriculumDate, "yyyy-MM-dd") + "    " + c.CurriculumBeginTime + "-" + c.CurriculumEndTime + '</p>';
                str += ' </div>';
                //str += '  <div class="am-u-sm-2 am-u-md-2 am-u-lg-2 ClassD">';
                if (iyk == 1) {
                    //  str += ' <input type="checkbox" data-pkid="' + c.PKID + '" data-vid="' + c.VenueID + '"  data-coachid="'+c.CoachID+'"  >'; //int sid, int pkid, int cid, int vid, string sname
                    str += "  <button type='button' class='am-u-sm-2 am-u-md-2 am-u-lg-2 ClassD am-btn am-btn-success am-btn-xl am-round' onclick='Subscribe(" + c.PKID + ",1," + c.VenueID + "," + c.CoachID + ",this)' class='order-btn'> 约课 </button> ";
                }
                //str += '  </div>';
                str += '  </li>';

            });

            //if (str == "") {
            //    $('#btnMore a').text("没有课程啦");
            //} else { $('#btnMore a').text("更多课程 »"); }
            $("#panel0").append(str);

        }
    });
}

function LoadMyCurriculum2() {


    var CurrentDate = "";

    var myCuDate = new Date();
    CurrentDate = myCuDate.getFullYear() + "-" + (myCuDate.getMonth() + 1) + "-" + myCuDate.getDate();

    var requestPrm = " { PageIndex: " + $("#hdPageIndex2").val() + ", PageSize:" + $("#hdPageSize2").val() + ", RequestType: 1,CurrentDate:'" + CurrentDate + "',PKType:2,SearchCondition:{StudentID: " + $("#hdSID").val() + "}}";
    $.ajax({
        type: "get",
        url: ApiUrl + "/Student/GetTeachingScheduleByStudent/",
        dataType: "json",
        async: false,
        data: { query: requestPrm },
        success: function (data) {

            var str = "";
            var dataCu = data.data;
            //0 排课完成 1学生约课完成，2学校停课（需要判断，有没有学生预约）,3请假申请 4请假 5上课中 6上课完成 
            $.each(dataCu, function (i, c) {

                var KSstate = c.State;
                var PKType = c.PKType;
                var iyk = 0;
                if (KSstate == 0 || (KSstate == 1 && PKType == 2)) {
                    iyk = 1;
                }
                var strState = "可预约";
                if (KSstate > 0) {
                    if (KSstate == 1 && PKType == 1) {
                        strState = "已约完";
                    }
                    else {
                        if (KSstate == 2) {
                            strState = "停课";
                        }
                        else if (KSstate == 3) {
                            strState = "教练请假";
                        }
                        else if (KSstate == 4) {
                            strState = "教练请假";
                        }
                        else {
                            strState = "已过预约时间";
                        }
                    }
                }
                //str += " <li> ";
                //str += "  <a href='order_detail.html'>";;//格式 VenueID_CoachID_ClassNumber,VenueID_CoachID_ClassNumber

                //str += " <li> ";
                //str += "  <a href='order_detail.html'>";

                //str += "  <p class='time'>" + dateformat(c.CurriculumDate, "yyyy-MM-dd") + "    " + c.CurriculumBeginTime + "-" + c.CurriculumEndTime + "</p>";
                //str += "  <p class='address'><i class='iconfont'>&#xe600;</i>" + c.VenueName + "-" + c.CampusName + "</p>";
                //str += "  <p class='cx'><i class='iconfont'>&#xe612;</i>教练:" + c.CoachFullName + "  <font color='red'>" + strState + "</font></p> </a>";

                //if (iyk == 1) {
                //    str += "  <button type='button'  onclick='Subscribe(" + c.PKID + ",1," + c.VenueID + ","+c.CoachID+",this)' class='order-btn'>约课</button> ";
                //}
                //str += " </li>";

                str += '<li class="am-g">';
                str += ' <img src="http://s.amazeui.org/media/i/demos/bing-1.jpg" alt="" class="am-u-sm-3 am-u-md-3 am-u-lg-3">';
                str += ' <div class="am-u-sm-7 am-u-md-7 am-u-lg-7 ClassD">';
                str += '<p>场馆：<span>' + c.VenueName + "-" + c.CampusName + '</span></p>';
                str += '<p>科目：<span>' + c.Title + '</span></p>';
                str += '   <p>教练：<span>' + c.CoachFullName + '</span></p>';
                str += '  <p>约课状态：<span>' + strState + '</span></p>';

                str += '  <p>上课时间：' + dateformat(c.CurriculumDate, "yyyy-MM-dd") + "    " + c.CurriculumBeginTime + "-" + c.CurriculumEndTime + '</p>';
                str += ' </div>';
                str += '  <div class="am-checkbox am-u-sm-1 am-u-md-1 am-u-lg-1 ClassX">';
                if (iyk == 1) {
                    //  str += ' <input type="checkbox" data-pkid="' + c.PKID + '" data-vid="' + c.VenueID + '"  data-coachid="'+c.CoachID+'"  >'; //int sid, int pkid, int cid, int vid, string sname
                    str += "  <button type='button' class='am-btn am-btn-default' onclick='Subscribe(" + c.PKID + ",1," + c.VenueID + "," + c.CoachID + ",this)' class='order-btn'>约课</button> ";
                }
                str += '  </div>';
                str += '  </li>';
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

function nextPagePlus() {

    var iPindex = parseInt($("#hdPageIndex").val()) + 1;
    $("#hdPageIndex").val(iPindex);

}

$(document).ready(function () {
    LoadMyCurriculum();

    LoadMyCurriculum2();
});

$('#btnMore a').bind('click', function () {
    //hdPageIndex
    var iPindex = parseInt($("#hdPageIndex").val()) + 1;
    $("#hdPageIndex").val(iPindex);
    LoadMyCurriculum();
    //hdPageSize
});

function Subscribe(pkid, sid, VenueID, cid, obj) {

    var FullName = $("#hdSName").val();
    var sid = $("#hdSID").val();

    $.ajax({
        type: "Post",
        url: ApiUrl + "/Student/SubscribeCurriculum/",
        contentType: "application/json",
        //data: { sid: sid, pkid: pkid, cid: cid, vid: VenueID, sname: FullName },
        data: JSON.stringify({ PKID: pkid, VenueID: VenueID, SID: sid, CID: cid, SName: FullName }),
        success: function (data, status) {

            if (status == "success") {

                if (!data.Error) {

                  
                    $(obj).attr("disabled", "disabled");
                    $(obj).css({ 'background-color': '#C0C0C0' });
                    LoadMyCurriculum();
                    LoadMyCurriculum2();
                    alert("约课成功，快去我的课程里查看吧");
                   // $(obj).text("预约成功");
                   
                }
                else {
                    alert("约课失败，刷新下课时列表再重新约下吧");
                }
            }
            else {
                alert("约课失败，刷新下课时列表再重新约下吧");
            }
        },
        error: function (e) {
            alert("约课失败，刷新下课时列表再重新约下吧!" + e.toString());
            console.log('error');
        },
        complete: function () {

        }
    });
}

