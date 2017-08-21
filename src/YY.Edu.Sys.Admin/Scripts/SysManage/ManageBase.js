﻿jQuery.support.cors = true;
var ApiUrl = "http://localhost:53262/";
var oTable;


function bindTable(table, form, action, columns_data) {
   
    oTable = $('#' + table).dataTable({
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
            url: ApiUrl + action,
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
            data: function (query_data) {

                query = JSON.stringify({
                    'SearchCondition': $('#' + form).serializeJSON(),
                    'draw': query_data.draw,
                    'PageIndex': query_data.start,
                    'PageSize': query_data.length
                });
                console.log(query);
                return 'query=' + query;
            }
        },
        "columns": columns_data,
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

function create_base() {

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