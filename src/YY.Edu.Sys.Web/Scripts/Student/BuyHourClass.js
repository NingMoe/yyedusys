
jQuery.support.cors = true;
function LoadCoach() {


    var CurrentDate = "";
    
      
    $.ajax({
        type: "get",
        url: "http://localhost:53262/api/Coach/GetCoachListByHourClass/",
        dataType: "json",
        async: false,
        data: { StudentID: $("#hdStudentID").val(), VenueID: $("#hdVenueID").val(),PKType:1 },
        beforeSend: function () {
        },
        success: function (data) {

            var str = '<ul class="choseList">';
            var dataCu = data;
         
            //取得教练
            $.each(dataCu, function (i, c) {
                str += ' <li class="clear am-g">';             

                str += '<img src="http://s.amazeui.org/media/i/demos/bing-1.jpg" alt="" class=" am-thumbnail le am-u-sm-3 am-u-md-3 am-u-lg-3">';
                str += ' <div class="am-u-sm-9 am-u-md-9 am-u-lg-9 ri">';
                str += '<div class="CouListT">';
                str += ' <p class="courseName">'+c.VenueName+'</p>';
                str += ' <p class="courseTea">教练：<span>'+c.FullName+'</span></p>';
                str += ' <p class="courseTime">我的剩余课时：<span>' + hNumber + '次</span></p>';
                str += ' <p class="coursePay">收费：<span>'+c.Price+'/课时</span></p>';
              
                str+='</div>';
                str+='  <div class="clear am-g CouListB">';
                str+='<a href="" class="le textAlign am-u-sm-4 am-u-md-4 am-u-lg-4" onclick="MyInfo('+ c.CoachID + ','+c.VenueID+')">了解教练</a>';
                str+=' <a href="" class="ri textAlign am-u-sm-4 am-u-md-4 am-u-lg-4" onclick="Buy('+ c.CoachID + ',1,this,'+c.Price+','+c.VenueID+',1)">购买</a>';
                str += '</div> </div> </li>';          

                //str += " <li data-v='ab'> ";
                //str += "  <a href='order_detail.html'>";
               
                //str += "  <p class='time'>" + c.FullName + "    </p>";
                //str += "  <p class='address'><i class='iconfont'>&#xe600;</i>" + c.Introduce +"</p>";
                //str += "  <p class='cx'><i class='iconfont'>&#xe612;</i>剩余课时:  <font color='red'>" + c.hNumber + "</font></p> </a>";
                //str += "  <p class='cx1'><i class='iconfont'>&#xe612;</i>购买课时:<input type='text' value='7' id='txtNumber' style='border:1;color:#000000' class='Number'></p>";
             
                //    str += "  <button type='button' data-CurriculumID='" + c.CurriculumID + "' data-PKID='" + c.PKID + "' onclick='Buy(" + c.CoachID + ",1,this,"+c.Price+")' class='order-btn'>购买</button> ";
            
                //str += " </li>";

            });
           
            //if (str == "") {
            //    $('#btnMore a').text("场馆正在入住教练");
            //}
            str += "</ul>";
            $("#panel0").html(str);

        }
    });
}

//1对多
function LoadCoachMore() {


    var CurrentDate = "";


    $.ajax({
        type: "get",
        url: "http://localhost:53262/api/Coach/GetCoachListByHourClass/",
        dataType: "json",
        async: false,
        data: { StudentID: $("#hdStudentID").val(), VenueID: $("#hdVenueID").val(), PKType: 2},
        beforeSend: function () {
        },
        success: function (data) {

            var str = '<ul class="choseList">';
            var dataCu = data;
          
            //取得教练
            $.each(dataCu, function (i, c) {
                str += ' <li class="clear am-g">';

                str += '<img src="http://s.amazeui.org/media/i/demos/bing-1.jpg" alt="" class=" am-thumbnail le am-u-sm-3 am-u-md-3 am-u-lg-3">';
                str += ' <div class="am-u-sm-9 am-u-md-9 am-u-lg-9 ri">';
                str += '<div class="CouListT">';
                str += ' <p class="courseName">' + c.VenueName + '</p>';
                str += ' <p class="courseTea">教练：<span>' + c.FullName + '</span></p>';
                str += ' <p class="courseTime">我的剩余课时：<span>' + hNumber + '次</span></p>';
                str += ' <p class="coursePay">收费：<span>' + c.PriceMore + '/课时</span></p>';

                str += '</div>';
                str += '  <div class="clear am-g CouListB">';
                str += '<a href="" class="le textAlign am-u-sm-4 am-u-md-4 am-u-lg-4" onclick="MyInfo(' + c.CoachID + ',' + c.VenueID + ')">了解教练</a>';
                str += ' <a href="" class="ri textAlign am-u-sm-4 am-u-md-4 am-u-lg-4" onclick="Buy(' + c.CoachID + ',1,this,' + c.PriceMore + ',' + c.VenueID + ',2)">购买</a>';
                str += '</div> </div> </li>';

                //str += " <li data-v='ab'> ";
                //str += "  <a href='order_detail.html'>";

                //str += "  <p class='time'>" + c.FullName + "    </p>";
                //str += "  <p class='address'><i class='iconfont'>&#xe600;</i>" + c.Introduce +"</p>";
                //str += "  <p class='cx'><i class='iconfont'>&#xe612;</i>剩余课时:  <font color='red'>" + c.hNumber + "</font></p> </a>";
                //str += "  <p class='cx1'><i class='iconfont'>&#xe612;</i>购买课时:<input type='text' value='7' id='txtNumber' style='border:1;color:#000000' class='Number'></p>";

                //    str += "  <button type='button' data-CurriculumID='" + c.CurriculumID + "' data-PKID='" + c.PKID + "' onclick='Buy(" + c.CoachID + ",1,this,"+c.Price+")' class='order-btn'>购买</button> ";

                //str += " </li>";

            });

            //if (str == "") {
            //    $('#btnMore a').text("场馆正在入住教练");
            //}
            str += "</ul>";
            $("#panel1").html(str);

        }
    });
}


$(document).ready(function () {
   
    LoadCoach();
    LoadCoachMore();
    
});


function Buy(coachid, sid1, obj,price,vid,pktype) {
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
        alert('请输入要购买的课时数');
        return false;
    }
    CMoney = price * inumber;
    // BuyCurriculum(int StudentID,int CoachID,int number)


    $.ajax({
        type: "get",
        url: "http://localhost:53262/api/Student/ApplyBuy/",//BuyCurriculum(int StudentID,int CoachID,int number)
        dataType: "json",
        async: false,
        data: { StudentID: sid, CoachID: coachid, ClassNumber: inumber, VenueID: vid, PayMoney: CMoney, Status: 0, PKType: pktype, PaidMoney:CMoney },
        beforeSend: function () {
        },
        success: function (data, status) {
            if (status == "success") {
                if (data.Code == 1001) {
                    alert("申请购买课时提交成功，快去支付吧");
                    console.log('ok');
                }
                else {
                    alert("申请购买课时失败，再操作一次吧");
                }
            }
            else {
                alert("申请购买课时失败，再操作一次吧");
            }


        }
    });

}


function MyInfo(coachID,venueID)
{
    location.href = "coach/myInfo/?CoachID=" + coachID + "&VenueID="+venueID+")";
}