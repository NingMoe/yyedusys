
function bind_data() {

    columns_data = [
        { "data": "CurriculumID" },
        { "data": "VenueName" },
        { "data": "CampusName" },
        { "data": "StudentFullName" },
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
                return transtionCurriculumScheState(data);
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

                if (data == 6) {
                    var edithtml = '<button class="btn btn-xs btn-danger" onclick="return change_state(' + "'" + 'AgreeLeave' + "'," + full['CurriculumID'] + ',' + full['VenueID'] + ');"><i class="fa fa-edit"> 同意请假 </i></button>&nbsp;&nbsp;';
                    return edithtml;
                } else {
                    return "";
                }
            }
        },
    ];

    bindTable('teachingscheduletable', 'teachingschedulefrom', 'api/Curriculum/Page4Venue', columns_data, false);
}

function change_state(action, curriculumID, venueId) {

    var flag = confirm('确定此操作?');
    if (flag) {
        $.ajax({
            type: "post",
            url: ApiUrl + "api/Curriculum/" + action,
            contentType: 'application/json',
            data: JSON.stringify({ CurriculumID: curriculumID, VenueID: venueId }),
            success: function (data, status) {
                if (status == "success") {
                    if (!data.Error) {
                        bind_data();
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

    var venueId = $("#VenueID").val();

    showCampus(venueId);
    showCoach(venueId);
    showStudent(venueId);
})

