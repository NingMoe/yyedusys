
function bind_data() {

    columns_data = [
        { "data": "CampusID" },
        { "data": "CampusName" },
        { "data": "LinkMan" },
        { "data": "LinkTel" },
        { "data": "CityName" },
        {
            "data": "CampusStatus",
            "render": function (data, type, full, meta) {
                if (data == 2) {
                    var edithtml = '<span>禁用</span>';
                } else {
                    var edithtml = '<span>启用</span>';
                }
                return edithtml;
            }
        },
        {
            "data": "CampusStatus",
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

    bindTable('campustable', 'campusform', 'api/campus/Page', columns_data, true);
}

function SetEnable(val) {
    //添加
    $.ajax({
        type: "POST",
        url: ApiUrl + "api/campus/SetEnable",
        contentType: "application/json",
        data: JSON.stringify({ id: val }),
        success: function (data, status) {
            if (status == "success") {
                alert("ok");
                console.log('ok');
                bind_data();
            }
        },
        error: function (e) {
            console.log('error');
        },
        complete: function () {

        }
    });
}

function SetDisable(val) {
    //添加
    $.ajax({
        type: "POST",
        url: ApiUrl + "api/campus/SetDisable",
        contentType: "application/json",
        data: JSON.stringify({ id: val }),
        success: function (data, status) {
            if (status == "success") {
                alert("ok");
                console.log('ok');
                bind_data();
            }
        },
        error: function (e) {
            console.log('error');
        },
        complete: function () {

        }
    });
}

function create() {

    //添加
    $.ajax({
        type: "POST",
        url: ApiUrl + "api/campus/create",
        contentType: "application/json",
        data: JSON.stringify($('#campusfrom').serializeJSON()),
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

function edit() {

    //添加
    $.ajax({
        type: "POST",
        url: ApiUrl + "api/campus/edit",
        contentType: "application/json",
        data: JSON.stringify($('#campusfrom').serializeJSON()),
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

function show() {

    var venueid = $("#CampusID").val();

    //查询
    $.ajax({
        type: "get",
        url: ApiUrl + "api/Campus/get/" + venueid,
        data: {},
        success: function (data, status) {
            if (status == "success") {
                console.log(data);
                if (data.Info == null) {
                    alert('error');
                } else {
                    $("#CityID").val(data.Info.CityID);
                    $("#CampusName").val(data.Info.CampusName);
                    $("#LinkMan").val(data.Info.LinkMan);
                    $("#LinkTel").val(data.Info.LinkTel);
                    $("#Address").val(data.Info.Address);
                }
            }
        },
        error: function (e) {
        },
        complete: function () {

        }
    });

}

function show_city() {

    //查询
    $.ajax({
        type: "get",
        url: ApiUrl + "api/city/get",
        data: {},
        success: function (data, status) {
            if (status == "success") {
                
                if (data.data == null) {
                    alert('error');
                } else {
                    var programme_sel = [];

                    for (var i = 0; i < data.data.length; i++) {
                        var programme = data.data[i];
                        if (i == 0) {
                            programme_sel.push('<option selected value="' + programme.CityID + '">' + programme.CityName + '</option>')
                        } else {
                            programme_sel.push('<option value="' + programme.CityID + '">' + programme.CityName + '</option>')
                        }
                    }
                    $("#CityID").html(programme_sel.join(' '));
                }
            }
        },
        error: function (e) {
        },
        complete: function () {

        }
    });
}
