﻿


function LoadPJInfo() {
    var str = "";
    $.ajax({
        type: "get",
        url: ApiUrl+"/Coach/GetCurriculumEvaluateByPKID/",
        dataType: "json",
        async: false,
        data: { pkid: $("#hdPKID").val() },
        //beforeSend: function () {
        //},
        success: function (data) {
            var Zcount = 0;
            var JsCount = 0;
            var XGCount = 0;
            $.each(data, function (i, c) {
                Zcount = Zcount + 1;
                JsCount = JsCount + c.StarLevel;
                XGCount = XGCount + c.EffectStarLevel;
            });

            var ZStarCount = Zcount * 5; //总星数
            var JsStarCount = (JsCount / ZStarCount).toFixed(2); //平均体验星数
            var XGStarCount = (XGCount / ZStarCount).toFixed(2); //平均效果星数



            var cuJSNum = 1;
            var cXGSNum = 1;
            if (JsStarCount >= 0.2 && JsStarCount < 0.4) {
                cuJSNum = 2;
            }
            else if (JsStarCount >= 0.4 && JsStarCount < 0.6) {
                cuJSNum = 3;
            }
            else if (JsStarCount >= 0.6 && JsStarCount < 1) {
                cuJSNum = 4;
            }
            else if (JsStarCount == 1) {
                cuJSNum = 5;
            }


            if (XGStarCount >= 0.2 && XGStarCount < 0.4) {
                cXGSNum = 2;
            }
            else if (XGStarCount >= 0.4 && XGStarCount < 0.6) {
                cXGSNum = 3;
            }
            else if (XGStarCount >= 0.6 && XGStarCount < 1) {
                cXGSNum = 4;
            }
            else if (XGStarCount == 1) {
                cXGSNum = 5;
            }



            var TY = "";
            for (var i = 1 ; i < 6; i++) {
                if (cuJSNum >= i) {
                    TY = TY + '<span class="am-icon-star"></span>';
                }
                else { TY = TY + '<span class="am-icon-star-o"></span>'; }
            }
            $("#pTY").html(TY);

            var XG = "";
            for (var j = 1 ; j < 6; j++) {
                if (cXGSNum >= j) {
                    XG = XG + '<span class="am-icon-star"></span>';
                }
                else { XG = XG + '<span class="am-icon-star-o"></span>'; }
            }
            $("#pXG").html(XG);


        }
    });
}

$("#pQJ").on("click", function () {
    var State = parseInt($("#hdState").val());
    //0 排课完成 1学生约课完成，2学校停课（需要判断，有没有学生预约）,3请假申请 4请假 5上课中 6上课完成 7发放工资完成  8学生请假
    if (State == 1) {
        $('#my-confirm').modal();
        return true;
    }
    else {
        var strInfo = "";

        switch (State) {
            case 0:
                strInfo = "排课完成";
                break;
            case 2:
                strInfo = "学校停课";
                break;
            case 3:
                strInfo = "请假申请";
                break;
            case 4:
                strInfo = "请假";
                break;
            case 5:
                strInfo = "上课中";
                break;
            case 6:
                strInfo = "上课完成";
                break;
            case 7:
                strInfo = "发放工资完成";
                break;
            case 8:
                strInfo = "学生请假";
                break;
        }
        if (State == 8)
        {
            alert("学生请假了，快联系下场馆核实下吧");
        }
        else { alert("只有【学生约课成功】的状态才可以提交请假申请，当前课时的状态是【" + strInfo + "】"); }
        return false;
    }
});

$("#pQD").on("click", function () {
    //0 排课完成 1学生约课完成，2学校停课（需要判断，有没有学生预约）,3请假申请 4请假 5上课中 6上课完成 7发放工资完成  8学生请假
    var State = $("#hdState").val();
    if (State == 1 || State ==5) {
        $('#my-confirm2').modal();
        return true;
    }
    else {
        alert("只有待上课或上课中的的课时才可以签到学生");
        return false;
    }
});

