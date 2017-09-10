﻿
function bind_data() {

    columns_data = [
        { "data": "VenueID" },
        //{
        //    "data": "HeadUrl",
        //    "render": function (data, type, full, meta) {
        //        var edithtml = "<img src='" + data + "' />"
        //        return edithtml;
        //    }
        //},
        { "data": "VenueCode" },
        { "data": "VenueName" },
        { "data": "VenueAddress" },
        { "data": "LinkMan" },
        { "data": "LinkManMobile" },
        { "data": "LegalPerson" },
        {
            "data": "AddTime",
            "render": function (data, type, full, meta) {
                var dataStr = Date.parse(data);
                return new Date(dataStr).Format("yyyy-mm-dd hh:MM:ss");
            }
        },
        //{
        //    "data": "State",
        //    "render": function (data, type, full, meta) {

                //if (data == 1) {
                //    //设置课时工资 课时价格 等级
                //    var edithtml = '<button  class="btn btn-xs btn-success" onclick="return showcoachwage(' + full['CoachID'] + ');"><i class="fa fa-edit"> 设置课时工资 </i></button>&nbsp;&nbsp;';
                //    edithtml += '<button  class="btn btn-xs btn-warning" onclick="return showcoachprice(' + full['CoachID'] + ');"><i class="fa fa-edit"> 设置课时费用 </i></button>&nbsp;&nbsp;';

                //} else {
                //    var edithtml = '<button class="btn btn-xs btn-success" onclick="return setcoachaudited(' + full['CoachID'] + ',' + full['VenueID'] + ');"><i class="fa fa-edit"> 审核通过 </i></button>&nbsp;&nbsp;';
                //    edithtml += '<button class="btn btn-xs btn-danger" onclick="return deletecoach(' + full['CoachID'] + ',' + full['VenueID'] + ');"><i class="fa fa-edit"> 审核不通过 </i></button>&nbsp;&nbsp;';
                //}

                //edithtml += '<a href="/Coach/Details/' + full['CoachID'] + '" class="btn btn-xs btn-warning"><i class="fa fa-edit"> 查看 </i></a>&nbsp;&nbsp;';
                //return edithtml;
        //    }
        //},
    ];

    bindTable('venuetable', 'venuefrom', 'api/Venue/Page', columns_data, false);
}

function create() {
    
    var data = JSON.stringify($('#venuefrom').serializeJSON());

    var fd = new FormData();
    fd.append("venueInfo", data);
    fd.append("file", $("#file").get(0).files[0]);
    $.ajax({
        url: ApiUrl + "api/Venue/Create",
        type: "POST",
        processData: false,
        contentType: false,
        data: fd,
        success: function (data, status) {
            if (status == "success") {
                if (data.Error) {
                    alert(data.Msg);
                } else {
                    alert("添加成功");
                }
            } else {
                alert("添加异常");
            }
        }
    });

    ////添加
    //$.ajax({
    //    type: "POST",
    //    url: ApiUrl + "api/Venue/create",
    //    contentType: "application/json",
    //    data: JSON.stringify($('#venuefrom').serializeJSON()),
    //    success: function (data, status) {
    //        if (status == "success") {
    //            alert("添加成功");
    //        }
    //    },
    //    error: function (e) {
    //        console.log('error');
    //    },
    //    complete: function () {

    //    }
    //});

}