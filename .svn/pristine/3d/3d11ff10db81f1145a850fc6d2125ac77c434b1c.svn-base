$("#timepicker_am_start").on("change", function () {
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
                //console.log(data);
                var dataStr = Date.parse(data);
                return new Date(dataStr).Format("MM-dd");
            }
        },
        {
            "data": "CurriculumStr",
            "render": function (data, type, full, meta) {
                //console.log(data);
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
    if (parseInt(id) < 0) {
        return;
    }
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
    if (parseInt($(this).val()) <= 0)
        return;
    if (parseInt(pkType) <= 0) {
        return;
    }

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

    //展示已排课信息
    if ($(this).val() == -1)
        return false;

    if ($("#CurriculumDateStr").val().length == 0)
        return false;

    bindTable('coachteachingscheduletable', '', 'api/coach/GetCoachTeachingSche', show_coach_teachingsche(), true, { coachID: $(this).val(), curriculumDate: $("#CurriculumDateStr").val() });


})

//$("#CoachID").on("change", function () {

//    if ($(this).val() == -1)
//        return false;

//    if ($("#CurriculumDateStr").val().length == 0)
//        return false;

//    bindTable('coachteachingscheduletable', '', 'api/coach/GetCoachTeachingSche', show_coach_teachingsche(), true, { coachID: $(this).val(), curriculumDate: $("#CurriculumDateStr").val() });
//})

$("#CurriculumDateStr").on("change", function () {

    if ($(this).val().length == 0)
        return false;

    if ($("#CoachID").val() == -1)
        return false;

    bindTable('coachteachingscheduletable', '', 'api/coach/GetCoachTeachingSche', show_coach_teachingsche(), true, { coachID: $("#CoachID").val(), curriculumDate: $(this).val() });
})

$("#save").on('click', function () {

    $("#save").attr("disabled", "disabled");

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
                    //alert("排课成功");
                    location.href = "/TeachingSchedule/Index";
                } else {
                    $("#save").removeAttr("disabled");
                    alert(data.Msg);
                }
            }
        },
        error: function (e) {
            $("#save").removeAttr("disabled");
            console.log('error');
        },
        complete: function () {

        }
    });
});
