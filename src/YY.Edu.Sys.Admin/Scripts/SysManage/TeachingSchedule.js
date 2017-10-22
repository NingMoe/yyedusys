﻿
function show_wage(wage, price) {
    $("#CoachPrice").val(wage);
    $("#Price").val(price);
}

function show_campus(venueId, isCreate) {

    //查询
    $.ajax({
        type: "get",
        url: ApiUrl + "api/campus/page?VenueID=" + venueId,
        contentType: 'application/json',
        success: function (data, status) {
            //console.log(data);
            if (status == "success") {

                if (data.data == null) {
                    alert('error');
                } else {
                    var programme_sel = [];

                    for (var i = 0; i < data.data.length; i++) {
                        var programme = data.data[i];
                        if (i == 0) {
                            if (isCreate) {
                                programme_sel.push('<option selected value="' + programme.CampusID + '">' + programme.CampusName + '</option>')
                            } else {
                                programme_sel.push('<option selected value="-1">选择校区</option>')
                                programme_sel.push('<option value="' + programme.CampusID + '">' + programme.CampusName + '</option>')
                            }
                        } else {
                            programme_sel.push('<option value="' + programme.CampusID + '">' + programme.CampusName + '</option>')
                        }
                    }
                    $("#CampusID").html(programme_sel.join(' '));
                }
            }
        },
        error: function (e) {
        },
        complete: function () {

        }
    });
}

function show_coach(venueId, isCreate) {

    //查询
    $.ajax({
        type: "get",
        url: ApiUrl + "api/coach/GetCoach4Teaching?VenueID=" + venueId,
        contentType: 'application/json',
        success: function (data, status) {
            //console.log(data);
            if (status == "success") {

                if (data.data == null) {
                    alert('error');
                } else {
                    var programme_sel = [];

                    for (var i = 0; i < data.data.length; i++) {
                        var programme = data.data[i];
                        if (i == 0) {
                            if (isCreate) {
                                programme_sel.push('<option selected value="' + programme.CoachID + '">' + programme.FullName + '</option>')
                            } else {
                                programme_sel.push('<option selected value="-1">选择老师</option>')
                                programme_sel.push('<option value="' + programme.CoachID + '">' + programme.FullName + '</option>')
                            }
                            show_wage(programme.Wage, programme.Price);
                        } else {
                            programme_sel.push('<option value="' + programme.CoachID + '">' + programme.FullName + '</option>')
                        }
                    }
                    $("#CoachID").html(programme_sel.join(' '));
                }
            }
        },
        error: function (e) {
        },
        complete: function () {

        }
    });
}

function change_state(action, pKID, venueId) {

    var flag = confirm('确定此操作?');
    if (flag) {
        $.ajax({
            type: "post",
            url: ApiUrl + "api/TeachingSchedule/" + action,
            contentType: 'application/json',
            data: JSON.stringify({ PKID: pKID, VenueID: venueId }),
            success: function (data, status) {
                //console.log(data);
                if (status == "success") {
                    if (!data.Error) {
                        bind_data();
                        //alert('设置成功');
                    } else {
                        alert(data.Msg);
                    }
                }
            },
            error: function (e) {
            },
            complete: function () {

            }
        });
    }
}

function showdetail() {

    pkId = $("#PKID").val();
    venueId = $("#VenueID").val();

    //查询
    $.ajax({
        type: "get",
        url: ApiUrl + "api/TeachingSchedule/GetTeachingScheDetail?pkId=" + pkId + "&venueID=" + venueId,
        contentType: 'application/json',
        success: function (data, status) {
            if (status == "success") {
                if (data.Error) {
                    alert('error');
                } else {
                    $("#Title").html(data.Info.Title);
                    $("#CoachFullName").html(data.Info.CoachFullName);
                    $("#Wage").html(data.Info.CoachPrice);
                    $("#Price").html(data.Info.Price);

                    var dataStr = Date.parse(data.Info.CurriculumDate);
                    $("#CurriculumDate").html(new Date(dataStr).Format("yyyy-MM-dd"));
                    $("#CurriculumBeginTime").html(data.Info.CurriculumBeginTime);
                    $("#CurriculumEndTime").html(data.Info.CurriculumEndTime);
                    $("#StudentFullName").html(data.Info.StudentFullName);
                    //$("#AddTime").html(data.Info.AddTime);
                    $("#State").html(data.Info.State);
                    $("#Info").html(data.Info.Info);
                }
            }
        },
        error: function (e) {
        },
        complete: function () {

        }
    });
}

function bind_data() {

    columns_data = [
        { "data": "PKID" },
        { "data": "VenueName" },
        { "data": "CampusName" },
        { "data": "CoachFullName" },
        //{ "data": "CoachPrice" },
        //{ "data": "StudentFullName" },
        { "data": "Title" },
        {
            "data": "CurriculumDate",
            "render": function (data, type, full, meta) {
                var dataStr = Date.parse(data);
                return new Date(dataStr).Format("yyyy-MM-dd");
            }
        },
        { "data": "CurriculumBeginTime" },
        { "data": "CurriculumEndTime" },
        { "data": "Price" },
        { "data": "CoachPrice" },
        {
            "data": "PKType",
            "render": function (data, type, full, meta) {
                if (data == 1) {
                    var edithtml = '<span class="btn btn-xs btn-info">一对一</span>';
                }else {
                    var edithtml = '<span class="btn btn-xs btn-info">一对多</span>';
                }
                return edithtml;
            }
        },
        {
            "data": "State",
            "render": function (data, type, full, meta) {
                return transtionTeachingScheState(data);
            }
        },
        {
            "data": "AddTime",
            "render": function (data, type, full, meta) {
                var dataStr = Date.parse(data);
                return new Date(dataStr).Format("yyyy-MM-dd");
            }
        },
        {
            "data": "State",
            "render": function (data, type, full, meta) {
                return transtionTeachingScheStateHandle(data, full['PKID'], full['VenueID']);
            }
        },
    ];

    bindTable('teachingscheduletable', 'teachingschedulefrom', 'api/TeachingSchedule/Page4Venue', columns_data, false);
}

