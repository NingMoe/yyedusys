$("#timepicker_am_start").on("change",function () {
    console.log($(this).val());
    $("#CurriculumAMTime").empty();
    $("#CurriculumAMTime").val($(this).val() + '-' + $("#timepicker_am_end").val());
    //console.log($("#CurriculumAMTime").val());
})

$("#timepicker_am_end").on("change", function () {
    console.log($(this).val());
    $("#CurriculumAMTime").empty();
    $("#CurriculumAMTime").val($("#timepicker_am_start").val() + '-' + $(this).val());
    //console.log($("#CurriculumAMTime").val());
})

$("#timepicker_pm_start").on("change", function () {
    console.log($(this).val());
    $("#CurriculumPMTime").empty();
    $("#CurriculumPMTime").val($(this).val() + '-' + $("#timepicker_pm_end").val());
    //console.log($("#CurriculumPMTime").val());
})

$("#timepicker_pm_end").on("change", function () {
    console.log($(this).val());
    $("#CurriculumPMTime").empty();
    $("#CurriculumPMTime").val($("#timepicker_pm_start").val() + '-' + $(this).val());
    //console.log($("#CurriculumPMTime").val());
})
 
function show_coach_teachingsche() {
    return columns_data = [
        {
            "data": "CurriculumDate",
            "render": function (data, type, full, meta) {
                console.log(data);
                var dataStr = Date.parse(data);
                return new Date(dataStr).Format("MM-dd");
            }
        },
        {
            "data": "CurriculumStr",
            "render": function (data, type, full, meta) {
                console.log(data);
                var edithtml = "";
                var parent_data = data.split(',');
                for (var i = 0; i < parent_data.length; i++) {
                    var sub_data = parent_data[i].split('|');
                    edithtml += "<div class='col-sm-2'>" + sub_data[0] + "&nbsp;" + transtionTeachingScheState(sub_data[1]) + "</div>";
                }
                return edithtml;
            }
        }
    ];
}

$("#preview").on("click", function () {

    var data = '{"data": [{"CurriculumDate": "2017-08-28T00: 00: 00","CurriculumStr": "09: 00-09: 30|0|1|3,09: 40-10: 10|0|1|3,10: 20-10: 50|0|1|3,11: 00-11: 30|0|1|3,11: 40-12: 00|0|1|3,14: 00-14: 30|0|1|3,14: 40-15: 10|0|1|3,15: 20-15: 50|0|1|3,16: 00-16: 30|0|1|3,16: 0-17: 10|0|1|3,17: 20-17: 50|0|1|3,18: 00-18: 00|0|1|3"}]}';
    //console.log(JSON.parse(data));
    bindStaticTable('previewteachingscheduletable', JSON.parse(data), show_coach_teachingsche());
})


$("#PKType").on("change", function () {

    var id = $("#CoachID").val();
    var pkType = $("#PKType").val();

    $.ajax({
        type: "get",
        url: ApiUrl + "api/coach/get/" + id,
        contentType: 'application/json',
        success: function (data, status) {
            if (status == "success") {
                if (data.Info == null) {
                    alert('error');
                } else {
                    if (pkType == 1) {
                        show_wage(data.Info.Wage, data.Info.Price);
                    } else {
                        show_wage(data.Info.WageMore, data.Info.PriceMore);
                    }
                }
            }
        },
        error: function (e) {
        },
        complete: function () {

        }
    });
})

$("#CoachID").on("change", function () {

    var pkType = $("#PKType").val();
    
    $.ajax({
        type: "get",
        url: ApiUrl + "api/coach/get/" + $(this).val(),
        contentType: 'application/json',
        success: function (data, status) {
            if (status == "success") {
                if (data.Info == null) {
                    alert('error');
                } else {
                    if (pkType == 1) {
                        show_wage(data.Info.Wage, data.Info.Price);
                    } else {
                        show_wage(data.Info.WageMore, data.Info.PriceMore);
                    }
                }
            }
        },
        error: function (e) {
        },
        complete: function () {

        }
    });
})

$("#CoachID").on("change", function () {
    
    if ($(this).val()==-1)
        return false;
    
    if ($("#CurriculumDateStr").val().length == 0)
        return false;

    bindTable('coachteachingscheduletable', '', 'api/coach/GetCoachTeachingSche', show_coach_teachingsche(), true, { coachID: $(this).val(), curriculumDate: $("#CurriculumDateStr").val() });
})

$("#CurriculumDateStr").on("change", function () {

    if ($(this).val().length == 0)
        return false;

    if ($("#CoachID").val() == -1)
        return false;

    bindTable('coachteachingscheduletable', '', 'api/coach/GetCoachTeachingSche', show_coach_teachingsche(), true, { coachID: $("#CoachID").val(), curriculumDate: $(this).val() });
})

$("#save").on('click', function () {

    var data = $('#teachingschedulefrom').serializeJSON();
    data['TimepickerPMCheckd'] = $("#TimepickerPMCheckd").prop('checked');
    data['TimepickerAMCheckd'] = $("#TimepickerAMCheckd").prop('checked');

    //添加
    $.ajax({
        type: "POST",
        url: ApiUrl + "api/teachingschedule/create",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (data, status) {
            if (status == "success") {
                if (!data.Error) {
                    alert("排课成功");
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
                                programme_sel.push('<option selected value="-1">全部校区</option>')
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
                                programme_sel.push('<option selected value="-1">全部教练</option>')
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
                console.log(data);
                if (status == "success") {
                    if (!data.Error) {
                        bind_data();
                        alert('设置成功');
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

$(function () {

    var venueId = $("#VenueID").val();

    var url = window.location.href;
    var isCreate = url.indexOf("Create") > 0 || false;
    show_campus(venueId, isCreate);
    show_coach(venueId, isCreate);
})
