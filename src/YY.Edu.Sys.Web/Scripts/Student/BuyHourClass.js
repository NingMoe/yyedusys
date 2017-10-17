﻿
jQuery.support.cors = true;
function LoadCoach() {

    var CurrentDate = "";
    
    $.ajax({
        type: "get",
        url: ApiUrl + "/Coach/GetCoachListByHourClass/",
        dataType: "json",
        async: false,
        data: { StudentID: $("#hdStudentID").val(), VenueID: $("#hdVenueID").val(),PKType:1 },
        success: function (data) {

            var str = '<ul class="choseList">';
            var str = '';
            var dataCu = data;
            $("#more").show();

            //取得教练
            $.each(dataCu, function (i, c) {
                //str += ' <li class="clear am-g">';             
                //str += '<img src="http://s.amazeui.org/media/i/demos/bing-1.jpg" alt="" class=" am-thumbnail le am-u-sm-3 am-u-md-3 am-u-lg-3">';
                //str += ' <div class="am-u-sm-9 am-u-md-9 am-u-lg-9 ri">';
                //str += '<div class="CouListT">';
                //str += ' <p class="courseName">'+c.VenueName+'</p>';
                //str += ' <p class="courseTea">教练：<span>'+c.FullName+'</span></p>';
                //str += ' <p class="courseTime">我的剩余课时：<span>' + c.hNumber + '次</span></p>';
                //str += ' <p class="coursePay">收费：<span>'+c.Price+'/课时</span></p>';
                //str += ' <p class="coursePay">购买时数：<input type="text" value="7" id="txtNumber" style="border:1;color:#000000" class="Number"></p>';
                //str+='</div>';
                //str+='  <div class="clear am-g CouListB">';
                //str+='<a href="#" class="le textAlign am-u-sm-4 am-u-md-4 am-u-lg-4" onclick="MyInfo('+ c.CoachID + ','+c.VenueID+')">了解教练</a>';
                //str+=' <a href="#" class="ri textAlign am-u-sm-4 am-u-md-4 am-u-lg-4" onclick="Buy('+ c.CoachID + ',1,this,'+c.Price+','+c.VenueID+',1,\''+c.FullName+'\')">购买</a>';
                //str += '</div> </div> </li>';          

                str += ' <li >';             
                str += "<a href='javascript:void(0);'> ";
                str += ' <p class="time" onclick="MyInfo(' + c.CoachID + ',' + c.VenueID + ')"><i class="iconfont">&#xe612;</i>' + c.VenueName + ' <span>' + c.FullName + '</span></p>';
                str += ' <p class="address"><i class="iconfont">&#xe605</i>剩余课时：<span>' + c.hNumber + '次</span></p>';
                str += ' <p class="cx"><i class="iconfont">&#xe612;</i>收费：<span>' + c.Price + '/课时</span></p>';
                str += '<p class="cx"><i class="iconfont">&#xe612;</i>购买时数：<input type="text" name="mobile" placeholder="请输入购买数" value="7" id="txtNumber" class=".am-input-group-sm buycount mt2" required></p>';
                str += '  </a>';
                //str += '  <button type="button" class="order-btn" onclick="MyInfo(' + c.CoachID + ',' + c.VenueID + ')">了解教练</button>';
                str += '  <button type="button" class="order-btn" onclick="Buy(' + c.CoachID + ',1,this,' + c.Price + ',' + c.VenueID + ',1,\'' + c.FullName + '\')">购买</button>';
                str += '</li>';          

            });
           
            $("#panel0").html(str);
        }
    });
}

