﻿

//取的当前学生信息
function LoadInfo() {
    var str = "";
    $.ajax({
        type: "get",
        url: ApiUrl+"/Coach/GetCoachCommentByPKID/",
        dataType: "json",
        async: false,
        data: { PKID: $("#hdPKID").val() },
        //beforeSend: function () {
        //},
        success: function (data) {
            var str = '';
            $.each(data, function (i, c) {

                str += ' <li> ';
                str += '<p>学生：<span>' + c.StudentFullName + '</span></p> ';               
                str += '<p>内容：' + c.Info + '</p> ';
                str += '<p>时间：<span>' + c.ModifyTime + '</span></p> ';
                str += '</li> ';
            });

            $("#ulDP").html(str);
        }
    });
}

