
function bind_data() {

    columns_data = [
        { "data": "StudentID" },
        {
            "data": "HeadUrl",
            "render": function (data, type, full, meta) {
                var edithtml = "<img src='"+data+"' />"
                return edithtml;
            }
        },
        { "data": "UserName" },
        { "data": "FullName" },
        { "data": "Mobile" },
        { "data": "ParentFullName" },
        { "data": "ParentMobile" },
        { "data": "LinkMan" },
        { "data": "LinkManMobile" },
        { "data": "Address" },
    ];

    bindTable('studenttable', 'studentfrom', 'api/student/Page4Venue', columns_data);
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
