
jQuery.support.cors = true;
function LoadClassHoursDetailed() {


    var CurrentDate = "";
    
        var myCuDate = new Date();
        CurrentDate = myCuDate.getFullYear() + "-" + (myCuDate.getMonth() + 1) + "-" + myCuDate.getDate();
   $.ajax({
        type: "get",
        url: ApiUrl + "/Student/GetClassHoursDetailedByStudentID/",
        dataType: "json",
        async: false,
        data: { StudentID: $("#hdStudentID").val(),PageIndex:$("#hdPageIndex").val(),PageSize: $("#hdPageSize").val()  },
        //beforeSend: function () {
        //},
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


                str +=' <li>';
                str +='<div class="am-g">';
                str += '   <p class="_PaymentName am-u-sm-9 am-u-md-9 am-u-lg-9">' + c.VenueName + "-" + c.FullName + '</p>';
                str += '  <span class="am-u-sm-3 am-u-md-3 am-u-lg-3 _payBiao">' + strState + '</span>';
                str +=' </div>';
                str += ' <div class="am-g"><p class="am-u-sm-6 am-u-md-6 am-u-lg-6">课时数：<span>' + c.DNumber + '次</span></p><p class="am-u-sm-6 am-u-md-6 am-u-lg-6">金额：'+ c.CMoney +'<span></span></p></div>';
                str += ' <div class="am-g"><p class="am-u-sm-6 am-u-md-6 am-u-lg-6">时间：<span>' + dateformat(c.AddTime, "yyyy-MM-dd HH:mm:ss") + '</span></p></div>';
                str +='</li>';

              

            });
           
            //if (str == "") {
            //    $('#btnMore a').text("没有课程啦");
            //} else { $('#btnMore a').text("更多课程 »"); }
           
            $("._Payment").append(str);

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

