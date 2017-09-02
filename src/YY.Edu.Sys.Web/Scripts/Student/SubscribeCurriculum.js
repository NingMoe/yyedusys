
jQuery.support.cors = true;
function LoadMyCurriculum() {

  
    var CurrentDate = "";

    var myCuDate = new Date();
    CurrentDate = myCuDate.getFullYear() + "-" + (myCuDate.getMonth() + 1) + "-" + myCuDate.getDate();

    var requestPrm = " { PageIndex: " + $("#hdPageIndex").val() + ", PageSize:" + $("#hdPageSize").val() + ", RequestType: 1,CurrentDate:'" + CurrentDate + "',SearchCondition:{StudentID: 1}}";
    $.ajax({
        type: "get",
        url: "http://localhost:53262/api/Student/GetTeachingScheduleByStudent/",
        dataType: "json",
        async: false,
        data: { query: requestPrm },
        beforeSend: function () {
        },
        success: function (data) {
           
             var str = "";
            var dataCu = data.data;
            //0 排课完成 1学生约课完成，2学校停课（需要判断，有没有学生预约）,3请假申请 4请假 5上课中 6上课完成 
            $.each(dataCu, function (i, c) {

                var KSstate = c.State;
                var PKType = c.PKType;
                var iyk = 0;
                if (KSstate == 0 || (KSstate == 1 && PKType == 2))
                {
                    iyk = 1;
                }
                var strState = "约课中";
                if (KSstate > 0) {
                    if (KSstate == 1 && PKType == 1) {
                        strState = "已被约完";
                    }
                    else
                    {
                        if (KSstate == 2)
                        {
                            strState = "停课";
                        }
                        else if (KSstate == 3)
                        {
                            strState = "教练请假";
                        }
                        else if (KSstate == 4)
                        {
                            strState = "教练请假";
                        }
                        else
                        {
                            strState = "已过预约时间";
                        }
                    }
                }

                var ksInfo="V"+c.VenueID+"_C"+c.CoachID+"_"+c.ClassNumber;//,VenueID_CoachID_ClassNumber
                if($("#hdKS").val()=="")
                {
                    $("#hdKS").val(ksInfo); //VenueID_CoachID_ClassNumber,VenueID_CoachID_ClassNumber
                }
                else
                {
                    var aryInfo= $("#hdKS").val().split(',');
                    var isExisit=0;
                    var temp="V"+c.VenueID+"_C"+c.CoachID;
                    for(var i=0;i<aryInfo.length;i++)
                    {
                        if(aryInfo[i].indexOf(temp)>=0)
                        {
                            isExisit=1;
                        }
                    }
                    if(isExisit==0) //不存在加入
                    {
                        var v=$("#hdKS").val();
                        v=v+","+ksInfo;
                        $("#hdKS").val(v);
                    }
                }
                str += " <li> ";
                str += "  <a href='order_detail.html'>";;//格式 VenueID_CoachID_ClassNumber,VenueID_CoachID_ClassNumber

                str += " <li> ";
                str += "  <a href='order_detail.html'>";

                str += "  <p class='time'>" + dateformat(c.CurriculumDate, "yyyy-MM-dd") + "    " + c.CurriculumBeginTime + "-" + c.CurriculumEndTime + "</p>";
                str += "  <p class='address'><i class='iconfont'>&#xe600;</i>" + c.VenueName + "-" + c.CampusName + "</p>";
                str += "  <p class='cx'><i class='iconfont'>&#xe612;</i>教练:" + c.CoachFullName + "  <font color='red'>" + strState + "</font></p> </a>";

                if (iyk == 1) {
                    str += "  <button type='button'  onclick='Subscribe(" + c.PKID + ",1," + c.VenueID + ","+c.CoachID+",this)' class='order-btn'>约课</button> ";
                }
                str += " </li>";

            });

            if (str == "") {
                $('#btnMore a').text("没有课程啦");
            } else { $('#btnMore a').text("更多课程 »"); }
            $("#list").append(str);

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



function Subscribe(pkid, sid, VenueID, cid,obj) {
  
    var FullName = $("#hdFullName").val();   
    
    $.ajax({
        type: "Get",
        url: "http://localhost:53262/api/Student/SubscribeCurriculum/",
        contentType: "application/json",
        data: { sid: sid, pkid: pkid, cid: cid, vid: VenueID, sname: FullName },
        success: function (data, status) {
       
            if (status == "success") {

                if (data.Code == 1001) {

                    alert("约课成功，快去我的课程里查看吧");

                    $(obj).text("预约成功");
                    $(obj).attr("disabled", "disabled");
                    $(obj).css({ 'background-color': '#C0C0C0' });
                }
                else
                {
                    alert("约课失败，刷新下课时列表再重新约下吧");
                }
              
            }
            else {
                alert("约课失败，刷新下课时列表再重新约下吧");
            }
        },
        error: function (e) {
            alert("约课失败，刷新下课时列表再重新约下吧!"+e.toString());
            console.log('error');
        },
        complete: function () {

        }
    });
}