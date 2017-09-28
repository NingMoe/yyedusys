jQuery.support.cors = true;
var ApiUrl = "http://localhost:53262/";
var oTable;

function bindTable() {

    //$('#demo').html('<table class="table table-bordered table-hover" cellpadding="0" cellspacing="0" border="0" class="display" id="example"></table>');
    queryData = JSON.stringify($('#cityfrom_query').serializeJSON());
    //console.log(queryData);
    //var $searchResult = $("#demo");
    //if (oTable) {
    //    oTable.fnClearTable();
    //    $searchResult.dataTable().fnDestroy();
    //    //$("#resultList").empty();
    //} else {
    //    //$searchResult.show();
    //}
    oTable = $('#example').dataTable({
        "paging": true,
        "lengthChange": true,
        "searching": false,
        "ordering": true,
        "aaSorting": [[0, "desc"]], //默认的排序方式，第1列，升序排列  
        "info": true,
        "autoWidth": false,
        "destroy": true,
        "processing": true,
        "serverSide": true,    //true代表后台处理分页，false代表前台处理分页 
        //"draw":false,
        "aLengthMenu": [1, 3, 4],
        "paginationType": "full_numbers", //详细分页组，可以支持直接跳转到某页  
        "deferRender": true,//当处理大数据时，延迟渲染数据，有效提高Datatables处理能力 
        "ajax": {
            //contentType: "application/json",
            url: ApiUrl + "api/city/page",
            dataSrc: function (data) {
                //console.log(data);
                //setTimeout('oTable.fnDraw(false)', 3000); //重新加载bc_Table.ajax.reload()
                if (data.callbackCount == null) {
                    data.callbackCount = 0;
                }
                //抛出异常  
                if (data.sqlException) {
                    alert(data.sqlException);
                }
                //查询结束取消按钮不可用  
                //$("#queryDataByParams").removeAttr("disabled");
                return data.data;
                //return data.dataList;             //自定义数据源，默认为data  
            },
            dataType: 'json',
            type: 'get',
            //data: queryData,
            data: function (query_data) {
                //param = queryData;
                //param.start = query_data.start;
                //param.length = query_data.length;
                //param.draw = query_data.draw;
                //request_data = JSON.stringify(param);

                //ss = JSON.stringify($('#cityfrom_query').serializeJSON());

                query = JSON.stringify({
                    'SearchCondition': $('#cityfrom_query').serializeJSON(),
                    'draw': query_data.draw,
                    'PageIndex': query_data.start,
                    'PageSize': query_data.length
                });
                console.log(query);

                return 'query='+query;
            }
        },
        //"data": data,
        "columns": [
            { "data": "CityID" },
            { "data": "CityName" },
        ],
        /*是否开启主题*/
        "bJQueryUI": true,
        "oLanguage": {    // 语言设置  
            "sLengthMenu": "每页显示 _MENU_ 条记录",
            "sZeroRecords": "抱歉， 没有找到",
            "sInfo": "从 _START_ 到 _END_ /共 _TOTAL_ 条数据",
            "sInfoEmpty": "没有数据",
            "sInfoFiltered": "(从 _MAX_ 条数据中检索)",
            "sZeroRecords": "没有检索到数据",
            "sSearch": "检索:",
            "oPaginate": {
                "sFirst": "首页",
                "sPrevious": "前一页",
                "sNext": "后一页",
                "sLast": "尾页"
            }
        },
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
        bindTable()
    }

   

    ////查询
    //$.ajax({
    //    type: "get",
    //    url: ApiUrl + "api/city/get",
    //    data: {},
    //    success: function (data, status) {
    //        if (status == "success") {
    //            console.log(data);

               
    //        }
    //    },
    //    error: function (e) {
    //    },
    //    complete: function () {

    //    }
    //});

});

function create() {

    console.log(JSON.stringify($('#cityfrom').serializeJSON()));

    //待调整
    //swalSuccess () {
    //    swal('Good job!', 'You clicked the button!', 'success')
    //}

    //添加
    $.ajax({
        type: "POST",
        url: ApiUrl + "api/city/add",
        contentType: "application/json",
        data: JSON.stringify($('#cityfrom').serializeJSON()),
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

}