function LoadInfo() {
    
    var str = "";
    $.ajax({
        type: "get",
        url: ApiUrl+"/Coach/GetCoachCurriculumByID/",
        dataType: "json",
        async: false,
        data: { PKID:$("#hdPKID").val() },
        //beforeSend: function () {
        //},
        success: function (data) {         
            var c = data[0];

            $("#aTitle").text(c.Title);
            $("#sVenue").text(c.VenueName);
            $("#Cdate").text(c.CurriculumDate.substring(0,10)+"  ");
            $("#CTime").text(c.CurriculumBeginTime + "-" + c.CurriculumEndTime);
            $("#SJL").text(c.CoachFullName);
            $("#Scount").text(c.Sucount);
            $("#sqjcount").text(c.sqjcount);

            $("#hdState").val(c.State);
            var ja = c.PlanContent;
            var strJA = "";
          
            if (ja==null||ja=='null'||ja == "")
            {
                strJA+='<div class="_CD_NOTsc">';
                strJA+=' <div class="_CD_jiaoan textAlign">';              
                strJA += '    <textarea class="" rows="2"  id="txtJA" style="width:90%;height:100px"></textarea>';
                strJA+='  </div>';
                strJA += '  <p class="_CD_sc textAlign" onclick="ckJA()">写教案</p>';
                strJA+='   </div> ';
            }
            else
            {
                strJA += '  <div class="_CD_ALLSC">';
                         
                strJA += ' <ul>';
                                
                strJA += '<li>';                                  
                strJA += '<p>'+ja+'</p>';
                strJA += '</li>';
                strJA += '</ul>';             
                strJA += '</div>';

            }
            $("#JA").html(strJA);

            var zj = c.SContent;
            var strZJ = "";
            if (zj == null || zj=="null"||zj == "") {
                strZJ += '<div class="_CD_NOTsc">';
                strZJ += ' <div class="_CD_jiaoan textAlign">';
                strZJ += ' <textarea class="" rows="2"  id="txtZJ" style="width:90%;height:100px"></textarea>';

                strZJ += '  </div>';
                strZJ += '  <p class="_CD_sc textAlign" onclick="ckZJ()">写总结</p>';
                strZJ += '   </div> ';           

            }
            else {
                strZJ += '  <div class="_CD_ALLSC">';

                strZJ += ' <ul>';

                strZJ += '<li>';
                strZJ += '<p>' + zj + '</p>';
                strZJ += '</li>';
                strZJ += '</ul>';
                strZJ += '</div>';

            }
            $("#ZJ").html(strZJ);
            
        }
    });


}


$("#btnSign").on('click', function () {

    var StudentIDs = "";
    $("#ulStudent :checkbox").each(function () {
        if ($(this).is(":checked"))
        {
            StudentIDs = StudentIDs + $(this).attr("data-id") + ",";
        }
    })

    if (StudentIDs == "")
    {
        alert("请选择签到的学生");
        return false;
    }

    StudentIDs = StudentIDs.substring(0, StudentIDs.length - 1);

    $.ajax({
        type: "get",
        url: ApiUrl+"/Coach/UpdateCurriculumState/",
        dataType: "json",
        data: {State:7, StudentIDs:StudentIDs , pkid:$("#hdPKID").val(), VenueID:0, CoachID:0},
        success: function (data, status) {  
            if (status == "success") {
                if (data.Code == 1001) {
                    alert('签到提交成功，快上课吧');
                    $("#my-confirm2").modal('close');
                }
                else { alert('签到提交失败，再来一次吧1'); }
            }
        },
        error: function (e) {
            alert('签到提交失败，再来一次吧!');
        },
        complete: function () {

        }
    });

});


//教案
function ckJA()
{
    var parm = { PKID: $("#hdPKID").val(), PlanContent: $("#txtJA").val() };
    $.ajax({
        type: "POST",
        url: ApiUrl+"/Coach/AddCurriculumTeachingPlan/",
        contentType: "application/json",
        data: JSON.stringify(parm),
        success: function (data, status) {
            if (status == "success") {
                if (data.Code == 1001) {
                    alert('添加成功');
                    LoadInfo();
                }
                else { alert('添加失败，再来一次吧'); }
            }
        },
        error: function (e) {
            alert('添加出错了，再来一次吧'+e);
        },
        complete: function () {

        }
    });
}

