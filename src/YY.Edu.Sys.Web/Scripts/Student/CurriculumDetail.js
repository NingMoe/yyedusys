﻿


function LoadPJInfo() {
    var str = "";
    $.ajax({
        type: "get",
        url: ApiUrl + "/Coach/GetCurriculumEvaluateByPKID/",
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



            var cuJSNum = 0;
            var cXGSNum =0;
            if (JsStarCount >= 0.2 && JsStarCount < 0.4) {
                cuJSNum = 1;
            }
            else if (JsStarCount >= 0.2 && JsStarCount < 0.4) {
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
                cXGSNum = 1;
            }
            else if (XGStarCount >= 0.2 && XGStarCount < 0.4) {
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

function LoadInfo() {

    var str = "";
    $.ajax({
        type: "get",
        url: ApiUrl + "/Coach/GetCoachCurriculumByID/",
        dataType: "json",
        async: false,
        data: { PKID: $("#hdPKID").val() },
        //beforeSend: function () {
        //},
        success: function (data) {
            var c = data[0];

            $("#aTitle").text(c.Title);
            $("#sVenue").text(c.VenueName);
            $("#Cdate").text(c.CurriculumDate.substring(0, 10) + "  ");
            $("#CTime").text(c.CurriculumBeginTime + "-" + c.CurriculumEndTime);
            $("#SJL").text(c.CoachFullName);
            $("#Scount").text(c.Sucount);
            $("#sqjcount").text(c.sqjcount);
            $("#hdDate").val(c.CurriculumDate.substring(0, 10) + " " + c.CurriculumBeginTime + ":00");
            
            var ja = c.PlanContent;
            var strJA = "";

            if (ja == null || ja == 'null' || ja == "") {
                strJA += '<div class="_CD_NOTsc">';
                strJA += ' <div class="_CD_jiaoan textAlign">';
                strJA += '老师还没有上传教案信息';
                strJA += '  </div>';              
                strJA += '   </div> ';
            }
            else {
                strJA += '  <div class="_CD_ALLSC">';

                strJA += ' <ul>';

                strJA += '<li>';
                strJA += '<p>' + ja + '</p>';
                strJA += '</li>';
                strJA += '</ul>';
                strJA += '</div>';

            }
            $("#JA").html(strJA);

            var zj = c.SContent;
            var strZJ = "";
            if (zj == null || zj == "null" || zj == "") {
                strZJ += '<div class="_CD_NOTsc">';
                strZJ += ' <div class="_CD_jiaoan textAlign">';
                strZJ += ' 老师还没有上传总结信息';

                strZJ += '  </div>';
              
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

function GetComment()
{
    $.ajax({
        type: "get",
        url: ApiUrl + "/Coach/GetCoachCommentDetail",
        contentType: "application/json",
        data: { StudentID: $("#hdStudentID").val(), CurriculumID: $("#hdCUID").val() },
        success: function (data) {

            var strRec = '';
            if (data != null && data != "") {
                var c = data[0];
                var strDP = c.Info;

                strRec += '  <div class="_CD_ALLSC">';

                strRec += ' <ul>';

                strRec += '<li>';
                strRec += '<p>' + strDP + '</p>';
                strRec += '</li>';
                strRec += '</ul>';
                strRec += '</div>';

            }
            else
            {
                strRec += '<div class="_CD_NOTsc">';
                strRec += ' <div class="_CD_jiaoan textAlign">';
                strRec += ' 老师还没有上传点评信息';

                strRec += '  </div>';

                strRec += '   </div> ';
            }

            $("#DP").html(strRec);
        },
        error: function (e) {
            console.log('error');
        },
        complete: function () {

        }
    });
}




$("#btndp").on('click', function () {
    var strInfo = "";

    //0预约成功，1上课成功，2学生请假，3老师请假4场馆停课 5发放工资完成 6请假申请 7上课中
    var State = parseInt($("#hdState").val());
    if (State == 1 || State == 5) {
        location.href = "../MyEvaluate/?pkid=" + $("#hdPKID").val() + "&sid=" + $("#hdStudentID").val() + "&cid=" + $("#hdCUID").val();
        return false;
    }
    else {
        switch (State) {
            case 1:
                strInfo = "上课完成";
                break;
            case 2:
                strInfo = "请假成功";
                break;
            case 3:
                strInfo = "老师请假";
                break;
            case 4:
                strInfo = "场馆停课";
                break;
            case 5:
                strInfo = "上课完成";
                break;
            case 6:
                strInfo = "请假申请成功";
                break;
            case 7:
                strInfo = "上课中";
                break;
        }
    }
  
    var ctime = $("#CTime").text().split('-')[0];
    var myDate = new Date();
    var currTime = myDate.getFullYear() + "-" + (myDate.getMonth() + 1) + "-" + myDate.getDate() + " " + myDate.getHours() + ":" + myDate.getMinutes() + ":00";
    var endTime = $("#hdDate").val();

    var dcTime = NewDateTime(currTime);
    var deTime = NewDateTime(endTime);

    if (dcTime >= deTime) {
        strState = "未到";
        alert("本课时你没有来上课不能请假,快联系场馆吧");
        return false;
    }

    alert("只有【上课成功】的状态才可以评价老师，当前课时的状态是【" + strInfo + "】");
    return false;

});

$("#pQJ").on("click", function () {
    var strInfo = "";
    //0预约成功，1上课成功，2学生请假，3老师请假4场馆停课 5发放工资完成 6请假申请 7上课中
    var State = parseInt($("#hdState").val());

    if (State == 0) {

        var ctime = $("#CTime").text().split('-')[0];
        var myDate = new Date();
        var currTime = myDate.getFullYear() + "-" + (myDate.getMonth() + 1) + "-" + myDate.getDate() + " " + myDate.getHours() + ":" + myDate.getMinutes() + ":00";
        var endTime = $("#hdDate").val() ;

        var dcTime = NewDateTime(currTime);
        var deTime = NewDateTime(endTime);

        if (dcTime >= deTime) {
            strState = "未到";
            alert("本课时你没有来上课不能请假,快联系场馆吧");
            return false;
        }
        else {
            $('#my-confirm').modal();
            return true;
        }

        
    }
    else {
      

        switch (State)
        {
            case 1:
                strInfo = "上课完成";
                break;
            case 2:
                strInfo = "请假成功";
                break;
            case 3:
                strInfo = "老师请假";
                break;
            case 4:
                strInfo = "场馆停课";
                break;
            case 5:
                strInfo = "上课完成";
                break;
            case 6:
                strInfo = "请假申请成功";
                break;
            case 7:
                strInfo = "上课中";
                break;
        }
        alert(State);
       alert("只有【约课成功】的状态才可以提交请假申请，当前课时的状态是【" + strInfo + "】");
    }
});


$("#btnqj").on("click", function () {
    $.ajax({
        type: "get",
        url: ApiUrl + "/Student/ApplyLeave/",
        contentType: "application/json",
        data: { State: 6, cID: $("#hdCUID").val() },
        success: function (data, status) {
            if (status == "success") {
                if (data.Code == 1001) {
                    alert('提交请假申请成功');
                }
                else { alert('提交请假申请失败，再来一次吧'); }
            }
        },
        error: function (e) {
            alert('提交请假申请失败，再来一次吧');
        },
        complete: function () {

        }
    });
});




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
