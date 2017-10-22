﻿
function bind_data() {

    columns_data = [
        { "data": "StudentID" },
        {
            "data": "HeadUrl",
            "render": function (data, type, full, meta) {
                var edithtml = "<img src='" + data + "' style='width:48px;height:48px;' />"
                return edithtml;
            }
        },
        { "data": "UserName" },
        { "data": "FullName" },
        {
            "data": "State",
            "render": function (data, type, full, meta) {

                if (data == 1) {
                    return '<span class="btn btn-xs btn-success">正常</span>';
                } else {
                    return '<span class="btn btn-xs btn-danger">流失</span>';
                }
            }
        },
        { "data": "Mobile" },
        { "data": "ParentFullName" },
        { "data": "ParentMobile" },
        //{ "data": "LinkMan" },
        //{ "data": "LinkManMobile" },
        { "data": "Address" },
        {
            "data": "State",
            "render": function (data, type, full, meta) {

                var edithtml = '<a href="/Student/Edit/' + full['StudentID'] + '" class="btn btn-xs btn-warning"><i class="fa fa-edit"> 编辑 </i></a>&nbsp;&nbsp;';

                if (data == 1) {
                    edithtml += '<button class="btn btn-xs btn-danger" onclick="return signLoss(' + full['StudentID'] + ',' + full['VenueID'] + ');"><i class="fa fa-edit"> 流失 </i></button>&nbsp;&nbsp;';
                }
                return edithtml;

            }
        },
    ];

    bindTable('studenttable', 'studentfrom', 'api/student/Page4Venue', columns_data);
}

function signLoss(studentID, venueId) {

    var flag = confirm('确定标记为流失?');
    if (flag) {

        $.ajax({
            type: "post",
            url: ApiUrl + "api/Student/SignLoss",
            contentType: 'application/json',
            data: JSON.stringify({ StudentID: studentID, VenueID: venueId }),
            success: function (data, status) {
                if (status == "success") {
                    console.log(data);
                    if (data.Error) {
                        alert(data.Msg);
                    } else {
                        bind_data();
                    }
                }
            },
            error: function (e) {
            },
            complete: function () {

            }
        });
    } else {

    }
}

function show() {

    var studentId = $("#StudentID").val();

    //查询
    $.ajax({
        type: "get",
        url: ApiUrl + "api/Student/get/" + studentId,
        data: {},
        success: function (data, status) {
            if (status == "success") {
                if (data.Info == null) {
                    alert('error');
                } else {
                    $("#UserName").val(data.Info.UserName);
                    $("#UserNameShow").val(data.Info.UserName);
                    $("#FullName").val(data.Info.FullName);
                    $("#ParentFullName").val(data.Info.ParentFullName);
                    $("#ParentMobile").val(data.Info.ParentMobile);
                    $("#BirthDate").val(data.Info.BirthDate.substring(0,10));
                    $("#Address").val(data.Info.Address);
                    $("#State").val(data.Info.State);
                }
            }
        },
        error: function (e) {
        },
        complete: function () {

        }
    });

}

function edit() {

    var data = $('#studentfrom').serializeJSON();
    data['Mobile'] = data['UserName'];
    data['VenueID'] = VenueID;

    //添加
    $.ajax({
        type: "POST",
        url: ApiUrl + "api/Student/edit",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (data, status) {
            if (data.Error) {
                alert(data.Msg);
            } else {
                location.href = "/Student/Index";
            }
        },
        error: function (e) {
        },
        complete: function () {

        }
    });
}

$(function () {

    if (oTable != null || oTable != undefined) {
        //        dataTable初始化之后不能再设值，需要销毁重新定义，(数据量大时不适用)  
        //oTable.fnClearTable(false);
        //oTable.destroy();
        //bigDataTable(get_query());

        //              var src={  
        //                url:"getJson_BigData_queryDataByParams",  
        //                dataSrc :"dataList",      //自定义数据源，默认为data  
        //                type:"post",  
        //                data:{"test":$("#protocolType").val()}  
        //              };  

        //oTable.fnSettings().ajax = src; //ajax访问数据  
        //oTable.fnPageChange(0, true);  //分页的页数从0开始  
        oTable.settings()[0].ajax.data = get_query();
        oTable.ajax.reload();
    } else {
        bind_data();
    }

});

$("#import").on('click', function () {

    var fd = new FormData();
    fd.append("venueID", $("#VenueID").val());
    fd.append("file", $("#file").get(0).files[0]);
    $.ajax({
        url: ApiUrl + "api/Student/Import",
        type: "POST",
        processData: false,
        contentType: false,
        data: fd,
        success: function (data, status) {
            if (status == "success") {
                if (data.Error) {
                    alert(data.Msg);
                } else {
                    alert("导入成功");
                }
            } else {
                alert("导入异常");
            }
            //if (data.Msg != null && data.Msg.length > 0) {
            //    alert("导入成功," + data.Msg + "已经添加");
            //} else {
            //    alert("导入成功");
            //}
        }
    });
});