//总结
function ckZJ() {
  
    var parm = { PKID: $("#hdPKID").val(), SContent: $("#txtZJ").val() };
    $.ajax({
        type: "POST",
        url: ApiUrl+"/Coach/AddCurriculumSummary/",
        contentType: "application/json",
        data: JSON.stringify(parm),
        success: function (data, status) {
            if (status == "success") {
                if (data.Code == 1001) {
                    alert('添加成功');
                    LoadInfo();
                }
                else { alert('添加失败，再来一次吧'); }
            }
        },
        error: function (e) {
            alert('添加出错了，再来一次吧');
        },
        complete: function () {

        }
    });
}

//请假
$("#btnConfirm").on("click", function () {
    $.ajax({
        type: "get",
        url: ApiUrl+"/Coach/ApplyLeaveByPKID/",
        contentType: "application/json",
        data: { PKID: $("#hdPKID").val(), State: 3 },
        success: function (data, status) {
            if (status == "success") {
                if (data.Code == 1001) {
                    alert('提交请假申请成功');
                }
                else { alert('请核实下课程的状态吧'); }
            }
            else { alert('请核实下课程的状态吧.'); }
        },
        error: function (e) {
            alert('提交请假申请失败，再来一次吧');
        },
        complete: function () {

        }
    });

});


//取的学生列表
function GetStudent() {
    $.ajax({
        type: "get",
        url: ApiUrl+"/Coach/GetCurriculumStudentByPKID/",
        dataType: "json",
        data: { pkid: $("#hdPKID").val() },
        success: function (data) {
            var str = '';
            $.each(data, function (i, c) {
                var CStete = c.CState;//0预约成功，1上课成功，2学生请假，3老师请假4场馆停课 5发放工资完成6,申请请假            
                var strState = "---";
                if (CStete == 0) {
                    strState = "未签到";
                }
               else if(CStete==1||CStete==7)
                {                  
                    strState = "已签到";
                }
                else if (CStete == 2)
                {
                    strState = "请假";
                }
                else if (CStete == 6) {
                    strState = "申请请假";
                }

                str += '  <li class="am-g"  style="margin-left:0;margin-right:0">';
            //    str += ' <hr data-am-widget="divider" style="" class="am-divider am-divider-dashed" />';
                str += '<div class="am-u-sm-5 am-u-md-5 am-u-lg-5"> ';
             
               // str += '	<img src="' + c.HeadUrl + '" alt="" class="le am-circle"> ';
                str += '	<p class="_CDC_name le">' + c.FullName + '</p> ';
                str += '	</div> ';


                str += '	<div class="am-u-sm-2 am-u-md-2 am-u-lg-2 _CDC_phone"> ';
                //    str += '<div class="am-checkbox"> ';
                if (CStete == 0) {
                    str += ' <label class="am-checkbox"><input type="checkbox" data-id="' + c.StudentID + '" data-cid="' + c.CurriculumID + '"> </lable>';
                }
                else
                {
                    str += ' <label class="am-checkbox"><input style="margin:0.7rem 7rem 0 0!important"  disabled type="checkbox" data-id="' + c.StudentID + '" data-cid="' + c.CurriculumID + '"> </lable>';
                }
             //   str += '</div> ';
                str += '</div>';
                str += '<div class="am-u-sm-5 am-u-md-5 am-u-lg-5"> ';

                // str += '	<img src="' + c.HeadUrl + '" alt="" class="le am-circle"> ';
                str += '	<p class="_CDC_name le" id="cpqd"><font color="red">' + strState + '</font></p> ';
                str += '	</div> ';
                str+='<hr data-am-widget="divider" style="" class="am-divider am-divider-dashed" /></li> ';

              
            });
        
            $("#ulStudent").html(str);
        },
        error: function (e) {
            alert('获取数据异常');
        },
        complete: function () {

        }
    });
}

//签到
function StudentSign()
{

}

//点评
$("#btnCom").on("click", function () {
    var State = $("#hdState").val();
    if (State == 6 || State == 7) {
        location.href = "../MyComment/?pkid=" + $("#hdPKID").val() + "&cid=0";
        return true;
    }
    else {
        alert("只有【上课完成】的课时才可以点评学生");
        return false;
    }

   
});