

//取的当前学员信息
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
                str += '<p>点评时间：<span>' + c.AddTime + '</span></p> ';
                str += '<p>' + c.Info + '</p> ';
                str += '</li> ';
            });

            $("#ulDP").html(str);
        }
    });
}

$(document).ready(function () {

    LoadInfo();


});