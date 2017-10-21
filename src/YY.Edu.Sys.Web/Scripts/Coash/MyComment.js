﻿$("#SelLog").on("click", function () {
    location.href = "../CommentLog/?pkid=" + $("#hdPKID").val();

});

$("#save").bind("click", function () {

    if ($("#hdStudentID").val() == "")
    {
        alert("请选择要点评的学生");
        return false;
    }
   
    if ($("#hdCommentID").val() != "")//修改
    {
        var parm = { CommentID: $("#hdCommentID").val(), Info: $("#txtInfo").val() };
        $.ajax({
            type: "POST",
            url: ApiUrl+"/Coach/UpdateCoachComment",
            contentType: "application/json",
            data: JSON.stringify(parm),
            success: function (data, status) {
                if (status == "success") {
                    alert("点评信息提交成功");                   
                }
            },
            error: function (e) {
                alert("点评信息提交失败，再操作一次吧");
            },
            complete: function () {

            }
        });
    }
    else
    {
        var parm = { CurriculumID: $("#hdCurriculumID").val(), StudentID: $("#hdStudentID").val(), Info: $("#txtInfo").val(), CoachID: $("#hdCoachID").val(), PKID: $("#hdPKID").val() };
        $.ajax({
            type: "POST",
            url: ApiUrl+"/Coach/AddCoachComment",
            contentType: "application/json",
            data: JSON.stringify(parm),
            success: function (data, status) {
                if (status == "success") {
                    alert("添加点评信息成功");
                    console.log('ok');
                }
            },
            error: function (e) {
                alert("添加点评信息失败，再操作一次吧");
            },
            complete: function () {

            }
        });
    }

});

//取的当前学生信息
function LoadInfo() {
    var str = ""; 
    $.ajax({
        type: "get",
        url: ApiUrl+"/Coach/GetCurriculumStudentByPKID/",
        dataType: "json",
        async: false,
        data: { PKID: $("#hdPKID").val() },
        //beforeSend: function () {
        //},
        success: function (data) {

            var str = ' <li class="am-dropdown-header">学生列表</li>';
            $.each(data, function (i, c) {
                str += '  <li><a href="#" data-id="'+c.StudentID+'" data-cid="'+c.CurriculumID+'">'+c.FullName+'</a></li>';
            });

            $("#cStudent").html(str);

            $("#cStudent li a").on("click", function () {
                $("#hdCommentID").val('');
                $("#txtInfo").text('');
                $("#hdStudentID").val($(this).attr("data-id"));
                $("#hdCurriculumID").val($(this).attr("data-cid"))
                $("#xyInfo").html($(this).text() + '<span class="am-icon-caret-down"></span>');
                $.ajax({
                    type: "get",
                    url: ApiUrl+"/Coach/GetCoachCommentDetail",
                    contentType: "application/json",
                    data: { StudentID: $(this).attr("data-id"), CurriculumID: $("#hdCurriculumID").val() },
                    success: function (data) {
                       
                        if (data != null&&data!="") {
                            var c = data[0];
                            $("#txtInfo").text(c.Info);
                            $("#hdCommentID").val(c.CommentID);
                        }
                    },
                    error: function (e) {
                        console.log('error');
                    },
                    complete: function () {

                    }
                });
            }
            );
        }
    });


}


