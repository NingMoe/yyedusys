
function bind_data() {

    columns_data = [
        { "data": "CoachID" },
        { "data": "FullName" },
        { "data": "UserName" },
        { "data": "NickName" },
        { "data": "Address" },
        {
            "data": "Sex",
            "render": function (data, type, full, meta) {
                if (data == 1) {
                    var edithtml = '<span>男</span>';
                } else {
                    var edithtml = '<span>女</span>';
                }
                return edithtml;
            }
        },
        { "data": "Introduce" },
        {
            "data": "CoachID",
            "render": function (data, type, full, meta) {
                if (data == 2) {
                    var edithtml = '<a href="javascript:void(0)" class="btn btn-xs btn-success" onclick="return SetEnable(' + full['CampusID'] + ');"><i class="fa fa-edit"> 启用 </i></a>&nbsp;&nbsp;';

                } else {
                    var edithtml = '<a href="javascript:void(0)" class="btn btn-xs btn-danger" onclick="return SetDisable(' + full['CampusID'] + ');"><i class="fa fa-edit"> 禁用 </i></a>&nbsp;&nbsp;';
                }

                edithtml += '<a href="/Campus/Edit/' + full['CampusID'] + '" class="btn btn-xs btn-warning"><i class="fa fa-edit"> 编辑 </i></a>&nbsp;&nbsp;';
                return edithtml;
            }
        },
    ];

    bindTable('coachtable', 'coachform', 'api/coachtable/Page', columns_data, true);
}

