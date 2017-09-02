
$("#Save").on('click', function () {

    var data = $('#replyfeedbacklogform').serializeJSON();
    console.log(data);
    //添加
    $.ajax({
        type: "POST",
        url: ApiUrl + "api/FeedBackLog/Reply",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (data, status) {
            console.log(data);
            if (status == "success") {
                if (!data.Error) {
                    alert("回复反馈成功");
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

function showInfo(venueId, fdId, fdMsg) {

    $('#replymodal').on('show.bs.modal', function (e) {

        $("[name=VenueID]").val(e.relatedTarget.venueId);
        $("#FDId").val(e.relatedTarget.fdId);
        $("#FDMsg").html(e.relatedTarget.fdMsg);

    });

    $('#replymodal').modal({
        show: true
    }, {
        venueId: venueId,
        fdId: fdId,
        fdMsg: fdMsg,
    });
}

function bind_data() {

    columns_data = [
        { "data": "FDId" },
        { "data": "UserName" },
        { "data": "FDMsg" },
        {
            "data": "AddTime",
            "render": function (data, type, full, meta) {
                var dataStr = Date.parse(data);
                return new Date(dataStr).Format("yyyy-MM-dd hh:mm:ss");
            }
        },
        { "data": "FullName" },
        {
            "data": "State",
            "render": function (data, type, full, meta) {
                if (data == 1) {
                    return '<span class="btn btn-xs btn-success">已回复</span>';
                } else {
                    return '<span class="btn btn-xs btn-danger">未回复</span>';
                }
            }
        },
        {
            "data": "State",
            "render": function (data, type, full, meta) {
                var edithtml = '<button class="btn btn-xs btn-warning" onclick="return showInfo(' + full['VenueID'] + ',' + full['FDId'] + ',' + "'" + full['FDMsg'] + "'" + ');"><i class="fa fa-edit"> 回复 </i></button>&nbsp;&nbsp;';
                return edithtml;
            }
        },
    ];

    bindTable('feedbacklogtable', 'feedbacklogform', 'api/FeedBackLog/Page', columns_data, false);
}
