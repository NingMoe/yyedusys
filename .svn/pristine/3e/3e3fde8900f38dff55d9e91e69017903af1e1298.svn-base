
$("#selectAll").click(function () {
    $("table :checkbox").prop("checked", true);
});

$("#unSelect").click(function () {
    $("table :checkbox").prop("checked", false);
});

$("#save").click(function () {

    var valArr = new Array;
    $("#teachingscheduletable :checkbox:checked").each(function (i) {
        valArr[i] = $(this).val();
    });
    if (valArr.length==0) {
        alert("请选择需要录入工资的课程");
        return false;
    }

    var vals = valArr.join(',');//转换为逗号隔开的字符串

    var data = $('#teachingschedulefrom').serializeJSON();
    data['CurriculumIds'] = valArr;

    $.ajax({
        type: "POST",
        url: ApiUrl + "api/CoachWages/Create",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (data, status) {
            if (status == "success") {
                if (!data.Error) {
                    bindData();
                    alert("工资录入成功");
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

$("#coachWagesSave").click(function () {

    var valArr = new Array;
    $("#coachwagestable :checkbox:checked").each(function (i) {
        valArr[i] = $(this).val();
    });
    if (valArr.length == 0) {
        alert("请选择工资记录");
        return false;
    }

    var vals = valArr.join(',');//转换为逗号隔开的字符串

    var data = $('#coachwagesfrom').serializeJSON();
    data['WagesIDs'] = valArr;

    $.ajax({
        type: "POST",
        url: ApiUrl + "api/CoachWages/SetWagesOver",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (data, status) {
            if (status == "success") {
                if (!data.Error) {
                    bindCoachWagesData();
                    alert("工资发放完成");
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


function bindData() {

    columns_data = [
        {
            "data": "CurriculumID",
            "render": function (data, type, full, meta) {
                return '<label><input type="checkbox" value="' + data + '"></label>';
            }
        },
        { "data": "PKID" },
        //{ "data": "VenueName" },
        //{ "data": "CampusName" },
        { "data": "CoachFullName" },
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
            "data": "State",
            "render": function (data, type, full, meta) {
                return transtionTeachingScheStateHandle(data, full['PKID'], full['VenueID']);
            }
        },
    ];

    bindTable('teachingscheduletable', 'teachingschedulefrom', 'api/TeachingSchedule/GetClassOverTeachingSches', columns_data, false);

    $("#WorkDate").val($("#CurriculumBeginTime").val());
}

function bindCoachWagesData() {

    columns_data = [
        {
            "data": "WagesID",
            "render": function (data, type, full, meta) {
                return '<label><input type="checkbox" value="' + data + '"></label>';
            }
        },
        { "data": "WagesID" },
        { "data": "CoachFullName" },
        //{ "data": "Remark" },
        {
            "data": "WorkDate",
            "render": function (data, type, full, meta) {
                var dataStr = Date.parse(data);
                return new Date(dataStr).Format("yyyy-MM");
            }
        },
        { "data": "Price" },
        {
            "data": "State",
            "render": function (data, type, full, meta) {
                return transtionCoachWagesState(data);
            }
        },
        {
            "data": "AddTime",
            "render": function (data, type, full, meta) {
                var dataStr = Date.parse(data);
                return new Date(dataStr).Format("yyyy-MM-dd hh:mm:ss");
            }
        },
        {
            "data": "State",
            "render": function (data, type, full, meta) {
                if (data == 1) {
                    var dataStr = Date.parse(full['PayTime']);
                    return new Date(dataStr).Format("yyyy-MM-dd hh:mm:ss");
                } else {
                    return '';
                }
            }
        },
        {
            "data": "State",
            "render": function (data, type, full, meta) {
                var edithtml = '<a href="/CoachWagesSub/Index/' + full['WagesID'] + '" class="btn btn-xs btn-success"><i class="fa fa-edit"> 查看 </i></a>&nbsp;&nbsp;';
                return edithtml;
            }
        },
    ];

    bindTable('coachwagestable', 'coachwagesfrom', 'api/CoachWages/GetCoachWages', columns_data, false);
}

function bindCoachWagesDetailData() {

    columns_data = [
        
        { "data": "WagesSubID" },
        { "data": "CoachFullName" },
        { "data": "StudentFullName" },
        { "data": "CoachPrice" },
    ];

    bindTable('coachwagesdetailtable', 'coachwagesdetailfrom', 'api/CoachWagesSub/GetClassOverTeachingSches', columns_data, true);
}

function initDate() {
    var ddd = new Date();
    var day = ddd.getDate();

    if (ddd.getMonth() < 10) {
        var month = "0" + (ddd.getMonth() + 1);
    }

    if (ddd.getDate() < 10) {
        day = "0" + ddd.getDate();
    }

    var datew = ddd.getFullYear() + "-" + month;// + "-" + day;
    var datew = datew.toString();

    $("#CurriculumBeginTime").val(datew);
    //$("#CurriculumEndTime").val(datew);
}

