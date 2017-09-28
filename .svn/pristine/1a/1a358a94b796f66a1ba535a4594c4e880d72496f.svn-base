
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

