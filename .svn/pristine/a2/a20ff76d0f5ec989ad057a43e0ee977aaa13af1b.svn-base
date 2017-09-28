
jQuery.support.cors = true;
function LoadClassHoursDetailed() {


    var CurrentDate = "";
    
        var myCuDate = new Date();
        CurrentDate = myCuDate.getFullYear() + "-" + (myCuDate.getMonth() + 1) + "-" + myCuDate.getDate();
   $.ajax({
        type: "get",
        url: "http://localhost:53262/api/Student/GetClassHoursDetailedByStudentID/",
        dataType: "json",
        async: false,
        data: { StudentID: $("#hdStudentID").val(),PageIndex:$("#hdPageIndex").val(),PageSize: $("#hdPageSize").val()  },
        beforeSend: function () {
        },
        success: function (data) {

            var str = "";
            var dataCu = data.data;          
            //1购买，2约课3学生请假退回，4老师请假退回，5学校停课退回
            $.each(dataCu, function (i, c) {
            
                var Dtype = c.DType;
                var strState = "";
                if(Dtype==1)
                {
                    strState = "购买课时";
                }
                else if (Dtype == 2) {
                    strState = "约课";
                }
                else if (Dtype == 3) {
                    strState = "退回(请假)";
                }
                else if (Dtype == 4) {
                    strState = "退回(教练请假)";
                }
                else if (Dtype == 5) {
                    strState = "退回(停课)";
                }
                str += " <li> ";
                str += "  <a href='order_detail.html'>";
                str += "  <p class='cx'><font color='red'>" + strState + "</font></p>";
                str += "  <p class='time'>" + dateformat(c.AddTime, "yyyy-MM-dd HH:mm:ss")+"</p>";
                str += "  <p class='address'><i class='iconfont'>&#xe600;</i>" + c.VenueName + "-" + c.FullName + "</p>";
                str += "  <p class='cx'><i class='iconfont'>&#xe612;</i>课时数：<font color='red'>" + c.DNumber + "</font>        金额：<font color='red'>" + c.CMoney + "</font> </p> </a>";
              
                str += " </li>";

            });
           
            if (str == "") {
                $('#btnMore a').text("没有课程啦");
            } else { $('#btnMore a').text("更多课程 »"); }
            $("#list").append(str);

        }
    });
}

function dateformat(date)
{
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
   
    LoadClassHoursDetailed();
   
    
});



$('#btnMore a').bind('click', function () {
    //hdPageIndex
    var iPindex = parseInt($("#hdPageIndex").val()) + 1;
    $("#hdPageIndex").val(iPindex);
    LoadClassHoursDetailed();
    //hdPageSize
});

