$('.Pu-img img').bind('mouseover', function () {
    var cImg = $(this).attr("src");
    if (cImg.indexOf("star_full") >= 0) {
        $(this).attr("src", "/images/star_empty.png");
    }
    else { $(this).attr("src", "/images/star_full.png"); }
});

$(".save").bind("click", function () {
    var icount = 0;
    $('.Pu-img img').each(function () {
        var cImg = $(this).attr("src");
        if (cImg.indexOf("star_full") >= 0) {
            icount = icount + 1;
        }
    });

    var parm = { CurriculumID: $("#hdCID").val(), StudentID: $("#hdSID").val(), Info: $("#txtInfo").val(), StarLevel: icount };
    $.ajax({
        type: "POST",
        url: "http://localhost:53262/api/Student/AddCurriculumEvaluate",
        contentType: "application/json",
        data: JSON.stringify(parm),
        success: function (data, status) {
            if (status == "success") {
                alert("ok");
                console.log('ok');
            }
        },
        error: function (e) {
            console.log('error');
        },
        complete: function () {

        }
    });

});


function LoadInfo() {
    var str = "";
    $.ajax({
        type: "get",
        url: "http://localhost:53262/api/Coach/GetCoachCurriculumByID/",
        dataType: "json",
        async: false,
        data: { PKID: $("#hdID").val() },
        beforeSend: function () {
        },
        success: function (data) {
            var c = data[0];

            str += "  <p class='time'>" + dateformat(c.CurriculumDate, "yyyy-MM-dd") + "    " + c.CurriculumBeginTime + "-" + c.CurriculumEndTime + "</p>";
            str += "  <p class='address'><i class='iconfont'>&#xe600;</i>" + c.VenueName + "-" + c.CampusName + "</p>";
            str += "  <p class='cx'><i class='iconfont'>&#xe612;</i>学员数:" + c.Sucount + "  <font color='red'>已学完</font></p> </a>";
            $(".find-left").html(str);
        }
    });


}

$(document).ready(function () {

    LoadInfo();

    GetPJ();

});


function GetPJ() {


    var str = "";
    $.ajax({
        type: "get",
        url: "http://localhost:53262/api/Coach/GetCoachCommentByPKID/",
        dataType: "json",
        async: false,
        data: { pkid: $("#hdID").val()},
        beforeSend: function () {
        },
        success: function (data) {   

            $.each(data, function (i, c) {
                str = str + " <article class='fabu'>";

                str = str + "    <a href=''>   <img class='am-comment-avatar' src='/images/icon01.png' />  </a>";
                var icount = c.StarLevel;

                str = str + " <div class='Pu-img'>";
                for (var i = 1; i < 6; i++) {
                    if (i <= icount) {
                        str = str + " <img src='/images/star_full.png' style='width:20px;height:20px' />";
                    }
                    else {
                        str = str + " <img src='/images/star_empty.png' style='width:20px;height:20px' />";
                    }
                }
                str = str + "</div>";
                str = str + "   <div class='am-comment-main'>";
                str = str + " <header class='am-comment-hd'>";
                str = str + " <div class='am-comment-meta'>";
                str = str + " <a href='#link-to-user' class='am-comment-author'>..</a> 点评学员【" + c.StudentFullName + "】于 <time datetime=''>" + c.AddTime + "</time>";
                str = str + "  </div>  </header><div class='am-comment-bd'> " + c.Info + "</div> </div>    </article>";

            });
            $(".PJ").html(str);
        
        }
    });
}

function dateformat(date) {
    var myDate = new Date(date);
    return myDate.getFullYear() + "-" + (myDate.getMonth() + 1) + "-" + myDate.getDate();
}
