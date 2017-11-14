﻿
$("#Save").on('click', function () {

    var data = $('#venuenoticefrom').serializeJSON();

    //添加
    $.ajax({
        type: "POST",
        url: ApiUrl + "api/VenueNotice/create",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (data, status) {
            if (status == "success") {
                if (!data.Error) {
                    location.href = "/VenueNotice/Index";
                    //alert("添加公告成功");
                } else {
                    alert(data.Msg);
                }
            }
        },
        error: function (e) {
            console.log('error');
        },
        complete: function () {

        }
    });
});

function bind_data() {

    columns_data = [
        { "data": "NoticeId" },
        //{ "data": "UserName" },
         //1公告 2活动 3微信消息 4短信消息 5提醒
        {
            "data": "NoticeType", "render": function (data, type, full, meta) {

                if (data == 1) {
                    return '<span class="btn btn-xs btn-success">公告</span>';
                } else if (data == 2) {
                    return '<span class="btn btn-xs btn-success">活动</span>';
                } else if (data == 3) {
                    return '<span class="btn btn-xs btn-success">微信消息</span>';
                } else if (data == 4) {
                    return '<span class="btn btn-xs btn-success">短信消息</span>';
                } else if (data == 5) {
                    return '<span class="btn btn-xs btn-success">提醒</span>';
                } else {
                    return '<span class="btn btn-xs btn-danger">未知</span>';
                }
            }
        },
        //{
        //    "data": "Source", "render": function (data, type, full, meta) {
        //        if (data == 1) {
        //            return '<span class="btn btn-xs btn-warning">场馆</span>';
        //        } else if (data == 2) {
        //            return '<span class="btn btn-xs btn-success">运营</span>';
        //        } else {
        //            return '<span class="btn btn-xs btn-danger">老师</span>';
        //        }
        //    }
        //},
        { "data": "NoticeTitle" },
        { "data": "NoticeMsg" },
        {
            "data": "NoticeRange",
            "render": function (data, type, full, meta) {
                return transtionVenueNoticeRange(data);
            }
        },
        //{
        //    "data": "AddTime",
        //    "render": function (data, type, full, meta) {
        //        var dataStr = Date.parse(data);
        //        return new Date(dataStr).Format("yyyy-MM-dd hh:mm:ss");
        //    }
        //},
        {
            "data": "SendTime",
            "render": function (data, type, full, meta) {
                var dataStr = Date.parse(data);
                return new Date(dataStr).Format("yyyy-MM-dd hh:mm:ss");
            }
        },
        {
            "data": "State",
            "render": function (data, type, full, meta) {
                if (data == 1) {
                    return '<span class="btn btn-xs btn-success">有效</span>';
                } else if (data == 2) {
                    return '<span class="btn btn-xs btn-success">已发送</span>';
                } else {
                    return '<span class="btn btn-xs btn-danger">无效</span>';
                }
            }
        },
        {
            "data": "State",
            "render": function (data, type, full, meta) {
                var edithtml = '';//'<button class="btn btn-xs btn-warning" ><i class="fa fa-edit"> 发送 </i></button>&nbsp;&nbsp;';
                return edithtml;
            }
        },
    ];

    bindTable('venuenoticetable', 'venuenoticeform', 'api/VenueNotice/Page', columns_data, false);
}
