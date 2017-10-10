


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

            var ja = c.PlanContent;
            var strJA = "";

            if (ja == null || ja == 'null' || ja == "") {
                strJA += '<div class="_CD_NOTsc">';
                strJA += ' <div class="_CD_jiaoan textAlign">';
                strJA += '教练还没有上传教案信息';
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
                strZJ += ' 教练还没有上传总结信息';

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





$(document).ready(function () {

    LoadInfo();
    LoadPJInfo();
  
});


$("#btndp").on('click', function () {
    location.href = "MyEvaluate/?pkid=" + $("#hdPKID").val() + "&sid=" + $("#hdStudentID").val() + "&cid=" + $("#hdCUID").val();
});





//请假
function ApplyLeave() {
    $.ajax({
        type: "POST",
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

}