function bind_leave_data() {

    columns_data = [
        { "data": "PKID" },
        { "data": "VenueName" },
        { "data": "CampusName" },
        { "data": "CoachFullName" },
        //{ "data": "CoachPrice" },
        //{ "data": "StudentFullName" },
        { "data": "Title" },
        {
            "data": "CurriculumDate",
            "render": function (data, type, full, meta) {
                var dataStr = Date.parse(data);
                return new Date(dataStr).Format("yyyy-MM-dd");
            }
        },
        { "data": "CurriculumBeginTime" },
        { "data": "CurriculumEndTime" },
        { "data": "Price" },
        { "data": "CoachPrice" },
        {
            "data": "PKType",
            "render": function (data, type, full, meta) {
                if (data == 1) {
                    var edithtml = '<span class="btn btn-xs btn-info">一对一</span>';
                } else {
                    var edithtml = '<span class="btn btn-xs btn-info">一对多</span>';
                }
                return edithtml;
            }
        },
        {
            "data": "State",
            "render": function (data, type, full, meta) {
                return transtionTeachingScheState(data);
            }
        },
        {
            "data": "AddTime",
            "render": function (data, type, full, meta) {
                var dataStr = Date.parse(data);
                return new Date(dataStr).Format("yyyy-MM-dd");
            }
        },
        {
            "data": "State",
            "render": function (data, type, full, meta) {
                return transtionTeachingScheStateHandle(data, full['PKID'], full['VenueID']);
            }
        },
    ];

    bindTable('leavetable', 'leavefrom', 'api/TeachingSchedule/Page4Venue', columns_data, false);
}

function bind_wait4classover_data() {

    columns_data = [
        {
            "data": "PKID",
            "render": function (data, type, full, meta) {
                return '<label><input type="checkbox" value="' + data + '"></label>';
            }
        },
        { "data": "PKID" },
        { "data": "VenueName" },
        { "data": "CampusName" },
        { "data": "CoachFullName" },
        //{ "data": "CoachPrice" },
        //{ "data": "StudentFullName" },
        { "data": "Title" },
        {
            "data": "CurriculumDate",
            "render": function (data, type, full, meta) {
                var dataStr = Date.parse(data);
                return new Date(dataStr).Format("yyyy-MM-dd");
            }
        },
        { "data": "CurriculumBeginTime" },
        { "data": "CurriculumEndTime" },
        { "data": "Price" },
        { "data": "CoachPrice" },
        {
            "data": "PKType",
            "render": function (data, type, full, meta) {
                if (data == 1) {
                    var edithtml = '<span class="btn btn-xs btn-info">一对一</span>';
                } else {
                    var edithtml = '<span class="btn btn-xs btn-info">一对多</span>';
                }
                return edithtml;
            }
        },
        {
            "data": "State",
            "render": function (data, type, full, meta) {
                return transtionTeachingScheState(data);
            }
        },
        {
            "data": "AddTime",
            "render": function (data, type, full, meta) {
                var dataStr = Date.parse(data);
                return new Date(dataStr).Format("yyyy-MM-dd");
            }
        },
        //{
        //    "data": "State",
        //    "render": function (data, type, full, meta) {
        //        return transtionTeachingScheStateHandle(data, full['PKID'], full['VenueID']);
        //    }
        //},
    ];

    bindTable('wait4classovertstable', 'wait4classovertsfrom', 'api/TeachingSchedule/Page4Venue', columns_data, false);
}

$("#selectAll").click(function () {
    $("table :checkbox").prop("checked", true);
});

$("#unSelect").click(function () {
    $("table :checkbox").prop("checked", false);
});

$("#saveclassover").click(function () {

    var data = $('#wait4classovertsfrom').serializeJSON();
    //if (parseInt(data['CoachID']) <= 0) {
    //    alert("请选择一个老师进行查询");
    //    return false;
    //}
    var valArr = new Array;
    $("#wait4classovertstable :checkbox:checked").each(function (i) {
        valArr[i] = $(this).val();
    });
    if (valArr.length == 0) {
        alert("请选择课程");
        return false;
    }

    var vals = valArr.join(',');//转换为逗号隔开的字符串

    data['PKIDs'] = valArr;
    data['VenueID'] = VenueID;

    $.ajax({
        type: "POST",
        url: ApiUrl + "api/TeachingSchedule/DoneBatch",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (data, status) {
            if (status == "success") {
                if (!data.Error) {
                    bind_wait4classover_data();
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

})

function initDate() {
    var ddd = new Date();
    var day = ddd.getDate();

    var month = ddd.getMonth() + 1;
    if (month < 10) {
        var month = "0" + (ddd.getMonth());
    }

    if (ddd.getDate() < 10) {
        day = "0" + ddd.getDate();
    }

    var datew = ddd.getFullYear() + "-" + month + "-" + day;
    var datew = datew.toString();
    //console.log(datew);
    $("#CurriculumBeginTime").val(datew);
    $("#CurriculumEndTime").val(datew);
}

$(function () {

    //var venueId = $("#VenueID").val();

    var url = window.location.href;
    var isCreate = url.indexOf("Create") > 0 || false;
    show_campus(VenueID, isCreate);
    show_coach(VenueID, isCreate);

})
