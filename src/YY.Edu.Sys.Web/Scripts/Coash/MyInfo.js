function LoadInfo() {
    var str = "";
    $.ajax({
        type: "get",
        url: "http://localhost:53262/api/Coach/Get/",
        dataType: "json",
        async: false,
        data: { id: $("#hdID").val() },
        beforeSend: function () {
        },
        success: function (data) {           
            var c = data.Info;
            $("#pName").text(c.FullName);
            $("#pTel").text("电话：" + c.Mobile);
            $("#pInfo").text(c.Introduce);
        }
    });
}

function LoadPJInfo() {
    var str = "";
    $.ajax({
        type: "get",
        url: "http://localhost:53262/api/Coach/GetCurriculumEvaluateByCoachID/",
        dataType: "json",
        async: false,
        data: { CoachID: $("#hdID").val() },
        beforeSend: function () {
        },
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


            
            var cuJSNum=1;
            var cXGSNum=1;
            if (JsStarCount >= 0.2 && JsStarCount < 0.4) {
                cuJSNum = 2;
            }
            else if (JsStarCount >= 0.4 && JsStarCount < 0.6)
            {
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


                       
            var TY="";
            for (var i = 1 ; i < 6; i++)
            {
                if(cuJSNum>=i)
                {
                    TY = TY + '<span class="am-icon-star"></span>';
                }
                else
                { TY = TY + '<span class="am-icon-star-o"></span>'; }
            }
            $("#pTY").html(TY);

            var XG = "";
            for (var j = 1 ; j< 6;j++) {
                if (cXGSNum >= j) {
                    XG = XG + '<span class="am-icon-star"></span>';
                }
                else { XG = XG + '<span class="am-icon-star-o"></span>'; }
            }
            $("#pXG").html(XG);

            $("#PJSF").text(JsStarCount*10+'分');
            $("#PXGF").text(XGStarCount*10+'分');
         
        }
    });
}


function LoadPresenceInfo() {
    var str = "";
    $.ajax({
        type: "get",
        url: "http://localhost:53262/api/Coach/GetCoachingPresence/",
        dataType: "json",
        async: false,
        data: { VenueID: $("#hdVenveID").val(), CoachID: $("#hdID").val(), FCType: 1 },
        beforeSend: function () {
        },
        success: function (data) {
            alert(JSON.stringify(data));
                   $.each(data.data, function (i, c) {
                       str += '<li><img class="am-thumbnail" src="/' + c.FCUrl + '" /></li>';
                   });

        }
    });
    alert(str);
    $("#ulFC").html(str);
}

 $(document).ready(function () {

    LoadInfo();
    LoadPJInfo();
    LoadPresenceInfo();

});


 