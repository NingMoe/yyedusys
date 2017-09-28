


function LoadPJInfo() {
    var str = "";
    $.ajax({
        type: "get",
        url: "http://localhost:53262/api/Coach/GetCurriculumEvaluateByPKID/",
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
        url: "http://localhost:53262/api/Coach/GetCoachCurriculumByID/",
        dataType: "json",
        async: false,
        data: { PKID: $("#hdID").val() },
        //beforeSend: function () {
        //},
        success: function (data) {
            var c = data[0];

            $("#aTitle").text(c.Title);
            $("#sVenue").text(c.VenueName);
            $("#Cdate").text(c.CurriculumDate);
            $("#Ctime").text(c.CurriculumBeginTime + "-" + c.CurriculumEndTime);
            $("#SJL").text(c.CoachFullName);
            $("#Scount").text(c.Sucount);
            $("#sqjcount").text(c.sqjcount);

            var ja = c.PlanContent;
            var strJA = "";
            if (ja == "")
            {
                strJA+='<div class="_CD_NOTsc">';
                strJA+=' <div class="_CD_jiaoan textAlign">';
                strJA+='    <p class="icon iconfont icon-cuo"></p>';
                strJA += '     <p><textarea class="" rows="5" id="doc-ta-1" id="txtJA"></textarea></p>';
                strJA+='  </div>';
                strZJ += '  <p class="_CD_sc textAlign" onclick="ckJA">写教案</p>';
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
            if (ja == "") {
                strZJ += '<div class="_CD_NOTsc">';
                strZJ += ' <div class="_CD_jiaoan textAlign">';
                strZJ += '    <p class="icon iconfont icon-cuo"></p>';
                strZJ += '     <p><textarea class="" rows="5" id="doc-ta-1" id="txtZJ"></textarea></p>';
                strZJ += '  </div>';
                strZJ += '  <p class="_CD_sc textAlign" onclick="ckZJ">写总结</p>';
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
    configQJ();

});

function configQJ() {
    $(function () {
        $('#doc-confirm-toggle').
          on('click', function () {
              $('#my-confirm').modal({
                  relatedTarget: this,
                  onConfirm: function (options) {
                      var $link = $(this.relatedTarget).prev('a');
                      ApplyLeave();
                  },
                  // closeOnConfirm: false,
                  onCancel: function () {

                  }
              });
          });

        $('#doc-confirm-toggle2').
          on('click', function () {
              $('#my-confirm2').modal({
                  relatedTarget: this,
                  onConfirm: function (options) {
                      var $link = $(this.relatedTarget).prev('a');
                      ApplyLeave();
                  },
                  // closeOnConfirm: false,
                  onCancel: function () {

                  }
              });
          });
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
        type: "POST",
        url: "http://localhost:53262/api/Coash/UpdateCurriculumState",
        contentType: "application/json",
        data: {State:1, StudentIDs:StudentIDs , pkid:$("#hdPKID").val(), VenueID:0, CoachID:0},
        success: function (data, status) {
            if (status == "success") {
                if (data.Code == 1000) {
                    alert('签到提交成功，快上课吧');
                }
                else { alert('签到提交失败，再来一次吧'); }
            }
        },
        error: function (e) {
            alert('签到提交失败，再来一次吧');
        },
        complete: function () {

        }
    });

});

function ckJA()
{
    var parm = { PKID: $("#hdPKID").val(), PlanContent: $("#txtJA").val() };
    $.ajax({
        type: "POST",
        url: "http://localhost:53262/api/Coash/AddCurriculumTeachingPlan",
        contentType: "application/json",
        data: JSON.stringify(parm),
        success: function (data, status) {
            if (status == "success") {
                if (data.Code == 1001) {
                    alert('添加成功');                  
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

function ckZJ() {
    var parm = { PKID: $("#hdPKID").val(), SContent: $("#txtZJ").val() };
    $.ajax({
        type: "POST",
        url: "http://localhost:53262/api/Coash/AddCurriculumSummary",
        contentType: "application/json",
        data: JSON.stringify(parm),
        success: function (data, status) {
            if (status == "success") {
                if (data.Code == 1001) {
                    alert('添加成功');
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


function ApplyLeave()
{
    $.ajax({
    type: "POST",
    url: "http://localhost:53262/api/Coash/ApplyLeaveByPKID",
    contentType: "application/json",
    data: {PKID:$("#hdPKID").val(),State:3},
    success: function (data, status) {
        if (status == "success") {
            if (data.Code == 1000) {
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

function GetStudent() {
    $.ajax({
        type: "POST",
        url: "http://localhost:53262/api/Coash/GetCurriculumStudentByPKID",
        contentType: "application/json",
        data: { pkid: $("#hdPKID").val() },
        success: function (data) {
            var str = '';
            $.each(data, function (i, c) {


                str += '  <li class="am-g"> ';
                str += '<a href="" class="am-u-sm-5 am-u-md-5 am-u-lg-5 clear"> ';
                str += '	<img src="' + c.HeadUrl + '" alt="" class="le am-circle"> ';
                str += '	<p class="_CDC_name le">' + c.FullName + '</p> ';
                str += '	</a> ';


                str += '	<div class="am-u-sm-2 am-u-md-2 am-u-lg-2 _CDC_phone"> ';
                str += '<div class="am-checkbox"> ';
                str += '<label><input type="checkbox" data-id="' + c.StudentID + '"> </label> ';
                str += '</div> ';
                str += '</div></li> ';

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


function StudentSign()
{

}