//1对多
function LoadCoachMore() {

    var CurrentDate = "";

    $.ajax({
        type: "get",
        url: ApiUrl + "/Coach/GetCoachListByHourClass/",
        dataType: "json",
        async: false,
        data: { StudentID: $("#hdStudentID").val(), VenueID: $("#hdVenueID").val(), PKType: 2},
        success: function (data) {

            var str = '<ul class="choseList">';
            var dataCu = data;
            $("#more02").show();

            //取得教练
            $.each(dataCu, function (i, c) {
                //str += ' <li class="clear am-g">';
                //str += '<img src="http://s.amazeui.org/media/i/demos/bing-1.jpg" alt="" class=" am-thumbnail le am-u-sm-3 am-u-md-3 am-u-lg-3">';
                //str += ' <div class="am-u-sm-9 am-u-md-9 am-u-lg-9 ri">';
                //str += '<div class="CouListT">';
                //str += ' <p class="courseName">' + c.VenueName + '</p>';
                //str += ' <p class="courseTea">教练：<span>' + c.FullName + '</span></p>';
                //str += ' <p class="courseTime">我的剩余课时：<span>' + c.hNumber + '次</span></p>';
                //str += ' <p class="coursePay">收费：<span>' + c.PriceMore + '/课时</span></p>';
                //str += ' <p class="coursePay">购买时数：<input type="text" value="7" id="txtNumber" style="border:1;color:#000000" class="Number"></p>';
                //str += '</div>';
                //str += '  <div class="clear am-g CouListB">';
                //str += '<a href="#" class="le textAlign am-u-sm-5 am-u-md-5 am-u-lg-5" onclick="MyInfo(' + c.CoachID + ',' + c.VenueID + ')">了解教练</a>';
                //str += ' <a href="#" class="ri textAlign am-u-sm-4 am-u-md-4 am-u-lg-4" onclick="Buy(' + c.CoachID + ',1,this,' + c.PriceMore + ',' + c.VenueID + ',2,\'' + c.FullName + '\')">购买</a>';
                //str += '</div> </div> </li>';

                str += ' <li >';
                str += "<a href='javascript:void(0);'> ";
                str += ' <p class="time" onclick="MyInfo(' + c.CoachID + ',' + c.VenueID + ')"><i class="iconfont">&#xe612;</i>' + c.VenueName + ' <span>' + c.FullName + '</span></p>';
                str += ' <p class="address"><i class="iconfont">&#xe605</i>剩余课时：<span>' + c.hNumber + '次</span></p>';
                str += ' <p class="cx"><i class="iconfont">&#xe612;</i>收费：<span>' + c.PriceMore + '/课时</span></p>';
                str += '<p class="cx"><i class="iconfont">&#xe612;</i>购买时数：<input type="text" name="mobile" placeholder="请输入购买数" value="7" id="txtNumber" class=".am-input-group-sm buycount mt2" required></p>';
                str += '  </a>';
                //str += '  <button type="button" class="order-btn" onclick="MyInfo(' + c.CoachID + ',' + c.VenueID + ')">了解教练</button>';
                str += '  <button type="button" class="order-btn" onclick="Buy(' + c.CoachID + ',1,this,' + c.PriceMore + ',' + c.VenueID + ',2,\'' + c.FullName + '\')">购买</button>';
                str += '</li>';

            });

            $("#panel1").html(str);
        }
    });
}

function Buy(coachid, sid1, obj, price, vid, pktype,coachFullName) {

    var sid = $("#hdStudentID").val();

    //金额
    var CMoney=0;
    var oj = $(obj).parent().find("input");

    var inumber = 0;
    if (oj.val() != "") {
        inumber = oj.val();
    }  
    if (inumber == 0)
    {
        model_alert('请输入要购买的课时数');
        return false;
    }
    CMoney = price * inumber;
    // BuyCurriculum(int StudentID,int CoachID,int number)

    var parm = { StudentID: sid, CoachID: coachid, ClassNumber: inumber, VenueID: vid, PayMoney: CMoney, Status: 0, PKType: pktype, PaidMoney: CMoney, StudentFullName: $("#hdFullName").val(), CoachFullName: coachFullName };
    $.ajax({
        type: "POST",
        url: ApiUrl + "/Student/ApplyBuy",//BuyCurriculum(int StudentID,int CoachID,int number)
        contentType: "application/json",
        data: JSON.stringify(parm),
        success: function (data, status) {       
            if (status == "success") {
                if (data.Code == 1001) {
                    model_alert("申请购买课时提交成功，快去支付吧");
                }
                else {
                    model_alert("申请购买课时失败，再操作一次吧");
                }
            }
            else {
                model_alert("申请购买课时失败，再操作一次吧");
            }
        }
    });
}

function MyInfo(coachID,venueID)
{
    location.href = "/coash/CoachInfo/?coachid=" + coachID + "&venueid=" + venueID;
}

